using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web.TYS.Areas.Seguimiento.Models.Seguimiento
{
    public class MaestroTablaModel
    {
        public int idmaestrotabla { get; set; }
        public string maestrotabla { get; set; }

    }
    public class ValorTablaModel
    {
        public int? idvalortabla { get; set; }
        public string valor { get; set; }
        public int idmaestrotabla { get; set; }
        public int orden { get; set; }
        public bool activo { get; set; }

    }
    public class ClienteModel
    {
        public int? idcliente { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string nombrecorto { get; set; }
        public int? iddireccion { get; set; }
        public int idubigeo { get; set; }
        public decimal lineacredito { get; set; }
        public int idmonedalinea { get; set; }
        public string rutalogo { get; set; }
        public bool activo { get; set; }
        public string criterio { get; set; }
        public string ubigeo {get;set;} 
        public int iddistrito { get; set; }
        public string codigodireccion { get; set; }
        public string direccion { get; set; }
        public bool pagocontado { get; set; }
        public int idprovincia { get; set; }
    }
    public class ServicioModel
    {
        public long? idserviciooperacion { get; set; }
        public long idcarga { get; set; }
        public int idservicio { get; set; }
        public int cantidad { get; set; }
    }
    public class DireccionModel
    {
        public int? iddireccion { get; set; }
        public string direccion { get; set; }
        public int iddistrito { get; set; }
        public string ubigeo { get; set; }
        public string cliente { get; set; }
        public string codigo { get; set; }
        public int idcliente { get; set; }
        public bool activo { get; set; }
        public bool principal { get; set; }
        public bool puntopartida { get; set; }

    }

    public class UbigeoModel
    {
        public int iddistrito { get; set; }
        public string ubigeo { get; set; }
    }
    public class ProveedorClienteModel
    {
        public int idproveedor { get; set; }
        public int idcliente { get;set;}



    }
    public class FormulaModel
    {
        public int? idformula { get; set; }
        public string nombre { get; set; }
        public string formula { get; set; }
        public bool activo { get; set; }
        public string criterio { get; set; }
        public string aux { get; set; }
        public string operacion {get;set;}
    }
    public class RutaModel
    {
        public int? idruta { get; set; }
        public int? iddetalleruta { get; set; }
        public string nombre { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }
        public string ruta { get; set; }
        public string km { get; set; }
        public int idtipotransporte { get; set; }
        public int? iddistrito { get; set; }
        public string leadida { get; set; }
        public string leadretorno { get; set; }
        public string leaddocumentario { get; set; }
        public string etapas { get; set; }
        public int idorden { get; set; }
        public int iddepartamento { get; set; }
        public int? idprovincia { get; set; }


    }
    public class PrecintoModel
    {
        public int idprecinto { get; set; }
        public string precinto { get; set; }
        public long idvehiculo { get; set; }
        public int cantidad { get; set; }
        public IEnumerable<SelectListItem> ListaAccesorios { get; set; }
        public string[] AccesoriosSeleccionados { get; set; }
        public string inc_str_accesorios { get; set; }
    }
    public class CargaModel
    {
        public long? idcarga { get; set; }
        public int idestacion { get; set; }
        public int iddestino { get; set; }
        public int idcliente { get; set; }
        public int idtipooperacion { get; set;   }
        public int idagencia { get; set; }
        public int idruta { get; set; }
        public int idestaciondestino { get; set; }
        public DateTime fechacreacion { get; set; }
        public int idplanificador { get; set; }
        public int idproveedor { get; set; }
        public string numcarga { get; set; }
        public string numcp { get; set; }
        public string observacion { get; set; }
        public decimal peso { get; set; }
        public int? idvehiculo { get; set; }
        public int idmuelle { get; set; }
        public decimal vol { get; set; }
        public List<OrdenTrabajoModel> ordenes { get; set; }
        public bool activo { get; set; }
        public string placa { get; set; }
        public string nombrechofer { get; set; }
        public string proveedor { get; set; }
        public string tipo { get; set; }
        public DateTime? fechaconfirmacion { get; set; }
        public string horaconfirmacion { get; set; }
        public DateTime? fechasalida { get; set; }
        public string horasalida { get; set; }
        public string pesoporcentaje { get; set; }
        public string volporcentaje { get; set; }
        public int idtipotransporte { get; set; }
        public int _tipooperacion { get; set; }
        public string planificador { get; set; }
        public decimal volumenvehiculo { get; set; }
        public decimal cargautil { get; set; }
        public int usrregistrocarga { get; set; }
        public int idestado { get;set; }
        public string serviciosadicionales { get; set; }
        public string ids { get; set; }
    }
    public class operacionModel
    {
        public int id { get; set; }
        public string operador { get; set; }

    }
    public class ZonaModel
    {
        public int? idzona { get; set; }
        public string zona { get; set; }
        public int iddepartamento { get; set; }
        public string criterio { get; set; }
        public string distritos { get; set; }
    
    }

   
    public class DespachoModel
    {
        public long? iddespacho { get; set; }
        public int idvehiculo { get; set; }
        public DateTime? fechasalida { get; set; }
        public string horasalida { get; set; }
        public int? idusuariosalida { get; set; }
        public string placa { get; set; }
        public decimal? peso { get; set; }
        public decimal? vol { get; set; }
        public int _tipoop { get; set; }
        public int idvehiculo_old { get; set; }
        public int idvehiculo_new { get; set; }
    }
    public class GuiaRemisionModel
    {
        public long? idguiaremisioncliente { get; set; }
        public long? idordentrabajo { get; set; }
        public string nroguia { get; set; }
        public int idcobrarpor { get; set; }
        public int? bulto { get; set; }
        public decimal? peso { get; set; }
        public decimal? volumen { get; set; }
        public decimal? pesovol { get; set; }
        public string documento { get; set; }
    }
    public class TarifaModel
    {
        public int? idtarifa { get; set; }
        public int idcliente { get; set; }
        public int idclientecopia { get; set; }
        public int idorigen { get; set; }
        public int? iddepartamento { get; set; }
        public int? idprovincia { get; set; }
        public int? idorigenprovincia { get; set; }
        public int? iddistrito { get; set; }
        public int? idorigendistrito { get; set; }
        public int idformula { get; set; }
        public int? idzona { get; set; }
        public bool urgente { get; set; }
        public bool autoserv { get; set; }
        public bool val { get; set; }
        public int idtipotransporte { get; set; }
        public int idtipounidad { get; set; }
        public int idmoneda { get; set; }
        public decimal? montobase { get; set; }
        public decimal? minimo { get; set; }
        public decimal? desde { get; set; }
        public decimal? hasta { get; set; }
        public decimal precio { get; set; }
        public decimal? adicional { get; set; }
        public List<TarifaDetalleModel> conceptos { get; set; }
    }
    public class TarifaDetalleModel
    {
        public int? iddetalletarifa { get; set; }
        public int idtarifa { get; set; }
        public int idconceptocobro { get; set; }
    }

    public class CamionCompletoModel
    {



        public long? idcamioncompleto { get; set; }
        public string codigocamioncompleto { get; set; }
        public int idtipotransporte { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }
        public int idcliente { get; set; }
        public int idformula { get; set; }
        public int cantidaddestinos { get; set; }
        public int cantidadotscreaadas { get; set; }
        public long idcarga { get; set; }
        public int idvehiculo { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public decimal igv { get; set; }
        public int idruta { get; set; }
        public int? idestacionorigen { get; set; }
        public int idtipooperacion { get; set; }
        public int idtipounidad { get; set; }
        public decimal sobreestadia { get; set; }
        public int idconceptocobro { get; set; }
        public int usuarioregistro { get; set; }
        

    }
    public class MonitoreoVehiculoModel
    {
        public List<VehiculoModel> vehiculos { get; set; }
    }
    public class VehiculoModel
    {
        public int? idvehiculo { get; set; }
        public int idproveedor { get; set; }
        public int idtipo { get; set; }
        public int idmarca { get; set; }
        public int idmodelo { get; set; }
        public int idanio { get; set; }
        public int idcolor { get; set; }
        public int idcombustible { get; set; }
        public string regmtc { get; set; }
        public string placa { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public string confveh { get; set; }
        public decimal pesobruto { get; set; }
        public decimal cargautil { get; set; }
        public string seriemotor { get; set; }
        public string kilometraje { get; set; }
        public int idestado { get; set; }
        public int? idchofer { get; set; }
        public int idorigen { get; set; }
        public bool activo { get; set; }
        public string chofer { get; set; }
        public string inspecciones { get; set; }
        public int usuarioinspeccion { get; set; }
        public int usuarioasignado { get; set; }
        public int totalinspeecciones { get; set; }
        public int _tipoop { get; set; }
        public string estado { get; set; }
        
     

    }
    public class ChoferModel
    {
        public int? idchofer { get; set; }
        public int? idvehiculo { get; set; }
        public string nombrechofer { get; set; }
        public string apellidochofer { get; set; }
        public string dni { get; set; }
        public string brevete { get; set; }
        public int edad { get; set; }
        public int idsexo { get; set; }
        public string telefono { get; set; }
        public string direccionchofer { get; set; }
        public bool activo { get; set; }


    }
    public class ManifiestoModel
    {
        public long? idmanifiesto { get; set; }
        public long? idhojaruta { get; set; }
        public string nummanifiesto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int? idusuarioregistro { get; set; }
        public long iddespacho{ get; set; }
        public bool? activo { get; set; }
        public int _tipoop { get; set; }
        public string numhojaruta { get; set; }
        public int idvehiculo { get; set; }

        public int? iddestino { get; set; }
        public int idtipooperacion { get; set; }
        public string distrito { get; set; }
        public string numcp { get; set; }
        public long idordentrabajo { get; set; }
        public DateTime? fechadescarga { get; set; }
        public int? idusuariodescarga { get; set; }
        public bool embarquecompleto { get; set; }
        public DateTime? fechacontrolsunat { get; set;  }
        public int idpuerto { get; set; }

    }
    public class valijaModel
    {
        
        public int idvehiculo { get; set; }
        public int cantidadordenes { get; set; }

    }
    public class ReimpresionModel
    {
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public string numgrt { get; set; }
        public string numhojaruta { get; set; }
        public string numcarga { get; set; }

        public int idvehiculo { get; set; }
        public string placa { get; set; }
        public string transportista { get; set; }
        public string chofer { get; set; }
        public string rutas { get; set; }
        public decimal volumen { get; set; }
        public decimal peso { get; set; }
        public long iddespacho { get; set; }
        public long idcarga { get; set; }
        public long idmanifiesto { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }


        public long idordentrabajo { get; set; }
        public string GRT { get; set; }
        public string tipooperacion { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public int bulto { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }

    }
}
