

using CommandContracts.Common;
namespace CommandContracts.TYS.Pago
{
    public class InsertarActualizarEtapaCommand : Command
    {
        public int? idetapa { get; set; }
        public string etapa { get; set; }
        public bool requiereot { get; set; }
        public bool activo { get; set; }

    }
}
