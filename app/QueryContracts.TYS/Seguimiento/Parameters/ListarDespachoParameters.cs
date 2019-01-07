
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarDespachoParameters : QueryParameter
    {
        public int? idestado { get; set; }
        public int? idvehiculo { get; set; }
        public string numcp { get; set; }
        public string numcarga { get; set; }
        
    }
}
