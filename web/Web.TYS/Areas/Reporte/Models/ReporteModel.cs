using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.TYS.Areas.Models
{
    public class ReporteModel
    {

        public int iddistrito { get; set; }
        public string fecinicio { get; set; }
        public string fecfin { get; set; }
        public int idusuario { get; set; }
        public int idestacionorigen { get; set; }
        public int idestaciondestino { get; set; }
        public int idcliente { get; set; }
        public string grr { get; set; }
        public string numcp { get; set; }
        public string nummanifiesto { get; set; }
        public int idunidadmedida { get; set; }
        public int idestado { get; set; }
        public int idtipotransporte { get; set; }
        public string codtienda { get; set; }
        public string embarcacion { get; set; }
        public string placa { get; set; }
        public int anio { get; set; }

    }
    public class TipoUnidadMedidaModel
    {
        public int IdTipoUnidadMedida { get; set; }
        public string TipoUnidadMedida { get; set; }

    }
}