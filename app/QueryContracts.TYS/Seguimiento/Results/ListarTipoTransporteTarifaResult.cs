using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarTipoTransporteTarifaResult : QueryResult
    {
        public IEnumerable<ListarTipoTransporteTarifaDto> Hits { get; set; }
    }
    public class ListarTipoTransporteTarifaDto
    {
        public int idtipotransporte { get; set; }
        public string tipotransporte { get; set; }
        public int iddistrito { get; set; }
        public int idprovincia { get; set; }
        public int iddepartamento { get; set; }
        public string formula { get; set; }
    }
}


