
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarMaestroIncidenteResults : QueryResult
    {
        public IEnumerable<ListarMaestroIncidenteDto> Hits { get; set; }
        
    }
    public class ListarMaestroIncidenteDto
    {
        public int idmaestroincidencia { get; set; }
        public string codincidencia { get; set; }
        public string descripcion { get; set; }
        public DateTime fechafin { get; set; }
        public string tipo { get; set; }
        public bool seactualiza { get; set; }
        public bool esfecha { get; set; }
    }
}
