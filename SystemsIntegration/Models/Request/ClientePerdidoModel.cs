using Newtonsoft.Json;
namespace SystemsIntegration.Api.Models.Request
{
    public class ClientePerdidoModel
    {
        [JsonProperty("id")]
        public int Codigo { get; set; }

        [JsonProperty("firstName")]
        public string Nombres { get; set; }

        [JsonProperty("lastName")]
        public string Apellidos { get; set; }

        [JsonProperty("email")]
        public string Correo { get; set; }

        [JsonProperty("phone")]
        public string Telefono { get; set; }
    }
}
