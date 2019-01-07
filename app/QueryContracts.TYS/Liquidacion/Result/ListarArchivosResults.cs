
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarArchivosResults : QueryResult
    {
        public IEnumerable<ListarArchivosDto> Hits { get; set; }
        
    }
    public class ListarArchivosDto
    {
        public long idarchivo { get; set; }
        public long idordentrabajo { get; set; }
        public string nombrearchivo { get; set; }
        public string rutaacceso { get; set; }
        public string extension { get; set; }

    }
}
