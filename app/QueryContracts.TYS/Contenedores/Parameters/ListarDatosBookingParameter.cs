using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Contenedores.Parameters
{
    public class ListarDatosBookingParameter : QueryParameter
    {
        public string rb_str_numero_booking { get; set; }
        public DateTime? rb_dat_fecha_reserva_inicio { get; set; }
        public DateTime? rb_dat_fecha_reserva_fin { get; set; }
        public string mlt_str_estado_reserva { get; set; }
        //
        // --| Inicio
        // --| Autor  : MGZP 
        // --| Fecha  : 20/09/2016 03:31 p.m.
        // --| Motivo : Agregar Parametros Para Cargar Solo información Del Usuario Logueado
        // --|          - Tipo Usuario : EXT --> Valido Tipo de Cliente (Agente de Aduana o Agente de Carga)
        // --|          - Tipo Usuario : INT --> Muestra Toda la información 
        // --| 
        public string qp_str_Usuario { get; set; }
        //public string qp_str_Perfil { get; set; }
        // --| 
        // --| Fin
        //
    }
}
