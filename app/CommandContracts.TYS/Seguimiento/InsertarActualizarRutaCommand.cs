

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarRutaCommand : Command
    {
        public int? idruta { get; set; }
        public string nombre { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }
        public string ruta { get; set; }
        public string km { get; set; }
           

    }
}
