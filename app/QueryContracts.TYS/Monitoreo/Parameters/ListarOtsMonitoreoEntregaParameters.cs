

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarOtsMonitoreoEntregaParameters : QueryParameter
    {

        public int? idcliente { get; set; }
        public int? iddestino { get; set; }
        public string numhojaruta { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public int? idestado { get; set; }
        public string documento { get; set; }
        public string grr { get; set; }
        public string tienda { get; set; }
        public int idtipotransporte { get; set; }
    }
}
