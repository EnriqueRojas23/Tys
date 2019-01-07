

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.TYS.Ordenes.Result
{
    public class ListarServiciosResult : QueryResult
    {
        public IEnumerable<ListarServiciosDto> Hits { get; set; }
    }
    public class ListarServiciosDto
    {
        public string codcon14 { get; set; }
        public string descon14 { get; set; }
    }
}
