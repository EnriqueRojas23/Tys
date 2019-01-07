using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarComprobanteResult : QueryResult
    {
        public IEnumerable<ListarComprobanteDto> Hits { get; set; }
    }
    public class ListarComprobanteDto
    {
        public int? idcomprobante { get; set; }
        public string serienumero { get; set; }
        public string fecharegistro { get; set; }
        public string fechacomprobante { get; set; }
        public string moneda { get; set; }
        public string monto { get; set; }
        public string tipocomprobante { get; set; }
        public string etapa { get; set; }
        public string tipotransporte { get; set; }
        public string razonsocial { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string ots { get; set; }
        public string numerocomprobanterelacionado { get; set; }
    }
}


