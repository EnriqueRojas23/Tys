using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.TYS.Areas.Monitoreo.Models;
using Web.TYS.DataAccess;
using Web.TYS.DataAccess.Monitoreo;
using Web.TYS.DataAccess.Seguimiento;

namespace Web.TYS.Areas.Seguimiento.Models.Seguimiento
{
    public class OrdenTrabajoModelSimple
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
    }

    public class OrdenTrabajoSeguimiento 
    {
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

    public class OrdenTrabajoModel 
    {
        public long? idordentrabajo { get; set; }
        public long? idmanifiesto { get; set; }
        public string numcp { get; set; }
        public int idtipotransporte { get; set; }
        public string tipotransporte { get; set; }
        public int idestacion { get; set; }
        public int idconceptocobro { get; set; }
        public string conceptoscobro { get; set; }
        public string conceptocobro { get; set; }
        public int? idestacionorigen { get; set; }
        public int? idorigen { get; set; }
        public int? iddestino { get; set; }
        public int idcargadaen { get; set; }
        public int identregara { get; set; }
        public int idtipomercaderia { get; set; }
        public int idcliente { get; set; }
        public string razonsocial { get; set; }
        public int idclienteconceptocobro { get; set; }
        public int idclientetipounidad { get; set; }
        public int idcobrarpor { get; set; }
        public int idremitente { get; set; }
        public int idremitentedireccion { get; set; }
        public bool cbox1 { get; set; }
        public bool cbox2 { get; set; }

        public int iddestinatario { get; set; }
        public int iddestinatariodireccion { get; set; }
        public string direcciondestinatario { get; set; }
        public string razonsocialdestinatario { get; set; }
        public string rucdestinatario { get; set; }
        public string destinatario { get; set; }

        public string dnipersonarecojo { get; set; }
        public string personarecojo { get; set; }
        public string numeroinicial { get; set; }
        public string cantidad { get; set; }
        public int? idtipovehiculo { get; set; }
        public int descripciongeneral { get; set; }
        public string nuevadescripciongeneral { get; set; }
        public decimal? pesogeneral { get; set; }
        public decimal? volgeneral { get; set; }

        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public int bulto { get; set; }
        public string dni { get; set; }

        public decimal? pesovol { get; set; }
        public int? bultogeneral { get; set; }
        public string docgeneral { get; set; }
        public int idvehiculo { get; set; }
        public string chofer { get; set; }
        public string guiarecojo { get; set; }
        public string puntopartida { get; set; }
        public string guiatransportista { get; set; }
        public decimal? subtotal { get; set; }
        public decimal? igv { get; set; }
        public decimal? total { get; set; }
        public int idformula { get; set; }
        public string placa { get; set; }
        public int? iddistrito { get; set; }
        public decimal base1 { get; set; }
        public decimal tarifa { get; set; }
        public decimal minimo { get; set; }
        public String[] arrayconceptos { get; set; }
        public int idestado { get; set; }
        public int? idtipooperacion { get; set; }
        public int? idestaciondestino { get; set; }
        public int? idruta { get; set; }
        public int? idagencia { get; set; }

        public string cambioncompleto { get; set; }
        public long? idcamioncompleto { get; set; }

        public long? idcarga { get; set; }

        public string cepan { get; set; }
        public string osembarque { get; set; }
        public long? iddespacho { get; set; }

        public DateTime? fechainicio { get; set; }
        public DateTime? fechafin { get; set; }
        public int _tipoop { get; set; }
        public bool activo { get; set; }

        public string destino { get; set; }
        public string remitente { get; set; }
        public string tipooperacion { get; set; }
        public int idusuarioregistro { get; set; }
        public int idusuarioentrega { get; set; }
        public int idremitente_selected { get; set; }
        public int iddestinatario_selected { get; set; }
        public List<GuiaRemisionModel> guias { get; set; }

        public bool? cierreoperativo { get; set; }
        public DateTime? fechaentrega { get; set; }
        public string horaentrega { get; set; }
        public DateTime? fecharecojo { get; set; }
        public string horarecojo { get; set; }
        public DateTime? fechaplanificacion { get; set; }
        public DateTime? fechaconfirmacion { get; set; }
        public DateTime? fechadespacho { get; set; }
        public DateTime? fechallegada { get; set; }
        public string horallegada { get; set; }
        public DateTime? fecharegistro { get; set; }
        public DateTime? fechaentregaconciliacion { get; set; }
        public string nummanifiesto { get; set; }

        public bool registrorapido { get; set; }
        public bool reintegrotributario { get; set; }
        public bool nofacturable { get; set; }
        public decimal costoextra { get; set; }

        public bool devolucion { get; set; }
        public int idtarifa { get; set; }

        public decimal? recargo { get; set; }
        public string tipoot { get; set; }

        //public bool devolucion { get; set; }
        //public bool registrorapido { get; set; }
        public bool camioncompleto { get; set; }

        public long idordentrabajoasociada { get; set; }
        public string numcamioncompleto { get; set; }
        public long idpreliquidacion { get; set; }
        public string otasociada { get; set; }
        public string numcc { get; set; }
        public int idunidad { get; set; }
        public int idposicion { get; set; }
        public int idclienteaux { get; set; }
        public int idorigenaux { get; set; }
        public bool facturado { get; set; }
        public string grr { get; set; }


    }
}