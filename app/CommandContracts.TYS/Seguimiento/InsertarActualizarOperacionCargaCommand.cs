

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarOperacionCargaCommand : Command
    {
        public long? idcarga { get; set; }
        public string numcarga { get; set; }
        public int idcliente { get; set; }
        public decimal vol { get; set; }
        public decimal peso { get; set; }
        public int idproveedor { get; set; }
        public int? idvehiculo { get; set; }
        public DateTime fechacreacion { get; set; }
        public string observacion { get; set; }
        public int idplanificador { get; set; }
        public int idagencia { get; set; }
        public bool activo { get; set; }

        public int idtipooperacion { get; set; }
        public int idestacion { get; set; }
        public int idruta { get; set; }
        public int? idmuelle { get; set; }
        public int idestado { get; set; }
        public int tipooperacion { get; set; }


        public DateTime? fechaconfirmacion { get; set; }
        public string horaconfirmacion { get; set; }
        public DateTime? fechasalida { get; set; }
        public string horasalida { get; set; }
    }
}
