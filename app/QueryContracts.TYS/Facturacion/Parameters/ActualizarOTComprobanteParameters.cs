
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Facturacion.Parameters
{
    public class ActualizarOTComprobanteParameters : QueryParameter
    {
        public string idsordentrabajo { get; set; }
        public DateTime? fechacomprobante { get; set; }
        

    }
}

