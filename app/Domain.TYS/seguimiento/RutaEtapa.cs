

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class RutaEtapa : Entity
    {
        [Key]
        public int idrutasetapas { get; set; }
        public int iddetalleruta { get; set; }
        public int idetapa { get; set; }

    }
}
