
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.TYS.Ordenes.Result
{
    public class ListarConsolidadorResult : QueryResult
    {
        public IEnumerable<ListarConsolidadorDto> Hits { get; set; }
    }
    public class ListarConsolidadorDto
    {
        public string CONTRIBUY { get; set; }
        public string NOMBRE { get; set; }
    }
}