

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerTarifaResult : QueryResult
    {
        public int idarifa { get; set; }
        public int idorigendepartamento { get; set; }
        public int iddepartamento { get; set; }
        public int idorigenprovincia { get; set; }
        public int idprovincia { get; set; }
      

    }
}
