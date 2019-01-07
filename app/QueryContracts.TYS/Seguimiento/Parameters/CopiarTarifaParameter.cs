
using QueryContracts.Common;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class  CopiarTarifaParameter : QueryParameter
    {
        public int idtarifa { get; set; }
        public int idcliente { get; set; }
    }
}
