

using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarOrdenesTrabajoResults : QueryResult
    {
        public IEnumerable<ListarOrdenesTrabajoDto> Hits { get; set; }
        
    }
    public class ListarOrdenesTrabajoDto
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string nummanifiesto { get; set; }
        public string destinatario { get; set; }
       
    }
}
