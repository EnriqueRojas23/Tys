

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarLineaConsumidaClienteCommand : Command
    {
        public int? idcliente { get; set; }
        public decimal lineaconsumida { get; set; }       

    }
}
