

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Facturacion
{
    public class InsertarActualizarComprobanteCommand : Command
    {
        public long? idcomprobantepago { get; set; }
        public string numerocomprobante { get; set; }
        public int idtipocomprobante { get; set; }
        public int emisionrapida { get; set; }
        public int idpreliquidacion { get; set; }
        public int idcliente { get; set; }
        public DateTime fechaemision { get; set; }
        public int idusuarioregistro { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public string descripcion { get; set; }
        public int idestado { get; set; }
        public int _tipoop { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public int totalbulto { get; set; }
        public decimal recargo { get; set; }
        public long idfacturavinculada { get; set; }
        public string ordencompra { get; set; }
    }
}
