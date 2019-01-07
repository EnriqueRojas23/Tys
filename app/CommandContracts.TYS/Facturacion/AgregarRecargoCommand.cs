

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Facturacion
{
    public class AgregarRecargoCommand : Command
    {
        public long? idordentrabajo { get; set; }
        public decimal recargo { get; set; }
    }
}
