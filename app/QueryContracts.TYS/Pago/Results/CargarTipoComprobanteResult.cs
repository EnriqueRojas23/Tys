using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class CargarTipoComprobanteResult : QueryResult
    {
        public IEnumerable<CargarTipoComprobanteDto> Hits { get; set; }
    }
    public class CargarTipoComprobanteDto
    {
        public int idtipocomprobante { get; set; }
        public string tipocomprobante { get; set; }
    }
}


