using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguridad
{
    public class Alerta : Entity
    {

        [Key]
        public int? idalerta { get; set; }
        public int? usr_int_id { get; set; }
        public int idperiodicidad { get; set; }
        public string estados { get; set; }
        public int idmedio { get; set; }

    }
}
