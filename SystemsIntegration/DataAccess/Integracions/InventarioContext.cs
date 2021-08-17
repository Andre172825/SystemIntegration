using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SystemsIntegration.Api.Configurations.appSetting;
using SystemsIntegration.Api.DataAccess.Contracts;

namespace SystemsIntegration.Api.DataAccess.Integracions
{
    public class InventarioContext : IInventarioContext
    {
        private readonly IAppConfig _appConfig;
        public InventarioContext(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task</*int codCliente, string mensaje,*/ List<(string, string)>> ObtieneProductosPorActualizar()
        {
            /*int codCliente, string mensaje,*/ List<(string, string)> result;

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                result = await satelliteContext.QuerySingleAsync</*int codCliente, string mensaje,*/ List<(string, string)>>("usp_ObtieneProductosPorActualizar",
                                                    commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }
            return result;
        }
    }
}
