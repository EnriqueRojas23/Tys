

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Seguimiento
{
    public class Vehiculo : Entity
    {
        [Key]
        public int idvehiculo { get; set; }
        public int? idproveedor { get; set; }
        public int idtipo { get; set; }
        public int idmarca { get; set; }
        public int idmodelo { get; set; }
        public int idanio { get; set; }
        public int idcolor { get; set; }
        public int idcombustible { get; set; }
        public string regmtc { get; set; }
        public string placa { get; set; }
        public string confveh { get; set; }
        public decimal pesobruto { get; set; }
        public decimal cargautil { get; set; }
        public string seriemotor { get; set; }
        public string kilometraje { get; set; }
        public int idestado { get; set; }
        public int? idchofer { get; set; }
        public int? idorigen { get; set; }
        public bool activo { get; set; }
        public string inspecciones { get; set; }
        public DateTime? fechainspeccion { get; set; }
        public DateTime? fechaasignado { get; set; }
        public int? usuarioinspeccion { get; set; }
        public int? usuarioasignado { get; set; }

    }
}
