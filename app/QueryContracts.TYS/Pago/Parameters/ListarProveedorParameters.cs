
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Pago.Parameters
{
    public class ListarProveedorParameters : QueryParameter
    {
        public string criterio{ get; set; }
        public bool activo { get; set; }
    }
}
