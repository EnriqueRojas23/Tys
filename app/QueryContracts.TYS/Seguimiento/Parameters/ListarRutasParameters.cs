
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarRutaParameters : QueryParameter
    {
        public int? idorigen { get; set; }
        public int? iddestino { get; set; }
        public int? idruta { get; set; }
        public string criterio { get; set; }
        
    }
}
