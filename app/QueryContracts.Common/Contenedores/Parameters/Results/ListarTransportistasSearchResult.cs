
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Results
{
    public class ListarTransportistasSearchResult : QueryResult
    {
        public IEnumerable<ListarTransportistasSearchDto> Hits { get; set; }
    }

    public class ListarTransportistasSearchDto
    {
        public string Id { get; set; }
        public string EmpresaTransporte { get; set; }
        public string Conductor { get; set; }
        public string Camion { get; set; }
        public string NIFConductor { get; set; }
        public string CodItemDescripcion { get { return string.Format("{0}|{1}", Id, Conductor); } }

    }
}
