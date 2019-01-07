using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarOrdenTrabajoResult : QueryResult
    {
        public IEnumerable<ListarOrdenTrabajoDto> Hits { get; set; }
    }
    public class ListarOrdenTrabajoDto
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string razonsocial { get; set; }
        public int idtipotransporte { get; set; }
        public int idconceptocobro { get; set; }
        public string destino { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string tipotransporte { get; set; }
        public string conceptocobro { get; set; }
        public long idpreliquidacion { get; set; }
        public bool facturado { get; set; }
        public DateTime fecharegistro { get; set; }

    }
}


