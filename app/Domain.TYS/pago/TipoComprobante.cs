

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Pago
{
    public class TipoComprobante : Entity
    {
        [Key]
        public int idtipocomprobante { get; set; }
        public string codigo { get; set; }
        public string tipocomprobante { get; set; }
        public bool activo { get; set; }

    }
}
