

namespace SystemsIntegration.Api.Models.Request
{
    public class DatosRegistroPedidoDetalle
    {
        public int CodigoPedido { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
