

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarDetalleValijaCommand : Command
    {
        public long? iddespachovalija { get; set; }
        public long idordentransporte { get; set; }
        public long iddespacho { get; set; }

    }
}
