using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SystemsIntegration.Api.Configurations.appSetting;
using SystemsIntegration.Api.CrossCutting;
using SystemsIntegration.Api.Models.Entities;
using SystemsIntegration.Api.Models.Request;
using SystemsIntegration.Api.Services.Contracts;

namespace SystemsIntegration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoServices _pedidoServices;
        private readonly IServicioExternoServices _servicioExternoServices;
        private readonly IAppConfig _appConfig;

        public PedidoController(IPedidoServices pedidoServices, IAppConfig appConfig, IServicioExternoServices servicioExternoServices)
        {
            _pedidoServices = pedidoServices;
            _appConfig = appConfig;
            _servicioExternoServices = servicioExternoServices;
        }

        [HttpPost("EventWebHook")]
        public async Task<ActionResult> WebHookRiqra([FromHeader(Name = "X-Riqra-Signature")] string key_Riqra, [FromHeader(Name = "X-Riqra-Platform")] int plataforma_Riqra)
        {

            using StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8);
            string body = await reader.ReadToEndAsync();

            var requestUpdateStatus = JsonConvert.DeserializeObject<RequestEventModel<DatosEventStatusUpdateModel>>(body);

            try
            {
              
                TrazaServicioModel trazaServicio = new TrazaServicioModel();
                trazaServicio.IdServicio = plataforma_Riqra;
                trazaServicio.CodigoObjeto = requestUpdateStatus.Data.Id.ToString();
                trazaServicio.Cuerpo = body;
                trazaServicio.Firma = key_Riqra;

                _ = await _servicioExternoServices.RegistrarTrazaServicio(trazaServicio);

                string llave = plataforma_Riqra == 1 ? _appConfig.RiqraWebHookKey_B2C : _appConfig.RiqraWebHookKey_B2B;

                string encryptedBody = "sha256=" + Shared.EncryptSha256Key(body, llave);
                
                if (encryptedBody != key_Riqra)
                { 
                    _ = await _servicioExternoServices.RegistrarLogPedidoUnishop(requestUpdateStatus.Data.Id, 
                                Constantes.EVENTO_SISTEMA_ERROR_VALIDACION_FIRMA, plataforma_Riqra, null);

                    List<NotificacionDestinoEntity> listaNotificacion = (List<NotificacionDestinoEntity>) await _servicioExternoServices.ObtenerDestinoNotificacion("SystemIntegration", "PedidoUnishop");

                    Shared.EnviarCorreo(listaNotificacion, $"Firma no válida del pedido {requestUpdateStatus.Data.Id}");

                    return BadRequest();
                }
                
                if (requestUpdateStatus.Evento.Equals("order-status-updated"))
                {
                    _ = await _pedidoServices.ActualizarEstadoPedido(plataforma_Riqra, 
                            requestUpdateStatus.Data.Id, requestUpdateStatus.Data.CodigoPedido, requestUpdateStatus.Data.Estado);
                }
                else
                {
                   RequestEventModel<PedidoModel> datosDelPedido = JsonConvert.DeserializeObject<RequestEventModel<PedidoModel>>(body);
                   _ = await _pedidoServices.RegistrarPedidoWebHook(datosDelPedido.Data, plataforma_Riqra);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                //_ = await _servicioExternoServices.RegistrarLogPedidoUnishop(requestUpdateStatus.Data.Id,
               //                 Constantes.EVENTO_SISTEMA_ERROR_DE_SERVICIO, plataforma_Riqra, ex.Message);
                return Ok();
            }
        }

        [HttpPut("ActualizaStock")]
        public async Task<string> ActualizarStockAsync()
        {
            string sku = "PAEAE00002";
            string cantidad = "5";
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("https://sandbox.api.riqra.com/logistics/inventory/skus/" + sku + "/warehouses/339"),
                    Headers =
            {
                { "api-key", "2ZN6S4D-Y0344EB-MP0XHY8-AQ7X47W" },
            },
                    Content = new StringContent("{\"quantity\":" + cantidad + "}")
                    {
                        Headers =
            {
                ContentType = new MediaTypeHeaderValue("application/json")
            }
                    }
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                }
                return "Bien";
            }
            catch (Exception e)
            {
                return e.Message;
            }


            ///*int codCliente, string mensaje, */List<(string, string)> productos;
            //productos = await _inventarioServices.ObtieneProductosPorActualizar();

            //var client = new HttpClient();

            //foreach(var producto in productos)
            //{
            //    string sku = producto.Item1;
            //    string cantidad = "{\"quantity\":" + producto.Item2 + "}";
            //    string warehouseId = "339";
            //    string url = "https://sandbox.api.riqra.com/logistics/inventory/skus/" + sku + "/warehouses/" + warehouseId;

            //    var request = new HttpRequestMessage
            //    {
            //        Method = HttpMethod.Put,
            //        RequestUri = new Uri(url),
            //        Headers =
            //        {
            //            { "api-key", "2ZN6S4D-Y0344EB-MP0XHY8-AQ7X47W" },
            //        },
            //        Content = new StringContent(cantidad)
            //        {
            //            Headers =
            //            {
            //                ContentType = new MediaTypeHeaderValue("application/json")
            //            }
            //        }
            //    };
            //    using (var response = await client.SendAsync(request))
            //    {
            //        response.EnsureSuccessStatusCode();
            //        var body = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine(body);//result
            //    }
            //}
        }

    }
}
