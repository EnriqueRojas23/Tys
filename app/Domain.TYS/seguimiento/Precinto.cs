

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Precinto : Entity
    {
        [Key]
        public int idprecinto { get; set; }
        public string precinto { get; set; }


    }
}
