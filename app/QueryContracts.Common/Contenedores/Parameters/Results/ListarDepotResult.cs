
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Results
{
    public class ListarDepotResult : QueryResult
    {
        public IEnumerable<ListarDepotDto> Hits { get; set; }
    }

    public class ListarDepotDto
    {
        public string IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
        public string DireccionAlmacen { get; set; }
        public string TipoAlmacen { get; set; }
        public string IdOficina { get; set; }
        public string DescripcionOficina { get; set; }

    }
}
