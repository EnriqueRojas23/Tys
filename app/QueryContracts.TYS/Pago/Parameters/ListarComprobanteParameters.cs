
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Pago.Parameters
{
    public class ListarComprobanteParameters : QueryParameter
    {
        public string serie { get; set; }
        public string fechaini { get; set; }
        public string fechafin { get; set; }
        
    }
}
