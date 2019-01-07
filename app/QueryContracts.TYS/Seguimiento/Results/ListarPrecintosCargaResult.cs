using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarPrecintosCargaResult : QueryResult
    {
        public IEnumerable<ListarPrecintosCargaDto> Hits { get; set; }
    }
    public class ListarPrecintosCargaDto
    {
        public int idprecinto { get; set; }
        public string precinto { get; set; }

    }
}


