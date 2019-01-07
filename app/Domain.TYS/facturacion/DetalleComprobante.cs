

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Facturacion
{
    public class DetalleComprobante : Entity
    {
        [Key]
        public long iddetallecomprobante { get; set; }
        public long idcomprobantepago { get; set; }
        public long? idordentrabajo { get; set; }
        public string descripcion { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal? recargo { get; set; }
    }
}
