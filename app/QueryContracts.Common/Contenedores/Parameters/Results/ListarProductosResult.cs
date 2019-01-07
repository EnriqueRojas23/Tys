

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Terminal.Contenedores.Results
{
    public class ListarProductosResult : QueryResult
    {
        public IEnumerable<ListarProductosDto> Hits { get; set; }
    }
    public class ListarProductosDto 
    {
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }

    }
}
