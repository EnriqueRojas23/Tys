using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDetalleCamionDirectoResult : QueryResult
    {
        public IEnumerable<ListarDetalleCamionDirectoDto> Hits { get; set; }
    }
    public class ListarDetalleCamionDirectoDto
    {
        public int idcamiondirecto { get; set; }
        public int idcamion { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }
        public int idcliente { get; set; }
        public int idplaca { get; set; }
        public int destinos { get; set; }
        public int otscreadas { get; set; }
    }
}


