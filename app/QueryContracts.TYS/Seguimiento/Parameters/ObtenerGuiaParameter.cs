
using QueryContracts.Common;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ObtenerGuiaParameter : QueryParameter
    {
        public string numguia { get; set; }
        public long? idguiaremisioncliente { get; set;    }
    }
}
