using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SystemsIntegration.Api.CrossCutting
{
    public static class Constantes
    {
        // SERVICIOS PROVEEDORES
        public const int SERVICIO_RIQRA_VENTA_B2C = 1;
        public const int SERVICIO_RIQRA_VENTA_B2B = 2;


        // EVENTOS POR SISTEMA
        public const int EVENTO_SISTEMA_ERROR_VALIDACION_FIRMA = 1;
        public const int EVENTO_SISTEMA_ERROR_OBTENER_CLIENTE = 2;
        public const int EVENTO_SISTEMA_ERROR_VALIDAR_PEDIDO_ALMACEN = 3;
        public const int EVENTO_SISTEMA_ERROR_REGISTRAR_PEDIDO_UNISHOP = 4;
        public const int EVENTO_SISTEMA_REGISTRO_EXITOSO_SPRING = 5;
        public const int EVENTO_SISTEMA_ERROR_DE_SERVICIO = 6;
        public const int EVENTO_SISTEMA_ERROR_ACTUALZAR_ESTADO_PEDIDO = 7;
        public const int EVENTO_SISTEMA_ACTUALZACION_ESTADO_PEDIDO = 8;


        // DESCRIPCION PLATAFORMA
        //public const string RIQRA_PLATAFOMA_B2C = "B2C";


        // ESTADOS DE PEDIDOS
        public const string SPRING_ESTADO_FACTURADO = "FA";
        public const string SPRING_ESTADO_APROBADO = "AP";
        public const string SPRING_ESTADO_CERRADO = "CE";
        public const string SPRING_ESTADO_ANULADO = "AN";
        public const string SPRING_ESTADO_EN_PREPARACION = "PR";

        // DESTINATARIOS DE NOTIFICACIONES
        public static readonly IList<string> LISTA_DESTINARIOS_ERROR = 
                new ReadOnlyCollection<string>(new List<string> {"eddiegomez@unilene.com", "dantegalvan@unilene.com"});

        public static readonly IList<string> LISTA_DESTINARIOS_SUCCESS =
                new ReadOnlyCollection<string>(new List<string> {"eddiegomez@unilene.com", "dantegalvan@unilene.com"});
    }
}
