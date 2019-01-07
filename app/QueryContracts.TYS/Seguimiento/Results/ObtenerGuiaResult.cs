

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerGuiaResult : QueryResult
    {
        public long idguiaremisioncliente { get; set; }
        public string numcp { get; set; }

    }
}
