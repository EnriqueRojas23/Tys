using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarImpresionManifiestoResult : QueryResult
    {
        public IEnumerable<ListarImpresionManifiestoDto> Hits { get; set; }
    }
    public class ListarImpresionManifiestoDto
    {
        public long idordentrabajo { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public string GRT { get; set; }
        public string tipooperacion { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public int bulto { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public long iddespacho { get; set; }
        public long idmanifiesto { get; set; }
        public long idcarga { get; set; }
        public int idvehiculo { get; set; }

    }
}


