using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarDetalleComprobanteResult : QueryResult
    {
        public IEnumerable<ListarDetalleComprobanteDto> Hits { get; set; }
    }
    public class ListarDetalleComprobanteDto
    {
        public long? idcomprobantedetalle { get; set; }
        public long idcomprobante { get; set; }
        public string numcp { get; set; }
        public int PKID { get; set; }
        public string subtotal { get; set; }
        public string total { get; set; }
        public string precio { get;set; }
        public string totalpeso { get; set; }
        public string totalbultos { get; set; }
        public string guiarecojo { get; set; }
        public string nroguia { get; set; }
        public string manifiesto { get; set; }
        public string valorventa { get;set;}
    }
}


