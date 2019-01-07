

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerChoferResult : QueryResult
    {
        public int idchofer { get; set; }
        public string nombrechofer { get; set; }
        public string apellidochofer { get; set; }
        public string dni { get; set; }
        public string brevete { get; set; }
        public int edad { get; set; }
        public int idsexo { get; set; }
        public string telefono { get; set; }
        public string direccionchofer { get; set; }
        public bool activo { get; set; }

    }
}
