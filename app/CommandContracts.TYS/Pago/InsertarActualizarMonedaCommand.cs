

using CommandContracts.Common;
namespace CommandContracts.TYS.Pago
{
    public class InsertarActualizarMonedaCommand : Command
    {
        public int? idmoneda { get; set; }
        public string moneda { get; set; }
        public bool activo { get; set; }

    }
}
