
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Core.SGCW.Results
{
    public class ListarProductosResult : QueryResult
    {
        public IEnumerable<ListarProductosDto> Hits { get; set; }
    }

    public class ListarProductosDto
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
    }
}
