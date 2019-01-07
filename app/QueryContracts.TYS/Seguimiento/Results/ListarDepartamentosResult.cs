using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDepartamentosResult : QueryResult
    {
        public IEnumerable<ListarDepartamentosDto> Hits { get; set; }
    }
    public class ListarDepartamentosDto
    {
        public int iddepartamento { get; set; }
        public string departamento { get; set; }

    }
}


