using CommandContracts.Common;
using System;

namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarOrdenTrabajoCommand : Command
    {
        public long? idordentrabajo { get; set; }
        public long? idmanifiesto { get; set; }
        public long? idcarga { get; set; }
        public int idtipotransporte { get; set; }
        public string numcp { get; set; }
        public int idconceptocobro { get; set; }
        public int? idestacionorigen { get; set; }
        public int? idorigen { get; set; }
        public int iddestino { get; set; }
        public int idcargadaen { get; set; }
        public int identregara { get; set; }
        public int idtipomercaderia { get; set; }
        public int idcliente { get; set; }
        public int idclientetipounidad { get; set; }
        public int idformula { get; set; }
        public int idremitente { get; set; }
        public int idremitentedireccion { get; set; }
        public int iddestinatario { get; set; }
        public int iddestinatariodireccion { get; set; }
        public string dnipersonarecojo { get; set; }
        public string personarecojo { get; set; }
        public int idvehiculo { get; set; }
        public string guiarecojo { get; set; }
        public string guiatercero { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public decimal total { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public string docgeneral { get; set; }
        public int descripciongeneral { get; set; }
        public string dni { get; set; }
        public string placa { get; set; }
        public string chofer { get; set; }
        public string conceptocobro { get; set; }
        public int? bulto { get; set; }
        public string puntopartida { get; set; }
        public int idestado { get; set; }

        public int? idtipooperacion { get; set; }
        public int? idestaciondestino { get; set; }
        public int? idagencia { get; set; }
        public int? idruta { get; set; }
        public long? idcamioncompleto { get; set; }

        public string cepan { get; set; }
        public string esembarque { get; set; }
        public DateTime fecharegistro { get; set; }

        public decimal base1 { get; set; }
        public decimal tarifa { get; set; }
        public decimal minino { get; set; }

        public decimal? pesovol { get; set; }
        public int _tipoop { get; set; }
        public long? iddespacho { get; set; }
        public bool activo { get; set; }
        public int idusuarioregistro { get; set; }

        public string razonsocialdestinatario { get; set; }
        public string rucdestinatario { get; set; }
        public string direcciondestinatario { get; set; }

        public bool? cierreoperativo { get; set; }

        public DateTime? fechaentrega { get; set; }
        public string horaentrega { get; set; }
        public DateTime? fecharecojo { get; set; }
        public DateTime? fechaplanificacion { get; set; }
        public DateTime? fechaconfirmacion { get; set; }
        public DateTime? fechadespacho { get; set; }
        public DateTime? fechallegada { get; set; }
        public string horallegada { get; set; }
        public bool reintegrotributario { get; set; }
        public int idtarifa { get; set; }
        public int idusuarioentrega { get; set; }
        public bool? registrorapido { get; set; }
        public bool facturado { get; set; }

      




    }
}