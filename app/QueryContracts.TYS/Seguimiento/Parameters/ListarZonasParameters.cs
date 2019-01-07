
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarZonasParameters : QueryParameter
    {
        public int? iddepartamento { get; set; }
        public string zona { get; set; }
        
    }
}
