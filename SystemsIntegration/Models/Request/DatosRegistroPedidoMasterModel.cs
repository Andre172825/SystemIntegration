using System;

namespace SystemsIntegration.Api.Models.Request
{
    public struct DatosRegistroPedidoMasterModel
    {
        public int IdPedido { get; set; }
        public int CodigoCliente { get; set; }
        public DateTime FechaEntrega{ get; set; }
        public string CodigoPedidoUnishop { get; set; }
        public string TipoRecojo { get; set; }
        public string DetallePedido { get; set; }
        public int ServicioProveedor { get; set; }
        public string MetodoPago { get; set; }
        public string TransaccionId { get; set; }
        public string Estado { get; set; }
    }
}
