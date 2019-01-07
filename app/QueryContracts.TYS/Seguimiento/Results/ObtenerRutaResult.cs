

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerRutaResult : QueryResult
    {
        public int idruta { get; set; }
        public string nombre { get; set; }
        public string ruta { get; set; }
        public int iddestino { get; set; }
        public int idorigen { get; set; }
        public string km { get; set; }
    }
}
