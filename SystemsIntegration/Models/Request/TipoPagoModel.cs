using Newtonsoft.Json;

namespace SystemsIntegration.Api.Models.Request
{
    public class TipoPagoModel
    {
        [JsonProperty("name")]
        public string TipoPago { get; set; }

        [JsonProperty("transactionId")]
        public string TransaccionId { get; set; }
    }
}
