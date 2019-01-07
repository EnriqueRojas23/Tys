

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class DetalleTarifa : Entity
    {
        [Key]
        public int iddetalletarifa { get; set; }
        public int idtarifa { get; set; }
        public int idconceptocobro { get; set; }

    }
}
