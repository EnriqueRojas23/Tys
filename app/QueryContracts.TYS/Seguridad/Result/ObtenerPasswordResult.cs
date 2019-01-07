
using QueryContracts.Common;
namespace QueryContracts.TYS.Seguridad.Result
{
    public class ObtenerPasswordResult : EliminarPaginaResult
    {
        public string usr_str_password { get; set; }
        public string usr_str_nombre { get; set; }
        public string usr_str_red { get; set; }


    }
}
