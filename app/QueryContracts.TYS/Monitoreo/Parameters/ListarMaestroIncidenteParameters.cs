

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarMaestroIncidenteParameters : QueryParameter
    {
        public string tipoincidencia { get; set; }
        public int? idmaestroincidencia { get; set; }
    
 
    }
}
