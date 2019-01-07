using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarTipoComprobanteResult : QueryResult
    {
        public IEnumerable<ListarTipoComprobanteDto> Hits { get; set; }
    }
    public class ListarTipoComprobanteDto
    {
        public int idtipocomprobante { get; set; }
        public string codigo { get; set; }
        public string tipocomprobante { get; set; }
        public bool activo { get; set; }

    }
}


