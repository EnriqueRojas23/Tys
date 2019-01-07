

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Monitoreo
{
    public class Embarque : Entity
    {
        [Key]
        public long idembarque { get; set; }
        public string numeroembarque { get; set; }
        public string conocimientoembarque { get; set; }
        public int idtransporte { get; set; }
        public int idpuerto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public DateTime? fechainiciocarga { get; set; }
        public DateTime? fechafincarga { get; set; }
        public DateTime? fechazarpe { get; set; }
        public DateTime? fechaatraque { get; set; }
        public DateTime? fechallegada { get; set; }
        public bool embarquecompleto { get; set; }


    }
}
