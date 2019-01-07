using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarFormulasResult : QueryResult
    {
        public IEnumerable<ListarFormulasDto> Hits { get; set; }
    }
    public class ListarFormulasDto
    {
        public int idformula { get; set; }
        public string nombre { get; set; }
        public string formula { get; set; }
        public bool activo { get; set; }

    }
}


