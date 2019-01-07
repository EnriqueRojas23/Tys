using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarOperacionCargaVehiculoResult : QueryResult
    {
        public IEnumerable<ListarOperacionCargaVehiculoDto> Hits { get; set; }
    }
    public class ListarOperacionCargaVehiculoDto
    {
        public long idcarga { get; set; }
        public string numcarga { get; set; }
        public string hojaruta { get; set; }
        public int idcliente { get; set; }
        public string vol { get; set; }
        public string peso { get; set; }
        public int idproveedor { get; set; }
        public string placa { get; set; }
        public string fechacreacion { get; set; }
        public string observacion { get; set; }
        public int idplanificador { get; set; }
        public int idagencia { get; set; }
        public int idtipooperacion { get; set; }
        public int idestacion { get; set; }
        public int idruta { get; set; }
        public string proveedor { get; set; }
        public string chofer { get; set; }
        public string nummanifiesto { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public int bulto { get; set; }
            

    }
}


