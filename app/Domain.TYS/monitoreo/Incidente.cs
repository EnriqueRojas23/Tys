

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Monitoreo
{
    public class Incidencia : Entity
    {
        [Key]
        public long idincidencia { get; set; }
        public int idmaestroincidencia { get; set; }
        public long idordentrabajo { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public DateTime fechaincidencia { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public bool activo { get; set; }

    }
}
