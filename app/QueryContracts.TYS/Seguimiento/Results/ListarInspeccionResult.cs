using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarInspeccionResult : QueryResult
    {
        public IEnumerable<ListarInspeccionDto> Hits { get; set; }
    }
    public class ListarInspeccionDto
    {
        public int idinspeccion { get; set; }
        public string concepto { get; set; }
        public string detalle { get; set; }
        public bool checkeado { get; set; }
        public int idestado { get; set; }

    }
}


