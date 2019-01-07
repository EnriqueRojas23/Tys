

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.TYS.Pago
{
    public class EtapaProveedor : Entity
    {
        [Key]
        public int idetapa { get; set; }
        public string etapa { get; set; }
        public bool requiereot { get; set; }
        public bool activo { get; set; }
    }
}
