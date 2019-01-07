

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Facturacion
{
    public class Preliquidacion : Entity
    {
        [Key]
        public long idpreliquidacion { get; set; }
        public string numeropreliquidacion { get; set; }
        public long? idcomprobantepago { get; set; }
        public int idcliente { get; set; }
        public int totalbulto { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public decimal recargo { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public int idestado { get; set; }


    }
}
