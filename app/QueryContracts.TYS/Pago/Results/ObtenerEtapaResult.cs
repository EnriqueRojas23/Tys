

using QueryContracts.Common;
namespace QueryContracts.TYS.Pago.Results
{
    public class ObtenerEtapaResult : QueryResult
    {
        public int idetapa { get; set; }
        public string etapa { get; set; }
        public bool requiereot { get; set; }
        public bool activo { get; set; }

    }
    
}
