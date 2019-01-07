
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ActualizarOTPlanificacionParameters : QueryParameter
    {
        public string idsordentrabajo { get; set; }
        public long? idcarga { get; set; }
        public int idestado { get; set; }
        public int idtipooperacion { get; set; }
        public int? idruta { get; set; }
        public int? idestaciondestino { get; set; }
        public int? idagencia { get; set; }
        public DateTime fechaplanificacion { get; set; }
        

    }
}

