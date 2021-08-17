using Newtonsoft.Json;

namespace SystemsIntegration.Api.Models.Request
{
    public class PrecioPedidoModel
    {
        [JsonProperty("adjustmentsTotal")]
        public decimal AjusteTotal { get; set; }

        [JsonProperty("subtotal")]
        public decimal SubTotal { get; set; }

        [JsonProperty("tax")]
        public decimal Impuesto { get; set; }

        [JsonProperty("deliveryCost")]
        public decimal CostoEnvio { get; set; }

        [JsonProperty("total")]
        public decimal MontoTotal { get; set; }
    }
}
