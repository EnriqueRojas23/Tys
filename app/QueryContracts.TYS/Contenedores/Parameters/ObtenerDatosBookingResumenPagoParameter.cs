

using QueryContracts.Common;
namespace QueryContracts.TYS.Contenedores.Parameters
{
    public class ObtenerDatosBookingResumenPagoParameter : QueryParameter
    {
        public string rb_str_numero_booking { get; set; }
        public string rbd_int_id_array { get; set; }
    }
}
