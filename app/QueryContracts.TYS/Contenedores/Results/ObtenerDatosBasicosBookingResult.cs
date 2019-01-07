

using QueryContracts.Common;
namespace QueryContracts.TYS.Contenedores.Results
{
    public class ObtenerDatosBasicosBookingResult : QueryResult
    {
        public long rb_int_id { get; set; } 
		public string rb_str_numero_booking  { get; set; }
		public int rb_int_espacios  { get; set; }
		public int rb_int_espacios_ocupados  { get; set; }
        public int rb_int_espacios_disponibles { get; set; }
        public decimal? rb_int_identificador_terminal { get; set; }
    }
}
