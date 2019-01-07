

using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarEventosResults : QueryResult
    {
        public IEnumerable<ListarEventosDto> Hits { get; set; }
        
    }
    public class ListarEventosDto
    {
        public long ot { get; set; }
        public string numcp { get; set; }
        public DateTime fechaevento { get; set; }
        public string tipoevento { get; set; }
        public int idmaestroevento { get; set; }
        public string evento { get; set; }
        public string observacion { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public int idusuarioregistro { get; set; }
        public DateTime fecharegistro { get; set; }
        public bool autogenerada { get; set; }
        public string usuario {get;set;}
        public string estacionorigen { get; set; }

       
    }
}
