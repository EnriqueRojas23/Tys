

using CommandContracts.Common;
namespace CommandContracts.TYS.Pago
{
    public class InsertarActualizarAsignacionCommand : Command
    {
        public int? idasignacion { get; set; }
        public int idproveedor { get; set; }
        public int idetapa { get; set; }
        public int idtipotransporte { get; set; }
        public int idmoneda { get; set; }
        public int idtipocomprobante { get; set; }

    }
}
