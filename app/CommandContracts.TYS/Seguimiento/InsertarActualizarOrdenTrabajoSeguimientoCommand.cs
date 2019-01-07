using CommandContracts.Common;
using System;

namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarOrdenTrabajoSeguimientoCommand : Command
    {
        public long? idordentrabajo { get; set; }
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