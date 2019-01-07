using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarEtapaResult : QueryResult
    {
        public IEnumerable<ListarEtapaDto> Hits { get; set; }
    }
    public class ListarEtapaDto
    {
        public int idetapa { get; set; }
        public string etapa { get; set; }
        public bool requiereot { get; set; }
        public bool activo { get; set; }
    }
}


