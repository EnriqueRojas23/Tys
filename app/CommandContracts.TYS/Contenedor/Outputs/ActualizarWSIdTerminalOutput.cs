

using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor.Outputs
{
    public class ActualizarWSIdTerminalOutput : CommandResult
    {
        public bool IdActualizado { get; set; }
        public string MensajeError { get; set; }
    }
}
