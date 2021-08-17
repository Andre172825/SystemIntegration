using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SystemsIntegration.Api.Configurations.appSetting;
using SystemsIntegration.Api.Services.Contracts;

namespace SystemsIntegration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class InventarioController : ControllerBase
    {

        private readonly IInventarioServices _inventarioServices;
        private readonly IServicioExternoServices _servicioExternoServices;
        private readonly IAppConfig _appConfig;

        public InventarioController(IInventarioServices inventarioServices, IAppConfig appConfig, IServicioExternoServices servicioExternoServices)
        {
            _inventarioServices = inventarioServices;
            _appConfig = appConfig;
            _servicioExternoServices = servicioExternoServices;
        }

        [HttpPost("prueba")]
        public string prueba()
        {
            return "x";
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
            }catch(Exception e)
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
