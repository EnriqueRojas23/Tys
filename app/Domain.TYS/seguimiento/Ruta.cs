

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Ruta : Entity
    {
        [Key]
        public int idruta { get; set; }
        public string nombre { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }
        public string ruta { get; set; }
        public string km { get; set; }

    }
}
