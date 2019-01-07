
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Contenedores.Results
{
    public class ListarDatosBookingResult : QueryResult
    {
        public IEnumerable<ListarDatosBookingDto> Hits { get; set; }
    }

    public class ListarDatosBookingDto
    {
        public long rb_int_id { get; set; }
        public string rb_str_numero_booking { get; set; }
        public DateTime? rb_dat_fecha_reserva { get; set; }
        public int rb_int_espacios { get; set; }
        public string mlt_str_estado_reserva { get; set; }
        public string mlt_str_estado_reserva_nombre { get; set; }
        public DateTime? rb_dat_fecha_creacion { get; set; }
        public string rb_str_usuario_creacion { get; set; }
    }
}
