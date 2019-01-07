

using CommandContracts.Common;
namespace CommandContracts.TYS.Facturacion.Output
{
    public class InsertarActualizarPreliquidacionOutput : CommandResult
    {
        public long? idpreliquidacion { get; set; }
        public long idordentrabajo { get; set; }
    }
}
