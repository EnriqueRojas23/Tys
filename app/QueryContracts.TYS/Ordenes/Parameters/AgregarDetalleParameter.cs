

using QueryContracts.Common;
namespace QueryContracts.TYS.Ordenes.Parameters
{
    public class AgregarDetalleParameter : QueryParameter
    {
        public string NroOr32 { get; set; }
        public string Valing32 { get; set; }
        public string NroCar { get; set; }
        public string NroSec { get; set; }
        public string Sucursal { get; set; }
    }
}
