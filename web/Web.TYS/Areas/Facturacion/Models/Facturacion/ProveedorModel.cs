using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.TYS.Areas.Facturacion.Models
{
    public class PreliquidacionModel
    {
        public int idcliente { get; set; }
        public int iddestino { get; set; }
        public long idordentrabajo { get; set; }
        public DateTime fecharegistro { get; set; }
        public DateTime fechaemision { get; set; }
        public string numcp { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string modotransporte { get; set; }
        public string tipooperacion { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public int totalbulto { get; set; }
        public decimal tarifa { get; set; }
        public decimal subtotal { get; set; }

        public string strsubtotal { get; set; }
        public string strigv { get; set; }
        public string strtotal { get; set; }

        public decimal base1 { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal peso { get; set; }
        public decimal pesovol { get; set; }
        public decimal volumen { get; set; }
        public int bulto { get; set; }
        public long? idpreliquidacion { get; set; }
        public string numeropreliquidacion { get; set; }
        public string numerocomprobante { get; set; }
        public long? idcomprobantepago { get; set; }
        public decimal? recargo { get; set; }
        public int idusuarioregistro { get; set; }
        public int idestado { get; set; }
        public string _fechaemision { get; set; }
        public string descripcion { get; set; }
        public string razonsocial { get; set; }
        public string direccion { get; set; }
        public string ruc { get; set; }
        public string ordenes { get; set; }
        public string placas { get; set; }
        public string guiasrr { get; set; }
        public string conceptocobro { get; set; }
        public int cantidad { get; set; }
        public int _tipoop { get; set; }
        public long idfacturavinculada { get; set; }
        public int idnumerodocumento { get; set; }
        public string ordencompra { get; set; }
    }

    public class ComprobanteDetalleModel
    {
        public long? iddetallecomprobante { get; set; }
        public long idcomprobantepago { get; set; }
        public long? idordentrabajo { get; set; }

        public int cantidad { get; set; }
        public string unidadmedida { get; set; }
        public string descripcion { get; set; }


        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal? recargo { get; set; }
    }

    public class ComprobanteModel
    {
        public long? idcomprobantepago { get; set; }
        public long idpreliquidacion { get; set; }
        public string numeropreliquidacion { get; set; }
        public int idtipocomprobante { get; set; }
        public string tipocomprobantepago { get; set; }
        public string numerocomprobante { get; set; }
        public string search_numerocomprobante { get; set; }
        public string numnotacredito { get; set; }

        public int idmotivoanulacion { get; set; }

        public int emisionrapida { get; set; }
        public DateTime? fechaemision { get; set; }
        public DateTime? fechainicio { get; set; }
        public DateTime? fechafin { get; set; }

        public string estadosunat { get; set; }


        public string _fechaemision { get; set; }
        public string cliente { get; set; }
        public string ruc { get; set; }
        public int totalbulto { get; set; }
        public decimal totalpeso { get; set; }
        public decimal totalvolumen { get; set; }
        public decimal recargo { get; set; }

        public int idcliente { get; set; }
        public decimal igv { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public int idusuarioregistro { get; set; }
        public int idestado { get; set; }
        public DateTime fechaimpresion { get; set; }
        public string razonsocial { get; set; }
        public string motivo { get; set; }
        public string direccion { get; set; }
        public string descripcion { get; set; }
        public int _tipoop { get; set; }
        public string strsubtotal { get; set; }
        public string strigv { get; set; }
        public string strtotal { get; set; }
        public long idfacturavinculada { get; set; }
        public string ordencompra { get; set; }

        public long DocEntry { get; set; }
        public string estado { get; set; }
        public string tipo_de_nota_de_credito { get; set; }
        public string tipo_de_nota_de_debito { get; set; }
        public string fechaRegistro { get; set; }

        public string numerocomprobantevinculada { get; set; }

    }

    public class DocumentoModel
    {
        public int? idnumerodocumento { get; set; }
        public string serie { get; set; }
        public string primernumero { get; set; }
        public string ultimonumero { get; set; }
        public int? idusuarioautorizado { get; set; }
        public int idestacion { get; set; }
        public int idtipocomprobante { get; set; }
    }
}