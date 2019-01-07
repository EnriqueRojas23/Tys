

using QueryContracts.Common;
using System;
using System.Collections.Generic;
    
namespace QueryContracts.TYS.Facturacion.Results
{
    public class ListarPreliquidacionResult : QueryResult
    {
        public IEnumerable<ListarPreliquidacionDto> Hits { get; set; }
    }
    public class ListarPreliquidacionDto  
    {
        public long idpreliquidacion { get; set; }
        public long idcomprobantepago { get; set; }
        public string numeropreliquidacion { get; set; }
        public string numerocomprobante { get; set; }
        public string tipocomprobantepago { get; set; }
        public string cliente { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public int totalbulto { get; set; }
        public decimal subtotal { get; set; }
        public decimal recargo { get; set; }
        public decimal total { get; set; }
        public DateTime? fechaemision { get; set; }
    }
}
