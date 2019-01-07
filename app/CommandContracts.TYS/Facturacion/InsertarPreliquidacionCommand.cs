

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Facturacion
{
    public class InsertarActualizarPreliquidacionCommand : Command
    {

        public long? idpreliquidacion { get; set; }
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
        public int _tipoop { get; set; }


    }
}
