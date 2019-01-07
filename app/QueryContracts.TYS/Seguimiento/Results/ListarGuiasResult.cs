using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarGuiasResult : QueryResult
    {
        public IEnumerable<ListarGuiasDto> Hits { get; set; }
    }
    public class ListarGuiasDto
    {
        public long idguiaremisioncliente { get; set; }
        public long idordentrabajo { get; set; }
        public string nroguia { get; set; }
        public string idcobrarpor { get; set; }
        public string bulto { get; set; }
        public string peso { get; set; }
        public string volumen { get; set; }
        public string pesovol { get; set; }
        public string documento { get; set; }
 

    }
}


