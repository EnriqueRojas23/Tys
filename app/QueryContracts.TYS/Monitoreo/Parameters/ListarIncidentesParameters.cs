

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarIncidentesParameters : QueryParameter
    {

        public long idorden  { get; set; }
        public int? idmaestroincidencia { get; set; }
        
 
    }
}
