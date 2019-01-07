
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarEtapasMonitoreoResults : QueryResult
    {
        public IEnumerable<ListarEtapasMonitoreoDto> Hits { get; set; }
        
    }
    public class ListarEtapasMonitoreoDto
    {
        public int idmaestroetapa { get; set; }
        public string descripcion { get; set; }
        public string tipoetapa { get; set; }
        public bool etapaunica { get; set; }
        public bool activo { get; set; }
        public bool autogenerada { get; set; }
        public string grr { get; set; }

    }
}
