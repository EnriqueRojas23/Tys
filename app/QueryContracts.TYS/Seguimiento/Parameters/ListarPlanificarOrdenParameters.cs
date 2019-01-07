
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarPlanificarOrdenParameters : QueryParameter
    {
        public int? idestacionorigen { get; set; }
        public int? iddestino { get; set; }
        public int? idcliente { get; set; }
        public int idestado { get; set; }
        public int? idtipotransporte { get; set; }
        
    }
}
