using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.TYS.Ordenes.Result
{
    public class ListarNaveResult : QueryResult
    {
        public IEnumerable<ListarNaveDto> Hits { get; set; }
    }
    public class ListarNaveDto
    {
        public string codnav08 { get; set; }
        public string desnav08 { get; set; }
    }
}