using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarConceptosTarifaResult : QueryResult
    {
        public IEnumerable<ListarConceptosTarifaDto> Hits { get; set; }
    }
    public class ListarConceptosTarifaDto
    {
        public int idconceptocobro { get; set; }
        public string concepto { get; set; }
        public int iddistrito { get; set; }
        public int idprovincia { get; set; }
        public int iddepartamento { get; set; }
        public string formula { get; set; }
   

    }
}


