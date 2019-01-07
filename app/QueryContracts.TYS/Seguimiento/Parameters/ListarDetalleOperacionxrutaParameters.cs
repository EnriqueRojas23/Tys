
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarDetalleOperacionxRutaParameters : QueryParameter
    {
        public int idruta { get; set; }
        public int idorigen { get; set; }
        public int? iddistrito  { get; set; }
        public int? idprovincia { get; set; }
        public int? iddepartamento { get; set; }
        
    }
}
