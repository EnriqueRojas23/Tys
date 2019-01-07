

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Pago
{
    public class TipoTransporte : Entity
    {
        [Key]
        public int idtipotransporte { get; set; }
        public string tipotransporte { get; set; }
        public bool activo { get; set; }

    }
}
