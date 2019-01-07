

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class OperacionCarga : Entity
    {
        [Key]
        public long idcarga { get; set; }
        public string numcarga { get; set; }
        public decimal vol { get; set; }
        public decimal peso { get; set; }
        public int? idproveedor { get; set; }
        public int? idvehiculo { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public string observacion { get; set; }
        public int idplanificador { get; set; }
        public int idagencia { get; set; }
        public int idestacion { get; set; }
        public int idruta { get; set; }
        public bool activo { get; set; }
        public int? idmuelle { get; set; }
        public int idestado { get; set; }
        public DateTime? fechaconfirmacion { get; set; }
        public DateTime? fechasalida { get; set; }

    }
}
