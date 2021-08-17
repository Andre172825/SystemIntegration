namespace SystemsIntegration.Api.Models.Entities
{
    public struct NotificacionDestinoEntity
    {
        public string Sistema { get; set; }
        public string Modulo { get; set; }
        public string Destino { get; set; }
        public string Accion { get; set; }
        public string TipoNotificacion { get; set; }
        public string Adicional1 { get; set; }
        public string Adicional2 { get; set; }
    }
}
