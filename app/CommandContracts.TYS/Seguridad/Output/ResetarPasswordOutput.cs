
using CommandContracts.Common;
namespace CommandContracts.TYS.Seguridad.Output
{
    public class ResetarPasswordOutput : CommandResult
    {
        public string PasswordClaro { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
    }
}
