using Newtonsoft.Json;

namespace SystemsIntegration.Api.Models
{
    public class LocalidadPedidoModel
    {
        [JsonProperty("division")]
        public string Division { get; set; }

        [JsonProperty("name")]
        public string Nombre { get; set; }
    }
}
