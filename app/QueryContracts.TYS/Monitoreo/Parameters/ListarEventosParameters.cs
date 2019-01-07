

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarEventosParameters : QueryParameter
    {

        public string numcp  { get; set; }
        public int? idmaestroetapa { get; set; }
        public int? idmaestroincidencia { get; set; }
        public long? idorden { get; set; }
        
 
    }
}
