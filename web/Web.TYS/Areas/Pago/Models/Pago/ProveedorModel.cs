using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.TYS.Areas.Pago.Models
{
    public class ProveedorModel
    {
        public int? idproveedor { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string placaasociada { get; set; }
        public bool activo { get; set; }
        public string direccion { get; set; }

    }
    public class TipoComprobanteModel
    {
        public int? idtipocomprobante { get; set; }
        public string codigo { get; set; }
        public string tipocomprobante { get; set; }
        public bool activo { get; set; }
    }
    public class MonedaModel
    {
        public int? idmoneda { get; set; }
        public string moneda { get; set; }
        public bool activo { get; set; }
    }
    public class EtapaModel
    {
        public int? idetapa { get; set; }
        public string etapa { get; set; }
        public bool requiereot { get; set; }
        public bool activo { get; set; }

    }
    public class TipoTransporteModel
    {
        public int? idtipotransporte { get; set; }
        public string tipotransporte { get; set; }
        public bool activo { get; set; }
    }


    public class ComprobanteModel
    {
        public long? idcomprobante { get; set; }
        public string serienumero { get; set; }
        public string fecharegistro { get; set; }
        public string fechacomprobante { get; set; }
        public string fechavencimiento { get; set; }
            
        public string fechainicio { get; set; }
        public string fechafin { get; set; }

        public string moneda { get; set; }

       
        [Required(ErrorMessage="Debe ingresar un monto")]
        public decimal? monto { get; set; }

        public string tipocomprobante { get; set; }
        public string etapa { get; set; }
        public string tipotransporte { get; set; }
        public string razonsocial { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Concepto { get; set; }

        public int? idtipotransporte { get; set; }
        public int? idetapa { get; set; }
        public int? idmoneda { get; set; }
        public int? idtipocomprobante { get; set; }
        public int? idproveedor { get; set; }
        public int? idorigen { get; set; }
        public int? iddestino { get; set; }
        public string observacion { get; set; }
        public string placa { get; set; }
        public int idvehiculo { get; set; }
        public int idtipofacturacion { get; set; }
        public string actainforme { get; set; }

        public bool activo { get; set; }
        public ComprobanteModel()
        {
            ots = new List<OrdenesTransporteModel>();
        }
        public List<OrdenesTransporteModel> ots { get; set; }
    }

    public class OrdenesTransporteModel
    {
        public int PKID { get; set; }
        public string NumCp { get; set; }
        public string ValorVenta { get; set; }
        public string TotalBultos { get; set; }
        public string TotalPeso { get; set; }
        public string Precio { get; set; }
        public string SubTotal { get; set; }
        public string Total { get; set; }
        public string nroguia { get; set; }
        public string manifiesto { get; set; }
    }
    public class DetalleComprobanteModel
    {
        public int? idcomprobantedetalle { get; set; }
        public int idcomprobante { get; set; }
        public string numcp { get; set; }
    }
   


    public class ReporteGeneralModel
    {
        public int idcomprobante { get; set; }
        public string tipocomprobante { get; set; }
        public string serienumero { get; set; }
        public string fechacomprobante { get; set; }
        public string razonsocial { get; set; }
        public string moneda { get; set; }
        public string monto { get; set; }
        public string fecharegistro { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string etapa { get; set; }
        public string tipotransporte { get; set; }
        public string concepto { get; set; }
        public string placa { get; set; }
        public string ots { get; set; }
        public string peso { get; set; }
        public string valorizado { get; set; }
        public string fechainicio { get; set; }
        public string fechafin { get; set; }
        public int idproveedor { get; set; }
        public int iddestino {get;set;}
    }
    public class ReporteGerencialModel
    {
        public int idproveedor { get; set; }
        public string etapa { get; set; }
        public string destino { get; set; }
        public string costo { get; set; }
        public string kgs { get; set; }
        public string cu { get; set; }
        public string produccion { get; set; }
        public string rentabilidad { get; set; }
        public string porcentaje { get; set; }
        public string fechainicio { get; set; }
        public string fechafin { get; set; }
        public string serienumero { get; set; }
        public int iddestino { get;set; }
    }

    public class AsignacionModel
    {
        public int? idasignacion { get; set; }
        public int idproveedor { get; set; }
        public int idetapa { get; set; }
        public int idtipotransporte { get; set; }
        public int idmoneda { get; set; }
        public int idtipocomprobante { get; set; }
        public string razonsocial { get; set; }
    }



}