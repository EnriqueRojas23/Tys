
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarImpresionCargaParameters : QueryParameter
    {
        public string numhojaruta { get; set; }
        public string numcarga { get; set; }
        public DateTime fecini { get; set; }
        public DateTime fecfin { get; set; }
    }
}
