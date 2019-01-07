

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class ProveedorCliente : Entity
    {
        [Key]
        public int idproveedorcliente { get; set; }
        public int idproveedor { get; set; }
        public int idcliente { get; set; }

    }
}
