using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarEstadoResult : QueryResult
    {
        public IEnumerable<ListarEstadoDto> Hits { get; set; }
    }
    public class ListarEstadoDto
    {
        public int idestado { get; set; }
        public string  estado { get; set; }
     





    }
}


