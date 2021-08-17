using Newtonsoft.Json;

namespace SystemsIntegration.Api.Models.Request
{
    public class RetiroEnTiendaPedido
    {
        [JsonProperty("name")]
        public string NombreTienda { get; set; }
        
        [JsonProperty("address")]
        public string DireccionTienda { get; set; }
    }
}
