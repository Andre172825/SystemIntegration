using Newtonsoft.Json;
using System;

namespace SystemsIntegration.Api.Models.Request
{
    public class EnvioPedidoModel
    {
        [JsonProperty("name")]
        public string TipoEnvio { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime FechaEntrega { get; set; }

        [JsonProperty("pickupDate")]
        public DateTime FechaRecojo{ get; set; }

        [JsonProperty("deliveryAddress")]
        public DireccionEntregaPedidoModel Delivery { get; set; }

        [JsonProperty("pickupInStore")]
        public RetiroEnTiendaPedido RetiroTienda { get; set; }
    }
}
