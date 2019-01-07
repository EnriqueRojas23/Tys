using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class CargarMonedasResult : QueryResult
    {
        public IEnumerable<CargarMonedasDto> Hits { get; set; }
    }
    public class CargarMonedasDto
    {
        public int idmoneda { get; set; }
        public string moneda { get; set; }
    }
}


