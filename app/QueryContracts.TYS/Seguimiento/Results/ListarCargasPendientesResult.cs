using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarCargasPendientesResult : QueryResult
    {
        public IEnumerable<ListarCargasPendientesDto> Hits { get; set; }
    }
    public class ListarCargasPendientesDto
    {
        public long idcarga { get; set; }
     





    }
}


