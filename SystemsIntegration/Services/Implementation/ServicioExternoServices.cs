using System.Collections.Generic;
using System.Threading.Tasks;
using SystemsIntegration.Api.DataAccess.Contracts;
using SystemsIntegration.Api.Models.Entities;
using SystemsIntegration.Api.Models.Request;
using SystemsIntegration.Api.Services.Contracts;

namespace SystemsIntegration.Api.Services.Implementation
{
    public class ServicioExternoServices : IServicioExternoServices
    {
        private readonly IServicioExternoContext _servicioExternoContext;

        public ServicioExternoServices(IServicioExternoContext servicioExternoContext)
        {
            _servicioExternoContext = servicioExternoContext;
        }

        public async Task<int> RegistrarTrazaServicio(TrazaServicioModel traza)
        {
            int response = await _servicioExternoContext.RegistrarTrazaServicio(traza);

            return response;
        }

        public async Task<int> RegistrarLogPedidoUnishop(int idPedido, int idEvento, int idServicio, string mensaje)
        {
            int response = await _servicioExternoContext.RegistrarLogPedidoUnishop(idPedido, idEvento, idServicio, mensaje);

            return response;
        }

        public async Task<IEnumerable<NotificacionDestinoEntity>> ObtenerDestinoNotificacion(string sistema, string modulo)
        {
            return await _servicioExternoContext.ObtenerDestinoNotificacion(sistema, modulo);
        }
    }
}
