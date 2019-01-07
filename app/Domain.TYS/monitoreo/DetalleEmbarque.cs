

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Monitoreo
{
    public class DetalleEmbarque : Entity
    {
        [Key]
        public long iddetalleembarque { get; set; }
        public long? idembarque { get; set; }
        public long? idmanifiesto { get; set; }
        public long? idordentrabajo { get; set; }
        public int? idpuerto { get; set; }
        public DateTime? fechadescarga { get; set; }
        public int? idusuariodescarga { get; set; }
        public DateTime? fechacontrolsunat { get; set; }
        public int? idusuariocontrolsunat { get; set; }
        public bool embarquecompleto { get; set; }


    }
}
