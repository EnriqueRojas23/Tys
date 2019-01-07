

using QueryContracts.Common;
namespace QueryContracts.TYS.Seguridad.Parameters
{
    public class CambiarContrasenaParameter : QueryParameter
    {
        public int IdUsuario { get; set; }
        public string password { get; set; }

    }
}
