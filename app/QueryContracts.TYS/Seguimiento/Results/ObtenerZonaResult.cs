

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerZonaResult : QueryResult
    {
        public int? idzona { get; set; }
        public string zona { get; set; }
    }
}
