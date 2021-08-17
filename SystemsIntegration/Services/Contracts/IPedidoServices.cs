using System.Threading.Tasks;
using SystemsIntegration.Api.Models.Request;

namespace SystemsIntegration.Api.Services.Contracts
{
    public interface IPedidoServices
    {
        /*
        public Task<PedidoModel> ObtenerPedidoxId(int pedido);
        public Task<string> RegistrarPedido(int codigoPedido);
        */

        public Task<string> RegistrarPedidoWebHook(PedidoModel pedido, int servicioProveedor);
        public Task<string> ActualizarEstadoPedido(int idServicioProveedor, int idPedido, string codigoPedido, string estado);
    }
}
