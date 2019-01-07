
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.TYS.Ordenes.Result
{
    public class ListarContenedorResult : QueryResult 
    {
        public IEnumerable<ListarContenedorDto> Hits { get; set; }
    }
    public class ListarContenedorDto
    {
        public string codcon04 { get; set; }
      
    }
}
