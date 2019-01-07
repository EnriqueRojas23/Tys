
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarManifiestosSinEmbarqueResults : QueryResult
    {
        public IEnumerable<ListarManifiestosSinEmbarqueDto> Hits { get; set; }
        
    }
    public class ListarManifiestosSinEmbarqueDto
    {
        public int iddestino { get; set; }
        public string distrito { get; set; }
        public long idmanifiesto { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public int idordentrabajo { get; set; }


    }
}
