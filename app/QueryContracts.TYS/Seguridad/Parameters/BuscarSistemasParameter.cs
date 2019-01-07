using QueryContracts.Common;
namespace QueryContracts.TYS.Seguridad.Parameters
{
    public class BuscarSistemasParameter : QueryParameter
    {
        public string nombre { get; set; }
        public string alias { get; set; }
    }
}
