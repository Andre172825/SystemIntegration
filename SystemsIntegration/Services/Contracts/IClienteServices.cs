using System.Threading.Tasks;
using SystemsIntegration.Api.Models.Request;

namespace SystemsIntegration.Api.Services.Contracts
{
    public interface IClienteServices
    {
        public Task<(int codCliente, string mensaje)> ObtenerCodigoClientePedido(DatosObtenerCliente cliente);
    }
}
