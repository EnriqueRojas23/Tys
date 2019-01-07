

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Facturacion
{
    public class ActualizarMontosOTCommand : Command
    {
        public long? idordentrabajo { get; set; }
        public decimal? subtotal { get; set; }
        public decimal? igv { get; set; }
        public decimal? total { get; set; }

    }
}
