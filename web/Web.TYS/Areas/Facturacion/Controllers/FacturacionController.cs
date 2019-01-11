using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Common.Controllers;
using Componentes.Common.Utilidades;
using Web.Common.Extensions;
using Web.Common.HtmlHelpers;
using Web.TYS.Areas.Facturacion.Models;
using QueryContracts.TYS.Pago.Results;
using QueryContracts.Core.CRM.Result;
using Web.TYS.DataAccess.Seguimiento;
using Web.TYS.DataAccess.Facturacion;
using Web.TYS.DataAccess;
using System.Configuration;
using Web.TYS.DataAccess.Seguridad;
using Web.TYS.Areas.Seguridad.Models.Usuarios;
using Web.TYS.Areas.Monitoreo.Models;
using Web.TYS.DataAccess.Monitoreo;

namespace Web.TYS.Areas.Facturacion.Controllers
{
    public class FacturacionController : BaseController
    {
        //
        // GET: /Facturacion/Facturacion/

            
        #region PartialView
                
        public PartialViewResult GenerarComprobante()
        {
            return PartialView("_GenerarComprobante");
        }

        public PartialViewResult AgregarRecargo()
        {
            return PartialView("_AgregarRecargo");
        }

        public PartialViewResult AgregarOtsPreliquidacion()
        {
            return PartialView("_AgregarOtsPreliquidacion");
        }

        public PartialViewResult PreBoleta(long idpreliquidacion)
        {
            string descripcion = "";
            Session["detallefacturacion"] = null;


            decimal igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            var liquidaciones = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion).ToList();



            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);
            var series = new FacturacionData().GetListarDocumentos((Int32)Constantes.TipoComprobante.Boleta, usuario.usr_int_id, usuario.idestacionorigen).ToList();

            if (series.Count == 0)
                series = new FacturacionData().GetListarDocumentos((Int32)Constantes.TipoComprobante.Boleta, null, usuario.idestacionorigen).ToList();

            var listaseries = new SelectList(
                   series,
                   "idnumerodocumento"
                   , "serie"
                   );
            ViewData["ListaSeries"] = listaseries;


            string guias = string.Empty;

            liquidaciones.ForEach(x =>
            {
                guias = guias + x.guiasrr;
            });

            var liquidacion = liquidaciones.FirstOrDefault();

            if (guias.Length >= 600)
            {
                guias = guias.Substring(0, 600);
            }
            descripcion = string.Format(descripcion
                , liquidacion.totalbulto
                , liquidacion.totalpeso
                , liquidacion.totalvolumen
                , liquidacion.ordenes
                , guias
                , liquidacion.placas).ToUpper();

            liquidacion.fechaemision = DateTime.Now;
            liquidacion.descripcion = descripcion;

            if (liquidacion.recargo.HasValue)
            {
                liquidacion.subtotal = liquidacion.subtotal + liquidacion.recargo.Value;
                liquidacion.igv = liquidacion.subtotal * igv;
                liquidacion.total = liquidacion.subtotal + liquidacion.igv;
            }

            liquidacion.strtotal = String.Format("{0:n}", liquidacion.total);//.ToString("#.##");
            liquidacion.strigv = liquidacion.igv.ToString("#.##");//String.Format("{0:n}", liquidacion.igv);//.ToString("#.##");
            liquidacion.strsubtotal = liquidacion.subtotal.ToString("#.##"); //String.Format("{0:n}", liquidacion.subtotal); //.ToString("#.##");

            return PartialView("_VistaBoleta", liquidacion);
        }

        public PartialViewResult PreFactura(long idpreliquidacion)
        {
            Session["detallefacturacion"] = null;


            decimal igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            var liquidaciones = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion).ToList();

            var liquidacion = liquidaciones.First();

            

            string guias = "", descripcion = "";

            liquidaciones.ForEach(x =>
            {
                guias = guias + x.guiasrr;
            });
            if (guias.Length >= 600)
            {
                guias = guias.Substring(0, 600);
            }

            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);
            var series = new FacturacionData().GetListarDocumentos((int)Constantes.TipoComprobante.Factura , usuario.usr_int_id, usuario.idestacionorigen).ToList();

            if (series == null)
                series = new FacturacionData().GetListarDocumentos((int)Constantes.TipoComprobante.Factura, null, usuario.idestacionorigen).ToList();

            var listaseries = new SelectList(
                   series,
                   "idnumerodocumento"
                   , "serie"
                   );
            ViewData["ListaSeries"] = listaseries;


            liquidacion.fechaemision = DateTime.Now;
            liquidacion.descripcion = descripcion;

            if (liquidacion.recargo.HasValue)
            {
                liquidacion.subtotal = liquidacion.subtotal + liquidacion.recargo.Value;
                liquidacion.igv = liquidacion.subtotal * igv;
                liquidacion.total = liquidacion.subtotal + liquidacion.igv;
            }

            liquidacion.strtotal = String.Format("{0:n}", liquidacion.total);//.ToString("#.##");
            liquidacion.strigv = liquidacion.igv.ToString("#.##");//String.Format("{0:n}", liquidacion.igv);//.ToString("#.##");
            liquidacion.strsubtotal = liquidacion.subtotal.ToString("#.##"); //String.Format("{0:n}", liquidacion.subtotal); //.ToString("#.##");

            return PartialView("_VistaFactura", liquidacion);
        }

        public PartialViewResult PreNotaCredito(long idcomprobantepago)
        {
            //   var liquidacion = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion);
            var comprobante = new FacturacionData().ObtenerComprobante(null, idcomprobantepago);
           
            var tipounidad = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.MotivoAnulacion)).Where(x => x.activo == true).ToList();
            var listatipounidad = new SelectList(
               tipounidad,
               "idvalortabla",
               "valor");
            ViewData["ListaMovitos"] = listatipounidad;

            string next = string.Empty;

            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);
            var series = new FacturacionData().GetListarDocumentos((int)Constantes.TipoComprobante.NotaCredito, usuario.usr_int_id, usuario.idestacionorigen)
                .Where(x => x.idtipocomprobante == (Int32)Constantes.TipoComprobante.NotaCredito).ToList();

            if (series == null)
                series = new FacturacionData().GetListarDocumentos((int)Constantes.TipoComprobante.NotaCredito, null, usuario.idestacionorigen).ToList();

            if (series == null)
                return null;


            var numerocomprobante = series.First();
            int cantidadcaracteres = 0;
            if (numerocomprobante.ultimonumero == null)
            {
                next = numerocomprobante.primernumero.Trim();
                cantidadcaracteres = next.Trim().Split('-')[1].Length;
                next = next.Split('-')[0].ToString() + "-" + (Convert.ToInt32(next.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }
            else
            {
                cantidadcaracteres = numerocomprobante.ultimonumero.Trim().Length;
                next = numerocomprobante.serie + "-" + (Convert.ToInt32(numerocomprobante.ultimonumero) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }



            comprobante._fechaemision = DateTime.Now.ToShortDateString();
            comprobante.descripcion = "";
            comprobante.strtotal = String.Format("{0:n}", comprobante.total);//.ToString("#.##");
            comprobante.strigv = comprobante.igv.ToString("#.##");//String.Format("{0:n}", liquidacion.igv);//.ToString("#.##");
            comprobante.strsubtotal = comprobante.subtotal.ToString("#.##"); //String.Format("{0:n}", liquidacion.
            comprobante.numnotacredito = next;

            return PartialView("_VistaNC", comprobante);
        }
        public PartialViewResult EditarComprobante(long idcomprobante)
        {
            Session["detallefacturacion"] = null;

            var comprobante = new FacturacionData().ObtenerComprobante(null, idcomprobante);
            comprobante.strigv = comprobante.igv.ToString();
            comprobante.strsubtotal = comprobante.subtotal.ToString();
            comprobante.strtotal = comprobante.total.ToString();

            if (comprobante.idtipocomprobante == (Int16)Constantes.TipoComprobante.NotaCredito)
                return PartialView("_VistaNCEditar", comprobante);
            else if(comprobante.idtipocomprobante == (Int16)Constantes.TipoComprobante.Factura)
                return PartialView("_VistaFacturaEditar", comprobante);
            else if (comprobante.idtipocomprobante == (Int16)Constantes.TipoComprobante.Boleta)
                return PartialView("_VistaBoletaEditar", comprobante);
            else return PartialView("_VistaFacturaEditar", comprobante);

            
        }

        #endregion PartialView

        #region Preliquidacion

        public JsonResult JsonGenerarPreliquidacion(string ordenes)
        {
            var igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            var ords = new FacturacionData().GetListarOTS(ordenes);
            ords.ForEach(delegate(Seguimiento.Models.Seguimiento.OrdenTrabajoModel data)
            {
                if (data.recargo == null)
                    data.recargo = 0;
            });

            var otscc = ords.Where(x => x.camioncompleto == true).ToList();

            foreach (var item in otscc)
            {
                var otshijas = OrdenData.GetListarOrdenesHijasCC(item.idordentrabajo.Value);

                foreach (var item1 in otshijas)
                {
                    ordenes = ordenes + "," + item1.idordentrabajo;
                }
            }
            ords = new FacturacionData().GetListarOTS(ordenes);

            PreliquidacionModel model = new PreliquidacionModel();
            model.totalpeso = ords.Select(x => x.peso).Sum();
            model.totalvolumen = ords.Select(x => x.volumen).Sum();
            model.subtotal = ords.Select(x => x.subtotal.Value).Sum();
            model.total = ords.Select(x => x.total.Value).Sum();
            model.igv = ords.Select(x => x.igv.Value).Sum();
            model.totalbulto = ords.Select(x => x.bulto).Sum();
            model.recargo = ords.Select(x => x.recargo).Sum();
            model.fecharegistro = DateTime.Now;
            model.idusuarioregistro = Convert.ToInt32(Usuario.Idusuario);
            model.idcliente = ords.Select(x => x.idcliente).First();
            model.idestado = (Int32)DataAccess.Constantes.EstadoPreliquidacion.PendienteFactura;
            model.numeropreliquidacion = FacturacionData.ObtenerUltimaPreliquidacion();
            model._tipoop = 1;

            ////insertar preliquidacion
            long? idpreliquidacion =
                new FacturacionData().InsertarActualizarPreliquidacion(model);

            new FacturacionData().ActualizarOTS(ordenes, idpreliquidacion);

            EtapaModel modeletapa = new EtapaModel();
            modeletapa.fecharegistro = DateTime.Now;
            modeletapa.idusuarioregistro = (Int32)Usuario.Idusuario;
            modeletapa.visible = true;
            modeletapa.fechaetapa = DateTime.Now;
            modeletapa.idsorden = ordenes;
            modeletapa.descripcion = "N° Preliquidación : " + model.numeropreliquidacion;
            modeletapa.idmaestroetapa = (Int32)Constantes.MaestroEtapas.ConPreliquidacion;
            modeletapa.tipoetapa = "";

            var idetapa = DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modeletapa, null);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion Preliquidacion

        #region GestionPreliquidacion

        public JsonResult JsonGetListarGestionPreliquidacion(int? idcliente
          , int? idestado
          , string numerocomprobante
          , int? idtipocomprobante
          , string numeropreliquidacion
          , string sidx, string sord, int page, int rows)
        {
            if (numerocomprobante == string.Empty) numerocomprobante = null;
            if (numeropreliquidacion == string.Empty) numeropreliquidacion = null;
            decimal igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());

            var listadoTotal = new FacturacionData().GetListarPreliquidacion(idcliente, idestado, numerocomprobante, numeropreliquidacion, idtipocomprobante).ToList();
            listadoTotal.ForEach(x =>
            {
                x.subtotal = x.recargo + x.subtotal;
                x.igv = x.subtotal * igv;
                x.total = x.subtotal + x.igv;
            });
            var resjson1 = (new JqGridExtension<ComprobanteModel>()).DataBind(listadoTotal, listadoTotal.Count);

            return resjson1;

            //var listadoTotal = listado;
            //int pageindex = page - 1;
            //int pagesize = rows;

            //int totalrecord = listadoTotal.Count();
            //var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            //var jsonData = new
            //{
            //    total = totalpage,
            //    page,
            //    records = totalrecord,
            //    rows = listado
            //};

            //return Json(jsonData, JsonRequestBehavior.AllowGet);

            //return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditarPreliquidacion(long id)
        {
            // PreliquidacionModel model = new PreliquidacionModel();
            var igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            var model = new FacturacionData().ObtenerPreliquidacion(id).FirstOrDefault();
            var listado = new FacturacionData().GetListarCompletadoPreliquidacion(id).ToList();
            model.idpreliquidacion = id;
            //model.numeropreliquidacion = preliquidacion.numeropreliquidacion;
            if (model.recargo.HasValue)
            {
                model.subtotal = model.subtotal + model.recargo.Value;
                model.igv = model.subtotal * igv;
                model.total = model.igv + model.subtotal;
            }

            model.strsubtotal = model.subtotal.ToString(); //.ToString("###,###.##");
            model.strigv = model.igv.ToString(); //.ToString("###,###.##");
            model.strtotal = model.total.ToString();//.ToString("###,###.##");
            model.cantidad = listado.Count;
            return View(model);
        }

        #endregion GestionPreliquidacion

        #region Comprobante

        public ActionResult Comprobantes()
        {

            var tiposcomprobante = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoComprobante));
            var listatiposcomprobante = new SelectList(
               tiposcomprobante,
               "idvalortabla",
               "valor");
            ViewData["ListaTiposComprobante"] = listatiposcomprobante;

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;
            ComprobanteModel model = new ComprobanteModel();
            model.idestado = (Int32)Constantes.EstadoFactura.Emitido;
            model.fechainicio = DateTime.Now.Subtract(TimeSpan.FromDays(31));
            model.fechafin = DateTime.Now;

            return View(model);
        }

        public JsonResult JsonObtenerNumeroDocumento(int? id)
        {
            string next = string.Empty;
            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);
            var series = new FacturacionData().GetListarDocumentos(null, usuario.usr_int_id, usuario.idestacionorigen).ToList();

            if (series == null)
                series = new FacturacionData().GetListarDocumentos(null, null, usuario.idestacionorigen).ToList();

            var numerocomprobante = series.Where(x => x.idnumerodocumento.Equals(id)).SingleOrDefault();
            int cantidadcaracteres = 0;
            if (numerocomprobante.ultimonumero == null)
            {
                next = numerocomprobante.primernumero.Trim();
                cantidadcaracteres = next.Trim().Split('-')[1].Length;
                next = next.Split('-')[0].ToString() + "-" + (Convert.ToInt32(next.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }
            else
            {
                cantidadcaracteres = numerocomprobante.ultimonumero.Trim().Length;
                next = numerocomprobante.serie + "-" + (Convert.ToInt32(numerocomprobante.ultimonumero) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }

            return Json(new { next = next, res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetListarComprobantes(int? idcliente, string numerocomprobante, int? idtipocomprobante, string numeropreliquidacion
            , string fecinicio  , string fecfin
           , string sidx, string sord, int page, int rows)
        {
            if (numerocomprobante == string.Empty) numerocomprobante = null;
            if (numeropreliquidacion == string.Empty) numeropreliquidacion = null;
            decimal igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());

            var listadoTotal = new FacturacionData().GetListarComprobantes(idcliente, (Int16)(Constantes.EstadoFactura.Emitido) , numerocomprobante, numeropreliquidacion, idtipocomprobante, fecinicio,fecfin).ToList();

            var resjson1 = (new JqGridExtension<ComprobanteModel>()).DataBind(listadoTotal, listadoTotal.Count);

            return resjson1;

        }

        [HttpPost]
        public JsonResult JsonGenerarComprobante()
        {

            var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
            if (listado == null)
                return Json(new { res = false, msj = "Requiere de un item como mínimo." }, JsonRequestBehavior.AllowGet);



            long? idpreliquidacion = Convert.ToInt64(Request.Form.GetValues("idpreliquidacion").FirstOrDefault());
            var __descripcion = Request.Form.GetValues("__descripcion").FirstOrDefault();
            var igv = Request.Form.GetValues("igv").FirstOrDefault();
            var subtotal = Request.Form.GetValues("subtotal").FirstOrDefault();
            var fecha = Request.Form.GetValues("fecha").FirstOrDefault();
            var numerodocumento = Request.Form.GetValues("numerodocumento").FirstOrDefault();
            var ordencompra = Request.Form.GetValues("ordencompra").FirstOrDefault();

            if (subtotal == "")
                return Json(new { res = false, msj = "Debe ingresar un subtotal" }, JsonRequestBehavior.AllowGet);
            if (numerodocumento == "")
                return Json(new { res = false, msj = "Debe ingresar una serie y número de documento" }, JsonRequestBehavior.AllowGet);
            if (fecha == "")
                return Json(new { res = false, msj = "Debe seleccionar una fecha de emisión" }, JsonRequestBehavior.AllowGet);

            ComprobanteDetalleModel modDetalle;
            string tipocomprobante = "FACT";
            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);

            string valorigv = ConfigurationManager.AppSettings["igv"].ToString();

            var ordenes = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value);
            string idsordenes = string.Empty;
            foreach (var item in ordenes)
            {
                idsordenes = idsordenes + "," + item.idordentrabajo;
            }
            idsordenes = idsordenes.Substring(1, idsordenes.Length - 1);

            var preliquidaciones = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion.Value);
            var preliquidacion = preliquidaciones.First();

            AutoMapper.Mapper.CreateMap<PreliquidacionModel, ComprobanteModel>();
            var model = AutoMapper.Mapper.Map<PreliquidacionModel, ComprobanteModel>(preliquidacion);

            #region asignarpropiedades

            if (tipocomprobante == "FACT")
                model.idtipocomprobante = (Int16)DataAccess.Constantes.TipoComprobante.Factura;
            else
                model.idtipocomprobante = (Int16)DataAccess.Constantes.TipoComprobante.Boleta;
            model.idusuarioregistro = (Int32)Usuario.Idusuario;

            int cantidadcaracteres = 0;
            var numerocomprobante = new FacturacionData().obtenerNumeroComprobante(null
                , null
                , null, Convert.ToInt32(numerodocumento));

            if (numerocomprobante.ultimonumero == null)
            {
                model.numerocomprobante = numerocomprobante.primernumero.Trim();
                cantidadcaracteres = model.numerocomprobante.Trim().Split('-')[1].Length;
                model.numerocomprobante = model.numerocomprobante.Split('-')[0].ToString() + "-" + (Convert.ToInt32(model.numerocomprobante.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }
            else
            {
                cantidadcaracteres = numerocomprobante.ultimonumero.Trim().Split('-')[1].Length;
                model.numerocomprobante = numerocomprobante.ultimonumero.Split('-')[0].ToString() + "-" + (Convert.ToInt32(numerocomprobante.ultimonumero.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }

            model.emisionrapida = 0;
            model.fechaemision = Convert.ToDateTime(fecha);
            model.descripcion = __descripcion;
            model.subtotal = Convert.ToDecimal(subtotal);
            model.igv = Convert.ToDecimal(igv);
            model.total = model.subtotal + model.igv;
            model.idestado = (Int32)Constantes.EstadoFactura.Emitido;
            model._tipoop = 1; // nuevo
            model.ordencompra = ordencompra;
            #endregion asignarpropiedades

            var idcomprobante = FacturacionData.GenerarComprobante(model);

            if (idcomprobante > 0)
            {
                new FacturacionData().actualizarNumeroComprobante(model.idtipocomprobante, model.numerocomprobante.Split('-')[1].ToString(), numerocomprobante.idnumerodocumento);
                new FacturacionData().ActualizarComprobanteOTS(idsordenes, model.fechaemision);
            }
            var otspreliquidadas = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value);

            foreach (var item in listado)
            {
                modDetalle = new ComprobanteDetalleModel();
                modDetalle.idcomprobantepago = idcomprobante;
                modDetalle.igv = (item.subtotal * Convert.ToDecimal(valorigv));
                modDetalle.subtotal = item.subtotal;
                modDetalle.total = item.subtotal + modDetalle.igv;
                modDetalle.descripcion = item.descripcion;

                modDetalle.recargo = item.recargo;

                if (modDetalle.recargo > 0)
                {
                    #region actualizar liquidacion con recargo
                    preliquidacion.recargo = modDetalle.recargo;
                    preliquidacion._tipoop = 1;
                    idpreliquidacion = new FacturacionData().InsertarActualizarPreliquidacion(preliquidacion);

                    #endregion
                }

                DataAccess.Facturacion.FacturacionData.GenerarDetalleComprobante(modDetalle);
            }

            #region Incidencia

            IncidenciaModel modIncidencia = new IncidenciaModel();
            modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ComprobanteGenerado;
            modIncidencia.idsorden = idsordenes;
            modIncidencia.fechaincidencia = DateTime.Now;
            modIncidencia.fecharegistro = DateTime.Now;
            modIncidencia.descripcion = "Se ha generado la : " + tipocomprobante + " N°" + model.numerocomprobante;
            modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
            modIncidencia.activo = true;
            IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

            #endregion Incidencia

            #region PreliquidacionActualizar

            preliquidacion.idestado = (Int32)Constantes.EstadoPreliquidacion.Facturado;
            preliquidacion.idcomprobantepago = idcomprobante;
            preliquidacion._tipoop = 3;//Comprobante generado
            idpreliquidacion = new FacturacionData().InsertarActualizarPreliquidacion(preliquidacion);

            #endregion PreliquidacionActualizar

            return Json(new { res = true, idcomprobante = idcomprobante, valorigv = valorigv }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonEditarComprobante()
        {

            var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
            if (listado == null)
                return Json(new { res = false, msj = "Requiere de un item como mínimo." });



            long idcomprobantepago = Convert.ToInt64(Request.Form.GetValues("idcomprobante").FirstOrDefault());
            var igv = Request.Form.GetValues("igv").FirstOrDefault();
            var subtotal = Request.Form.GetValues("subtotal").FirstOrDefault();
            if (subtotal == "")
                return Json(new { res = false, msj = "Debe ingresar un subtotal" }, JsonRequestBehavior.AllowGet);
         

            ComprobanteDetalleModel modDetalle;
            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);

            string valorigv = ConfigurationManager.AppSettings["igv"].ToString();

            var comprobante = new FacturacionData().ObtenerComprobante(null,idcomprobantepago);
           
            comprobante.subtotal = Convert.ToDecimal(subtotal);
            comprobante.igv = Convert.ToDecimal(igv);
            comprobante.total = comprobante.subtotal + comprobante.igv;
            comprobante.idestado = (Int32)Constantes.EstadoFactura.Emitido;
            comprobante._tipoop = 1; // editar
        
            var idcomprobante = FacturacionData.GenerarComprobante(comprobante);

          

            foreach (var item in listado)
            {
                modDetalle = new ComprobanteDetalleModel();
                modDetalle.idcomprobantepago = idcomprobante;
                modDetalle.igv = (item.subtotal * Convert.ToDecimal(valorigv));
                modDetalle.subtotal = item.subtotal;
                modDetalle.total = item.subtotal + modDetalle.igv;
                modDetalle.descripcion = item.descripcion;
                DataAccess.Facturacion.FacturacionData.GenerarDetalleComprobante(modDetalle);
            }

            return Json(new { res = true, idcomprobante = idcomprobante, valorigv = valorigv }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult JsonGenerarBoleta()
        {

            var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
            if (listado == null)
                return Json(new { res = false, msj = "Requiere de un item como mínimo." }, JsonRequestBehavior.AllowGet);



            long? idpreliquidacion = Convert.ToInt64(Request.Form.GetValues("idpreliquidacion").FirstOrDefault());
            var __descripcion = Request.Form.GetValues("__descripcion").FirstOrDefault();
            var igv = Request.Form.GetValues("igv").FirstOrDefault();
            var subtotal = Request.Form.GetValues("subtotal").FirstOrDefault();
            var fecha = Request.Form.GetValues("fecha").FirstOrDefault();
            var numerodocumento = Request.Form.GetValues("numerodocumento").FirstOrDefault();

            if (subtotal == "")
                return Json(new { res = false, msj = "Debe ingresar un subtotal" }, JsonRequestBehavior.AllowGet);
            if (numerodocumento == "")
                return Json(new { res = false, msj = "Debe ingresar una serie y número de documento" }, JsonRequestBehavior.AllowGet);
            if (fecha == "")
                return Json(new { res = false, msj = "Debe seleccionar una fecha de emisión" }, JsonRequestBehavior.AllowGet);


            ComprobanteDetalleModel modDetalle;
            string tipocomprobante = "BOL";

            string valorigv = ConfigurationManager.AppSettings["igv"].ToString();

            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);

            var ordenes = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value);

            if (ordenes.Count() == 0)
            {
                return Json(new { res = false, msj = "Esta preliquidación no tiene órdenes asociadas" }, JsonRequestBehavior.AllowGet);
            }

            string idsordenes = string.Empty;
            foreach (var item in ordenes)
            {
                idsordenes = idsordenes + "," + item.idordentrabajo;
            }
            idsordenes = idsordenes.Substring(1, idsordenes.Length - 1);

            var preliquidaciones = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion.Value);
            var preliquidacion = preliquidaciones.First();

            AutoMapper.Mapper.CreateMap<PreliquidacionModel, ComprobanteModel>();
            var model = AutoMapper.Mapper.Map<PreliquidacionModel, ComprobanteModel>(preliquidacion);

            #region asignarpropiedades

            if (tipocomprobante == "FACT")
                model.idtipocomprobante = (Int16)DataAccess.Constantes.TipoComprobante.Factura;
            else
                model.idtipocomprobante = (Int16)DataAccess.Constantes.TipoComprobante.Boleta;
            model.idusuarioregistro = (Int32)Usuario.Idusuario;

            int cantidadcaracteres = 0;
            var numerocomprobante = new FacturacionData().obtenerNumeroComprobante((Int32)Usuario.Idusuario, model.idtipocomprobante, usuario.idestacionorigen, null);

            if (numerocomprobante.ultimonumero == null)
            {
                model.numerocomprobante = numerocomprobante.primernumero.Trim();
                cantidadcaracteres = model.numerocomprobante.Trim().Split('-')[1].Length;
                model.numerocomprobante = model.numerocomprobante.Split('-')[0].ToString() + "-" + (Convert.ToInt32(model.numerocomprobante.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }
            else
            {
                cantidadcaracteres = numerocomprobante.ultimonumero.Trim().Split('-')[1].Length;
                model.numerocomprobante = numerocomprobante.ultimonumero.Split('-')[0].ToString() + "-" + (Convert.ToInt32(numerocomprobante.ultimonumero.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }

            model.emisionrapida = 0;
            model.fechaemision = DateTime.Now;
            model.descripcion = __descripcion;
            model.subtotal = Convert.ToDecimal(subtotal);
            model.igv = Convert.ToDecimal(igv);
            model.total = model.subtotal + model.igv;
            model._tipoop = 1; // nuevo
            model.idestado = (Int16)Constantes.EstadoFactura.Emitido;

            #endregion asignarpropiedades

            var idcomprobante = FacturacionData.GenerarComprobante(model);

            if (idcomprobante > 0)
            {
                new FacturacionData().actualizarNumeroComprobante(model.idtipocomprobante, model.numerocomprobante.Split('-')[1].ToString(), numerocomprobante.idnumerodocumento);
                new FacturacionData().ActualizarComprobanteOTS(idsordenes, model.fechaemision);

            }
           // var otspreliquidadas = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value);

            foreach (var item in listado)
            {
                modDetalle = new ComprobanteDetalleModel();
                modDetalle.idcomprobantepago = idcomprobante;
                modDetalle.igv = (item.subtotal * Convert.ToDecimal(valorigv));
                modDetalle.subtotal = item.subtotal;
                modDetalle.total = item.subtotal + modDetalle.igv;
                modDetalle.descripcion = item.descripcion;

                modDetalle.recargo = item.recargo;

                if (modDetalle.recargo > 0)
                {
                    #region actualizar liquidacion con recargo
                    preliquidacion.recargo = modDetalle.recargo;
                    preliquidacion._tipoop = 1;
                    idpreliquidacion = new FacturacionData().InsertarActualizarPreliquidacion(preliquidacion);

                    #endregion
                }



                DataAccess.Facturacion.FacturacionData.GenerarDetalleComprobante(modDetalle);
            }

            preliquidacion.idestado = (Int32)Constantes.EstadoPreliquidacion.Facturado;
            idpreliquidacion =
         new FacturacionData().InsertarActualizarPreliquidacion(preliquidacion);

            return Json(new { res = true, idcomprobante = idcomprobante, valorigv = valorigv }, JsonRequestBehavior.AllowGet);
        }

       



        [HttpPost]
        public ActionResult GridSaveDetalle(FormCollection formcollection)
        {
            var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];

            long? iddetalle = Convert.ToInt64(formcollection["iddetallecomprobante"]);

            string descripcion = "";
            if (formcollection["descripcion"] != "")
                descripcion = formcollection["descripcion"].ToString();

            decimal subtotal = 0;
            if (formcollection["subtotal"] != "")
                subtotal = decimal.Parse(formcollection["subtotal"]);


            if (iddetalle == 0)
            {
                int max = listado.Count;
                var detalle = new ComprobanteDetalleModel
                {
                    idcomprobantepago = 1,
                    cantidad = 1,
                    descripcion = descripcion,
                    unidadmedida = DataAccess.Seguimiento.SeguimientoData
                                   .GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Unidad))
                                   .Where(x => x.idvalortabla == 76).First().valor,
                    subtotal = subtotal,
                    iddetallecomprobante = max + 1
                };
                listado.Add(detalle);
            }
            else
            {
                foreach (var item in listado)
                {
                    if (item.iddetallecomprobante.Value == iddetalle)
                    {
                        item.descripcion = descripcion;
                        item.subtotal = subtotal;

                        break;
                    }
                }
            }


            Session["detallefacturacion"] = listado;

            return Json(new { res = true, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            //return View();
        }

        [HttpPost]
        public JsonResult EliminarDetalleFactura(int id)
        {
            var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
            listado.Remove(listado.Where(x => x.iddetallecomprobante.Value.Equals(id)).First());
            Session["detallefacturacion"] = listado;
            return Json(new { res = true, Errors = ModelState.Errors() });
        }

        #endregion Comprobante

        [HttpPost]
        public JsonResult ValidarComprobante(long idpreliquidacion)
        {
            var comprobante = new FacturacionData().ObtenerComprobante(idpreliquidacion, null);
            if (comprobante != null)
                return Json(new { res = true, idcomprobante = comprobante.idcomprobantepago }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { res = true, idcomprobante = 0 }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ValidarTipoComprobante(long idcomprobante)
        {
            var comprobante_base = new FacturacionData().ObtenerComprobante(null, idcomprobante);

            if (comprobante_base.idtipocomprobante == (Int16)Constantes.TipoComprobante.NotaCredito
                || comprobante_base.idtipocomprobante == (Int16)Constantes.TipoComprobante.NotaDebito)
                return Json(new { res = false, msj = "No se puede generar NC de este documento." }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { res = true, idcomprobante = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidarEnvioSunat(long idcomprobante) //Edicion
        {

           

            var comprobante = new FacturacionData().ObtenerFacturaElectronica(idcomprobante);

            if (comprobante != null)
            {
                if(comprobante.estado ==  "E")
                return Json(new { res = false, msj="El documento ya fue enviado a Sunat" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { res = true, idcomprobante = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { res = true, idcomprobante = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Preliquidacion()
        {
            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            return View();
        }

        public JsonResult JsonGetListarCompletadoPreliquidacion(long idpreliquidacion, string sidx, string sord, int page, int rows)
        {
            var listado = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion).ToList();

            listado.ForEach(x =>
            {
                if (x.recargo.HasValue)
                    x.total = x.recargo.Value + x.subtotal;
                else
                    x.total = x.subtotal;
            });

            var listadoTotal = listado;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetListarDocumentos(int? idtipocomprobante, string sidx, string sord, int page, int rows)
        {
            var listado = new FacturacionData().GetListarDocumentos(idtipocomprobante, null, null).ToList();

            var listadoTotal = listado;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetListarPreliquidacion(int? idcliente, int? iddestino, string numcp, string sidx, string sord, int page, int rows)
        {
            if (numcp == string.Empty) numcp = null;

            if (idcliente == null)
                return Json(null, JsonRequestBehavior.AllowGet);

            var listadoTotal = new FacturacionData().GetListarPendientePreliquidacion(idcliente, iddestino, numcp).ToList();

            listadoTotal.ForEach(x =>
            {
                if (x.recargo.HasValue)
                    x.total = x.recargo.Value + x.subtotal;
            });

            return (new JqGridExtension<PreliquidacionModel>()).DataBind(listadoTotal, listadoTotal.Count);


        }

        //[HttpPost]
        //public JsonResult GetListarDetalleFacturacion(long idpreliquidacion)
        //{
        //    var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
        //    if(listado != null)
        //    {
        //        return (new JqGridExtension<ComprobanteDetalleModel>()).DataBind(listado, listado.Count);
        //    }

        //    var detallesfacturacion = new FacturacionData().GetListarComprobanteDetalles(idpreliquidacion).ToList();
        //    Session["detallefacturacion"] = detallesfacturacion;
        //    return (new JqGridExtension<ComprobanteDetalleModel>()).DataBind(detallesfacturacion, detallesfacturacion.Count);
        //}
        public JsonResult GetListarDetalleFacturacion(long idpreliquidacion, string sidx, string sord, int page, int rows)
        {
            var listadoTotal = new List<ComprobanteDetalleModel>();
            decimal igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            var liquidaciones = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion).ToList();
            var liquidacion = liquidaciones.First();


            if (Session["detallefacturacion"] == null)
            {
                var detalle = new ComprobanteDetalleModel
                {
                    idcomprobantepago = idpreliquidacion,
                    cantidad = 1,
                    descripcion = "POR SERVICIO DE TRANSPORTE Y DISTRIBUCIÓN DE MERCADERIA.",
                    unidadmedida = DataAccess.Seguimiento.SeguimientoData
                                   .GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Unidad))
                                   .Where(x => x.idvalortabla == 76).First().valor,
                    subtotal = liquidacion.subtotal + (liquidacion.recargo == null ? 0 : liquidacion.recargo.Value),
                    iddetallecomprobante = 1
                };
                listadoTotal.Add(detalle);
                Session["detallefacturacion"] = listadoTotal;
            }
            else
            {
                listadoTotal = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
            }

            return (new JqGridExtension<ComprobanteDetalleModel>()).DataBind(listadoTotal, listadoTotal.Count);
        }
        public JsonResult GetListarDetalleNotaCredito(long idpreliquidacion, string sidx, string sord, int page, int rows)
        {
            var listadoTotal = new List<ComprobanteDetalleModel>();
            decimal igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            var liquidaciones = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion).ToList();
            var liquidacion = liquidaciones.First();


            if (Session["detallefacturacion"] == null)
            {
                var detalle = new ComprobanteDetalleModel
                {
                    idcomprobantepago = idpreliquidacion,
                    cantidad = 1,
                    descripcion = "POR SERVICIO DE TRANSPORTE Y DISTRIBUCIÓN DE MERCADERIA.",
                    unidadmedida = DataAccess.Seguimiento.SeguimientoData
                                   .GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Unidad))
                                   .Where(x => x.idvalortabla == 76).First().valor,
                    subtotal = liquidacion.subtotal + (liquidacion.recargo == null ? 0 : liquidacion.recargo.Value),
                    iddetallecomprobante = 1
                };
                listadoTotal.Add(detalle);
                Session["detallefacturacion"] = listadoTotal;
            }
            else
            {
                listadoTotal = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
            }

            return (new JqGridExtension<ComprobanteDetalleModel>()).DataBind(listadoTotal, listadoTotal.Count);
        }

        [HttpPost]
        public JsonResult JsonAgregarOtsPopUP(string ids, long? idpreliquidacion)
        {
            // var ords = new FacturacionData().GetListarOTS(ids);
            string idspre = string.Empty;
            var igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            new FacturacionData().ActualizarOTS(ids, idpreliquidacion);
            //obtener ots por liquidacion
            var otspreliquidadas = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value);
            foreach (var item in otspreliquidadas)
            {
                idspre = idspre + ',' + item.idordentrabajo;
            }
            idspre = idspre.Substring(1, idspre.Length - 1);
            var ords = new FacturacionData().GetListarOTS(idspre);
            //actualizar preliquidacion

            var model = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion.Value).FirstOrDefault();

            model.totalpeso = ords.Select(x => x.peso).Sum();
            model.totalvolumen = ords.Select(x => x.volumen).Sum();
            model.subtotal = ords.Select(x => x.subtotal.Value).Sum();
            model.total = ords.Select(x => x.total.Value).Sum();
            model.igv = ords.Select(x => x.igv.Value).Sum();
            model.totalbulto = ords.Select(x => x.bulto).Sum();
            model.recargo = ords.Select(x => x.recargo).Sum();
            model._tipoop = 1;

            idpreliquidacion =
         new FacturacionData().InsertarActualizarPreliquidacion(model);

            if (model.recargo.HasValue)
            {
                model.subtotal = model.subtotal + model.recargo.Value;
                model.igv = (model.subtotal) * igv;
                model.total = model.subtotal + model.igv;
            }

            model.strsubtotal = model.subtotal.ToString(); //.ToString("###,###.##");
            model.strigv = model.igv.ToString(); //.ToString("###,###.##");
            model.strtotal = model.total.ToString();//.ToString("###,###.##");

            return Json(new
            {
                res = true
                ,
                subtotal = model.strsubtotal
                ,
                igv = model.strigv
                ,
                total = model.strtotal
                ,
                peso = model.totalpeso
                ,
                volumen = model.totalvolumen
                ,
                bultos = model.totalbulto
                       ,
                recargo = model.recargo,
                cantidad = ords.Count
            }, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult GestionPreliquidacion()
        {
            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados((Int32)Constantes.MaestroTablas.Preliquidacion);
            var listaestados = new SelectList(
                estados,
                "idestado"
                , "estado"
                );
            ViewData["ListaEstados"] = listaestados;

            var tiposcomprobante = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoComprobante));
            var listatiposcomprobante = new SelectList(
               tiposcomprobante,
               "idvalortabla",
               "valor");
            ViewData["ListaTiposComprobante"] = listatiposcomprobante;

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;
            ComprobanteModel model = new ComprobanteModel();
            model.idestado = 22;

            return View(model);
        }

        [HttpPost]
        public JsonResult JsonEliminarComprobante(long idcomprobante)
        {
            var comprobante = new FacturacionData().ObtenerComprobante(null, idcomprobante);
            var ordenes = new FacturacionData().GetListarCompletadoPreliquidacion(comprobante.idpreliquidacion);
            string idsordenes = string.Empty;
            foreach (var item in ordenes)
            {
                idsordenes = idsordenes + "," + item.idordentrabajo;
            }
            idsordenes = idsordenes.Substring(1, idsordenes.Length - 1);

            new DataAccess.Facturacion.FacturacionData().EliminarComprobante(idcomprobante);
            new FacturacionData().ActualizarComprobanteOTS(idsordenes, null);

            IncidenciaModel modIncidencia = new IncidenciaModel();
            modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ComprobanteEliminado;
            modIncidencia.idsorden = idsordenes;
            modIncidencia.fechaincidencia = DateTime.Now;
            modIncidencia.fecharegistro = DateTime.Now;
            modIncidencia.descripcion = "El comprobante ha sido eliminado: " + comprobante.tipocomprobantepago + " N° " + comprobante.numerocomprobante;
            modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
            modIncidencia.activo = true;
            IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAnularComprobante(long idcomprobante)
        {
            var comprobante = new FacturacionData().ObtenerComprobante(null, idcomprobante);

            var ordenes = new FacturacionData().GetListarCompletadoPreliquidacion(comprobante.idpreliquidacion);
            string idsordenes = string.Empty;
            foreach (var item in ordenes)
            {
                idsordenes = idsordenes + "," + item.idordentrabajo;
            }
            idsordenes = idsordenes.Substring(1, idsordenes.Length - 1);

            new DataAccess.Facturacion.FacturacionData().AnularComprobante(idcomprobante);

            new FacturacionData().ActualizarComprobanteOTS(idsordenes, null);

            IncidenciaModel modIncidencia = new IncidenciaModel();
            modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.Comprobanteanulado;
            modIncidencia.idsorden = idsordenes;
            modIncidencia.fechaincidencia = DateTime.Now;
            modIncidencia.fecharegistro = DateTime.Now;
            modIncidencia.descripcion = "El comprobante ha sido anulado: " + comprobante.tipocomprobantepago + " N° " + comprobante.numerocomprobante;
            modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
            modIncidencia.activo = true;
            IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

            var preliquidacion = new PreliquidacionModel();
            preliquidacion.idpreliquidacion = comprobante.idpreliquidacion;
            preliquidacion.idestado = (Int32)Constantes.EstadoPreliquidacion.PendienteFactura;
            preliquidacion._tipoop = 2;//Desvincular comprobante
            new FacturacionData().InsertarActualizarPreliquidacion(preliquidacion);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarRecargo(PreliquidacionModel model)
        {
            model.idpreliquidacion = model.idestado;

            //se usa el estado ya que idpreliqudiacion esta ocupado con la pantalla base

            long idordentrabajo = new FacturacionData().AgregarRecargo(model);
            var igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            if (model.idpreliquidacion == 0)
            {
                return Json(new
                {
                    res = true
                }, JsonRequestBehavior.AllowGet); ;
            }

            var otspreliquidadas = new FacturacionData().GetListarCompletadoPreliquidacion(model.idpreliquidacion.Value);

            var subtotal = otspreliquidadas.Sum(x => x.subtotal).ToString();
            var totalpeso = otspreliquidadas.Sum(x => x.peso).ToString();
            var totalvolumen = otspreliquidadas.Sum(x => x.volumen).ToString();
            var total = otspreliquidadas.Sum(x => x.total).ToString();
            var igvaux = otspreliquidadas.Sum(x => x.igv).ToString();
            var bulto = otspreliquidadas.Sum(x => x.bulto).ToString();
            var recargo = otspreliquidadas.Sum(x => x.recargo).ToString();

            string ots = "";
            foreach (var item in otspreliquidadas)
            {
                ots = ots + "," + item.idordentrabajo.ToString();
            }
            ots = ots.Substring(1, ots.Length - 1);

            //var ords = new FacturacionData().GetListarOTS(ots);

            var preliquidacion = new FacturacionData().ObtenerPreliquidacion(model.idpreliquidacion.Value).FirstOrDefault();
            preliquidacion.totalpeso = Convert.ToDecimal(totalpeso); //ords.Select(x => x.peso).Sum();
            preliquidacion.totalvolumen = Convert.ToDecimal(totalvolumen); //ords.Select(x => x.volumen).Sum();
            preliquidacion.subtotal = Convert.ToDecimal(subtotal);//ords.Select(x => x.subtotal.Value).Sum();
            preliquidacion.total = Convert.ToDecimal(total); // ords.Select(x => x.total.Value).Sum();
            preliquidacion.igv = Convert.ToDecimal(igvaux); //ords.Select(x => x.igv.Value).Sum();
            preliquidacion.totalbulto = Convert.ToInt32(bulto);  //ords.Select(x => x.bulto).Sum();
            preliquidacion.recargo = Convert.ToDecimal(recargo); //ords.Select(x => x.recargo);
            preliquidacion._tipoop = 1;

            var idpreliquidacion =
          new FacturacionData().InsertarActualizarPreliquidacion(preliquidacion);

            if (model.recargo.HasValue)
            {
                preliquidacion.subtotal = preliquidacion.subtotal + preliquidacion.recargo.Value;
                preliquidacion.igv = (preliquidacion.subtotal) * igv;
                preliquidacion.total = preliquidacion.subtotal + preliquidacion.igv;
            }

            model.strsubtotal = preliquidacion.subtotal.ToString(); //.ToString("###,###.##");
            model.strigv = preliquidacion.igv.ToString(); //.ToString("###,###.##");
            model.strtotal = preliquidacion.total.ToString();//.ToString("###,###.##");

            return Json(new
            {
                res = true
                ,
                subtotal = model.strsubtotal
                ,
                igv = model.strigv
                ,
                total = model.strtotal
                ,
                peso = preliquidacion.totalpeso
                ,
                volumen = preliquidacion.totalvolumen
                ,
                bultos = preliquidacion.totalbulto
                       ,
                recargo = preliquidacion.recargo,
                cantidad = otspreliquidadas.ToList().Count
            }, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult Documentos()
        {
            var tiposcomprobante = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoComprobante));
            var listatiposcomprobante = new SelectList(
               tiposcomprobante,
               "idvalortabla",
               "valor");
            ViewData["ListaTiposComprobante"] = listatiposcomprobante;

            return View();
        }

        public JsonResult JsonElminarOts(string ids, long? idpreliquidacion)
        {
            string idspre = string.Empty;
            //elminar ots
            new FacturacionData().ActualizarOTS(ids, null);
            var igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            //obtener ots por liquidacion
            var otspreliquidadas = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value).ToList();
            foreach (var item in otspreliquidadas)
            {
                idspre = idspre + ',' + item.idordentrabajo;
            }
            var model = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion.Value).FirstOrDefault();
            if (otspreliquidadas.Count != 0)
            {
                idspre = idspre.Substring(1, idspre.Length - 1);
                var ords = new FacturacionData().GetListarOTS(idspre);
                //actualizar preliquidacion

                model.totalpeso = ords.Select(x => x.peso).Sum();
                model.totalvolumen = ords.Select(x => x.volumen).Sum();
                model.subtotal = ords.Select(x => x.subtotal.Value).Sum();
                model.total = ords.Select(x => x.total.Value).Sum();
                model.igv = ords.Select(x => x.igv.Value).Sum();
                model.totalbulto = ords.Select(x => x.bulto).Sum();
                model.recargo = ords.Select(x => x.recargo).Sum();
                model._tipoop = 1;

                idpreliquidacion =
             new FacturacionData().InsertarActualizarPreliquidacion(model);

                if (model.recargo.HasValue)
                {
                    model.subtotal = model.subtotal + model.recargo.Value;
                    model.igv = model.subtotal * igv;
                    model.total = model.subtotal + model.igv;
                }
                model.strsubtotal = model.subtotal.ToString(); //.ToString("###,###.##");
                model.strigv = model.igv.ToString(); //.ToString("###,###.##");
                model.strtotal = model.total.ToString();//.ToString("###,###.##");
            }
            else
            {
                model.idestado = (Int32)DataAccess.Constantes.EstadoPreliquidacion.Anulado;
                model._tipoop = 2;
                model.totalpeso = 0;
                model.totalvolumen = 0;
                model.subtotal = 0;
                model.total = 0;
                model.igv = 0;
                model.totalbulto = 0;
                model.recargo = 0;
                model._tipoop = 1;
                idpreliquidacion =

         new FacturacionData().InsertarActualizarPreliquidacion(model);
            }

            return Json(new
            {
                res = true
                ,
                subtotal = model.strsubtotal
                ,
                igv = model.strigv
                ,
                total = model.strtotal
                ,
                peso = model.totalpeso
                ,
                volumen = model.totalvolumen
                ,
                bultos = model.totalbulto,
                recargo = model.recargo,
                cantidad = otspreliquidadas.Count
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAnularPreliquidacion(long? idpreliquidacion)
        {
            string idspre = string.Empty;
            var otspreliquidadas = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value);
            var model = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion.Value).FirstOrDefault();

            foreach (var item in otspreliquidadas)
            {
                idspre = idspre + ',' + item.idordentrabajo;
            }
            idspre = idspre.Substring(1, idspre.Length - 1);
            new FacturacionData().ActualizarOTS(idspre, null);

            model.idestado = (Int32)DataAccess.Constantes.EstadoPreliquidacion.Anulado;
            model._tipoop = 4;

            idpreliquidacion =
             new FacturacionData().InsertarActualizarPreliquidacion(model);

            IncidenciaModel modIncidencia = new IncidenciaModel();

            modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.Preliquidacionanulada;
            modIncidencia.idsorden = idspre;
            modIncidencia.fechaincidencia = DateTime.Now;
            modIncidencia.fecharegistro = DateTime.Now;
            modIncidencia.descripcion = "Se anuló la preliquidación: " + model.numeropreliquidacion;
            modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
            modIncidencia.activo = true;
            IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetControlDetailsGrid(string control, string id)
        {
            if (control == "usuario")
            {
                ListarUsuariosModel model = new ListarUsuariosModel();
                var departamentos = DataAccess.Seguridad.UsuariosData.GetListarUsuarios(model);
                var listadepartamentos = new SelectList(
                   departamentos,
                   "usr_int_id",
                   "nombrecompleto");
                ViewData["ListaUsuario"] = listadepartamentos;
            }
            if (control == "estacion")
            {
                var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
                var listaestacion = new SelectList(
                   estacion,
                   "idestacion",
                   "estacionorigen");
                ViewData["ListaEstacion"] = listaestacion;
            }
            if (control == "tipodocumento")
            {
                var documento = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.DocumentoAjuste)
                   ).Where(x => x.activo == true).ToList();
                documento.AddRange(DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoComprobante)
                   ).Where(x => x.activo == true).ToList());

                var listadocumento = new SelectList(
                   documento,
                   "idvalortabla",
                   "valor");
                ViewData["ListaDocumento"] = listadocumento;
            }
            return PartialView("_controlgrid", control);
        }

        [HttpPost]
        public ActionResult GridSaveDocumentos(FormCollection formcollection)
        {
            int? id = null;
            int? idnumerodocumento = int.Parse(formcollection["idnumerodocumento"]);
            string serie = formcollection["serie"];

            int tipodocumento = 0;
            if (formcollection["tipodocumento"] != "")
                tipodocumento = int.Parse(formcollection["tipodocumento"]);

            string primernumero = null;
            if (formcollection["primernumero"] != "")
                primernumero = formcollection["primernumero"];

            string ultimonumero = null;
            if (formcollection["ultimonumero"] != "")
                ultimonumero = formcollection["ultimonumero"];

            string estacionorigen = null;
            if (formcollection["estacionorigen"] != "")
                estacionorigen = formcollection["estacionorigen"];

            int? usuario = null;
            if (formcollection["usuario"] != "")
                usuario = Convert.ToInt32(formcollection["usuario"]);

            DocumentoModel model = new DocumentoModel();
            model.idestacion = Convert.ToInt32(estacionorigen);
            model.idnumerodocumento = (idnumerodocumento == 0 ? null : idnumerodocumento);
            model.idusuarioautorizado = (usuario == null ? null : usuario);
            model.primernumero = primernumero.Trim();
            model.serie = serie.Trim();
            if (ultimonumero != null)
                model.ultimonumero = ultimonumero.Trim();

            model.idtipocomprobante = tipodocumento;
            new FacturacionData().InsertarActualizarDocumento(model);

            return Json(new { res = true, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            //return View();
        }



        #region Notas de Credito

        [HttpPost]
        public JsonResult JsonGenerarNotaCredito()
        {
            ComprobanteDetalleModel modDetalle;
            var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];
            if (listado == null)
                return Json(new { res = false, msj = "Requiere de un item como mínimo." });

            string valorigv = ConfigurationManager.AppSettings["igv"].ToString();
            long idcomprobantepago = Convert.ToInt64(Request.Form.GetValues("idcomprobante").FirstOrDefault());

            var __descripcion = Request.Form.GetValues("__descripcion").FirstOrDefault();
            var igv = Request.Form.GetValues("igv").FirstOrDefault();
            var subtotal = Request.Form.GetValues("subtotal").FirstOrDefault();
            var idmotivoanulacion = Request.Form.GetValues("idmotivoanulacion").FirstOrDefault();

            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);
            var model = new FacturacionData().ObtenerComprobante(null, idcomprobantepago);

            
            int cantidadcaracteres = 0;
            var numerocomprobante = new FacturacionData().obtenerNumeroComprobante((Int32)Usuario.Idusuario
                , (Int16) Constantes.TipoComprobante.NotaCredito , usuario.idestacionorigen, null);

            if (numerocomprobante == null)
                return Json(new { res = false, msj = "No existe un número de documento asociado." });



            #region asignarpropiedades

            model.idtipocomprobante = (Int16)DataAccess.Constantes.TipoComprobante.NotaCredito;
            model.idusuarioregistro = (Int32)Usuario.Idusuario;

         

            if (numerocomprobante.ultimonumero == null)
            {
                model.numerocomprobante = numerocomprobante.primernumero.Trim();
                cantidadcaracteres = model.numerocomprobante.Trim().Split('-')[1].Length;
                model.numerocomprobante = model.numerocomprobante.Split('-')[0].ToString() + "-" + (Convert.ToInt32(model.numerocomprobante.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }
            else
            {
                cantidadcaracteres = numerocomprobante.ultimonumero.Trim().Split('-')[1].Length;
                model.numerocomprobante = numerocomprobante.ultimonumero.Split('-')[0].ToString() + "-" + (Convert.ToInt32(numerocomprobante.ultimonumero.Split('-')[1]) + 1).ToString().PadLeft(cantidadcaracteres, '0');
            }

            model.emisionrapida = 0;
            model.fechaemision = DateTime.Now;
            model.descripcion = __descripcion;
            model.subtotal = Convert.ToDecimal(subtotal);
            model.igv = Convert.ToDecimal(igv);
            model.total = model.subtotal + model.igv;
            model._tipoop = 1; // nuevo
            model.idfacturavinculada = idcomprobantepago;
            model.idcomprobantepago = null;
            model.idestado = (Int16) Constantes.EstadoFactura.Emitido;

            #endregion asignarpropiedades

            var idcomprobante = FacturacionData.GenerarComprobante(model);

          
            foreach (var item in listado)
            {
                modDetalle = new ComprobanteDetalleModel();
                modDetalle.idcomprobantepago = idcomprobante;
                modDetalle.igv = (item.subtotal * Convert.ToDecimal(valorigv));
                modDetalle.subtotal = item.subtotal;
                modDetalle.total = item.subtotal + modDetalle.igv;
                modDetalle.descripcion = item.descripcion;
                DataAccess.Facturacion.FacturacionData.GenerarDetalleComprobante(modDetalle);
            }

            return Json(new { res = true, idcomprobante = idcomprobante, valorigv = valorigv }, JsonRequestBehavior.AllowGet);
        }
        #endregion





    }
}