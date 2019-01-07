

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class DetalleValija : Entity
    {
        [Key]
        public long iddespachovalija { get; set; }
        public long idordentransporte { get; set; }
        public long iddespacho { get; set; }
      

    }
}
