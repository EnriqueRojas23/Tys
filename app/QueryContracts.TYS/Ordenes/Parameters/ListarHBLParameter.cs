
using QueryContracts.Common;
namespace QueryContracts.TYS.Ordenes.Parameters
{
    public class ListarHBLParameter : QueryParameter 
    {
        public string nave { get; set; }
        public string viaje { get; set; }
        public string consolidador { get; set; }
        public string contenedor { get; set; }
        public string servicio { get; set; }
        public string despachador { get; set; }
        public string numvia { get; set; }

    }
}
