

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Monitoreo
{
    public class DetalleEtapa : Entity
    {
        [Key]
        public long iddetalleetapa { get; set; }
        public long idetapa { get; set; }
        public long idguiaremision { get; set; }
        public int cantidad { get; set; }

    }
}
