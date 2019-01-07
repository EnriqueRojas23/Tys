using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarOTLigeraResult : QueryResult
    {
        public IEnumerable<ListarOTLigeraDto> Hits { get; set; }
    }
    public class ListarOTLigeraDto
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public DateTime fecharegistro { get; set; }
        public string razonsocial { get; set; }
        public string destino { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string tipotransporte { get; set; }
        public string conceptocobro { get; set; }
        public bool reintegrotributario { get; set; }
        public bool nofacturable { get; set; }
        public bool? devolucion { get; set; }
        public bool registrorapido { get;set; }
        public bool camioncompleto { get; set; }
        public long idordentrabajoasociada { get; set; }
        public string placa { get; set;  }
        public string otasociada { get; set; }

    }
}


