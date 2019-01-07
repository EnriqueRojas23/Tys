

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerOrdenTrabajoResult : QueryResult
    {
        
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public string razonsocial { get; set; }
        public int idtipotransporte { get; set; }
        public int idconceptocobro { get; set; }
        public string conceptocobro { get; set; }
        public string[] arrayconceptos { get; set; }
        public string destino { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string tipotransporte { get; set; }
        public int idcargadaen {get;set;}
        public int identregara {get;set;}
        public int idestacionorigen {get;set;}
        public int iddestino {get;set;}
        public int idtipomercaderia {get;set;}
        public int idcliente {get;set;}
        public int idcobrarpor {get;set;}
        public int idremitente {get;set;}
        public int idremitentedireccion {get;set;}
        public int iddestinatario {get;set;}
        public int iddestinatariodireccion {get;set;}
        public string dnipersonarecojo {get;set;}
        public string personarecojo {get;set;}
        public string direcciondestinatario { get; set; }
        public string rucdestinatario { get; set;    }


        public int descripciongeneral { get; set; }
        public int iddescripciongeneral { get; set; }
        public string docgeneral { get; set; }
        public string guiarecojo { get; set; }
        public string guiatransportista { get; set; }
        public int idclienteconceptocobro { get; set; }
        public int idclientetipounidad { get; set; }
        public int idformula { get; set; }
        public int idorigen { get; set; }
        public int idtipovehiculo { get; set; }
        public int idvehiculo { get; set; }
     
        public string nombrechofer { get; set; }
        public string numeroinicial { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public decimal igv { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public string dni {get;set;}
        public string chofer {get;set;}
        public string placa { get; set;  }
        public int bulto { get; set; }

        public int? idtipooperacion { get; set; }
        public int? idagencia { get; set; }
        public int? idestaciondestino { get; set; }
        public int? idruta { get; set; }
        public string operacion { get; set; }
        public string cepan { get; set; }
        public string esembarque { get; set; }

        public decimal base1 { get; set; }
        public decimal tarifa { get; set; }
        public decimal minimo { get; set; }
        public decimal? pesovol { get; set; }
        public long? iddespacho { get; set; }
        public long? idmanifiesto { get; set; }
        public long? idcarga { get; set; }
        public string puntopartida { get; set; }
        public int idestado { get; set; }
        public DateTime fecharecojo { get; set; }

        public DateTime fechaentrega { get; set; }
        public bool reintegrotributario { get; set; }
        public DateTime? fechaentregaconciliacion { get; set; }
        public int idtarifa { get; set; }
        public decimal recargo { get; set; }
        public bool? registrorapido { get; set; }
        public bool? devolucion { get; set;  }
        public bool camioncompleto { get; set; }
    }
}
