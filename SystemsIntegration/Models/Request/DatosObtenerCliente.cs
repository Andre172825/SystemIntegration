namespace SystemsIntegration.Api.Models.Request
{
    public class DatosObtenerCliente
    {
        public string TipoComprobante { get; set; } //
        public string NroDocumento { get; set; } //
        public string RazonSocial { get; set; } //
        public string Apellidos { get; set; } //
        public string Direccion { get; set; }//
        public string Departamento { get; set; } //
        public string Provincia { get; set; } //
        public string Distrito { get; set; } //
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}
