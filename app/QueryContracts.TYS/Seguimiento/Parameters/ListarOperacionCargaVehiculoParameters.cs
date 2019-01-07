
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarOperacionCargaVehiculoParameters : QueryParameter
    {
        public int idestado { get; set; }
        public long? idcarga { get; set; }
        
    }
}
