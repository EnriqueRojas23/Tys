

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Core.NPTP2.Results
{
    public class ListadoNaveResult : QueryResult
    {
        public IEnumerable<ListadoNaveDto> Hits { get; set; }
    }
    public class ListadoNaveDto 
    {
        public string CodNav08 { get; set; }
        public string DesNav08 { get; set; }

    }

}
