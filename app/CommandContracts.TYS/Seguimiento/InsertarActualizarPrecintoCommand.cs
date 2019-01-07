

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarPrecintoCommand : Command
    {
        public int? idprecinto { get; set; }
        public string precinto { get; set; }
     
    }
}
