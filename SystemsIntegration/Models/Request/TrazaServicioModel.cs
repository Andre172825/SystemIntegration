using System;

namespace SystemsIntegration.Api.Models.Request
{
    public struct TrazaServicioModel
    {
        public int IdServicio { get; set; }
        public string CodigoObjeto { get; set; }
        public string Cabecera { get; set; }
        public string Cuerpo { get; set; }
        public string Firma { get; set; }
    }
}
