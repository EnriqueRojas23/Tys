using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarUbigeoResult : QueryResult
    {
        public IEnumerable<ListarUbigeoDto> Hits { get; set; }
    }
    public class ListarUbigeoDto
    {
        public int idubigeo { get; set; }
        public string ubigeo { get; set; }


    }
}


