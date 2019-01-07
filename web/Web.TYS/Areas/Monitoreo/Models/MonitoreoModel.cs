using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.TYS.Areas.Monitoreo.Models
{




    public class MonitoreoModel 
    {
        public int? idcliente { get; set; }
        public int? iddestino { get; set; }
        public string hojaruta { get; set; }
        public string manifiesto { get; set; }
        public string numcp { get; set; }
        public string grr { get; set; }
        public int idestado { get; set; }
        public string documento { get; set; }
        public string tienda { get; set; }
        public string nummanifiesto { get; set; }
        public string numhojaruta { get; set; }
        public DateTime FechaDespacho { get; set; }
        public long? idmanifiesto { get; set; }
        public DateTime? fechainicio { get; set; }
        public DateTime? fechafin { get; set; }
        public long idmonitoreo { get; set; }
    }
    
   
    public class IncidenciaGuiasRechazadas
    {
        public long idguia { get; set; }
        public string guia { get; set; }
        public int cantidad { get; set; }
        public long idordentrabajo { get; set; }
    }
    public class TimeLine
    {
        public IEnumerable<IncidenciaModel> Incidencias { get; set; }
    }

}