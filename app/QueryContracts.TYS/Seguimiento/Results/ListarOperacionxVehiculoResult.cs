using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarOperacionxVehiculoResult : QueryResult
    {
        public IEnumerable<ListarOperacionxVehiculoDto> Hits { get; set; }
    }

    public class ListarOperacionxVehiculoDto
    {
        public long idcarga { get; set; }
        public string numcarga { get; set; }
        public int idestado { get; set; }
        public string origen { get; set; }
        public int idruta { get; set; }
    }
}