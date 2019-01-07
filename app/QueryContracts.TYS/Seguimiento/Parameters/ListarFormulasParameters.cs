
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarFormulasParameters : QueryParameter
    {
        public string criterio { get; set; }
        public bool activo {get;set;}
        
    }
}
