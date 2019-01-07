using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.TYS.Facturacion.Results
{
    public class ObtenerPreliquidacionResult : QueryResult
    {
        public IEnumerable<ObtenerPreliquidacionDto> Hits { get; set; }
    }

    public class ObtenerPreliquidacionDto
    {
        public long idpreliquidacion { get; set; }
        public string numeropreliquidacion { get; set; }
        public long? idcomprobantepago { get; set; }
        public int idcliente { get; set; }
        public decimal totalbulto { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public decimal recargo { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public int idestado { get; set; }
        public string direccion { get; set; }
        public string ruc { get; set; }
        public string razonsocial { get; set; }
        public string ordenes { get; set; }
        public string placas { get; set; }
        public string guiasrr { get; set; }
    }
}