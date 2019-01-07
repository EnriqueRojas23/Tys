

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarZonaCommand : Command
    {
        public int? idzona { get; set; }
        public string zona { get; set; }

    }
}
