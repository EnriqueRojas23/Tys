using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarPlanificarOrdenResult : QueryResult
    {
        public IEnumerable<ListarPlanificarOrdenDto> Hits { get; set; }
    }
    public class ListarPlanificarOrdenDto
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string razonsocial { get; set; }
        public string idtipotransporte { get; set; }
        public string idconceptocobro { get; set; }
        public string destino { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string tipotransporte { get; set; }
        public string conceptocobro { get; set; }
        public string peso { get; set; }
        public string volumen { get; set; }
        public string bulto { get; set; }
        public decimal? subtotal { get; set; }


    }
}


