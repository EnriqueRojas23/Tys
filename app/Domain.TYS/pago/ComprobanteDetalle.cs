

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Pago
{
    public class ComprobanteProveedorDetalle : Entity
    {
        [Key]
        public long idcomprobantedetalle { get; set; }
        public long idcomprobante { get; set; }
        public string numcp { get; set; }
        public int PKID { get; set; }
        public string ValorVenta { get; set; }
        public string TotalBultos { get; set; }
        public string TotalPeso { get; set; }
        public string Precio { get; set; }
        public string SubTotal { get; set; }
        public string Total  { get; set; }
        public string nroguia { get; set; }
        public string manifiesto { get; set; }


    }
}
