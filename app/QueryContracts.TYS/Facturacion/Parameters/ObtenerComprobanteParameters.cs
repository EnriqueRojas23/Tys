
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Facturacion.Parameters
{
    public class ObtenerComprobanteParameters : QueryParameter
    {
        public long? idpreliquidacion { get; set; }
        public long? idcomprobante { get; set; }
    }
}
