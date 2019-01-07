

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.TYS.Ordenes.Result
{
    public class ListarCargasResult : QueryResult 
    {
        public IEnumerable<ListarCargasDto> Hits { get; set; }
    }
    public class ListarCargasDto
    {
        public string nrocar16 { get; set; }
        public string sucursal { get; set; }
        public string NroSec { get; set; }
    }
}
