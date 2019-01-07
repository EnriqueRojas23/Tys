

using QueryContracts.Common;
namespace QueryContracts.TYS.Pago.Results
{
    public class ObtenerTipoComprobanteResult : QueryResult
    {
        public int idtipocomprobante { get; set; }
        public string codigo { get; set; }
        public string tipocomprobante { get; set; }
        public bool activo { get; set; }

    }
    
}
