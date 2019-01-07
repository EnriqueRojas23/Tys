

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Pago
{
    public class Asignacion : Entity
    {
        [Key]
        public int idasignacion  { get; set; }
        public int idproveedor { get; set; }
        public int idetapa { get; set; }
        public int idtipotransporte { get; set; }
        public int idmoneda { get; set; }
        public int idtipocomprobante { get; set; }
            
    }
}
