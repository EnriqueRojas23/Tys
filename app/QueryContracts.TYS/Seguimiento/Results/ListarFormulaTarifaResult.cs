using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarFormulaTarifaResult : QueryResult
    {
        public IEnumerable<ListarFormulaTarifaDto> Hits { get; set; }
    }
    public class ListarFormulaTarifaDto
    {
        public int idformula { get; set; }
        public string formula { get; set; }
        public int iddistrito { get; set; }
        public int idprovincia { get; set; }
        public int iddepartamento { get; set; }

    }
}


