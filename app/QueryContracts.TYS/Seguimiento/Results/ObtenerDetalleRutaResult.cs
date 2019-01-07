

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerDetalleRutaResult : QueryResult
    {
        public int iddetalleruta { get; set; }
        public int idruta { get; set; }
        public int idorden { get; set; }
        public int iddepartamento { get; set; }
        public int idprovincia { get; set; }
        public int iddistrito { get; set; }
    }
}
