

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Facturacion
{
    public class InsertarActualizarDocumentoCommand : Command
    {
        public int? idnumerodocumento { get; set; }
        public int idtipocomprobante { get; set; }
        public string serie { get; set; }
        public string primernumero { get; set; }
        public string ultimonumero { get; set; }
        public int? idusuarioautorizado { get; set; }
        public int idestacion { get; set; }
        public int _tipooperacion { get; set; }
    }
}
