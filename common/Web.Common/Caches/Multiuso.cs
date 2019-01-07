

using System;
namespace Web.Common.Caches
{
    public class Multiuso
    {
        public enum Tipo
        {
            EstadoReserva = 1,
            EstatusRegistroBooking = 7,
            TipoDocumento = 14,
        }

        public int mlt_int_id { get; set; }
        public int mlt_int_id_padre { get; set; }
        public string mlt_str_nombre { get; set; }
        public string mlt_str_descripcion { get; set; }
        public string mlt_str_valor { get; set; }
        public string mlt_str_alcance { get; set; }
        public DateTime mlt_dat_fecha_creacion { get; set; }
        public string mlt_str_usuario_creacion { get; set; }
    }
}
