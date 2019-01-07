using QueryContracts.Common;
using System;

namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarOrdenTrabajoParameters : QueryParameter
    {
        public int? idcliente { get; set; }
        public string numcp { get; set; }
        public string fecinicio { get; set; }
        public string fecfin { get; set; }
        public int? idestacion { get; set; }
        public int? pagenumber { get; set; }
        public int? pagesize { get; set; }
    }
}