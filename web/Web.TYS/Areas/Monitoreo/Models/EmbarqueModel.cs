using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.TYS.Areas.Monitoreo.Models
{
    public class EmbarqueModel 
    {
        public long idordentrabajo { get; set; }
        public long? idembarque { get; set; }
        public string numeroembarque { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un Transporte")]
        [DataType(DataType.Text)]
        public string conocimientoembarque { get; set; }

        [Required(ErrorMessage="Debe seleccionar un Transporte")]
        public int idtransporte { get; set; }

        public int idpuerto { get; set; }
        public string transporte { get; set; }
        public string puerto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public DateTime fechainiciocarga { get; set; }
        public string horainiciocarga { get; set; }
        public DateTime? fechafincarga { get; set; }
        public string horafincarga { get; set; }
        public DateTime? fechazarpe { get; set; }
        public string horazarpe { get; set; }
        public DateTime? fechaatraque { get; set; }
        public string horaatraque { get; set; }
        public DateTime? fechallegada { get; set; }
        public string horallegada { get; set; }
        public bool embarquecompleto { get; set; }
        public string idsorden { get; set; }
        public DateTime? fechacontrolsunat { get; set; }
        public string horacontrolsunat { get; set; }
        public DateTime? fechadescarga { get; set; }
        public string horadescarga { get; set; }
        public long? idmanifiesto { get; set; }
        public int idusuariocontrolsunat { get; set; }
        public int idusuariodescarga { get; set; }


        public string origen { get; set; }
        public string distrito { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public DateTime fechadespacho { get; set; }
        public string tipooperacion { get; set; }
        public decimal Peso { get; set; }
        public decimal Volumen { get; set; }
        public int cantidad { get; set; }
        public int bulto { get; set; }
        public bool reintegrotributario { get; set; }

    }
    
    public class EventoModel
    {

        public long ot { get; set; }
        public string numcp { get; set; }
        public DateTime fechaevento { get; set; }
        public string tipoevento { get; set; }
        public int idmaestroevento { get; set; }
        public string evento { get; set; }
        public string observacion { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public int idusuarioregistro { get; set; }
        public DateTime fecharegistro { get; set; }
        public bool autogenerada { get; set; }
        public int idmaestroincidencia { get; set; }
        public int idmaestroetapa { get; set; }
        public string idsorden { get; set; }
        public string usuario { get; set; }
        public string estacionorigen { get; set; }

    }
   
}