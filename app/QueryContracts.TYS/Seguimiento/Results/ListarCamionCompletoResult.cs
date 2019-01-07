using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarCamionCompletoResult : QueryResult
    {
        public IEnumerable<ListarCamionCompletoDto> Hits { get; set; }
    }
    public class ListarCamionCompletoDto
    {
        public long idcamioncompleto { get; set; }
        public string codigocamioncompleto { get; set; }
        public string tipotransporte { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string razonsocial { get; set; }
        public string  formula { get; set; }
        public string ruta { get; set; }
        public string placa { get; set; }
        public string fecharegistro { get; set; }



    }
}


