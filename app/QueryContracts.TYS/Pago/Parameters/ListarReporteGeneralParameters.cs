
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Pago.Parameters
{
    public class ListarReporteGeneralParameters : QueryParameter
    {
        public int? idproveedor { get; set; }
        public int? iddestino { get; set; }
        public string fechaini { get; set; }
        public string fechafin { get; set; }
    }
}
