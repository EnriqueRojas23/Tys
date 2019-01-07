

using QueryContracts.Common;
using System.Collections.Generic;
    
namespace QueryContracts.TYS.Facturacion.Results
{
    public class ListarOTSxIdsResult : QueryResult
    {
        public IEnumerable<ListarOTSxIdsDto> Hits { get; set; }
    }
    public class ListarOTSxIdsDto
    {
        public long idordentrabajo { get; set; }
        public int idcliente { get; set; }
        public decimal peso	 {get;set;}
        public decimal volumen { get; set; }
        public decimal bulto { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal recargo { get; set; }
        public bool camioncompleto { get; set; }
    }
}
