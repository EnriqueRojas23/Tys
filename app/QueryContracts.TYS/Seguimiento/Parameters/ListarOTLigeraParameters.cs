
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarOTLigeraParameters : QueryParameter
    {
        public int? idcliente { get; set; }
        public string numcp { get; set; }
        public bool devolucion { get; set; }
        
    }
}
