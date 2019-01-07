using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarReporteGerencialResult : QueryResult
    {
        public IEnumerable<ListarReporteGerencialDto> Hits { get; set; }
    }
    public class ListarReporteGerencialDto
    {
        public string proveedor { get; set; }
        public string etapa { get; set; }
        public string destino { get; set; }
        public string costo { get; set; }
        public string kgs { get; set; }
        public string cu { get; set; }
        public string produccion { get; set; }
        public string rentabilidad { get; set; }
        public string porcentaje { get; set; }

    }
}


