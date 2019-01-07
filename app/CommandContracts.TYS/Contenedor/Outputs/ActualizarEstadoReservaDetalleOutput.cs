
using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor.Outputs
{
    public class ActualizarEstadoReservaDetalleOutput : CommandResult
    {
        public long[] lst_rbd_int_id { get; set; }
        public int filas_actualizadas { get; set; }
    }
}
