
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Parameters
{
    public class InsertarDocumentosSolportParameter : QueryParameter
    {
        public int IdReserva { get; set; }
        public string Usuario { get; set; }
        public string Fichero { get; set; }
        public string DesFichero { get; set;}
    }
}
