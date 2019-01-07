
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Results
{
    public class ListarClavesAplicacionResult : QueryResult
    {
        public IEnumerable<ListarClavesAplicacionDto> Hits { get; set; }
    }

    public class ListarClavesAplicacionDto
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
