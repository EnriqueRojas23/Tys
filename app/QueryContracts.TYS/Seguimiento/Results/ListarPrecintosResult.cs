using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarPrecintosResult : QueryResult
    {
        public IEnumerable<ListarPrecintosDto> Hits { get; set; }
    }
    public class ListarPrecintosDto
    {
        public int idprecinto { get; set; }
        public string precinto { get; set; }

    }
}


