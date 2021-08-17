using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SystemsIntegration.Api.Models.Request
{
    public struct DatosEventStatusUpdateModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string CodigoPedido { get; set; }

        [JsonProperty("status")]
        public string Estado { get; set; }
    }
}
