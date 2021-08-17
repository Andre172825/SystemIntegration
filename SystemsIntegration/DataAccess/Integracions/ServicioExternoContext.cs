using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SystemsIntegration.Api.Configurations.appSetting;
using SystemsIntegration.Api.DataAccess.Contracts;
using SystemsIntegration.Api.Models.Entities;
using SystemsIntegration.Api.Models.Request;

namespace SystemsIntegration.Api.DataAccess.Integracions
{
    public class ServicioExternoContext : IServicioExternoContext
    {
        private readonly IAppConfig _appConfig;

        public ServicioExternoContext(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task<int> RegistrarTrazaServicio (TrazaServicioModel traza)
        {
            int response = 0;
           
            string query = "INSERT INTO TBLogTrazaServicio (IdServicio, CodigoObjeto, Cuerpo, Cabecera, Firma) " +
                " VALUES (@idServicio, @codigoObjeto, @cuerpo, @cabecera, @firma)";

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                response = await satelliteContext.ExecuteAsync(query, traza);
                satelliteContext.Dispose();
            }

            return response;
        }

        public async Task<int> RegistrarLogPedidoUnishop(int idPedido, int idEvento, int idServicio, string mensaje)
        {
            int response = 0;

            string query = "INSERT INTO TBLogRegistroPedido (IdPedido, IdEvento, IdServicio, Mensaje) " +
                                "VALUES (@idPedido, @idEvento, @idServicio, @mensaje)";

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                response = await satelliteContext.ExecuteAsync(query, new { idPedido, idEvento, idServicio, mensaje });
                satelliteContext.Dispose();
            }

            return response;
        }

        public async Task<IEnumerable<NotificacionDestinoEntity>> ObtenerDestinoNotificacion(string sistema, string modulo)
        {
            IEnumerable<NotificacionDestinoEntity> response = new List<NotificacionDestinoEntity>();

            string query = "SELECT Sistema, Modulo, Destino, Accion, TipoNotificacion, Adicional1, Adicional2 FROM TBMNotificacionDestino WHERE Estado = 'A' AND Sistema = @sistema AND Modulo = @modulo";

            using (var satelliteContext = new SqlConnection(_appConfig.SpringContext))
            {
                response = await satelliteContext.QueryAsync<NotificacionDestinoEntity>(query, new { sistema, modulo });
                satelliteContext.Dispose();
            }

            return response;
        }
    }
}
