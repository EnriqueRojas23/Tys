using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class CargarTipoTransporteResult : QueryResult
    {
        public IEnumerable<CargarTipoTransporteDto> Hits { get; set; }
    }
    public class CargarTipoTransporteDto
    {
        public int idtipotransporte { get; set; }
        public string tipotransporte { get; set; }
    }
}


