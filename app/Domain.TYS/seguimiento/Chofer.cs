

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Chofer : Entity
    {
        [Key]
        public int idchofer { get; set; }
        public string nombrechofer { get; set; }
        public string apellidochofer { get; set; }
        public string dni { get; set; }
        public string brevete { get; set; }
        public string telefono { get; set; }
        public string direccionchofer { get; set; }
        public bool activo { get; set; }

    }
}
