using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarEstacionOrigenResult : QueryResult
    {
        public IEnumerable<ListarEstacionOrigenDto> Hits { get; set; }
    }
    public class ListarEstacionOrigenDto
    {
        public int idestacion { get; set; }
        public string estacionorigen { get; set; }
        public int iddistrito { get; set; }
     

    }
}


