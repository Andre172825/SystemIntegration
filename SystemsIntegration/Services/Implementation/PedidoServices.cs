using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemsIntegration.Api.CrossCutting;
using SystemsIntegration.Api.DataAccess.Contracts;
using SystemsIntegration.Api.Models.Entities;
using SystemsIntegration.Api.Models.Request;
using SystemsIntegration.Api.Services.Contracts;

namespace SystemsIntegration.Api.Services.Implementation
{
    public class PedidoServices : IPedidoServices
    {
        private readonly IClienteServices _clienteServices;
        private readonly IPedidoContext _pedidoContext;
        private readonly IServicioExternoServices _servicioExternoServices;


        public PedidoServices(IClienteServices clienteServices, 
            IPedidoContext pedidoContext, IServicioExternoServices servicioExternoServices)
        {
            _clienteServices = clienteServices;
            _pedidoContext = pedidoContext;
            _servicioExternoServices = servicioExternoServices;
        }
        public PedidoServices(){}

        public async Task<string> ActualizarEstadoPedido(int idServicioProveedor, int idPedido, string codigoPedido, string estado)
        {
            (bool success, string message) response = await _pedidoContext.ActualizarEstadoPedido(idServicioProveedor, idPedido, codigoPedido, estado);

            if (!response.success)
            {
                await GuardarLogYNotificarPedidoAsync(idPedido, Constantes.EVENTO_SISTEMA_ERROR_ACTUALZAR_ESTADO_PEDIDO, idServicioProveedor, 
                        response.message, true);

                return "Error al actualizar el pedido";
            }

            await GuardarLogYNotificarPedidoAsync(idPedido, Constantes.EVENTO_SISTEMA_ACTUALZACION_ESTADO_PEDIDO, idServicioProveedor,
                        response.message, false);

            return response.message;

        }

        public async Task<string> RegistrarPedidoWebHook(PedidoModel pedido, int idServicioProveedor)
        {

            (int codCliente, string mensaje) responseCliente = await ObtenerClientePedido(pedido);

            if(responseCliente.codCliente == 0)
            {
                await GuardarLogYNotificarPedidoAsync(pedido.Id, Constantes.EVENTO_SISTEMA_ERROR_OBTENER_CLIENTE, idServicioProveedor,
                        $"Error al obtener al cliente del pedido Nro {pedido.Id} mensaje : {responseCliente.mensaje}", true);

                return "Error al obtener el cliente de la BD";
            }

            string detallePedido = ObtenerDetallePedido(pedido.Productos);

            (bool success, string message) resultValidacion = await ValidarPedido(detallePedido, idServicioProveedor);

            if (!resultValidacion.success)
            {
                await GuardarLogYNotificarPedidoAsync(pedido.Id, Constantes.EVENTO_SISTEMA_ERROR_VALIDAR_PEDIDO_ALMACEN, idServicioProveedor,
                        $"Error al validar el detalle del pedido Nro {pedido.CodigoPedido}: {resultValidacion.message}", true);

                return "No se puedo validar el detalle del pedido con almacen";
            }

            (bool codigo, string mensaje) resultRegistroPedido = await RegistrarPedidoBD(pedido, responseCliente.codCliente, detallePedido, idServicioProveedor);

            if (!resultRegistroPedido.codigo)
            {
                await GuardarLogYNotificarPedidoAsync(pedido.Id, Constantes.EVENTO_SISTEMA_ERROR_REGISTRAR_PEDIDO_UNISHOP, idServicioProveedor,
                        $"Error al registrar del pedido Nro {pedido.CodigoPedido} en el sistema Spring: {resultValidacion.message}", true);

                return "Error al registrar el pedido en la BD";
            }

            await GuardarLogYNotificarPedidoAsync(pedido.Id, Constantes.EVENTO_SISTEMA_REGISTRO_EXITOSO_SPRING, idServicioProveedor,
                        resultRegistroPedido.mensaje,  false);

            return "El pedido se registro correctamente";
        }

        private async Task<(int codCliente, string mensaje)> ObtenerClientePedido(PedidoModel pedido)
        {
            DatosObtenerCliente resquest = new DatosObtenerCliente();
            resquest.TipoComprobante = pedido.Facturacion.TipoComprobante;
            resquest.NroDocumento = pedido.Facturacion.NroDocumento;

            if (pedido.EnvioPedido.Delivery != null)
            {
                resquest.Departamento = pedido.EnvioPedido.Delivery.LocalidadDelivery.FirstOrDefault(x => x.Division == "Departamento").Nombre;
                resquest.Provincia = pedido.EnvioPedido.Delivery.LocalidadDelivery.FirstOrDefault(x => x.Division == "Provincia").Nombre;
                resquest.Distrito = pedido.EnvioPedido.Delivery.LocalidadDelivery.FirstOrDefault(x => x.Division == "Distrito").Nombre;
            }
            else
            {
                resquest.Departamento = "";
                resquest.Provincia = "";
                resquest.Distrito = "";
            }

            if (pedido.Facturacion.TipoComprobante == "Factura")
            {
                resquest.RazonSocial = pedido.Facturacion.NombreFiscal;
                resquest.Direccion = pedido.Facturacion.DireccionFiscal;
            }
            else
            {
                resquest.RazonSocial = pedido.Cliente.Nombres;
                resquest.Apellidos = pedido.Cliente.Apellidos;
                resquest.Direccion = pedido.EnvioPedido.TipoEnvio == "Entrega a Domicilio" ? pedido.EnvioPedido.Delivery.Direccion : pedido.EnvioPedido.RetiroTienda.DireccionTienda;
            }

            resquest.Telefono = pedido.Cliente.Telefono;
            resquest.Correo = pedido.Cliente.Correo;

            return await _clienteServices.ObtenerCodigoClientePedido(resquest);
        }

        private async Task<(bool success, string message)> ValidarPedido(string detallePedido, int idServicioProveedor)
        {
            (bool success, string message) response = await _pedidoContext.ValidarPedido(detallePedido, idServicioProveedor);
            return response;
        }

        private async Task<(bool codigo, string mensaje)> RegistrarPedidoBD(PedidoModel pedido, int codCliente, string detallePedido, int idServivioProveedor)
        {

            DatosRegistroPedidoMasterModel datosPedido = new DatosRegistroPedidoMasterModel()
            {
                IdPedido = pedido.Id,
                CodigoCliente = codCliente,
                FechaEntrega = pedido.EnvioPedido.TipoEnvio == "Entrega a Domicilio" ? pedido.EnvioPedido.FechaEntrega : pedido.EnvioPedido.FechaRecojo,
                CodigoPedidoUnishop = pedido.CodigoPedido,
                TipoRecojo = pedido.EnvioPedido.TipoEnvio,
                DetallePedido = detallePedido,
                ServicioProveedor = idServivioProveedor,
                MetodoPago = pedido.TipoPagoPedido.TipoPago,
                TransaccionId = pedido.TipoPagoPedido.TransaccionId,
                Estado = pedido.Estado
            };

            (bool codigo, string mensaje) response = await _pedidoContext.RegistrarPedido(datosPedido);

            return response;
        }

        private string ObtenerDetallePedido(List<ProductoPedidoModel> productos)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ProductoPedidoModel item in productos)
            {
                sb.Append(item.Sku);
                sb.Append("|");
                sb.Append(item.Cantidad);
                sb.Append("|");
                sb.Append(item.PrecioUnidad);
                sb.Append(",");
            }

            return sb.ToString();
        }

        private async Task GuardarLogYNotificarPedidoAsync(int idPedido, int idEvento, int idServicioProveedor, string mensaje, bool guardarDescripcion)
        {
            _ = await _servicioExternoServices.RegistrarLogPedidoUnishop(idPedido, idEvento, idServicioProveedor, guardarDescripcion ? mensaje : "");

            List<NotificacionDestinoEntity> listaNotificacion = (List<NotificacionDestinoEntity>)await _servicioExternoServices.ObtenerDestinoNotificacion("SystemIntegration", "PedidoUnishop");
            Shared.EnviarCorreo(listaNotificacion, mensaje);
        }

    }
}
