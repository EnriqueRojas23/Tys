using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class CargarEtapasResult : QueryResult
    {
        public IEnumerable<CargarEtapasDto> Hits { get; set; }
    }
    public class CargarEtapasDto
    {
        public int idetapa { get; set; }
        public string etapa { get; set; }
    }
}


