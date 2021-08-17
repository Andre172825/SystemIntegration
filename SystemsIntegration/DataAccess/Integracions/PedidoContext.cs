using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SystemsIntegration.Api.Configurations.appSetting;
using SystemsIntegration.Api.DataAccess.Contracts;
using SystemsIntegration.Api.Models.Request;

namespace SystemsIntegration.Api.DataAccess.Integracions
{
    public class PedidoContext : IPedidoContext
    {
        private readonly IAppConfig _appConfig;
        public PedidoContext(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<(bool success, string message)> ValidarPedido(string detallePedido, int idServicioProveedor)
        {
            (bool success, string message) tuple;

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                tuple = await satelliteContext.QuerySingleAsync<(bool success, string message)>("usp_validaDetallePedidoUnishop", new { detallePedido, idServicioProveedor }, commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }

            return tuple;
        }

        public async Task<(bool codigo, string mensaje)> RegistrarPedido(DatosRegistroPedidoMasterModel pedido)
        {

            (bool codigo, string mensaje) result;

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                result = await satelliteContext.QuerySingleAsync<(bool codigo, string mensaje)>("usp_Registrar_Pedido", pedido, commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }

            return result;

        }


        public async Task<(bool success, string message)> ActualizarEstadoPedido(int idServicio, int idPedido, string codigoPedido, string estado)
        {

            (bool success, string message) tuple;

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                tuple = await satelliteContext.QuerySingleAsync<(bool success, string message)>("usp_ActualizaEstadoPedido_Spring", new { idServicio, idPedido, codigoPedido, estado }, commandType: CommandType.StoredProcedure);
                satelliteContext.Dispose();
            }

            return tuple;

        }
    }
}
