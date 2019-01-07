using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDetalleOperacionxRutaResult : QueryResult
    {
        public IEnumerable<ListarDetalleOperacionxRutaDto> Hits { get; set; }
    }
    public class ListarDetalleOperacionxRutaDto
    {
        public int iddetalleruta { get; set; }
        public int idorden { get; set; }
        public string km { get; set; }

    }
}


