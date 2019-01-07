
using CommandContracts.Common;
using System.Collections.Generic;
namespace CommandContracts.TYS.Contenedor
{
    

    public class ActualizarEstadoReservaDetalleCommand : Command
    {
        //--| MGZP - Inicio
        //--| Fecha  : 12/09/2016
        //--| Motivo : Agregar Estados
        //--|          - Expirado  = 7
        //--|          - Extornado = 8
        public enum EstadoReservaDetalle { Registrado = 0, EnviadoPagoEfectivo = 1, PendientePagar = 2, Pagado = 3, AsignadoTransportista = 4, Cerrado = 5, Eliminado = 6, Expirado = 7, Extornado = 8 };
        //--| MGZP - Fin
        //
        public List<long> lst_rbd_int_id { get; set; }
        public string rb_str_numero_booking { get; set; }
        public EstadoReservaDetalle rbd_str_estado_bookingdet { get; set; }
        public string DescripcionOpcional { get; set; }
        public string UsuarioCreacion { get; set; }

    }
}
