

using CommandContracts.Common;
namespace CommandContracts.TYS.Pago
{
    public class InsertarActualizarTipoComprobanteCommand : Command
    {
        public int? idtipocomprobante { get; set; }
        public string codigo { get; set; }
        public string tipocomprobante { get; set; }
        public bool activo { get; set; }
    }
}
