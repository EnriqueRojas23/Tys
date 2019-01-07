

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarDetalleTarifaCommand : Command
    {
        public int? iddetalletarifa { get; set; }
        public int idtarifa { get; set; }
        public int idconceptocobro { get; set; }
           

    }
}
