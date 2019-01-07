

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class ServicioOperacion : Entity
    {
        [Key]
        public long idserviciooperacion { get; set; }
        public long idcarga { get; set; }
        public int idservicio { get; set; }
        public int cantidad { get; set; }
     

    }
}
