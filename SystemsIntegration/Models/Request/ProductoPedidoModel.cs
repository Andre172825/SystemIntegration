using Newtonsoft.Json;

namespace SystemsIntegration.Api.Models.Request
{
    public class ProductoPedidoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("quantity")]
        public int Cantidad { get; set; }

        [JsonProperty("price")]
        public decimal PrecioUnidad { get; set; }

        [JsonProperty("name")]
        public string NombreProducto { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }
    }
}
