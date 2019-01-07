
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Core.SGCW.Results
{
    public class ListarSubProductosResult : QueryResult
    {
        public IEnumerable<ListarSubProductosDto> Hits { get; set; }
    }

    public class ListarSubProductosDto
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
    }
}
