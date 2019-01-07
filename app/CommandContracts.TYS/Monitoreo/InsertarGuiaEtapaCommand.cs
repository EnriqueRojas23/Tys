

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarGuiaEtapaCommand : Command
    {
        public long? idguiaetapa { get; set; }
        public long idguiaremisioncliente { get; set; }
        public long idordentrabajo { get; set; }
        public int cantidad { get; set; }

    }
}
