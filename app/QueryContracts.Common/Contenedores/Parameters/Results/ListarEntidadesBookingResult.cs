
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Terminal.Contenedores.Results
{
    public class ListarEntidadesBookingResult : QueryResult
    {
        public IEnumerable<ListarEntidadesBookingDto> Hits { get; set; }
    }

    public class ListarEntidadesBookingDto
    {
        public string id { get; set; }
        public string cif { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
    }
}
