using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarRutaResult : QueryResult
    {
        public IEnumerable<ListarRutaDto> Hits { get; set; }
    }
    public class ListarRutaDto
    {
        public int idruta { get; set; }
        public string nombre { get; set; }
        public string idorigen { get; set; }
        public string iddestino { get; set; }
        public string ruta { get; set; }
        public string destino { get; set; }
        public string km { get; set; }
        public string origen { get; set; }


    }
}


