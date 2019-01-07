

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarRutaEtapaCommand : Command
    {
        public int? idrutasetapas { get; set; }
        public int iddetalleruta { get; set; }
        public int idetapa { get; set; }
            

    }
}
