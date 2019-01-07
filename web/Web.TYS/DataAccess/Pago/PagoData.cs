using QueryContracts.TYS.Pago.Parameters;
using QueryContracts.TYS.Pago.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Componentes.Common.Utilidades;
using ServiceAgents.Common;
using CommandContracts.TYS.Pago.Output;
using CommandContracts.TYS.Pago;
using QueryContracts.Core.CRM.Parameters;
using QueryContracts.Core.CRM.Result;
using Web.TYS.Areas.Pago.Models;

namespace Web.TYS.DataAccess.Pago
{
    public class PagoData
    {

        #region Proveedores

        public static List<ListarProveedorDto> GetListarProveedores(string search, bool inactivo)
        {

            var parametros = new ListarProveedorParameters { criterio = search , activo = (inactivo==true?false:true) };
            var resultado = (ListarProveedorResult)parametros.Execute();
            return resultado == null ? new  List<ListarProveedorDto>() : resultado.Hits.ToList();

        }
        public static InsertarProveedorOutput InsertarActualizarProveedor(ProveedorModel model , out string res )
        {

            if (model.idproveedor == 0)
                model.idproveedor = null;
            var comando = new InsertarActualizarProveedorCommand();
            comando.idproveedor = model.idproveedor;
            comando.placaasociada = model.placaasociada;
            comando.razonsocial = model.razonsocial;
            comando.ruc = model.ruc;
            comando.activo = model.activo;
            comando.direccion = model.direccion;
            res = "OK";

            var respuesta = (InsertarProveedorOutput)comando.Execute();
            return respuesta;
        }
        public ObtenerProveedorResult GetProveedor(int id)
        {
            var parametros = new ObtenerProveedorParameters { id = id };
            var resultado = (ObtenerProveedorResult)parametros.Execute();
            return resultado == null ? new ObtenerProveedorResult() : resultado;

        }

        #endregion


        #region tipocomprobante


        public static List<ListarTipoComprobanteDto> GetListarTipoComprobante(string search)
        {

            var parametros = new ListarTipoComprobanteParameters { criterio = search };
            var resultado = (ListarTipoComprobanteResult)parametros.Execute();
            return resultado == null ? new List<ListarTipoComprobanteDto>() : resultado.Hits.ToList();

        }
        public static InsertarTipoComprobanteOutput InsertarActualizarTipoComprobante(TipoComprobanteModel model, out string res)
        {

            if (model.idtipocomprobante == 0)
                model.idtipocomprobante = null;
            var comando = new InsertarActualizarTipoComprobanteCommand();
            comando.idtipocomprobante = model.idtipocomprobante;
            comando.codigo = model.codigo;
            comando.tipocomprobante = model.tipocomprobante;
            comando.activo = model.activo;

            res = "OK";

            var respuesta = (InsertarTipoComprobanteOutput)comando.Execute();
            return respuesta;
        }
        public ObtenerTipoComprobanteResult GetTipoComprobante(int id)
        {
            var parametros = new ObtenerTipoComprobanteParameter { id = id };
            var resultado = (ObtenerTipoComprobanteResult)parametros.Execute();
            return resultado == null ? new ObtenerTipoComprobanteResult() : resultado;

        }


        #endregion


        #region Moneda
        public static List<ListarMonedaDto> GetListarMoneda(string search)
        {

            var parametros = new ListarMonedaParameters { criterio = search };
            var resultado = (ListarMonedaResult)parametros.Execute();
            return resultado == null ? new List<ListarMonedaDto>() : resultado.Hits.ToList();

        }
        public static InsertarMonedaOutput InsertarActualizarMoneda(MonedaModel model, out string res)
        {

            if (model.idmoneda == 0)
                model.idmoneda = null;
            var comando = new InsertarActualizarMonedaCommand();
            comando.idmoneda = model.idmoneda;
            comando.moneda = model.moneda;
            comando.activo = model.activo;

            res = "OK";

            var respuesta = (InsertarMonedaOutput)comando.Execute();
            return respuesta;
        }
        public ObtenerMonedaResult GetMoneda(int id)
        {
            var parametros = new ObtenerMonedaParameter { id = id };
            var resultado = (ObtenerMonedaResult)parametros.Execute();
            return resultado == null ? new ObtenerMonedaResult() : resultado;

        }



        #endregion


        #region etapa
        public static List<ListarEtapaDto> GetListarEtapa(string search, bool inactivo)
        {

            var parametros = new ListarEtapaParameters { criterio = search , activo = (inactivo == true? false: true) };
            var resultado = (ListarEtapaResult)parametros.Execute();
            return resultado == null ? new List<ListarEtapaDto>() : resultado.Hits.ToList();

        }
        public static InsertarEtapaOutput InsertarActualizarEtapa(EtapaModel model, out string res)
        {

           

            if (model.idetapa == 0)
                model.idetapa = null;
            var comando = new InsertarActualizarEtapaCommand();
            comando.idetapa = model.idetapa;
            comando.etapa = model.etapa;
            comando.requiereot = model.requiereot;
            comando.activo = model.activo;

            res = "OK";

            var respuesta = (InsertarEtapaOutput)comando.Execute();
            return respuesta;
        }
        public ObtenerEtapaResult GetEtapa(int id)
        {
            var parametros = new ObtenerEtapaParameter { id = id };
            var resultado = (ObtenerEtapaResult)parametros.Execute();
            return resultado == null ? new ObtenerEtapaResult() : resultado;

        }



        #endregion

        #region TipoTransporte
        public static List<ListarTipoTransporteDto> GetListarTipoTransporte(string search)
        {

            var parametros = new ListarTipoTransporteParameters { criterio = search };
            var resultado = (ListarTipoTransporteResult)parametros.Execute();
            return resultado == null ? new List<ListarTipoTransporteDto>() : resultado.Hits.ToList();

        }
        public static InsertarTipoTransporteOutput InsertarActualizarTipoTransporte(TipoTransporteModel model, out string res)
        {

            if (model.idtipotransporte == 0)
                model.idtipotransporte = null;
            var comando = new InsertarActualizarTipoTransporteCommand();
            comando.idtipotransporte = model.idtipotransporte;
            comando.tipotransporte = model.tipotransporte;
            comando.activo = model.activo;

            res = "OK";

            var respuesta = (InsertarTipoTransporteOutput)comando.Execute();
            return respuesta;
        }
        public ObtenerTipoTransporteResult GetTipoTransporte(int id)
        {
            var parametros = new ObtenerTipoTransporteParameter { id = id };
            var resultado = (ObtenerTipoTransporteResult)parametros.Execute();
            return resultado == null ? new ObtenerTipoTransporteResult() : resultado;

        }


        #endregion 

        #region Comprobante

        public static List<ListarOrdenesTransporteDto> GetListarOT(string numcp, string guia, string manifiesto)
        {
            if (numcp == "") numcp = null;
            if (guia == "") guia = null;

                var cliente = new ServiceAgents.Core.CoreBackendClient();
                var result = (ListarOrdenesTransporteResult)cliente.ExecuteQuery(new ListarOrdenesTransporteParameters() { numcp = numcp, guia=  guia , manifiesto = manifiesto});
                if (result == null) return new List<ListarOrdenesTransporteDto>();
                return result.Hits.ToList();
        }


        public static List<ListarComprobanteDto> GetListarComprobante(string serie, string fechainicio, string fechafin)
        {
            if (fechainicio == "") fechainicio = null;
            if (fechafin == "") fechafin = null;
            var parametros = new ListarComprobanteParameters {  serie = serie , fechafin = fechafin , fechaini = fechainicio};
            var resultado = (ListarComprobanteResult)parametros.Execute();
            return resultado == null ? new List<ListarComprobanteDto>() : resultado.Hits.ToList();

        }
        public static ValidaOtsResult ValidarOTS(int id, int idetapa)
        {
            var parametros = new ValidaOtsParameters { id = id, idetapa = idetapa };
            var resultado = (ValidaOtsResult)parametros.Execute();
            return resultado == null ? new ValidaOtsResult() : resultado;

        }
        public static ValidaSerieResult ValidarSerie(string serie, int? idproveedor,int? idetapa, int? idtipocomprobante , long? idcomprobante)
        {
            var parametros = new ValidaSerieParameters { idcomprobante = idcomprobante,  idetapa =  idetapa, serie = serie, idproveedor = idproveedor , idtipocomprobante = idtipocomprobante };
            var resultado = (ValidaSerieResult)parametros.Execute();
            return resultado == null ? new ValidaSerieResult() : resultado;

        }
      
        public static InsertarComprobanteOutput InsertarActualizarComprobante(ComprobanteModel model, out string res)
        {

            if (model.idcomprobante == 0)
                model.idcomprobante = null;
            var comando = new InsertarActualizarComprobanteProveedorCommand();
            comando.idtipotransporte = model.idtipotransporte;
            if(model.fechacomprobante != string.Empty)
            comando.fechacomprobante = Convert.ToDateTime (model.fechacomprobante);
            comando.iddestino = model.iddestino;
            comando.idorigen = model.idorigen;
            comando.idmoneda = model.idmoneda;
            comando.idcomprobante = model.idcomprobante;
            comando.idetapa = model.idetapa;
            comando.idproveedor = model.idproveedor;
            comando.idtipocomprobante = model.idtipocomprobante;
            comando.monto = model.monto;
            comando.serienumero = model.serienumero;
            comando.concepto = model.Concepto;
            comando.activo = model.activo;
            comando.placa = model.placa;
            comando.observacion = model.observacion;
            comando.idtipofacturacion = model.idtipofacturacion;
            comando.idvehiculo = model.idvehiculo;
            comando.actainforme = model.actainforme;

            res = "OK";

            var respuesta = (InsertarComprobanteOutput)comando.Execute();


            var comandoedit = new InsertarActualizarComprobanteDetalleCommand();
            comandoedit.idcomprobante = respuesta.idcomprobante;
            comandoedit.alledit = true;
            comandoedit.Execute();






         

            foreach (var item in model.ots)
	        {
		        var comando1 = new InsertarActualizarComprobanteDetalleCommand();
                comando1.idcomprobante = respuesta.idcomprobante;

                    
                comando1.numcp = item.NumCp;
                comando1.PKID = item.PKID;
                comando1.Precio = item.Precio;
                comando1.SubTotal = item.SubTotal;
                comando1.Total = item.Total;
                comando1.TotalBultos = item.TotalBultos;
                comando1.TotalPeso = item.TotalPeso;
                comando1.ValorVenta = item.ValorVenta;
                comando1.nroguia = item.nroguia;
                comando1.manifiesto = item.manifiesto;





                comando1.Execute();
	        }
           
            


            return respuesta;
        }
        public ObtenerComprobanteResult GetComprobante(long id)
        {
            var parametros = new ObtenerComprobanteParameter { id = id };
            var resultado = (ObtenerComprobanteResult)parametros.Execute();
            return resultado == null ? new ObtenerComprobanteResult() : resultado;

        }
     

        #endregion

        #region DetalleComprobante

        public static List<ListarDetalleComprobanteDto> GetListarDetalleComprobante(long  idComprobante)
        {

            var parametros = new ListarDetalleComprobanteParameters {  idcomprobante = idComprobante };
            var resultado = (ListarDetalleComprobanteResult)parametros.Execute();
            return resultado == null ? new List<ListarDetalleComprobanteDto>() : resultado.Hits.ToList();

        }
        public static InsertarTipoTransporteOutput InsertarActualizarDetalleComprobante(DetalleComprobanteModel model, out string res)
        {

            if (model.idcomprobantedetalle == 0)
                model.idcomprobantedetalle = null;
            var comando = new InsertarActualizarComprobanteDetalleCommand();
            comando.idcomprobante = model.idcomprobante;
            comando.numcp = model.numcp;

            res = "OK";

            var respuesta = (InsertarTipoTransporteOutput)comando.Execute();
            return respuesta;
        }
        //public ObtenerTipoTransporteResult GetDetalleComprobante(int id)
        //{
        //    var parametros = new ObtenerTipoTransporteParameter { id = id };
        //    var resultado = (ObtenerTipoTransporteResult)parametros.Execute();
        //    return resultado == null ? new ObtenerTipoTransporteResult() : resultado;

        //}

        #endregion

        #region ReporteGeneral
        public static List<ListarReporteGeneralDto> GetListarReporteGeneral(int? idproveedor, int? iddestino, string fecinicio, string fecfinal)
        {
            if (fecinicio == "") fecinicio = null;
            if (fecfinal == "") fecfinal = null;
            var parametros = new ListarReporteGeneralParameters { fechafin = fecfinal,  iddestino  = iddestino, idproveedor = idproveedor, fechaini = fecinicio };
            var resultado = (ListarReporteGeneralResult)parametros.Execute();
            return resultado == null ? new List<ListarReporteGeneralDto>() : resultado.Hits.ToList();
        }
        #endregion

        #region ReporteGerencial
        public static List<ListarReporteGerencialDto> GetListarReporteGerencial(int? iddestino , int? idproveedor,string fecinicio, string fecfinal)
        {
            var parametros = new ListarReporteGerencialParameters { iddestino = iddestino, idproveedor = idproveedor, fechafin = fecfinal, fechaini = fecinicio };
            var resultado = (ListarReporteGerencialResult)parametros.Execute();
            return resultado == null ? new List<ListarReporteGerencialDto>() : resultado.Hits.ToList();
        }
        public static List<ExportarReporteGerencialDto> GetExportarReporteGerencial(int? iddestino, int? idproveedor, string fecinicio, string fecfinal)
        {
            var parametros = new ExportarReporteGerencialParameters { iddestino = iddestino, idproveedor = idproveedor, fechafin = fecfinal, fechaini = fecinicio };
            var resultado = (ExportarReporteGerencialResult)parametros.Execute();
            return resultado == null ? new List<ExportarReporteGerencialDto>() : resultado.Hits.ToList();
        }



        #endregion

        #region Combos
        public static List<ListarUbigeoDto> GetListarUbigeo()
        {

            var parametros = new ListarUbigeoParameters();
            var resultado = (ListarUbigeoResult)parametros.Execute(); 
            return resultado == null ? new List<ListarUbigeoDto>() : resultado.Hits.ToList();

        }
        public static List<CargarEtapasDto> GetCargarEtapas(int idproveedor)
        {

            var parametros = new CargarEtapasParameter{ idproveedor =idproveedor };
            var resultado = (CargarEtapasResult)parametros.Execute();
            return resultado == null ? new List<CargarEtapasDto>() : resultado.Hits.ToList();

        }
        public static List<CargarTipoTransporteDto> GetCargarTipoTransporte(int idetapa, int idproveedor)
        {

            var parametros = new CargarTipoTransporteParameter { idetapa = idetapa , idproveedor = idproveedor};
            var resultado = (CargarTipoTransporteResult)parametros.Execute();
            return resultado == null ? new List<CargarTipoTransporteDto>() : resultado.Hits.ToList();

        }
        public static List<CargarTipoComprobanteDto> GetCargarTipoComprobante(int idtipotransporte, int idetapa, int idproveedor)
        {

            var parametros = new CargarTipoComprobanteParameter { idtipotransporte = idtipotransporte , idetapa = idetapa, idproveedor = idproveedor };
            var resultado = (CargarTipoComprobanteResult)parametros.Execute();
            return resultado == null ? new List<CargarTipoComprobanteDto>() : resultado.Hits.ToList();

        }
        public static List<CargarMonedasDto> GetCargarMoneda(int idtipocomprobante, int idtipotransporte, int idetapa, int idproveedor)
        {

            var parametros = new CargarMonedasParameter {idtipotransporte = idtipotransporte 
                , idtipocomprobante = idtipocomprobante, idetapa = idetapa, idproveedor = idproveedor };
            var resultado = (CargarMonedasResult)parametros.Execute();
            return resultado == null ? new List<CargarMonedasDto>() : resultado.Hits.ToList();

        }

        public static List<ListarPlacaDto> GetListarPlaca(int? idproveedor)
        {

            var parametros = new ListarPlacaParameters {  idproveedor = idproveedor };
            var resultado = (ListarPlacaResult)parametros.Execute();
            return resultado == null ? new List<ListarPlacaDto>() : resultado.Hits.ToList();

        }

        #endregion

        #region Asignacion
        public static List<ListarAsignacionDto> GetListarAsignacion(string criterio)
        {
            var parametros = new ListarAsignacionParameters {  criterio = criterio};
            var resultado = (ListarAsignacionResult)parametros.Execute();
            return resultado == null ? new List<ListarAsignacionDto>() : resultado.Hits.ToList();
        }
        public static InsertarAsignacionOutput InsertarActualizarAsignacion(AsignacionModel model)
        {

            if (model.idasignacion == 0)
                model.idasignacion = null;
            var comando = new InsertarActualizarAsignacionCommand();
            comando.idtipotransporte = model.idtipotransporte;
            comando.idmoneda = model.idmoneda;
            comando.idetapa = model.idetapa;
            comando.idproveedor = model.idproveedor;
            comando.idtipocomprobante = model.idtipocomprobante;
            comando.idasignacion = model.idasignacion;

            var respuesta = (InsertarAsignacionOutput)comando.Execute();
            return respuesta;
        }
        public EliminarAsignacionResult EliminarAsignacion(int id)
        {
            var parametros = new EliminarAsignacionParameter { idasignacion = id };
            var resultado = (EliminarAsignacionResult)parametros.Execute();
            return resultado == null ? new EliminarAsignacionResult() : resultado;

        }
        public static List<ListarExportarAsignacionDto> GetExportarAsignacion(string criterio)
        {
            var parametros = new ListarExportarAsignacionParameters {  criterio = criterio };
            var resultado = (ListarExportarAsignacionResult)parametros.Execute();
            return resultado == null ? new List<ListarExportarAsignacionDto>() : resultado.Hits.ToList();
        }

        #endregion


    }
}