
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Parameters
{
    public class ListarTransportistasSearchParameter : QueryParameter
    {
        public string CodigoItem { get; set; }
        public string DescripcionItem { get; set; }
        public string TipoItem { get; set; }
    }
}
