
using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor.Outputs
{
    public class ActualizarBookingWSOutput : CommandResult
    {
        public bool TransaccionCorrecta { get; set; }
    }
}
