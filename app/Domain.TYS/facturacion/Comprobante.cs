

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Facturacion
{
    public class Comprobante : Entity
    {
        [Key]
        public long idcomprobantepago { get; set; }
        public string numerocomprobante { get; set; }
        public int idtipocomprobante { get; set; }
        public int emisionrapida { get; set; }
        public long? idpreliquidacion { get; set; }
        public int idcliente { get; set; }
        public DateTime fechaemision { get; set; }
        public int idusuarioregistro { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public string descripcion { get; set; }
        public int idestado { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public int totalbulto { get; set; }
        public long idfacturavinculada { get; set; }
        public string ordencompra { get; set; }
        


    }
}
