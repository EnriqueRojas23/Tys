
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Core.SGCW.Results
{
    public class ObtenerServicioGateOutResult : QueryResult
    {
        public IEnumerable<ObtenerServicioGateOutDto> Hits { get; set; }
    }

    public class ObtenerServicioGateOutDto
    {
        public int Ident_ServicioUS { get; set; }
        public string NombreUS { get; set; }
        public string CodigoServicioUS { get; set; }

        public int Ident_ServicioNS { get; set; }
        public string NombreNS { get; set; }
        public string CodigoServicioNS { get; set; }


    
    }
}
