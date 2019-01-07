
using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor.Outputs
{
    public class ActualizarPagoBookingOutput : CommandResult
    {
        public string booking { get; set; }
        public string detalle { get; set; }
    }
}
