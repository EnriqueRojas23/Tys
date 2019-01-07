
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Results
{
    public class ListarOficinaResult : QueryResult
    {
        public IEnumerable<ListarOficinaDto> Hits { get; set; }
    }

    public class ListarOficinaDto
    {
        public string IdOficina { get; set; }
        public string DescripcionOficina { get; set; }
        public string IdEmpresa { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string Provincia { get; set; }


    }
}
