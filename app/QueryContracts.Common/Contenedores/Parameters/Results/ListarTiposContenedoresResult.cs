

using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Results
{
    public class ListarTiposContenedoresResult : QueryResult
    {
        public IEnumerable<ListarTiposContenedoresDto> Hits { get; set; }
    }

    public class ListarTiposContenedoresDto
    {
        public string Tipo { get; set; }
        public int Cantidad { get; set; }
    }
}
