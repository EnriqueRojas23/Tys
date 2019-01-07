using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.TYS.Ordenes.Result
{
    public class ListarViajeResult : QueryResult
    {
        public IEnumerable<ListarViajeDto> Hits { get; set; }
    }
    public class ListarViajeDto
    {
        public string numvia11 { get; set; }
        public string navvia11 { get; set; }
        public string codnav08 { get; set; }
        public System.DateTime fecdes11 { get; set; }
    }
}
