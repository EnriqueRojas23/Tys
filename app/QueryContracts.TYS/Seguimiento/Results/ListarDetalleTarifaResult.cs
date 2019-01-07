using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDetalleTarifaResult : QueryResult
    {
        public IEnumerable<ListarDetalleTarifaDto> Hits { get; set; }
    }
    public class ListarDetalleTarifaDto
    {
        public int idconceptocobro { get; set; }
    
        

            


    }
}


