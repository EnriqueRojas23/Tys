
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarManifiestosAsignadosEmbarqueResults : QueryResult
    {
        public IEnumerable<ListarManifiestosAsignadosEmbarqueDto> Hits { get; set; }
        
    }
    public class ListarManifiestosAsignadosEmbarqueDto
    {
        public int iddestino { get; set; }
        public string distrito { get; set; }
        public long idmanifiesto { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public int idordentrabajo { get; set; }
        public DateTime? fechadescarga { get; set; }
        public DateTime? fechacontrolsunat { get; set; }


    }
}
