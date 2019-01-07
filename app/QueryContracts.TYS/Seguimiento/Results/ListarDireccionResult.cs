using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDireccionResult : QueryResult
    {
        public IEnumerable<ListarDireccionDto> Hits { get; set; }
    }
    public class ListarDireccionDto
    {
        public int iddireccion { get; set; }
        public string direccion { get; set; }
        public int iddistrito { get; set; }
        public string ubigeo { get; set; }
        public int iddepartamento { get; set; }
        public int idprovincia { get; set; }

    }
}


