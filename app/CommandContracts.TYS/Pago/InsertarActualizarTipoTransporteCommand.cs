

using CommandContracts.Common;
namespace CommandContracts.TYS.Pago
{
    public class InsertarActualizarTipoTransporteCommand : Command
    {
        public int? idtipotransporte { get;set; }
        public string tipotransporte { get; set; }
        public bool activo { get; set; }

    }
}
