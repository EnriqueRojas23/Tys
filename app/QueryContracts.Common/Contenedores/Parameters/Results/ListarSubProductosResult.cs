
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Terminal.Contenedores.Results
{
    public class ListarSubProductosResult : QueryResult
    {
        public IEnumerable<ListarSubProductosDto> Hits { get; set; }
    }

    public class ListarSubProductosDto
    {
        public string CodigoSubProducto { get; set; }
        public string DescripcionSubProducto { get; set; }

    }
}
