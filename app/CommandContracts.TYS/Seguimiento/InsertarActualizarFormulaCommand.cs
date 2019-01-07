

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarFormulaCommand : Command
    {
        public int? idformula { get; set; }
        public string nombre { get; set; }
        public string formula { get; set; }
        public bool activo { get; set; }

    }
}
