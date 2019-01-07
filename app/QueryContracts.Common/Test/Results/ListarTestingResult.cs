using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Terminal.Test.Results
{
    public class ListarTestingResult : QueryResult
    {
        public IEnumerable<ListarTestingDto> Hits { get; set; }
    }

    public class ListarTestingDto
    {
        public int id_test { get; set; }
        public string descripcion { get; set; }
    }
}
