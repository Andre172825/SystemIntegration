using Newtonsoft.Json;

namespace SystemsIntegration.Api.Models.Request
{
    public class FacturacionPedidoModel
    {
        [JsonProperty("name")]
        public string TipoComprobante { get; set; }

        [JsonProperty("documentNumber")]
        public string NroDocumento { get; set; }

        [JsonProperty("fiscalName")]
        public string NombreFiscal { get; set; }

        [JsonProperty("fiscalAddress")]
        public string DireccionFiscal { get; set; }

        [JsonProperty("email")]
        public string Correo { get; set; }
    }
}
