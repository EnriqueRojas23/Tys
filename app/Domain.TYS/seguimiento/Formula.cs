

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Formula : Entity
    {
        [Key]
        public int idformula { get; set; }
        public string nombre { get; set; }
        public string formula { get; set; }
        public bool activo { get; set; }

    }
}
