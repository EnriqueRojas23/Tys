using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarProveedorResult : QueryResult
    {
        public IEnumerable<ListarProveedorDto> Hits { get; set; }
    }
    public class ListarProveedorDto
    {
        public int idproveedor { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string placaasociada { get; set; }
        public bool activo { get; set; }
    }   
}


