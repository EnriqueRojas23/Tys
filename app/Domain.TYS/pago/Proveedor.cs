

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Pago
{
    public class Proveedor : Entity
    {
        [Key]
        public int idproveedor { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string placaasociada { get; set; }
        public bool activo { get; set; }
        public string direccion { get; set; }
    }
}
