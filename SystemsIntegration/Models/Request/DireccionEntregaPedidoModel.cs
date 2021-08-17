using Newtonsoft.Json;
using System.Collections.Generic;

namespace SystemsIntegration.Api.Models.Request
{
    public class DireccionEntregaPedidoModel
    {
        [JsonProperty("address")]
        public string Direccion { get; set; }

        [JsonProperty("reference")]
        public string Referencia { get; set; }

        [JsonProperty("contactPerson")]
        public string NombreContacto { get; set; }

        [JsonProperty("contactPhone")]
        public string NumeroContacto { get; set; }

        [JsonProperty("localityTree")]
        public List<LocalidadPedidoModel> LocalidadDelivery { get; set; }


    }
}
