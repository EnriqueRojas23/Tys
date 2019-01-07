

using QueryContracts.Common;
namespace QueryContracts.TYS.Pago.Results
{
    public class ObtenerMonedaResult : QueryResult
    {
        public int idmoneda { get; set; }
        public string moneda { get; set; }
        public bool activo { get; set; }

    }
    
}
