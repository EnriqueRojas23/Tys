
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarCamionCompletoParameters : QueryParameter
    {
        public int? iddestino { get; set; }
        public string codigo  { get; set; }
        public int? idestacion { get; set; }
        
    }
}
