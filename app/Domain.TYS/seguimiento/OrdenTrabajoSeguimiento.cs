using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.TYS.Seguimiento
{
    public class OrdenTrabajoSeguimiento : Entity
    {
        [Key]
        public long idseguimiento { get; set; }
        public long idordentrabajo { get; set; }
        public DateTime? fechallegadapuerto { get; set; }
        public DateTime? fechaembarque { get; set; }
        public DateTime? fechaconocimientoembarque { get; set; }
        public string numeroconocimiento { get; set; }
        public DateTime? fechasalidadepuerto { get; set; }
        public DateTime? fechaarribo { get; set; }
        public DateTime? fechadesembarque { get; set; }
        public DateTime? fechallegadaalmacen { get; set; }
    
    }
}