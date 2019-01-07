using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarOTHijasCCResult : QueryResult
    {
        public IEnumerable<ListarOTHijasCCDto> Hits { get; set; }
    }
    public class ListarOTHijasCCDto
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
        public bool devolucion { get; set; }
        public bool registrorapido { get;set; }
        public string numcc { get; set; }
        public long idordentrabajoasociada { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public int bulto { get; set; }




    }
}


