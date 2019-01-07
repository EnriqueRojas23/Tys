
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarVehiculoParameters : QueryParameter
    {
        public string placa { get; set; }
        public int? idestado { get; set; }
        
    }
}
