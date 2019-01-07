

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Pago
{
    public class Moneda : Entity
    {
        [Key]
        public int idmoneda { get; set; }
        public string moneda { get; set; }
        public bool activo { get; set; }
    }
}
