
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Results
{
    public class ListarItemSearchResult : QueryResult
    {
        public IEnumerable<ListarItemSearchDto> Hits { get; set; }
    }

    public class ListarItemSearchDto
    {
        public string IdItem { get; set; }
        public string DesItem { get; set; }
        public string CodItem { get; set; }
        public string DesItemComp { get; set; }
        public string CodItemDescripcion { get { return string.Format("{0}|{1}", CodItem, DesItem); } }

    }
}
