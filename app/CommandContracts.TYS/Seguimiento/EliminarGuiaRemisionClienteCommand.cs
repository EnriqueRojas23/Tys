using CommandContracts.Common;

namespace CommandContracts.TYS.Seguimiento
{
    public class EliminarGuiaRemisionClienteCommand : Command
    {
        public long idordentrabajo { get; set; }
    }
}