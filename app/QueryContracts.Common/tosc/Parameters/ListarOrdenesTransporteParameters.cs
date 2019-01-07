

using QueryContracts.Common;
namespace QueryContracts.Core.CRM.Parameters
{
    public class ListarOrdenesTransporteParameters : QueryParameter
    {
        public string numcp { get; set; }
        public string guia { get; set; }
        public string manifiesto { get; set; }
    }
}
