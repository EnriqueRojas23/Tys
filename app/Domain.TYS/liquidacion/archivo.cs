

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Monitoreo
{
    public class Archivo : Entity
    {
        [Key]
        public long idarchivo { get; set; }
        public long idordentrabajo { get; set; }
        public string nombrearchivo { get; set; }
        public string rutaacceso { get; set; }
        public string extension { get; set; }

    }
}
