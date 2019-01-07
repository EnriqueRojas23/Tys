using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarClientesResult : QueryResult
    {
        public IEnumerable<ListarClientesDto> Hits { get; set; }
    }
    public class ListarClientesDto
    {
        public int idcliente { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string nombrecorto { get; set; }
        public string direccion { get; set; }
        public decimal lineacredito { get; set; }
        public string ubigeo { get; set; }
        public bool activo { get; set; }
        public string moneda { get; set; }


    }
}


