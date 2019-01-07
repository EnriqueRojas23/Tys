
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Facturacion.Parameters
{
    public class ListarPreliquidacionParameters : QueryParameter
    {
        public int? idestado { get; set; }
        public string numerocomprobante { get; set; }
        public string numeroliquidacion { get; set; }
        public int? idcliente { get; set; }
        public int? idtipocomprobante { get; set; }

    }
}
