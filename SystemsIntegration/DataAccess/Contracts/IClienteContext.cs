using System.Threading.Tasks;
using SystemsIntegration.Api.Models.Request;

namespace SystemsIntegration.Api.DataAccess.Contracts
{
    public interface IClienteContext
    {
        public Task<(int codCliente, string mensaje)> ObtenerClientePedido(DatosObtenerCliente cliente);
    }
}
