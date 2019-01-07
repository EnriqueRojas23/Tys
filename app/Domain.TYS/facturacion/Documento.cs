

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Facturacion
{
    public class Documento : Entity
    {
        [Key]
        public int idnumerodocumento { get; set; }
        public int idtipocomprobante { get; set; }
        public string serie { get; set; }
        public string primernumero { get; set; }
        public string ultimonumero { get; set; }
        public int? idusuarioautorizado { get; set; }
        public int idestacion { get; set; }

    }
}
