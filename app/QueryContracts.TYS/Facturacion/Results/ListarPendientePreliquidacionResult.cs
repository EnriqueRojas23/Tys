using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.TYS.Facturacion.Results
{
    public class ListarPendientePreliquidacionResult : QueryResult
    {
        public IEnumerable<ListarPendientePreliquidacionDto> Hits { get; set; }
    }

    public class ListarPendientePreliquidacionDto
    {
        public long idordentrabajo { get; set; }
        public DateTime fecharegistro { get; set; }
        public string numcp { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string modotransporte { get; set; }
        public string tipooperacion { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public decimal pesovol { get; set; }
        public int bulto { get; set; }
        public decimal tarifa { get; set; }
        public decimal subtotal { get; set; }
        public decimal base1 { get; set; }
        public decimal total { get; set; }
        public decimal recargo { get; set; }
        public decimal igv { get; set; }
        public string guiatransportista { get; set; }
        public string conceptocobro { get; set; }
    }
}