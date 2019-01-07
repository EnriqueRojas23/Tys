

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarOtsFluvialParameters : QueryParameter
    {

        public int? idcliente { get; set; }
        public int? iddestino { get; set; }
        public string numhojaruta { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public int? idestado { get; set; }
        public string documento { get; set; }
    }
}
