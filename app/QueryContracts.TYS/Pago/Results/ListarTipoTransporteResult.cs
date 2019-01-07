using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarTipoTransporteResult : QueryResult
    {
        public IEnumerable<ListarTipoTransporteDto> Hits { get; set; }
    }
    public class ListarTipoTransporteDto
    {
        public int idtipotransporte { get; set; }
        public string tipotransporte { get; set; }
        public bool activo { get; set; }
    }
}


