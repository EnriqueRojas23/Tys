

using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class VehiculoInspeccion : Entity
    {
        [Key]
        public int idvehiculoinspeccion { get; set; }
        public int? idinspeccion { get; set; }
        public int idvehiculo { get; set; }
        public string observacion { get; set; }
        public bool checkeado { get; set; }
       

    }
}
