
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Facturacion.Parameters
{
    public class ListarDocumentosParameters : QueryParameter
    {
        public int? idtipocomprobante { get; set; }
        public int? idusuario { get; set; }
        public int? idestacionorigen { get; set; }

    }
}
