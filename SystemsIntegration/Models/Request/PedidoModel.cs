using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using System.Text.Json;


namespace SystemsIntegration.Api.Models.Request
{
    public class PedidoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string CodigoPedido { get; set; }

        [JsonProperty("status")]
        public string Estado { get; set; }

        [JsonProperty("createdAt")]
        public DateTime FechaCreacion { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("customer")]
        public ClientePerdidoModel Cliente { get; set; }

        [JsonProperty("lineItems")]
        public List<ProductoPedidoModel> Productos { get; set; }

        [JsonProperty("pricing")]
        public PrecioPedidoModel Precio { get; set; }

        [JsonProperty("shipping")]
        public EnvioPedidoModel EnvioPedido { get; set; }

        [JsonProperty("payment")]
        public TipoPagoModel TipoPagoPedido { get; set; }

        [JsonProperty("invoicing")]
        public FacturacionPedidoModel Facturacion { get; set; }

    }
}
