using Dapper;
using SystemsIntegration.Api.Configurations.appSetting;
using SystemsIntegration.Api.Models.Request;
using System.Data;
using System.Threading.Tasks;
using SystemsIntegration.Api.DataAccess.Contracts;
using System.Data.SqlClient;

namespace SystemsIntegration.Api.DataAccess.Integracions
{
    public class ClienteContext : IClienteContext
    {
        private readonly IAppConfig _appConfig;
        public ClienteContext(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<(int codCliente, string mensaje)> ObtenerClientePedido(DatosObtenerCliente cliente)
        {

            (int codCliente, string mensaje) result;

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                result = await satelliteContext.QuerySingleAsync<(int codCliente, string mensaje)>("usp_ObtenerClientePedidoSpring", cliente,
                                                    commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }
            return result;
           
            
        }
    }
}
