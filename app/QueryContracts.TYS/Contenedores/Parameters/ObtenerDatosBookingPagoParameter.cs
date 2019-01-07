

using QueryContracts.Common;
namespace QueryContracts.TYS.Contenedores.Parameters
{
    public class ObtenerDatosBookingPagoParameter : QueryParameter
    {
        public string rb_str_numero_booking { get; set; }
        public long rbp_int_id { get; set; }
    }
}
