using QueryContracts.Common;
namespace QueryContracts.TYS.Contenedores.Parameters
{
    public class ObtenerDatosBookingDespachoParameter : QueryParameter
    {
        public string rb_str_numero_booking { get; set; }
        public long? rbd_int_id { get; set; }
    }
}
