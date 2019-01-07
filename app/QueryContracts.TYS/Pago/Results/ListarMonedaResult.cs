using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarMonedaResult : QueryResult
    {
        public IEnumerable<ListarMonedaDto> Hits { get; set; }
    }
    public class ListarMonedaDto
    {
        public int idmoneda { get; set; }
        public string moneda { get; set; }
        public bool activo { get; set; }
    }
}


