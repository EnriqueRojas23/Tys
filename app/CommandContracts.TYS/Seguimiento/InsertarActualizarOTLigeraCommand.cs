

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarOTLigeraCommand : Command
    {
        public long? idordentrabajo { get; set; }
        public string numcp { get; set; }
        public int idcliente { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }

        public int iddestinatario { get; set; }
        public int  idremitente {get;set;}
        public string razonsocialdestinatario { get; set; }
        public string rucdestinatario { get; set; }
        public string direcciondestinatario { get; set; }
        public int iddestinatariodireccion { get; set; }

        public int idtipotransporte { get; set; }
        public int idconceptocobro { get; set; }
        public int idformula { get; set; }

        public bool reintegrotributario { get; set; }
        public bool facturable { get; set; }
        public int idvehiculo { get; set; }
        public string grt { get; set; }
        public int? idruta { get; set; }

        public decimal base1 { get; set; }
        public decimal tarifa { get; set; }
        public decimal minino { get; set; }

        public decimal? pesovol { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public int? bulto { get; set; }

        public int descripciongeneral { get; set; }
        public decimal total { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }

        
        public long? idcamioncompleto { get; set; }
        public int idestado { get; set; }

        
        public bool activo { get; set; }
        public int idusuarioregistro { get; set; }
        public DateTime? fecharegistro { get; set; }
        public bool devolucion { get; set; }

        public bool registrorapido { get; set; }
        public bool nofacturable { get; set; }
        public string guiatransportista { get; set; }
        public bool camioncompleto { get; set; }
        public int idclientetipounidad { get; set; }
           

    }
}
