

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Despacho : Entity
    {
        [Key]
        public long iddespacho { get; set; }
        public int idvehiculo { get; set; }
        public DateTime? fechasalida { get; set; }
        public int? idusuariosalida { get; set; }
        public int idestado { get; set; }

    }
}
