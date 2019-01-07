using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarChoferResult : QueryResult
    {
        public IEnumerable<ListarChoferDto> Hits { get; set; }
    }
    public class ListarChoferDto
    {
        public int idchofer { get; set; }
        public int idvehiculo { get; set; }
        public string nombrechofer { get; set; }
        public string apellidochofer { get; set; }
        public string dni { get; set; }
        public string brevete { get; set; }
        public int edad { get; set; }
        public int idsexo { get; set; }
        public string telefono { get; set; }
        public string direccionchofer { get; set; }
        public bool activo { get; set; }

        
    }
}


