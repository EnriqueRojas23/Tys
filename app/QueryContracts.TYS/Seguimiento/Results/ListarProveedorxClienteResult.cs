using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarProveedorxClienteResult : QueryResult
    {
        public IEnumerable<ListarProveedorxClienteDto> Hits { get; set; }
    }
    public class ListarProveedorxClienteDto
    {
        public int idproveedorcliente { get; set; }
        public int idproveedor { get; set; }
        public string razonsocial { get; set; }
        public int idcliente { get; set; }


    }
}


