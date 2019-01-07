
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Results
{
    public class ListarTamanyoContenedoresResult : QueryResult
    {
        public IEnumerable<ListarTamanyoContenedoresDto> Hits { get; set; }
    }

    public class ListarTamanyoContenedoresDto
    {
        public int Tamanyo { get; set; }
        public int Cantidad { get; set; }
    }
}
