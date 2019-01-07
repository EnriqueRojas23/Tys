

using QueryContracts.Common;
using System;
using System.Collections.Generic;
    
namespace QueryContracts.TYS.Facturacion.Results
{
    public class ListarComprobanteDetallesResult : QueryResult
    {
        public IEnumerable<ListarComprobanteDetallesDto> Hits { get; set; }
    }
    public class ListarComprobanteDetallesDto  
    {
        public long iddetallecomprobante { get; set; }
        public int idcomprobantepago { get; set; }
        public string descripcion { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal recargo { get; set; }

    }
}
