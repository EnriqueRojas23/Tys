
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarOrdenesxManifiestoResults : QueryResult
    {
        public IEnumerable<ListarOrdenesxManifiestoDto> Hits { get; set; }
        
    }
    public class ListarOrdenesxManifiestoDto
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public int idestado { get; set; }
       
    }
}
