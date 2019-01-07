
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarImpresionManifiestoParameters : QueryParameter
    {
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public string numgrt { get; set; }
        public string numhojaruta { get; set; }
        public string numcarga { get; set; }
        public string fecinicio { get; set; }
        public string fecfin { get; set; }
        
    }
}
