using System.Threading.Tasks;
using SystemsIntegration.Api.DataAccess.Contracts;
using SystemsIntegration.Api.Models.Request;
using SystemsIntegration.Api.Services.Contracts;

namespace SystemsIntegration.Api.Services.Implementation
{
    public class ClienteServices: IClienteServices
    {
        private readonly IClienteContext _clienteContext;

        public ClienteServices(IClienteContext clienteContext)
        {
            _clienteContext = clienteContext;
        }

        public async Task<(int codCliente, string mensaje)> ObtenerCodigoClientePedido(DatosObtenerCliente cliente)
        {
            return await _clienteContext.ObtenerClientePedido(cliente);
        }


    }
}
