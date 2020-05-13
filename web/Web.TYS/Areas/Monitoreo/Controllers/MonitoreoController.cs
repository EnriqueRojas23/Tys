using QueryContracts.TYS.Monitoreo.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Common.Controllers;
using Web.Common.Extensions;
using Web.TYS.Areas.Monitoreo.Models;
using Web.TYS.Areas.Seguimiento.Models.Seguimiento;
using Web.TYS.DataAccess;
using Web.TYS.DataAccess.Monitoreo;
using Web.TYS.DataAccess.Seguimiento;

namespace Web.TYS.Areas.Monitoreo.Controllers
{
    public class MonitoreoController : BaseController
    {
        //
        // GET: /Monitoreo/Default1/

        #region operacionesmonitoreo

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OperacionesMonitoreo()
        {
            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados((Int32)Constantes.MaestroTablas.Preliquidacion);
            var listaestados = new SelectList(
                estados,
                "idestado"
                , "estado"
                );
            ViewData["ListaEstados"] = listaestados;

            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            MonitoreoModel model = new MonitoreoModel();
            if (Session["Busqueda"] != null)
            {
                var busqueda = (MonitoreoModel)Session["Busqueda"];
                model.numcp = busqueda.numcp;
                model.idcliente = busqueda.idcliente;
                model.iddestino = busqueda.iddestino;
                model.nummanifiesto = busqueda.nummanifiesto;
                model.numhojaruta = busqueda.numhojaruta;
                model.grr = busqueda.grr;
                model.documento = busqueda.documento;
                model.tienda = busqueda.tienda;
            }

            //if (usuario.idestacionorigen != null)
            //    model.idestacion = usuario.idestacionorigen.Value;

            return View(model);
        }

        public JsonResult JsonListarOtsMonitoreo(int? idcliente, int? iddestino, string numcp
            , string nummanifiesto, string numhojaruta, string grr, string documento, string tienda, int? default_)
        {
            var busqueda = new MonitoreoModel();
            if (default_ != 1)
            {
                busqueda.numcp = numcp;
                busqueda.idcliente = idcliente;
                busqueda.iddestino = iddestino;
                busqueda.nummanifiesto = nummanifiesto;
                busqueda.numhojaruta = numhojaruta;
                busqueda.grr = grr;
                busqueda.documento = documento;
                busqueda.tienda = tienda;

                Session["Busqueda"] = busqueda;
            }
            else
            {
                busqueda = (MonitoreoModel)Session["Busqueda"];
                numcp = busqueda.numcp;
                idcliente = busqueda.idcliente;
                iddestino = busqueda.iddestino;
                nummanifiesto = busqueda.nummanifiesto;
                numhojaruta = busqueda.numhojaruta;
                grr = busqueda.grr;
                documento = busqueda.documento;
                tienda = busqueda.tienda;
            }

            if (numcp == "") numcp = null;
            if (grr == "") grr = null;
            if (numhojaruta == "") numhojaruta = null;
            if (nummanifiesto == "") nummanifiesto = null;
            if (documento == "") documento = null;
            if (tienda == "") tienda = null;

            var result = DataAccess.Monitoreo.MonitoreoData.OtsMonitoreo(idcliente, iddestino, numcp, nummanifiesto, numhojaruta, grr, documento, tienda);

            var ordenes = from p in result.GroupBy(x => x.distritodestino)
                          select new
                          {
                              text = p.First().distritodestino,
                              data = new
                              {
                                  idordentrabajo = "",
                                  fechadespacho = "",
                                  bultos = p.Select(x => x.bulto).Sum().ToString(),
                                  peso = p.Select(x => x.peso).Sum().ToString(),
                                  volumen = p.Select(x => x.volumen).Sum().ToString(),
                                  origen = p.First().origen,
                                  tienda = "",
                                  modalidad = "",
                                  grr = "",
                                  remitente = "",
                                  destinatario = "",
                                  direcciondestino = "",
                                  tipooperacion = "",
                                  ultimaetapa = "",
                                  ultimorecurso = "",
                                  ultimodocumento = "",
                                  incidencia = "",
                                  reintegro = "",
                                  estacionorigen = "",
                                  spanclass = "first"
                              },
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             data = new
                                             {
                                                 idordentrabajo = "",
                                                 fechadespacho = "",
                                                 bultos = a.Select(x => x.bulto).Sum().ToString(),
                                                 peso = a.Select(x => x.peso).Sum().ToString(),
                                                 volumen = a.Select(x => x.volumen).Sum().ToString(),
                                                 origen = a.Select(x => x.origen).First(),
                                                 modalidad = "",
                                                 tienda = "",
                                                 grr = "",
                                                 remitente = "",
                                                 destinatario = "",
                                                 direcciondestino = "",
                                                 tipooperacion = a.Select(x => x.tipooperacion).First(),
                                                 ultimaetapa = "",
                                                 ultimorecurso = "",
                                                 ultimodocumento = "",
                                                 incidencia = "",
                                                 reintegro = "",
                                                 estacionorigen = "",
                                                 spanclass = "root"
                                             },
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                fechadespacho = b.fechadespacho.ToShortDateString(),
                                                                bultos = b.bulto.ToString(),
                                                                peso = b.peso.ToString(),
                                                                volumen = b.volumen.ToString(),
                                                                origen = b.origen,
                                                                modalidad = b.tipotransporte,
                                                                tienda = b.tienda,
                                                                grr = b.grr,
                                                                remitente = b.remitente,
                                                                destinatario = b.destinatario,
                                                                direcciondestino = b.direcciondestino,
                                                                tipooperacion = b.tipooperacion,
                                                                ultimaetapa = b.ultimaetapa,
                                                                ultimorecurso = b.ultimorecurso,
                                                                ultimodocumento = b.ultimodocumento,
                                                                incidencia = b.Incidencia,
                                                                reintegro = b.reintegro,
                                                                estacionorigen = b.estacionorigen,
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };
            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        #endregion operacionesmonitoreo

        #region ConfirmarRecibo

        public PartialViewResult ConfirmarRecibo()
        {
            var maestroetapas = DataAccess.Monitoreo.MonitoreoData.GetListarMaestraEtapa("G");
            var listamaestroetapas = new SelectList(
                maestroetapas,
                "idmaestroetapa"
                , "descripcion"
                );
            ViewData["ListaMaestroEtapas"] = listamaestroetapas;
            EtapaModel model = new EtapaModel();
            model.idmaestroetapa = 4;
            model.horaetapa = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechaetapa = DateTime.Now;

            return PartialView("_ConfirmarRecibo", model);
        }

        #endregion ConfirmarRecibo

        #region EntregaCliente

        public JsonResult JsonGetListarGuias(string sidx, string sord, int page, int rows)
        {
            var guias = (List<IncidenciaGuiasRechazadas>)Session["guiasrechazadas"];
            var listadoTotal = guias;
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
                    var propertyInfo = typeof(OrdenTrabajoModel).GetProperty(parametro);
                    listadoTotal = listadoTotal.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(OrdenTrabajoModel).GetProperty(parametro);
                    listadoTotal = listadoTotal.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listadoTotal
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EntregaCliente(string items)
        {
            EtapaModel model = new EtapaModel();
            Session["guiasrechazadas"] = null;
            if (items == "")
                model.regresar = false;
            else
                model.regresar = true;

            if (items != null)
            {
                model.idsorden = items;

                if (model.idsorden.Split(',').Length == 1)
                {
                    var orden = OrdenData.GetObtenerOrden_model(Convert.ToInt64(model.idsorden.Split(',')[0]));
                    model.numcp_aux = orden.numcp;
                }
            }

            var mincidentes = DataAccess.Monitoreo.MonitoreoData.GetListarMaestraEtapa("E").ToList();
            var listamincidentes = new SelectList(
               mincidentes,
               "idmaestroetapa",
               "descripcion");
            ViewData["ListadoMaestroEtapa"] = listamincidentes;

            model.horaetapa = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechaetapa = DateTime.Now;

            return View(model);
        }

        [HttpPost]
        public JsonResult JsonBuscarOt(string numcp)
        {
            var orden = MonitoreoData.obtenerOt(numcp);
            if (orden.Count() == 0)
            {
                return Json(new { res = false }, JsonRequestBehavior.AllowGet);
            }
            var JsonOrden = new
            {
                nummanifiesto = orden.First().nummanifiesto,
                destinatario = orden.First().destinatario,
                idordentrabajo = orden.First().idordentrabajo
            };
            return Json(new { res = true, obj = JsonOrden }, JsonRequestBehavior.AllowGet);
        }

        #endregion EntregaCliente

        #region Etapas

        public PartialViewResult AgregarEtapa()
        {
            EtapaModel model = new EtapaModel();
            Session["guiasrechazadas"] = null;

            var maestroetapas = DataAccess.Monitoreo.MonitoreoData.GetListarMaestraEtapa("G");
            var listamaestroetapas = new SelectList(
                maestroetapas,
                "idmaestroetapa"
                , "descripcion"
                );
            ViewData["ListaMaestroEtapas"] = listamaestroetapas;

            model.horaetapa = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechaetapa = DateTime.Now;

            return PartialView("_AgregarEtapa", model);
        }

        #endregion Etapas

        [HttpPost]
        public JsonResult AgregarEtapa(EtapaModel model)
        {
            model.fecharegistro = DateTime.Now;
            model.idusuarioregistro = (Int32)Usuario.Idusuario;
            model.visible = true;
            var hm = model.horaetapa.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
            model.fechaetapa = model.fechaetapa.Value.Date + ts;

            var maestroetapas = DataAccess.Monitoreo.MonitoreoData.GetListarMaestraEtapa(null);
            var etapa = maestroetapas.Where(x => x.idmaestroetapa.Equals(model.idmaestroetapa)).Single();


            model.tipoetapa = etapa.tipoetapa;

            if (model.idmaestroetapa == 11)
            {
                var guiasaux = (List<IncidenciaGuiasRechazadas>)Session["guiasrechazadas"];
                if (guiasaux == null)
                    return Json(new { res = false, msj = "Debe ingresar al menos una Guía rechazada." }, JsonRequestBehavior.AllowGet);
                else if (guiasaux.Count == 0)
                    return Json(new { res = false, msj = "Debe ingresar al menos una Guía rechazada." }, JsonRequestBehavior.AllowGet);
            }

            var guias = (List<IncidenciaGuiasRechazadas>)Session["guiasrechazadas"];

            
            var idetapa = DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(model, guias);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        #region Incidentes

        public JsonResult JsonAnularIncidencia(long id)
        {
            var res = IncidenciaData.AnularIncidencia(id);
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult JsonTimeLine(string ids)
        {
            IncidenciaModel incidencia;
            List<IncidenciaModel> incidencias = new List<IncidenciaModel>();
            var result = IncidenciaData.ListarIncidenciasxOT(Convert.ToInt64(ids));
            foreach (var item in result)
            {
                incidencia = new IncidenciaModel();
                incidencia.codincidente = item.codincidencia + " - " + item.incidencia;
                incidencia.descripcion = item.descripcion;
                incidencia.numcp = item.numcp;
                incidencia.fechaincidencia = item.fechainicio;
                incidencia.fecharegistro = item.fecharegistro;
                incidencia.horaincidencia = item.fechainicio.ToShortTimeString();
                incidencia._fechaincidencia = item.fechainicio.ToShortDateString();
                incidencia.nombreusuario = item.usuario;

                incidencias.Add(incidencia);
            }

            TimeLine timeline = new TimeLine();
            timeline.Incidencias = incidencias;

            return PartialView("_TimeLine", timeline);
        }

        public JsonResult JsonValidarEtapa(int idmaestroincidencia)
        {
            var estacion = IncidenciaData.GetListarMaestroIncidencia(null).ToList();
            return Json(new { tipo = estacion[0].tipo }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult AgregarIncidentes(long? idmanifiesto, long? idorden)
        {
            IncidenciaModel model = new IncidenciaModel();
            model.idmanifiesto = idmanifiesto;
            model.idordentrabajo = idorden;
            var estacion = IncidenciaData.GetListarMaestroIncidencia("M").ToList();
            var listaestacion = new SelectList(
               estacion,
               "idmaestroincidencia",
               "descripcion");
            ViewData["ListadoMaestroIncidente"] = listaestacion;

            model.horaincidencia = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechaincidencia = DateTime.Now;

            return PartialView("_AgregarIncidente", model);
        }

        public PartialViewResult VerDetalleOT(long idorden)
        {
            Areas.Seguimiento.Models.Seguimiento.OrdenTrabajoModel model = new Seguimiento.Models.Seguimiento.OrdenTrabajoModel();
            var orden = OrdenData.GetObtenerOrden(idorden);
            model.numcp = orden.numcp;
            model.cepan = orden.cepan;
            model.chofer = orden.chofer;
            model.pesogeneral = orden.peso;
            model.volgeneral = orden.volumen;
            model.bultogeneral = Convert.ToInt32(orden.bulto);

            return PartialView("_VerDetalle", model);
        }

        [HttpPost]
        public JsonResult AgregarIncidente(IncidenciaModel model)
        {
            bool resultado = false;
            string mensaje = "";
            string titulo = "";
            String[] ordenes = model.idsorden.Split(',');
            var ordenseguimiento = new OrdenTrabajoSeguimiento();

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

            var ModelMaestroIncidencia = IncidenciaData.GetListarMaestroIncidencia(null, model.idmaestroincidencia)
                    .FirstOrDefault();

            if (ModelMaestroIncidencia.seactualiza)
            {
                foreach (var item in ordenes)
                {
                    var incidencia = DataAccess.Monitoreo.
                        IncidenciaData.ListarIncidenciasxOT(Convert.ToInt64(item), model.idmaestroincidencia).FirstOrDefault();
                    
                    model.idordentrabajo = Convert.ToInt64(item);
                    if (incidencia != null)
                        model.idincidencia = incidencia.idincidencia;
                    var resp = IncidenciaData.InsertarActualizarIncidenciaIndividual(model);
                    
                    ordenseguimiento.idordentrabajo = Convert.ToInt64(item);
                   if((Int32)Constantes.SeguimientoFluvial.FechaArribo == model.idmaestroincidencia)
                       ordenseguimiento.fechaarribo = model.fechaincidencia;

                   if ((Int32)Constantes.SeguimientoFluvial.FechaConocimientoEmbarque == model.idmaestroincidencia)
                       ordenseguimiento.fechaconocimientoembarque = model.fechaincidencia;

                   if ((Int32)Constantes.SeguimientoFluvial.FechaDesembarque == model.idmaestroincidencia)
                       ordenseguimiento.fechadesembarque = model.fechaincidencia;

                   if ((Int32)Constantes.SeguimientoFluvial.FechaEmbarque== model.idmaestroincidencia)
                       ordenseguimiento.fechaembarque = model.fechaincidencia;

                   if ((Int32)Constantes.SeguimientoFluvial.FechaLlegadaAlmacén== model.idmaestroincidencia)
                       ordenseguimiento.fechallegadaalmacen = model.fechaincidencia;

                   if ((Int32)Constantes.SeguimientoFluvial.FechaSalidaPuerto == model.idmaestroincidencia)
                       ordenseguimiento.fechasalidadepuerto = model.fechaincidencia;

                   if ((Int32)Constantes.SeguimientoFluvial.LlegadaPuerto== model.idmaestroincidencia)
                       ordenseguimiento.fechallegadapuerto = model.fechaincidencia;

                   if ((Int32)Constantes.SeguimientoFluvial.NumeroConocimientoEmbarque == model.idmaestroincidencia)
                       ordenseguimiento.numeroconocimiento = model.descripcion;


                    OrdenData.InsertarActualizarOrdenTransporteSeguimiento(ordenseguimiento);

                    resultado = true;
                    mensaje = "La incidencia se registó con exito";
                    titulo = "Se registró con exito";

                }
            }
            else
            {
                var resp = IncidenciaData.InsertarActualizarIncidencia(model);
                resultado = true;
                mensaje = "La incidencia se registó con exito";
                titulo = "Se registró con exito";
            }
            var jsonres = new
            {
                res = resultado,
                msj = mensaje,
                titulo = titulo
            };

            return Json(jsonres, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonEliminarGuia(long idguia)
        {
            var guias = (List<IncidenciaGuiasRechazadas>)Session["guiasrechazadas"];
            var guia = guias.Where(x => x.idguia.Equals(idguia)).Single();
            guias.Remove(guia);
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAgregarGuia(string numeroguia, int cantidad, string idorden)
        {
            var existe = OrdenData.ExisteGuia(numeroguia, null);

            if (idorden.Split(',').Length > 1)
            {
                return Json(new { res = false, msj = "Esta operación es para una sola OT." }, JsonRequestBehavior.AllowGet);
            }
            if (existe == null)
            {
                return Json(new { res = false, msj = "No existe." }, JsonRequestBehavior.AllowGet);
            }

            var guias = (List<IncidenciaGuiasRechazadas>)Session["guiasrechazadas"];
            if (guias == null)
                guias = new List<IncidenciaGuiasRechazadas>();
            else
            {
                if (guias.Where(x => x.idguia.Equals(existe.idguiaremisioncliente)).SingleOrDefault() != null)
                {
                    return Json(new { res = false, msj = "Ya ha sido ingresada." }, JsonRequestBehavior.AllowGet);
                }
            }

            guias.Add(new IncidenciaGuiasRechazadas
            {
                idguia = existe.idguiaremisioncliente,
                cantidad = cantidad,
                guia = numeroguia + "-" + cantidad,
                idordentrabajo = Convert.ToInt64(idorden)
            });

            var total_cantidad = guias.Select(x => x.cantidad).Sum();
            var orden = OrdenData.GetObtenerOrden_model(guias[0].idordentrabajo);
            if (orden.bulto < total_cantidad)
            {
                return Json(new { res = false, msj = "La cantidad de bultos no puede ser mayor al de la OT." }, JsonRequestBehavior.AllowGet);
            }

            var listaclases = new SelectList(guias, "idguia", "guia");
            Session["guiasrechazadas"] = guias;

            var res = new
            {
                data = listaclases,
                res = true
            };

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAutocomplete(int idcampo)
        {
            var helps = DataAccess.Monitoreo.MonitoreoData.ListarHelpResources(idcampo);
            List<string> helpsingle = new List<string>();
            foreach (var item in helps)
                helpsingle.Add(item.help);
            return Json(helpsingle, JsonRequestBehavior.AllowGet);
        }

        #endregion Incidentes

        #region fluvial

        public JsonResult JsonListarOtsFluvial(int? idcliente
     , int? iddestino
     , int? idestado
     , string numcp
     , string nummanifiesto
     , string numhojaruta
     , string documento)
        {
            if (numcp == "") numcp = null;
            var result = DataAccess.Monitoreo.MonitoreoData.OtsFluvial(idcliente, iddestino, idestado, numcp, nummanifiesto, numhojaruta, documento);

            var detalles = from p in result
                           select new
                           {
                               text = p.numcp,
                               data = new
                               {
                                   price = p.bulto.ToString(),
                                   size = p.destinatario.ToString(),
                                   spanclass = "root"
                               }
                           };

            var ordenes = from p in result.GroupBy(x => x.transporte)
                          select new
                          {
                              text = p.First().transporte,
                              data = new
                              {
                                  idordentrabajo = "",
                                  fechadespacho = "",
                                  bultos = p.Select(x => x.bulto).Sum().ToString(),
                                  peso = p.Select(x => x.peso).Sum().ToString(),
                                  volumen = p.Select(x => x.volumen).Sum().ToString(),
                                  origen = p.First().origen,
                                  tienda = "",
                                  modalidad = "",
                                  grr = "",
                                  remitente = "",
                                  destinatario = "",
                                  direcciondestino = "",
                                  tipooperacion = "",
                                  ultimaetapa = "",
                                  ultimorecurso = "",
                                  ultimodocumento = "",
                                  spanclass = "first"
                              },
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             data = new
                                             {
                                                 idordentrabajo = "",
                                                 fechadespacho = a.First().fechadespacho.ToShortDateString(),
                                                 bultos = a.Select(x => x.bulto).Sum().ToString(), // .bulto.ToString(),
                                                 peso = a.Select(x => x.peso).Sum().ToString(),
                                                 volumen = a.Select(x => x.volumen).Sum().ToString(),
                                                 origen = "",
                                                 tienda = "",
                                                 modalidad = "",
                                                 grr = "",
                                                 remitente = "",
                                                 destinatario = "",
                                                 direcciondestino = "",
                                                 tipooperacion = "",
                                                 ultimaetapa = "",
                                                 ultimorecurso = "",
                                                 ultimodocumento = "",
                                                 spanclass = "root"
                                             },
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                fechadespacho = b.fechadespacho.ToShortDateString(),
                                                                bultos = b.bulto.ToString(),
                                                                peso = b.peso.ToString(),
                                                                volumen = b.volumen.ToString(),
                                                                origen = b.origen,
                                                                tienda = b.tienda,
                                                                modalidad = b.tipotransporte,
                                                                grr = b.grr,
                                                                remitente = b.remitente,
                                                                destinatario = b.destinatario,
                                                                direcciondestino = b.direcciondestino,
                                                                tipooperacion = b.tipooperacion,
                                                                ultimaetapa = b.ultimaetapa,
                                                                ultimorecurso = b.ultimorecurso,
                                                                ultimodocumento = b.ultimodocumento,
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };

            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GestionarEmbarqueFluvial()
        {
            var vehiculofluvial = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.VehiculoFluvial));
            var listavehiculofluvial = new SelectList(
               vehiculofluvial,
               "idvalortabla",
               "valor");
            ViewData["ListaVehiculoFluvial"] = listavehiculofluvial;

            return View();
        }

        public JsonResult JsonGetListarDetalleEmbarqueFluvial(long? idembarque, string sidx, string sord, int page, int rows)
        {
            var listado = MonitoreoData.GetListarDetalleEmbarqueFluvial(idembarque.Value);

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
                    var propertyInfo = typeof(EmbarqueModel).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(EmbarqueModel).GetProperty(parametro);
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
        }

        public JsonResult JsonGetListarEmbarqueFluvial(string numeroembarque, int? idtransporte, string sidx, string sord, int page, int rows)
        {
            if (numeroembarque == string.Empty) numeroembarque = null;

            var listado = MonitoreoData.GetListarEmbarqueFluvial(numeroembarque, idtransporte);

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
                    var propertyInfo = typeof(EmbarqueModel).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(EmbarqueModel).GetProperty(parametro);
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
        }

        public PartialViewResult AgregarEmbarqueFluvial()
        {
            EmbarqueModel model = new EmbarqueModel();

            model.horainiciocarga = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechainiciocarga = DateTime.Now;

            var vehiculofluvial = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.VehiculoFluvial));
            var listavehiculofluvial = new SelectList(
               vehiculofluvial,
               "idvalortabla",
               "valor");
            ViewData["ListaVehiculoFluvial"] = listavehiculofluvial;

            var puerto = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Puerto));
            var listapuerto = new SelectList(
               puerto,
               "idvalortabla",
               "valor");
            ViewData["ListaPuerto"] = listapuerto;

            return PartialView("_AgregarEmbarqueFluvial", model);
        }

        public PartialViewResult EditarEmbarqueFluvial(long idembarque)
        {
            var model = DataAccess.Monitoreo.MonitoreoData.obtenerEmbarque(idembarque);

            model.horainiciocarga = model.fechainiciocarga.Hour.ToString().PadLeft(2, '0') + ":" + model.fechainiciocarga.Minute.ToString().PadLeft(2, '0');

            var vehiculofluvial = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.VehiculoFluvial));
            var listavehiculofluvial = new SelectList(
               vehiculofluvial,
               "idvalortabla",
               "valor");
            ViewData["ListaVehiculoFluvial"] = listavehiculofluvial;

            var puerto = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Puerto));
            var listapuerto = new SelectList(
               puerto,
               "idvalortabla",
               "valor");
            ViewData["ListaPuerto"] = listapuerto;

            return PartialView("_EditarEmbarqueFluvial", model);
        }

        public PartialViewResult SeguimientoEmbarqueFluvial(long idembarque)
        {
            var model = DataAccess.Monitoreo.MonitoreoData.obtenerEmbarque(idembarque);

            if (model.fechafincarga.HasValue)
                model.horafincarga = model.fechafincarga.Value.Hour.ToString().PadLeft(2, '0') + ":" + model.fechafincarga.Value.Minute.ToString().PadLeft(2, '0');
            if (model.fechazarpe.HasValue)
                model.horazarpe = model.fechazarpe.Value.Hour.ToString().PadLeft(2, '0') + ":" + model.fechazarpe.Value.Minute.ToString().PadLeft(2, '0');
            if (model.fechallegada.HasValue)
                model.horallegada = model.fechallegada.Value.Hour.ToString().PadLeft(2, '0') + ":" + model.fechallegada.Value.Minute.ToString().PadLeft(2, '0');
            if (model.fechaatraque.HasValue)
                model.horaatraque = model.fechaatraque.Value.Hour.ToString().PadLeft(2, '0') + ":" + model.fechaatraque.Value.Minute.ToString().PadLeft(2, '0');

            return PartialView("_SeguimientoEmbarque", model);
        }

        [HttpPost]
        public JsonResult GuardarSeguimientoEmbarqueFluvial(EmbarqueModel model)
        {
            EtapaModel modEtapa = new EtapaModel();

            var au = DataAccess.Monitoreo.MonitoreoData.obtenerEmbarque(model.idembarque.Value);
            var maestroetapas = DataAccess.Monitoreo.MonitoreoData.GetListarMaestraEtapa(null);
            var otsasignadas = MonitoreoData.GetListarAsignadosManifiesto(model.idembarque.Value);
            foreach (var item in otsasignadas)
            {
                modEtapa.idsorden = modEtapa.idsorden + ',' + item.idordentrabajo;
            }
            modEtapa.idsorden = modEtapa.idsorden.Substring(1, modEtapa.idsorden.Length - 1);
            modEtapa.fecharegistro = DateTime.Now;
            modEtapa.idusuarioregistro = (Int32)Usuario.Idusuario;
            modEtapa.visible = true;

            if (model.fechaatraque.HasValue)
            {
                var hm = model.horaatraque.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
                model.fechaatraque = model.fechaatraque.Value.Date + ts;

                if (au.fechaatraque != model.fechaatraque)
                {
                    var etapa = maestroetapas.Where(x => x.idmaestroetapa.Equals((Int32)Constantes.MaestroEtapas.FechaAtraque)).Single();
                    modEtapa.tipoetapa = etapa.tipoetapa;

                    au.fechaatraque = model.fechaatraque;
                    modEtapa.fechaetapa = au.fechaatraque.Value;
                    modEtapa.idmaestroetapa = (Int32)Constantes.MaestroEtapas.FechaAtraque;
                    DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modEtapa, null);
                }
            }
            if (model.fechafincarga.HasValue)
            {
                var hm = model.horafincarga.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
                model.fechafincarga = model.fechafincarga.Value.Date + ts;
                if (au.fechafincarga != model.fechafincarga)
                {
                    var etapa = maestroetapas.Where(x => x.idmaestroetapa.Equals((Int32)Constantes.MaestroEtapas.FechaFinCarga)).Single();
                    modEtapa.tipoetapa = etapa.tipoetapa;

                    au.fechafincarga = model.fechafincarga;
                    modEtapa.fechaetapa = au.fechafincarga.Value;
                    modEtapa.idmaestroetapa = (Int32)Constantes.MaestroEtapas.FechaFinCarga;
                    DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modEtapa, null);
                }
            }
            if (model.fechallegada.HasValue)
            {
                var hm = model.horallegada.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
                model.fechallegada = model.fechallegada.Value.Date + ts;

                if (au.fechallegada != model.fechallegada)
                {
                    var etapa = maestroetapas.Where(x => x.idmaestroetapa.Equals((Int32)Constantes.MaestroEtapas.FechaLlegadaPuerto)).Single();
                    modEtapa.tipoetapa = etapa.tipoetapa;

                    au.fechallegada = model.fechallegada;
                    modEtapa.fechaetapa = au.fechallegada.Value;
                    modEtapa.idmaestroetapa = (Int32)Constantes.MaestroEtapas.FechaLlegadaPuerto;
                    DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modEtapa, null);
                }
            }
            if (model.fechazarpe.HasValue)
            {
                var hm = model.horazarpe.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
                model.fechazarpe = model.fechazarpe.Value.Date + ts;
                if (au.fechazarpe != model.fechazarpe)
                {
                    var etapa = maestroetapas.Where(x => x.idmaestroetapa.Equals((Int32)Constantes.MaestroEtapas.FechaZarpe)).Single();
                    modEtapa.tipoetapa = etapa.tipoetapa;

                    au.fechazarpe = model.fechazarpe;
                    modEtapa.fechaetapa = au.fechazarpe.Value;
                    modEtapa.idmaestroetapa = (Int32)Constantes.MaestroEtapas.FechaZarpe;
                    DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modEtapa, null);
                }
            }

            var result = new MonitoreoData().InsertarActualizarEmbarqueFluvial(au);
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarEmbarqueFluvial(EmbarqueModel model)
        {
            //if (ModelState.IsValid)
            //{
            //}
            if (model.idembarque == null)
            {
                model.fecharegistro = DateTime.Now;
                model.idusuarioregistro = (Int32)Usuario.Idusuario;
                model.numeroembarque = MonitoreoData.ObtenerUltimoNumeroEmbarque();
            }

            var hm = model.horainiciocarga.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
            model.fechainiciocarga = model.fechainiciocarga.Date + ts;

            //Validar duplicidad de CE
            if (MonitoreoData.ExisteCE(model.conocimientoembarque, model.idtransporte, model.idembarque))
            {
                ModelState.AddModelError("", "No se genero el comprobante");
                return Json(new { res = false, msj = "El Conocimiento de embarque ya existe." }, JsonRequestBehavior.AllowGet);
            }

            var result = new MonitoreoData().InsertarActualizarEmbarqueFluvial(model);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult AsignarManifiesto(long idembarque)
        {
            Session["SinEmbarque"] = null;
            Session["ConEmbarque"] = null;

            var model = MonitoreoData.obtenerEmbarque(idembarque);

            return PartialView("_AsignarManifiestos", model);
        }

        public JsonResult JsonValidarAsociados(long idembarque)
        {
            var asignados = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(idembarque);
            if (asignados.Count > 0)
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            else return Json(new { res = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //public JsonResult AsignarManifiesto(EmbarqueModel model)
        public JsonResult JsonAsignarManifiesto(long idembarque)
        {
            EmbarqueModel model = new EmbarqueModel();
            List<long> desasignar = new List<long>();
            model.idembarque = idembarque;
            model.idusuarioregistro = (Int32)Usuario.Idusuario;

            var ConEmbarque = (List<ManifiestoModel>)Session["ConEmbarque"];

            var quitar = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(model.idembarque.Value);

            foreach (var item in quitar)
            {
                var aux = ConEmbarque.Where(x => x.idordentrabajo.Equals(item.idordentrabajo)).SingleOrDefault();
                if (aux == null)
                {
                    //desasignar ot
                    MonitoreoData.EliminarDetalleEmbarque(item.idordentrabajo);
                    //Agregar seguimiento
                    EtapaModel modEtapa = new EtapaModel();
                    modEtapa.fecharegistro = DateTime.Now;
                    modEtapa.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modEtapa.visible = true;
                    modEtapa.fechaetapa = DateTime.Now;
                    modEtapa.idmaestroetapa = (Int32)Constantes.MaestroEtapas.DesasignacionEmbarque;
                    modEtapa.tipoetapa = "M";
                    modEtapa.idsorden = item.idordentrabajo.ToString();
                    var idetapa = DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modEtapa, null);
                }
                else ConEmbarque.Remove(aux);
            }

            if (ConEmbarque.Count == 0)
            {
                return Json(new { res = false, msj = "Ud. no ha asignado ningún manifiesto para este embarque" }, JsonRequestBehavior.AllowGet);
            }
            var embarque = MonitoreoData.obtenerEmbarque(model.idembarque.Value);
            model.conocimientoembarque = embarque.conocimientoembarque;

            MonitoreoData.InsertarDetalleEmbarque(model, ConEmbarque);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListarManifiestosEmbarque(long idembarque)
        {
            var ConEmbarque = (List<ManifiestoModel>)Session["ConEmbarque"];

            if (ConEmbarque == null)
            {
                ConEmbarque = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(idembarque).Where(x => x.fechadescarga == null).ToList();
                Session["ConEmbarque"] = ConEmbarque;
            }

            var ordenes = from p in ConEmbarque.GroupBy(x => x.distrito)
                          select new
                          {
                              text = p.First().distrito,
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };

            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListarManifiestosSinEmbarque(long idembarque)
        {
            var SinEmbarque = (List<ManifiestoModel>)Session["SinEmbarque"];

            if (SinEmbarque == null)
            {
                SinEmbarque = DataAccess.Monitoreo.MonitoreoData.GetListarManifiesto(idembarque);
                Session["SinEmbarque"] = SinEmbarque;
            }

            var ordenes = from p in SinEmbarque.OrderBy(x => x.distrito).GroupBy(x => x.distrito)
                          select new
                          {
                              text = p.First().distrito,
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };

            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonQuitarManifiesto(string ids)
        {
            string[] ordenes = ids.Split(',');

            var SinEmbarque = (List<ManifiestoModel>)Session["SinEmbarque"];
            var ConEmbarque = (List<ManifiestoModel>)Session["ConEmbarque"];

            foreach (var item in ordenes)
            {
                var aux = ConEmbarque.Where(x => x.idordentrabajo.Equals(Convert.ToInt64(item))).SingleOrDefault();
                if (aux != null)
                {
                    ConEmbarque.Remove(aux);
                    SinEmbarque.Add(aux);
                }
            }
            Session["SinEmbarque"] = SinEmbarque;
            Session["ConEmbarque"] = ConEmbarque;

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAgregarManifiesto(string ids)
        {
            string[] ordenes = ids.Split(',');

            var SinEmbarque = (List<ManifiestoModel>)Session["SinEmbarque"];
            var ConEmbarque = (List<ManifiestoModel>)Session["ConEmbarque"];

            foreach (var item in ordenes)
            {
                var aux = SinEmbarque.Where(x => x.idordentrabajo.Equals(Convert.ToInt64(item))).SingleOrDefault();
                if (aux != null)
                {
                    ConEmbarque.Add(aux);
                    SinEmbarque.Remove(aux);
                }
            }

            Session["SinEmbarque"] = SinEmbarque;
            Session["ConEmbarque"] = ConEmbarque;

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonEliminarEmbarque(long idembarque)
        {
            var result = MonitoreoData.eliminarEmbarque(idembarque);
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MonitoreoFluvial()
        {
            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados((Int32)Constantes.MaestroTablas.Preliquidacion);
            var listaestados = new SelectList(
                estados,
                "idestado"
                , "estado"
                );
            ViewData["ListaEstados"] = listaestados;

            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            MonitoreoModel model = new MonitoreoModel();

            //if (usuario.idestacionorigen != null)
            //    model.idestacion = usuario.idestacionorigen.Value;

            return View(model);
        }

        #endregion fluvial

        #region Eventos

        public ActionResult Eventos(string id)
        {
            EventoModel model = new EventoModel();

            if (id != null)
            {
                var orden = OrdenData.GetObtenerOrden_model(Convert.ToInt64(id));
                model.numcp = orden.numcp;
                model.idsorden = orden.idordentrabajo.Value.ToString();
            }
            var maestroetapas = DataAccess.Monitoreo.MonitoreoData.GetListarMaestraEtapa("G");
            var listamaestroetapas = new SelectList(
                maestroetapas,
                "idmaestroetapa"
                , "descripcion"
                );
            ViewData["ListaMaestroEtapas"] = listamaestroetapas;

            var maestroincidencia = IncidenciaData.GetListarMaestroIncidencia("M").ToList();
            var listamaestroincidencia = new SelectList(
               maestroincidencia,
               "idmaestroincidencia",
               "descripcion");

            ViewData["ListaMaestroIncidencia"] = listamaestroincidencia;

            return View(model);
        }

        public JsonResult JsonGetListarEventos(string numcp, int? idmaestroincidencia, int? idmaestroetapa, string sidx, string sord, int page, int rows)
        {
            if (numcp == "") numcp = null;
            IEnumerable<EventoModel> listado = null;
            if (numcp == null)
                listado = null;
            else
                listado = MonitoreoData.GetListarEventos(numcp, idmaestroincidencia, idmaestroetapa, null);


            

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
        }

        public JsonResult JsonEliminarUltimoEvento(long? idorden, string numcp)
        {
            IncidenciaModel model = new IncidenciaModel();
            OrdenTrabajoModel orden = null;

            model.idusuarioregistro = Convert.ToInt32(Usuario.Idusuario);
            model.fechaincidencia = DateTime.Now;
            model.idmaestroincidencia = 11;

            model.fecharegistro = DateTime.Now;

            if (idorden == null)
            {
                orden = OrdenData.GetListarOrdenes(numcp, null, null, null, null, null, null).FirstOrDefault();
                if (orden == null)
                    return Json(new { res = false, msj = "El número de orden no existe." }, JsonRequestBehavior.AllowGet);
                else
                {
                    model.idsorden = orden.idordentrabajo.ToString();
                }
            }
            else
            {
                model.idsorden = idorden.Value.ToString();
            }

            var resp = IncidenciaData.InsertarActualizarIncidencia(model);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion Eventos

        #region descarga

        public JsonResult JsonAgregarDescarga(string ids)
        {
            string[] ordenes = ids.Split(',');

            var SinDescarga = (List<ManifiestoModel>)Session["SinDescarga"];
            var ConDescarga = (List<ManifiestoModel>)Session["ConDescarga"];

            foreach (var item in ordenes)
            {
                var aux = SinDescarga.Where(x => x.idordentrabajo.Equals(Convert.ToInt64(item))).SingleOrDefault();
                if (aux != null)
                {
                    ConDescarga.Add(aux);
                    SinDescarga.Remove(aux);
                }
            }

            Session["SinDescarga"] = SinDescarga;
            Session["ConDescarga"] = ConDescarga;

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonQuitarDescarga(string ids)
        {
            string[] ordenes = ids.Split(',');

            var SinDescarga = (List<ManifiestoModel>)Session["SinDescarga"];
            var ConDescarga = (List<ManifiestoModel>)Session["ConDescarga"];

            foreach (var item in ordenes)
            {
                var aux = ConDescarga.Where(x => x.idordentrabajo.Equals(Convert.ToInt64(item))).SingleOrDefault();
                if (aux != null)
                {
                    ConDescarga.Remove(aux);
                    SinDescarga.Add(aux);
                }
            }
            Session["SinDescarga"] = SinDescarga;
            Session["ConDescarga"] = ConDescarga;

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult DescargaFluvial(long idembarque)
        {
            Session["SinDescarga"] = null;
            Session["ConDescarga"] = null;

            var puerto = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Puerto));
            var listapuerto = new SelectList(
               puerto,
               "idvalortabla",
               "valor");
            ViewData["ListaPuerto"] = listapuerto;

            var model = MonitoreoData.obtenerEmbarque(idembarque);
            model.horadescarga = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechadescarga = DateTime.Now;

            return PartialView("_AgregarDescargaFluvial", model);
        }

        [HttpPost]
        public JsonResult JsonAsignarDescarga(string fechadescarga, string horadescarga, int idpuerto, long idembarque)
        {
            var SinDescarga = (List<ManifiestoModel>)Session["SinDescarga"];
            var ConDescarga = (List<ManifiestoModel>)Session["ConDescarga"];

            var SinControl = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(idembarque).Where(x => x.fechacontrolsunat == null
                    && x.fechadescarga != null && x.embarquecompleto == false).ToList();

            var model = MonitoreoData.obtenerEmbarque(idembarque);
            model.horadescarga = horadescarga;
            model.fechadescarga = Convert.ToDateTime(fechadescarga);

            if (SinDescarga.Count == 0)
            {
                model.embarquecompleto = true;
                MonitoreoData.ActualizarEmbarque(model);
            }
            if (SinControl.Count > 0)
            {
                model.embarquecompleto = false;
                MonitoreoData.ActualizarEmbarque(model);
            }

            var hm = model.horadescarga.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
            model.fechadescarga = model.fechadescarga.Value.Date + ts;

            foreach (var item in ConDescarga)
            {
                var orden = OrdenData.GetObtenerOrden_model(Convert.ToInt64(item.idordentrabajo));
                var rein = orden.reintegrotributario;

                item.idusuariodescarga = (Int32)Usuario.Idusuario;
                item.fechadescarga = model.fechadescarga;
                if (rein)
                {
                    item.embarquecompleto = false;
                    model.embarquecompleto = false;
                    MonitoreoData.ActualizarEmbarque(model);
                }
                else
                    item.embarquecompleto = true;
                item.idpuerto = idpuerto;

                MonitoreoData.DescargaFluvial(ConDescarga);

                #region AgregarEtapa

                /////////////////////////////////////////////////
                var puerto = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Puerto));
                var resppuerto = puerto.Where(x => x.idvalortabla.Equals(idpuerto)).FirstOrDefault();
                EtapaModel modeletapa = new EtapaModel();
                modeletapa.fecharegistro = DateTime.Now;
                modeletapa.idusuarioregistro = (Int32)Usuario.Idusuario;
                modeletapa.visible = true;
                modeletapa.fechaetapa = model.fechadescarga.Value;
                modeletapa.idsorden = item.idordentrabajo.ToString();
                modeletapa.descripcion = "Descarga Puerto : " + resppuerto.valor;
                modeletapa.idmaestroetapa = 7;

                modeletapa.tipoetapa = "";
                var guias = (List<IncidenciaGuiasRechazadas>)Session["guiasrechazadas"];

                var idetapa = DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modeletapa, guias);

                ///////////////////////////////////////////////

                #endregion AgregarEtapa
            }

            //var embarque =  MonitoreoData.obtenerEmbarque(model.idembarque.Value);
            //MonitoreoData.ActualizarEmbarque(embarque);
            if (ConDescarga.Count == 0)
            {
                return Json(new { res = false, msj = "Debe asignar al menos un manifiesto." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListarManifiestosSinDescargaFluvial(long idembarque)
        {
            var SinDescarga = (List<ManifiestoModel>)Session["SinDescarga"];

            if (SinDescarga == null)
            {
                SinDescarga = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(idembarque).Where(x => x.fechadescarga == null).ToList();
                Session["SinDescarga"] = SinDescarga;
            }

            var ordenes = from p in SinDescarga.GroupBy(x => x.distrito)
                          select new
                          {
                              text = p.First().distrito,
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };

            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListarManifiestosDescargaFluvial(long idembarque)
        {
            var ConDescarga = (List<ManifiestoModel>)Session["ConDescarga"];

            if (ConDescarga == null)
            {
                ConDescarga = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(idembarque).Where(x => x.fechadescarga != null && x.fechadescarga == null).ToList();
                Session["ConDescarga"] = ConDescarga;
            }

            var ordenes = from p in ConDescarga.GroupBy(x => x.distrito)
                          select new
                          {
                              text = p.First().distrito,
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };
            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        #endregion descarga

        #region controlsunat

        public JsonResult JsonAgregarControl(string ids)
        {
            string[] ordenes = ids.Split(',');

            var SinControl = (List<ManifiestoModel>)Session["SinControl"];
            var ConControl = (List<ManifiestoModel>)Session["ConControl"];

            foreach (var item in ordenes)
            {
                var aux = SinControl.Where(x => x.idordentrabajo.Equals(Convert.ToInt64(item))).SingleOrDefault();
                if (aux != null)
                {
                    ConControl.Add(aux);
                    SinControl.Remove(aux);
                }
            }
            Session["SinDescarga"] = SinControl;
            Session["ConDescarga"] = ConControl;

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ControlSunat(long idembarque)
        {
            Session["SinControl"] = null;
            Session["ConControl"] = null;

            var puerto = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Puerto));
            var listapuerto = new SelectList(
               puerto,
               "idvalortabla",
               "valor");
            ViewData["ListaPuerto"] = listapuerto;

            var model = MonitoreoData.obtenerEmbarque(idembarque);
            model.horacontrolsunat = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechacontrolsunat = DateTime.Now;

            return PartialView("_AgregarControlSunat", model);
        }

        public JsonResult JsonListarManifiestosSinControlSunat(long idembarque)
        {
            var SinControl = (List<ManifiestoModel>)Session["SinControl"];
            if (SinControl == null)
            {
                SinControl = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(idembarque).Where(x => x.fechacontrolsunat == null
                    && x.fechadescarga != null && x.embarquecompleto == false).ToList();
                Session["SinControl"] = SinControl;
            }

            var ordenes = from p in SinControl.GroupBy(x => x.distrito)
                          select new
                          {
                              text = p.First().distrito,
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };

            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListarManifiestosControlSunat(long idembarque)
        {
            var ConControl = (List<ManifiestoModel>)Session["ConControl"];

            if (ConControl == null)
            {
                ConControl = DataAccess.Monitoreo.MonitoreoData.GetListarAsignadosManifiesto(idembarque).Where(x => x.fechacontrolsunat != null).ToList();
                Session["ConControl"] = ConControl;
            }

            var ordenes = from p in ConControl.GroupBy(x => x.distrito)
                          select new
                          {
                              text = p.First().distrito,
                              children = from a in p.GroupBy(x => x.nummanifiesto)
                                         select new
                                         {
                                             text = a.First().nummanifiesto,
                                             children = from b in a
                                                        select new
                                                        {
                                                            text = b.numcp,
                                                            data = new
                                                            {
                                                                idordentrabajo = b.idordentrabajo.ToString(),
                                                                spanclass = "root"
                                                            },
                                                            icon = "glyphicon glyphicon-list-alt"
                                                        },
                                             icon = "glyphicon glyphicon-folder-open"
                                         },
                              icon = "glyphicon glyphicon-folder-open"
                          };
            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonQuitarControl(string ids)
        {
            string[] ordenes = ids.Split(',');

            var SinControl = (List<ManifiestoModel>)Session["SinControl"];
            var ConControl = (List<ManifiestoModel>)Session["ConControl"];

            foreach (var item in ordenes)
            {
                var aux = ConControl.Where(x => x.idordentrabajo.Equals(Convert.ToInt64(item))).SingleOrDefault();
                if (aux != null)
                {
                    ConControl.Remove(aux);
                    SinControl.Add(aux);
                }
            }
            Session["SinControl"] = SinControl;
            Session["ConControl"] = ConControl;

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAsignarControl(string fechacontrol, string horacontrol, long idembarque)
        {
            var SinDescarga = (List<ManifiestoModel>)Session["SinControl"];
            var ConDescarga = (List<ManifiestoModel>)Session["ConControl"];

            var model = MonitoreoData.obtenerEmbarque(idembarque);
            model.horacontrolsunat = horacontrol;
            model.fechacontrolsunat = Convert.ToDateTime(fechacontrol);

            if (SinDescarga.Count == 0)
            {
                model.embarquecompleto = true;
                MonitoreoData.ActualizarEmbarque(model);
            }

            var hm = model.horacontrolsunat.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
            model.fechacontrolsunat = model.fechacontrolsunat.Value.Date + ts;

            foreach (var item in ConDescarga)
            {
                var orden = OrdenData.GetObtenerOrden_model(Convert.ToInt64(item.idordentrabajo));
                var rein = orden.reintegrotributario;

                item.idusuariodescarga = (Int32)Usuario.Idusuario;
                item.fechacontrolsunat = model.fechacontrolsunat;
                item.embarquecompleto = true;

                MonitoreoData.DescargaFluvial(ConDescarga);

                #region AgregarEtapa

                /////////////////////////////////////////////////
                EtapaModel modeletapa = new EtapaModel();
                modeletapa.fecharegistro = DateTime.Now;
                modeletapa.idusuarioregistro = (Int32)Usuario.Idusuario;
                modeletapa.visible = true;
                modeletapa.fechaetapa = model.fechacontrolsunat.Value;
                modeletapa.idsorden = item.idordentrabajo.ToString(); ;
                modeletapa.idmaestroetapa = (Int32)DataAccess.Constantes.MaestroEtapas.ControlSUNAT;

                modeletapa.tipoetapa = "";
                var guias = (List<IncidenciaGuiasRechazadas>)Session["guiasrechazadas"];

                var idetapa = DataAccess.Monitoreo.MonitoreoData.InsertarEtapa(modeletapa, guias);

                ///////////////////////////////////////////////

                #endregion AgregarEtapa
            }

            //var embarque =  MonitoreoData.obtenerEmbarque(model.idembarque.Value);
            //MonitoreoData.ActualizarEmbarque(embarque);
            if (ConDescarga.Count == 0)
            {
                return Json(new { res = false, msj = "Debe asignar al menos un manifiesto." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion controlsunat


        #region GestiónEntrega

        public ActionResult GestionEntrega()
        { 
            return View(); 
        }
        public ActionResult GestionEntregaFluvial()
        {
            return View();
        }
        public ActionResult MonitoreoEntrega()
        {
            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados((Int32)Constantes.MaestroTablas.OT);
            var listaestados = new SelectList(
                estados.Where(x => x.idestado.Equals((Int16)Constantes.EstadoOT.PendienteEntrega)
                || x.idestado.Equals((Int16)Constantes.EstadoOT.PendienteRetornoDocumentario)
                || x.idestado.Equals((Int16)Constantes.EstadoOT.PendienteFacturacion)
                || x.idestado.Equals((Int16)Constantes.EstadoOT.Cerrado)).ToList(),
                "idestado"
                , "estado"
                );
            ViewData["ListaEstados"] = listaestados ;

            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            MonitoreoModel model = new MonitoreoModel();
            if (Session["Busqueda"] != null)
            {
                var busqueda = (MonitoreoModel)Session["Busqueda"];
                model.numcp = busqueda.numcp;
                model.idcliente = busqueda.idcliente;
                model.iddestino = busqueda.iddestino;
                model.nummanifiesto = busqueda.nummanifiesto;
                model.numhojaruta = busqueda.numhojaruta;
                model.grr = busqueda.grr;
                model.documento = busqueda.documento;
                model.tienda = busqueda.tienda;
            }

            //if (usuario.idestacionorigen != null)
            //    model.idestacion = usuario.idestacionorigen.Value;

            return View(model);
        }
        public ActionResult MonitoreoEntregaFluvial()
        {
            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados((Int32)Constantes.MaestroTablas.OT);
            var listaestados = new SelectList(
                estados.Where(x => x.idestado.Equals((Int16)Constantes.EstadoOT.PendienteEntrega)
                || x.idestado.Equals((Int16)Constantes.EstadoOT.PendienteRetornoDocumentario)
                || x.idestado.Equals((Int16)Constantes.EstadoOT.PendienteFacturacion)
                || x.idestado.Equals((Int16)Constantes.EstadoOT.Cerrado)).ToList(),
                "idestado"
                , "estado"
                );
            ViewData["ListaEstados"] = listaestados;

            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            MonitoreoModel model = new MonitoreoModel();
            if (Session["Busqueda"] != null)
            {
                var busqueda = (MonitoreoModel)Session["Busqueda"];
                model.numcp = busqueda.numcp;
                model.idcliente = busqueda.idcliente;
                model.iddestino = busqueda.iddestino;
                model.nummanifiesto = busqueda.nummanifiesto;
                model.numhojaruta = busqueda.numhojaruta;
                model.grr = busqueda.grr;
                model.documento = busqueda.documento;
                model.tienda = busqueda.tienda;
            }

            //if (usuario.idestacionorigen != null)
            //    model.idestacion = usuario.idestacionorigen.Value;

            return View(model);
        }
        [HttpPost]
        public JsonResult JsonListarOTsEntrega( int? iddestino, string numcp, string nummanifiesto, int? idestado)
        {

            if (numcp == "") numcp = null;
            if (nummanifiesto == "") nummanifiesto = null;
            if (iddestino == null && nummanifiesto  == null && numcp == null)
            {
                return Json(new { res = true });
            }
            var listadoTotal = DataAccess.Monitoreo.MonitoreoData.OtsMonitoreoEntrega( null, iddestino, numcp, nummanifiesto, idestado, (Int32) Constantes.TipoTransporte.Terrestre);
            listadoTotal.AddRange(DataAccess.Monitoreo.MonitoreoData.OtsMonitoreoEntrega(null, iddestino, numcp, nummanifiesto, idestado, (Int32)Constantes.TipoTransporte.Aereo));

            return (new JqGridExtension<ListarOtsMonitoreoEntregaDto>()).DataBind(listadoTotal, listadoTotal.Count);




        }
        [HttpPost]
        public JsonResult JsonListarOTsEntregaFluvial(int? iddestino, string numcp, string nummanifiesto, int? idestado)
        {

            if (numcp == "") numcp = null;
            if (nummanifiesto == "") nummanifiesto = null;
            if (iddestino == null && nummanifiesto == null && numcp == null)
            {
                return Json(new { res = true });
            }
            var listadoTotal = DataAccess.Monitoreo.MonitoreoData.OtsMonitoreoEntrega(null, iddestino, numcp, nummanifiesto, idestado, (Int32)Constantes.TipoTransporte.Fluvial);

            return (new JqGridExtension<ListarOtsMonitoreoEntregaDto>()).DataBind(listadoTotal, listadoTotal.Count);




        }

        public PartialViewResult ModalEntregaCliente(long idordentrabajo)
        {




            EtapaModel model = new EtapaModel();
            var mincidentes = DataAccess.Monitoreo.MonitoreoData.GetListarMaestraEtapa("E").ToList();
            var listamincidentes = new SelectList(
               mincidentes,
               "idmaestroetapa",
               "descripcion");
            ViewData["ListadoMaestroEtapa"] = listamincidentes;

            model.idordentrabajo = idordentrabajo;
            model.idsorden = idordentrabajo.ToString();
            //model.idsorden = model.idsorden.Substring(1, model.idsorden.Length - 1);

            return PartialView("_modalEntregaCliente", model);
        }

        public PartialViewResult JsonAgregarIncidentes(string idsorden, string tipoincidencia)
        {
            //string[] ordenes = idordenes.Split(',');

            IncidenciaModel model = new IncidenciaModel();
            model.idsorden = idsorden;
            var maestroincidencia = IncidenciaData.GetListarMaestroIncidencia("MF").ToList();
            maestroincidencia.AddRange(IncidenciaData.GetListarMaestroIncidencia("M").ToList());
            var listamaestroincidencia = new SelectList(
               maestroincidencia,
               "idmaestroincidencia",
               "descripcion");
            ViewData["ListadoMaestroIncidente"] = listamaestroincidencia;

            model.horaincidencia = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechaincidencia = DateTime.Now;

            return PartialView("_AgregarIncidente", model);
        }
        [HttpPost]
        public JsonResult JsonObtenerMaestroIncidencia(int idmaestroincidencia,  string tipoincidencia)
        {
            var ModelMaestroIncidencia = IncidenciaData.GetListarMaestroIncidencia(tipoincidencia)
                        .Where(x => x.idmaestroincidencia.Equals(idmaestroincidencia))
                        .FirstOrDefault();



            return Json(new { res = true , ModelMaestroIncidencia.esfecha, ModelMaestroIncidencia.seactualiza });
        }

        #endregion 


    }
}