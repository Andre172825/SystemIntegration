using System.Threading.Tasks;
using SystemsIntegration.Api.Models.Request;

namespace SystemsIntegration.Api.DataAccess.Contracts
{
    public interface IPedidoContext
    {
        public Task<(bool codigo, string mensaje)> RegistrarPedido (DatosRegistroPedidoMasterModel pedido);
        public Task<(bool success, string message)> ValidarPedido(string detallePedido, int idServicioProveedor);
        public Task<(bool success, string message)> ActualizarEstadoPedido(int idServicio, int idPedido, string codigoPedido, string estado);
    }
}
