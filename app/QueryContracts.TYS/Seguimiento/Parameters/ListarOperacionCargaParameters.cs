
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarOperacionCargaParameters : QueryParameter
    {
        public string numcp { get; set; }
        public string numcarga { get; set; }
        public int idestado { get; set; }
        public int? idestacion { get; set; }
        
    }
}
