using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.TYS.DataAccess.Monitoreo;

namespace Web.TYS.Areas.Monitoreo.Models
{

    public class MaestroEtapaModel
    {
        public int idmaestroetapa { get; set; }
        public string descripcion { get; set; }
        public string tipoetapa { get; set; }
        public bool etapaunica { get; set; }
        public bool activo { get; set; }
        public bool autogenerada { get; set; }
        public string grr { get; set; }

    }
    public class EtapaModel
    {
        public long? idetapa { get; set; }
        public int idmaestroetapa { get; set; }
        public long idordentrabajo { get; set; }
        public int? idmanifiesto { get; set; }
        public string descripcion { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public DateTime? fechaetapa { get; set; }
        public string horaetapa { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public bool visible { get; set; }
        public string idsorden { get; set; }
        public string guia { get; set; }
        public int cantidad { get; set; }
        public string numcp_aux { get; set; }
        public string grr { get; set; }
        public string tipoetapa { get; set; }
        public bool regresar { get; set; }
    }
    public class MaestroIncidenteModel
    {
        public int idmaestroincidencia { get; set; }
        public string codincidencia { get; set; }
        public string descripcion { get; set; }
        public DateTime fechafin { get; set; }
        public string tipo { get; set; }
        public bool esfecha { get; set; }
        public bool seactualiza { get;set;}
    }


    public class IncidenciaModel
    {
        public long? idincidencia { get; set; }
        public string codincidente { get; set; }
        public DateTime fecharegistro { get; set; }
        public DateTime fechaincidencia { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public int idmaestroincidencia { get; set; }
        public string idsorden { get; set; }
        public int idusuarioregistro { get; set; }
        public bool activo { get; set; }
        public long? idordentrabajo { get; set; }


        public string numhojaruta { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public long? idmanifiesto { get; set; }
        public string guia { get; set; }
        public string nroguia { get; set; }
        public int cantidad { get; set; }
        public IncidenciaGuiasRechazadas GuiasRechazadas { get; set; }
        public string documento { get; set; }
        public string recurso { get; set; }
        public string horaincidencia { get; set; }
        public string _fechaincidencia { get; set; }
        public string nombreusuario { get; set; }
        public bool visible { get; set; }
        public bool carga { get; set; }
        public char tipo { get; set; }

       
        public long Insertar(IncidenciaModel model)
        {
            return IncidenciaData.InsertarActualizarIncidencia(model);
        }




    }
}