

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Zona : Entity
    {
        [Key]
        public int idzona { get; set; }
        public string zona { get; set; }

    }
}
