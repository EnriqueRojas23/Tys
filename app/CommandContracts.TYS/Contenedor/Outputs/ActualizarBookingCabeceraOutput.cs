
using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor.Outputs
{
    public class ActualizarBookingCabeceraOutput : CommandResult
    {
        public bool booking_actualizado { get; set; }

        public decimal? rb_int_identificador_terminal { get; set; }
        public int filas_afectadas { get; set; }
        
    }
}
