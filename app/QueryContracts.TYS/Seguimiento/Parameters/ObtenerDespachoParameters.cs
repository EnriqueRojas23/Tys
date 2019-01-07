
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ObtenerDespachoParameters : QueryParameter
    {
        public int idvehiculo { get; set; }
        public int idestado { get; set; }
        
    }
}
