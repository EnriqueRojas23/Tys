using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarOperacionCargaResult : QueryResult
    {
        public IEnumerable<ListarOperacionCargaDto> Hits { get; set; }
    }
    public class ListarOperacionCargaDto
    {
        public long idcarga { get; set; }
        public string numcarga { get; set; }
        public string hojaruta { get; set; }
        public int idcliente { get; set; }
        public string vol { get; set; }
        public string peso { get; set; }
        public int idproveedor { get; set; }
        public string placa { get; set; }
        public DateTime fecharegistro { get; set; }
        public string observacion { get; set; }
        public int idplanificador { get; set; }
        public int idagencia { get; set; }
        public int idtipooperacion { get; set; }
        public int idestacion { get; set; }
        public int idruta { get; set; }
        public string proveedor { get; set; }
        public string chofer { get; set; }
        public string nummanifiesto { get; set; }
        public string ruta { get; set; }
            

    }
}


