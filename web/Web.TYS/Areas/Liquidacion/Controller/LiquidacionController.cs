using Componentes.Common.Utilidades;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryContracts.TYS.Seguimiento.Results;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Web.Common.Controllers;
using Web.TYS.Areas.Liquidacion.Models;
using Web.TYS.Areas.Monitoreo.Models;
using Web.TYS.Areas.Seguimiento.Models.Seguimiento;
using Web.TYS.DataAccess.Monitoreo;
using ServiceAgents.Common;
using Web.TYS.DataAccess.Liquidacion;
using Web.Common.Extensions;

namespace Web.TYS.Areas.Liquidacion.Controller
{
    public class LiquidacionController : BaseController
    {
        //
        // GET: /Liquidacion/Liquidacion/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OperacionesLiquidacion()
        {
            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var diastranscurridos = LiquidacionData.GetListarDiasTranscurridos();
            var listadiastranscurridos = new SelectList(
           diastranscurridos,
           "DiasTranscurridos"
           , "DiasTranscurridos"
           );
            ViewData["ListaDiasTranscurridos"] = listadiastranscurridos;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            return View();
        }

        public List<ListarUbigeoDto> GetListarUbigeo_Cache()
        {
            var ubigeo = HttpContext.Cache.Get("Ubigeo") as List<ListarUbigeoDto>;
            if (HttpContext.Cache["Ubigeo"] == null)
            {
                ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
                HttpContext.Cache.Insert("Ubigeo", ubigeo, null, DateTime.Now.AddSeconds(1500), Cache.NoSlidingExpiration);
            }
            else
                ubigeo = (List<ListarUbigeoDto>)HttpContext.Cache["Ubigeo"];

            return ubigeo;
        }

        public IEnumerable<ClienteModel> GetListarClientes_Cache()
        {
            var clientes = HttpContext.Cache.Get("Clientes") as IEnumerable<ClienteModel>;
            if (HttpContext.Cache["Clientes"] == null)
            {
                clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
                HttpContext.Cache.Insert("Clientes", clientes, null, DateTime.Now.AddSeconds(1500), Cache.NoSlidingExpiration);
            }
            else
                clientes = (List<ClienteModel>)HttpContext.Cache["Clientes"];

            return clientes;
        }

        public ActionResult ExportarLiquidacion(int? idcliente, int? iddestinatario, string numcp, string grr, string fechainicio, string fechafin, int? diastranscurridos)
        {
            if (fechainicio == string.Empty) fechainicio = null;
            if (fechafin == string.Empty) fechafin = null;
            if (grr == string.Empty) grr = null;
            if (numcp == string.Empty) numcp = null;

            var result = DataAccess.Liquidacion.LiquidacionData.GetListarLiquidacionOperaciones(idcliente, iddestinatario, numcp, grr, fechainicio, fechafin, diastranscurridos);
            var output = Utilidades.Exportar<ListarLiquidacionOperacionesDto>(result);
            return File(output.ToArray(), "application/vnd.ms-excel", "Liquidacion_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
        }

        public JsonResult JsonGetListarLiquidacion(int? idcliente, int? iddestinatario, string numcp, string grr, string fechainicio, string fechafin, int? diastranscurridos,
           string sidx, string sord, int page, int rows)
        {
            if (fechainicio == string.Empty) fechainicio = null;
            if (fechafin == string.Empty) fechafin = null;
            if (grr == string.Empty) grr = null;
            if (numcp == string.Empty) numcp = null;
            //?fechainicio=&fechafin=&idcliente=&iddestino=undefined&iddestinatario=&diastranscurridos=&_search=false&nd=1514563148958&rows=10&page=1&sidx=&sord=asc

            var listadoTotal = DataAccess.Liquidacion.LiquidacionData.GetListarLiquidacionOperaciones(idcliente, iddestinatario, numcp, grr, fechainicio, fechafin, diastranscurridos);

            var resjson1 = (new JqGridExtension<ListarLiquidacionOperacionesDto>()).DataBind(listadoTotal, listadoTotal.Count);
            return resjson1;

            //var listadoTotal = result;
            //int pageindex = page - 1;
            //int pagesize = rows;
            //int totalrecord = listadoTotal.Count();
            //var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            //if (sidx != "" && sord != "")
            //{
            //    sidx = sidx.Split(' ')[0];
            //    if (sord.ToUpper() == "DESC")
            //    {
            //        var parametro = sidx;
            //        var propertyInfo = typeof(ListarLiquidacionOperacionesDto).GetProperty(parametro);
            //        listadoTotal = listadoTotal.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
            //    }
            //    else
            //    {
            //        var parametro = sidx;
            //        var propertyInfo = typeof(ListarLiquidacionOperacionesDto).GetProperty(parametro);
            //        listadoTotal = listadoTotal.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();

            //    }
            //}

            //var jsonData = new
            //{
            //    total = totalpage,
            //    page,
            //    records = totalrecord,
            //    rows = listadoTotal
            //};

            //return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListarArchivos(long? idorden, string sord, int page, int rows)
        {
            Session["idorden"] = idorden;
            var result = DataAccess.Liquidacion.LiquidacionData.GetListarArchivos(idorden, null);

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;
            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listadoTotal
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetListarMonitoreoDetalle(long? idorden, string sidx, string sord, int page, int rows)
        {
            //var result = DataAccess.Monitoreo.MonitoreoData.GetListarDetalleMonitoreoOperaciones(idorden).OrderByDescending(x => x.fechainicio).ToList();
            if (idorden == null) idorden = -1;
            var listado = MonitoreoData.GetListarEventos(null, null, null, idorden);

            var listadoTotal = listado;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sidx != "" && sord != "")
            {
                sidx = sidx.Split(' ')[0];
                if (sord.ToUpper() == "DESC")
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(EventoModel).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(EventoModel).GetProperty(parametro);
                    listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubirArchivo(HttpPostedFileBase archivo, LiquidacionModel model)
        {
            string RutaArchivos = ConfigurationManager.AppSettings["Uploads"].ToString();
            string Carpeta = model.idorden.ToString();
            string Rutagrabar = RutaArchivos + Carpeta + "\\";
            string archivo_subir = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + archivo.FileName).ToLower();
            archivo_subir = archivo_subir.Replace(" ", "_").ToString();

            ArchivoModel mod_archivo = new ArchivoModel();

            var allowedExtensions = new[] { ".pdf", ".jpg", ".docx", ".doc", ".xlsx", ".xls", ".png" };

            var checkextension = Path.GetExtension(archivo.FileName).ToLower();

            if (!allowedExtensions.Contains(checkextension))
            {
                return Json(new { res = false, msj = "No se puede subir archivos con la extensión: " + checkextension }, JsonRequestBehavior.AllowGet);
            }

            mod_archivo.extension = checkextension;
            mod_archivo.nombrearchivo = archivo_subir;
            mod_archivo.rutaacceso = Rutagrabar;
            mod_archivo.idordentrabajo = model.idorden;

            try
            {
                if (Directory.Exists(Rutagrabar))
                    archivo.SaveAs(Rutagrabar + archivo_subir);
                else
                {
                    Directory.CreateDirectory(Rutagrabar);
                    archivo.SaveAs(Rutagrabar + archivo_subir);
                }
                var idarchivo = new DataAccess.Liquidacion.LiquidacionData().InsertarArchivo(mod_archivo);
                return Json(new { res = true, msj = "Se cargó el archivo correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { res = false, msj = "Ocurrió un error al momento de cargar el archivo." });
            }
        }

        public PartialViewResult VerDetalleOT(long idorden)
        {
            LiquidacionModel model = new LiquidacionModel();

            var result = DataAccess.Seguimiento.OrdenData.GetListarGuias(idorden);
            var orden = DataAccess.Seguimiento.OrdenData.GetObtenerOrden(idorden);

            foreach (var guia in result.Hits)
            {
                model.grr = model.grr + "; " + guia.nroguia;
            }

            model.grr = model.grr.Substring(1, model.grr.Length - 1);
            model.numcp = orden.numcp;

            return PartialView("_VerGRR", model);
        }

        public PartialViewResult EditarLiquidacion(long idorden)
        {
            var orden = DataAccess.Seguimiento.OrdenData.GetObtenerOrden_model(idorden);

            LiquidacionModel model = new LiquidacionModel();
            if (orden.fechaentregaconciliacion.HasValue)
            {
                model.fechaentregaconciliacion = orden.fechaentregaconciliacion.Value.Date;
                model.horaentregaconciliacion = orden.fechaentregaconciliacion.Value.Hour.ToString().PadLeft(2, '0') + ":" + orden.fechaentregaconciliacion.Value.Minute.ToString().PadLeft(2, '0');
            }
            model.idordentrabajo = idorden;
            model.archivado = false;

            return PartialView("_EditarLiquidacion", model);
        }

        public PartialViewResult ListarArchivos(long idorden)
        {
            return PartialView("_VerArchivos");
        }

        public PartialViewResult SubirArchivo(long idorden)
        {
            LiquidacionModel model = new LiquidacionModel();
            model.idorden = idorden;
            return PartialView("_UploadFile", model);
        }

        public PartialViewResult JsonAgregarIncidentes(long? idmanifiesto, long? idorden)
        {
            IncidenciaModel model = new IncidenciaModel();
            model.idmanifiesto = idmanifiesto;
            model.idordentrabajo = idorden;
            var estacion = IncidenciaData.GetListarMaestroIncidencia("L").ToList();
            var listaestacion = new SelectList(
               estacion,
               "idmaestroincidencia",
               "descripcion");
            ViewData["ListadoMaestroIncidente"] = listaestacion;

            model.horaincidencia = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechaincidencia = DateTime.Now;

            return PartialView("_AgregarIncidente", model);
        }

        public FileResult DownloadArchivo(long idarchivo)
        {
            //var listaArchivos = (List<ListarArchivosIncidenciaDto>)Session["Archivos"];
            string filePath = ConfigurationManager.AppSettings["Uploads"];
            long idorden = Convert.ToInt64(Session["idorden"]);
            var result = DataAccess.Liquidacion.LiquidacionData.GetListarArchivos(idorden, null);

            var archivo = result.Where(x => x.idarchivo.Equals(idarchivo)).SingleOrDefault();

            string filename = Server.HtmlEncode(archivo.nombrearchivo);
            filePath = archivo.rutaacceso + "//";
            string contentType = System.Net.Mime.MediaTypeNames.Application.Octet;

            return File(Path.Combine(filePath, filename), contentType, filename);
        }

        public JsonResult JsonEditarLiquidacion(DateTime? fechaentrega, string horaentrega, bool? archivado, long? idorden)
        {
            LiquidacionModel model = new LiquidacionModel();

            var hm = horaentrega.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
            model.fechaentregaconciliacion = fechaentrega.Value.Date + ts;
            model.archivado = archivado.Value;
            model.idusuarioconciliacion = Convert.ToInt32(Usuario.Idusuario);

            InsertarEtapaParameterDto param;
            InsertarEtapaParameter parameters = new InsertarEtapaParameter();
            parameters.Hits = new List<InsertarEtapaParameterDto>();

            if (archivado.Value)
            {
                model.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteFacturacion;

                param = new InsertarEtapaParameterDto();
                param.descripcion = "";
                param.documento = "";
                param.fechaetapa = model.fechaentregaconciliacion.Value;
                param.fecharegistro = DateTime.Now;
                param.idordentrabajo = Convert.ToInt64(idorden);
                param.idusuarioregistro = (Int32)Usuario.Idusuario;
                param.recurso = "";
                param.visible = true;
                param.idmaestroetapa = 21;
                parameters.Hits.Add(param);
                parameters.Execute();
            }
            else model.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteRetornoDocumentario;
            model.idordentrabajo = idorden.Value;

            DataAccess.Liquidacion.LiquidacionData.ActualizarLiquidacion(model);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonValidarFecha(DateTime fechaentrega, string horaentrega, long idorden)
        {
            var orden = DataAccess.Seguimiento.OrdenData.GetObtenerOrden_model(idorden);
            var hour = orden.fechaentrega.Value.Hour;
            var minute = orden.fechaentrega.Value.Minute;
            var fecha = orden.fechaentrega.Value.ToShortDateString();
            var fechaconciliacion = fechaentrega.Date.ToShortDateString();
            var hourconciliacion = Convert.ToInt16(horaentrega.Split(':')[0]);
            var minuteconciliacion = Convert.ToInt16(horaentrega.Split(':')[1]);

            if (fecha != fechaconciliacion || hour != hourconciliacion || minute != minuteconciliacion)
            {
                return Json(new { res = false, msj = "La fecha o la hora no corresponden." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { res = true });
        }

        [HttpPost]
        public JsonResult AgregarIncidente(IncidenciaModel model)
        {
            model.fecharegistro = DateTime.Now;
            model.idusuarioregistro = Convert.ToInt32(Usuario.Idusuario);

            if (model.horaincidencia != null)
            {
                var hm = model.horaincidencia.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
                model.fechaincidencia = model.fechaincidencia.Date + ts;
            }
            else model.fechaincidencia = DateTime.Now;
            model.idusuarioregistro = Convert.ToInt32(Usuario.Idusuario);
            model.fecharegistro = DateTime.Now;

            DataAccess.Liquidacion.LiquidacionData.InsertarIncidencia(model);

            var jsonres = new
            {
                res = true,
                msj = "Se registró con éxito.",
                titulo = "Registro exitoso  "
            };

            return Json(jsonres, JsonRequestBehavior.AllowGet);
        }
    }
}