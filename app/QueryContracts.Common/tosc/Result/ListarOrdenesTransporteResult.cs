

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Core.CRM.Result
{
    public class ListarOrdenesTransporteResult : QueryResult
    {
        public IEnumerable<ListarOrdenesTransporteDto> Hits { get; set; }

    }
    public class ListarOrdenesTransporteDto
    {
        public int PKID  { get; set; }
        public string NumCp { get; set; }
        public string ValorVenta { get; set; }
        public string TotalBultos { get; set; }
        public string TotalPeso { get; set; }
        public string Precio { get; set; }
        public string SubTotal { get; set; }
        public string Total { get; set; }
        public string GR { get;set; }
        public string manifiesto { get; set; }
    }
}
