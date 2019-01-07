using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarOrdenTrabajoComprobanteResult : QueryResult
    {
        public IEnumerable<ListarOrdenTrabajoComprobanteDto> Hits { get; set; }
    }
    public class ListarOrdenTrabajoComprobanteDto
    {
        public int idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string bulto { get; set; }
        public string peso { get; set; }
        public string Precio { get; set; }
        public string ValorVenta { get; set; }
        public string SubTotal { get; set; }
        public string Total { get; set; }
        public string hojaruta { get; set; }
        public string manifiesto { get; set; }
        public string nroguia { get; set; }


    }
}


