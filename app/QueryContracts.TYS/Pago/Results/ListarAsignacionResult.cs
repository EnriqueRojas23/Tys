using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarAsignacionResult : QueryResult
    {
        public IEnumerable<ListarAsignacionDto> Hits { get; set; }
    }
    public class ListarAsignacionDto
    {
        public int idasignacion { get; set; }
        public int idproveedor { get; set; }
        public int idetapa { get; set; }
        public int idtipotransporte { get; set; }
        public int idmoneda { get; set; }
        public int idtipocomprobante { get; set; }
        public string razonsocial { get; set; }
        public string etapa { get; set; }
        public string moneda { get; set; }
        public string tipocomprobante { get; set; }
        public string tipotransporte { get; set; }
    }
}


