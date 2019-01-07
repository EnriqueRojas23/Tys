using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarPlacaResult : QueryResult
    {
        public IEnumerable<ListarPlacaDto> Hits { get; set; }
    }
    public class ListarPlacaDto
    {
        public int idvehiculo { get; set; }
        public string placa { get; set; }
        public int idproveedor { get; set; }
        public int idestado { get; set; }
    }
}


