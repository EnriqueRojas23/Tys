

using QueryContracts.Common;
namespace QueryContracts.TYS.Pago.Results
{
    public class ObtenerTipoTransporteResult : QueryResult
    {
        public int idtipotransporte { get; set; }
        public string tipotransporte { get; set; }
        public bool activo { get; set; }

    }
    
}
