

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerFormulaResult : QueryResult
    {
        public int idformula { get; set; }
        public string nombre { get; set; }
        public string formula { get; set; }
        public bool activo { get; set; }
    }
}
