

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Cliente : Entity
    {
        [Key]
        public int idcliente { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string nombrecorto { get; set; }
        public decimal lineacredito { get; set; }
        public decimal? lineaconsumida { get; set; }
        public int idmonedalinea { get; set; }
        public string rutalogo { get; set; }
        public bool activo { get; set; }
        public bool pagocontado { get; set;  }
    }
}
