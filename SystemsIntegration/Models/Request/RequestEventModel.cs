
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SystemsIntegration.Api.Models.Request
{
    public struct RequestEventModel<T>
    {
        [JsonProperty("event")]
        public string Evento { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
