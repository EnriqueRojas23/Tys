

using QueryContracts.Common;
namespace QueryContracts.TYS.Pago.Results
{
    public class ObtenerProveedorResult : QueryResult
    {
        public int idproveedor { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string placaasociada { get; set; }
        public bool activo { get; set; }
        public string direccion { get; set; }

    }
    
}
