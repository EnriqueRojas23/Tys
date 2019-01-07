
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Facturacion.Parameters
{
    public class ListarPendientePreliquidacionParameters : QueryParameter
    {
        public string numcp { get; set; }
        public int? iddestino { get; set; }
        public int? idcliente { get; set; }
    }
}
