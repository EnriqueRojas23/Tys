

using QueryContracts.Common;
using System;
using System.Collections.Generic;
    
namespace QueryContracts.TYS.Facturacion.Results
{
    public class ObtenerComprobanteResult : QueryResult
    {
        public long idcomprobantepago { get; set; }
        public string numerocomprobante { get; set; }
        public int idtipocomprobante { get; set; }
        public int emisionrapida { get; set; }
        public long idpreliquidacion { get; set; }
        public int idcliente { get; set; }
        public string razonsocial { get; set; }
        public DateTime fechaemision { get; set; }
        public int idusuarioregistro { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public string ruc { get; set; }
        public int totalbulto { get; set; }
        public string motivo { get; set; }
        public string direccion { get; set; }
        public string tipocomprobantepago { get; set; }

    }
}
