

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarDetalleEtapaCommand : Command
    {
        public long? iddetalleetapa { get; set; }
        public long idetapa { get; set; }
        public long idguiaremision { get; set; }
        public int cantidad { get; set; }

    }
}
