using System.Collections.Generic;
using System.Threading.Tasks;
using SystemsIntegration.Api.Models.Entities;
using SystemsIntegration.Api.Models.Request;

namespace SystemsIntegration.Api.Services.Contracts
{
    public interface IServicioExternoServices
    {
        public Task<int> RegistrarTrazaServicio(TrazaServicioModel traza);
        public Task<int> RegistrarLogPedidoUnishop(int idPedido, int idEvento, int idServicio, string mensaje);
        public Task<IEnumerable<NotificacionDestinoEntity>> ObtenerDestinoNotificacion(string sistema, string modulo);
    }
}   
