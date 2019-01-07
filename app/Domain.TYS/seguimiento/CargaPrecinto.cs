

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class CargaPrecinto : Entity
    {
        [Key]
        public long idcargaprecinto { get; set; }
        public long? iddespacho { get; set; }
        public long? idprecinto { get; set; }


    }
}
