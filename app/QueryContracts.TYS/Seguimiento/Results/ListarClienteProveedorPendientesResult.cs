using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarClienteProveedorPendientesResult : QueryResult
    {
        public IEnumerable<ListarClienteProveedorPendientesDto> Hits { get; set; }
    }
    public class ListarClienteProveedorPendientesDto
    {
        public int idcliente { get; set; }
        public string razonsocial { get; set; }

    }
}


