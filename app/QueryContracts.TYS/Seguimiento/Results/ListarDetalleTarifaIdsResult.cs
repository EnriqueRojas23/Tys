using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDetalleTarifaIdsResult : QueryResult
    {
        public IEnumerable<ListarDetalleTarifaIdsDto> Hits { get; set; }
    }
    public class ListarDetalleTarifaIdsDto
    {
        public string idsconceptocobro { get; set; }

    }
}


