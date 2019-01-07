
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ActualizarOTConfirmacionParameters : QueryParameter
    {
        public string idsordentrabajo { get; set; }
        public int idestado { get; set; }
        public DateTime fechaconfirmacion { get; set; }
        

    }
}

