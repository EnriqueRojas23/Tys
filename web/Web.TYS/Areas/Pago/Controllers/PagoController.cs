using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Common.Controllers;
using Componentes.Common.Utilidades;
using Web.Common.Extensions;
using Web.Common.HtmlHelpers;
using QueryContracts.TYS.Pago.Results;
using QueryContracts.Core.CRM.Result;
using Web.TYS.DataAccess.Seguimiento;
using Web.TYS.Areas.Pago.Models;
using Web.TYS.DataAccess.Pago;
using Web.TYS.DataAccess;


namespace Web.TYS.Areas.Pago.Controllers
{
    public class PagoController : BaseController
    {
        //
        // GET: /Facturacion/Facturacion/
        private const string LISTAUBIGEO = "ListaUbigeo";
        private const string LISTAPROVEEDOR = "ListaProveedor";
        private const string LISTAETAPA = "ListaEtapa";
        private const string LISTATIPOTRANSPORTE = "ListaTipoTransporte";
        private const string LISTAMONEDA = "ListaMoneda";
        private const string LISTATIPOCOMPROBANTE = "ListaTipoComprobante";
        private const string LISTADETALLECOMPROBATE = "ListaDetalleComprobante";


        public ActionResult Index()
        {
            return RedirectToAction("Comprobante", "Facturacion");
        }

        #region proveedor

        public ActionResult Proveedor()
        {
            return View();
        }
        [HttpPost]
        public JsonResult JsonGetListarProveedores(string criterio, bool inactivo)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = PagoData.GetListarProveedores(criterio, inactivo);
            

            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult AgregarProveedorModal()
        {
            return PartialView("_AgregarProveedorModal");
        }
        public PartialViewResult AgregarProveedorModalHelp()
        {
            return PartialView("_AgregarProveedorModalHelp");
        }
        [HttpPost]
        public ActionResult EditarProveedorModal( ProveedorModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            try
            {
                var proveedor = PagoData.InsertarActualizarProveedor(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpPost]
        public ActionResult AgregarProveedorAux(string ruc , string razonsocial)
        {
            ProveedorModel Modelo = new ProveedorModel();
            Modelo.activo = true;
            Modelo.razonsocial = razonsocial;
            Modelo.ruc = ruc;

            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = true;
            try
            {
                var proveedor = PagoData.InsertarActualizarProveedor(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AgregarProveedorModal( ProveedorModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = true;
            try
            {
                var proveedor = PagoData.InsertarActualizarProveedor(Modelo, out respuesta);
                return Json(new { res = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult EditarProveedorModal(int id)
        {
            var model = new ProveedorModel();

            var oproveedor = new PagoData().GetProveedor(id);
            model.idproveedor = oproveedor.idproveedor;
            model.placaasociada = oproveedor.placaasociada;
            model.razonsocial = oproveedor.razonsocial;
            model.ruc = oproveedor.ruc;
            model.activo = oproveedor.activo;
            model.direccion = oproveedor.direccion;



            return PartialView("_EditarProveedorModal", model);

        }
        [HttpPost]
        public ActionResult EliminarProveedor(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new ProveedorModel();
            Modelo.activo = false;
            Modelo.idproveedor = id;

            try
            {
                var proveedor = PagoData.InsertarActualizarProveedor(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }



        }
        #endregion


        #region TipoComprobante

        public ActionResult TipoComprobante()
        {
            return View();
        }
        [HttpPost]
        public JsonResult JsonGetListarTipoComprobante(string criterio)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = PagoData.GetListarTipoComprobante(criterio);


            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult AgregarTipoComprobanteModal()
        {
            return PartialView("_AgregarTipoComprobanteModal");
        }
        [HttpPost]
        public ActionResult EditarTipoComprobanteModal( TipoComprobanteModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = false;

            try
            {
                var proveedor = PagoData.InsertarActualizarTipoComprobante(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult AgregarTipoComprobanteModal( TipoComprobanteModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = false;
            try
            {
                var proveedor = PagoData.InsertarActualizarTipoComprobante(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult EditarTipoComprobanteModal(int id)
        {
            var model = new TipoComprobanteModel();

            var otipocomprobante = new PagoData().GetTipoComprobante(id);
            model.idtipocomprobante = otipocomprobante.idtipocomprobante;
            model.tipocomprobante = otipocomprobante.tipocomprobante;
            model.codigo = otipocomprobante.codigo;




            return PartialView("_EditarTipoComprobanteModal", model);

        }
        [HttpPost]
        public ActionResult EliminarTipoComprobante(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new TipoComprobanteModel();
            Modelo.activo = true;
            Modelo.idtipocomprobante = id;

            try
            {
                var tipocomprobante = PagoData.InsertarActualizarTipoComprobante(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }



        }

        #endregion


        #region Moneda
        public ActionResult Moneda()
        {
            return View();
        }
        [HttpPost]
        public JsonResult JsonGetListarMoneda(string criterio)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = PagoData.GetListarMoneda(criterio);


            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult AgregarMonedaModal()
        {
            return PartialView("_AgregarMonedaModal");
        }
        [HttpPost]
        public ActionResult EditarMonedaModal(MonedaModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = false;

            try
            {
                var proveedor = PagoData.InsertarActualizarMoneda(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult AgregarMonedaModal(MonedaModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = false;
            try
            {
                var proveedor = PagoData.InsertarActualizarMoneda(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult EditarMonedaModal(int id)
        {
            var model = new MonedaModel();

            var omoneda = new PagoData().GetMoneda(id);
            model.idmoneda = omoneda.idmoneda;
            model.moneda = omoneda.moneda;




            return PartialView("_EditarMonedaModal", model);

        }
        [HttpPost]
        public ActionResult EliminarMoneda(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new MonedaModel();
            Modelo.activo = true;
            Modelo.idmoneda = id;

            try
            {
                var tipocomprobante = PagoData.InsertarActualizarMoneda(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }



        }
        #endregion 

        #region Etapa

        public ActionResult Etapa()
        {
            return View();
        }
        [HttpPost]
        public JsonResult JsonGetListarEtapa(string criterio, bool inactivo)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = PagoData.GetListarEtapa(criterio, inactivo);


            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult AgregarEtapaModal()
        {
            return PartialView("_AgregarEtapaModal");
        }
        [HttpPost]
        public ActionResult EditarEtapaModal(EtapaModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            try
            {
                var proveedor = PagoData.InsertarActualizarEtapa(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult AgregarEtapaModal(EtapaModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = true;
            try
            {

                var listado = PagoData.GetListarEtapa(Modelo.etapa, false);
                foreach (var item in listado)
                {
                    if (item.etapa == Modelo.etapa)
                        return Json(new { res = false, Mensaje= "Ya existe una etapa con este nombre." }, JsonRequestBehavior.AllowGet);

                }
                var proveedor = PagoData.InsertarActualizarEtapa(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult EditarEtapaModal(int id)
        {
            var model = new EtapaModel();

            var oetapa = new PagoData().GetEtapa(id);
            model.idetapa = oetapa.idetapa;
            model.etapa = oetapa.etapa;
            model.requiereot = oetapa.requiereot;
            model.activo = oetapa.activo;

            return PartialView("_EditarEtapaModal", model);

        }
        [HttpPost]
        public ActionResult EliminarEtapa(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new EtapaModel();
            Modelo.activo = false;
            Modelo.idetapa = id;

            try
            {
                var tipocomprobante = PagoData.InsertarActualizarEtapa(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }



        }

        #endregion

                
        #region TipoTransporte
        public ActionResult TipoTransporte()
        {
            return View();
        }
        [HttpPost]
        public JsonResult JsonGetListarTipoTransporte(string criterio)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = PagoData.GetListarTipoTransporte(criterio);


            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult AgregarTipoTransporteModal()
        {
            return PartialView("_AgregarTipoTransporteModal");
        }
        [HttpPost]
        public ActionResult EditarTipoTransporteModal( TipoTransporteModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = false;

            try
            {
                var proveedor = PagoData.InsertarActualizarTipoTransporte(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult AgregarTipoTransporteModal( TipoTransporteModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            Modelo.activo = false;
            try
            {
                var proveedor = PagoData.InsertarActualizarTipoTransporte(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult EditarTipoTransporteModal(int id)
        {
            var model = new TipoTransporteModel();

            var otipocomprobante = new PagoData().GetTipoTransporte(id);
            model.idtipotransporte = otipocomprobante.idtipotransporte;
            model.tipotransporte = otipocomprobante.tipotransporte;


            return PartialView("_EditarTipoTransporteModal", model);

        }
        [HttpPost]
        public ActionResult EliminarTipoTransporte(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new TipoTransporteModel();
            Modelo.activo = true;
            Modelo.idtipotransporte = id;

            try
            {
                var tipocomprobante = PagoData.InsertarActualizarTipoTransporte(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }



        }



        #endregion


        #region Comprobante


        #region RegistroComprobante

        public ActionResult NuevoComprobante()
        {
            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;


            //var ubigeos = PagoData.GetListarUbigeo();
            //var listaubigeo = new SelectList(
            //    ubigeos,
            //    "idubigeo",
            //    "ubigeo");
            //ViewData[LISTAUBIGEO] = listaubigeo;


            var proveedores = PagoData.GetListarProveedores("", false).Where(x => x.activo == true).ToList();
            var listaproveedor = new SelectList(
                proveedores,
                "idproveedor",
                "razonsocial");
            ViewData[LISTAPROVEEDOR] = listaproveedor;


            var etapas = PagoData.GetListarEtapa("", false).Where(x => x.activo != true && x.activo != false).ToList();
            var listaetapa = new SelectList(
                etapas,
                "idetapa",
                "etapa");
            ViewData[LISTAETAPA] = listaetapa;


            var tipostransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte)).Where(x => x.activo == true).ToList();
            var listatipos = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData[LISTATIPOTRANSPORTE] = listatipos;



            var monedas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Moneda)).Where(x => x.activo == true).ToList();
            var listamonedas = new SelectList(
                monedas,
                "idvalortabla",
                "valor");
            ViewData[LISTAMONEDA] = listamonedas;



            var tipocomprobantes = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoComprobante)).Where(x => x.activo == true).ToList();
            var listatipocomprobantes = new SelectList(
                tipocomprobantes,
                "idvalortabla",
                "valor");
            ViewData[LISTATIPOCOMPROBANTE] = listatipocomprobantes;



            var tiposfacturacion = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoFacturacion)).Where(x => x.activo == true).ToList();
            var listatiposfacturacion = new SelectList(
                tiposfacturacion,
                "idvalortabla",
                "valor");
            ViewData["ListaTiposFacturacion"] = listatiposfacturacion;




            Session["ots"] = null;
            var Model = new ComprobanteModel();

            return View();

        }
        [HttpPost]
        public ActionResult AgregarComprobanteModal(ComprobanteModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario
            ListarEtapaDto etapa = new ListarEtapaDto();
            if (Modelo.idtipofacturacion != 99)
            {
                var etapas = PagoData.GetListarEtapa("", false).Where(x => x.activo == true).ToList();
                etapa = etapas.Where(x => x.idetapa.Equals(Modelo.idetapa)).Single();
            }


            var existe = PagoData.ValidarSerie(Modelo.serienumero
                , Modelo.idproveedor, Modelo.idetapa, Modelo.idtipocomprobante, null);
            if (existe.cantidad > 0)
                return Json(new { res = false, mensaje = "El número de serie de comprobante ya existe." }, JsonRequestBehavior.AllowGet);

         

                var ots = (List<OrdenesTransporteModel>)Session["ots"];
                if (ots == null) ots = new List<OrdenesTransporteModel>();

                if (etapa.requiereot)
                {
                    if (Modelo.idtipofacturacion != 86)
                    {
                        if (ots.Count == 0)
                        {
                            return Json(new { res = false, mensaje = "Requiere el ingreso de OTS" }, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
                List<OrdenesTransporteModel> otslist = new List<OrdenesTransporteModel>();
                foreach (var item in ots)
                {
                    otslist.Add(new OrdenesTransporteModel()
                    {
                        NumCp = item.NumCp,
                        PKID = item.PKID,
                        TotalBultos = item.TotalBultos,
                        TotalPeso = item.TotalPeso,
                        Precio = item.Precio,
                        SubTotal = item.SubTotal,
                        Total = item.Total,
                        ValorVenta = item.ValorVenta,
                        nroguia =  item.nroguia,
                        manifiesto =  item.manifiesto

                    });
                }
                if (otslist.Count > 0)
                {
                    Modelo.ots = new List<OrdenesTransporteModel>();
                    Modelo.ots = otslist;
                }

                Modelo.activo = true;
                try
                {
                    var proveedor = PagoData.InsertarActualizarComprobante(Modelo, out respuesta);
                    return Json(new { res = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.ToString());
                    return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
                }
           
                //return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
        }


        #endregion 


        public ActionResult Comprobante()
        {
            Session["ots"] = null;
            return View();
        }

        public ActionResult EditarComprobante(long id)
        {
            var model = new ComprobanteModel();
            Session["ots"] = null;
            var oComprobante = new PagoData().GetComprobante(id);

            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;
            

            //var ubigeos = PagoData.GetListarUbigeo();
            //var listaubigeo = new SelectList(
            //    ubigeos,
            //    "idubigeo",
            //    "ubigeo");
            //ViewData[LISTAUBIGEO] = listaubigeo;


            var proveedores = PagoData.GetListarProveedores("", false).Where(x => x.activo == true).ToList();
            var listaproveedor = new SelectList(
                proveedores,
                "idproveedor",
                "razonsocial");
            ViewData[LISTAPROVEEDOR] = listaproveedor;


            var etapas = PagoData.GetCargarEtapas(oComprobante.idproveedor);
            var listaetapa = new SelectList(
                etapas,
                "idetapa",
                "etapa");

            ViewData[LISTAETAPA] = listaetapa;


            var tipostransporte = PagoData.GetCargarTipoTransporte(oComprobante.idetapa, oComprobante.idproveedor);
            var listatipos = new SelectList(
                tipostransporte,
                "idtipotransporte",
                "tipotransporte");
            ViewData[LISTATIPOTRANSPORTE] = listatipos;



            var monedas = PagoData.GetCargarMoneda(oComprobante.idtipocomprobante
             , oComprobante.idtipotransporte, oComprobante.idetapa, oComprobante.idproveedor);
            var listamonedas = new SelectList(
                monedas,
                "idmoneda",
                "moneda");
            ViewData[LISTAMONEDA] = listamonedas;


            var tipocomprobantes = PagoData.GetCargarTipoComprobante(oComprobante.idtipotransporte
                , oComprobante.idetapa, oComprobante.idproveedor);
            var listatipocomprobantes = new SelectList(
                tipocomprobantes,
                "idtipocomprobante",
                "tipocomprobante");
            ViewData[LISTATIPOCOMPROBANTE] = listatipocomprobantes;


            var tiposfacturacion = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoFacturacion)).Where(x => x.activo == true).ToList();
            var listatiposfacturacion = new SelectList(
                tiposfacturacion,
                "idvalortabla",
                "valor");
            ViewData["ListaTiposFacturacion"] = listatiposfacturacion;


            var clases = PagoData.GetListarPlaca(oComprobante.idproveedor);
            var listaclases = new SelectList(
                clases,
                "idvehiculo",
                "placa");
            ViewData["ListaPlacas"] = listaclases;



            var oDetalleComprobante = PagoData.GetListarDetalleComprobante(id);
            List<OrdenesTransporteModel> objSession = new List<OrdenesTransporteModel>();


            
            foreach (var item in oDetalleComprobante)
            {
               

                List<ListarOrdenesTransporteDto> detalles = new List<ListarOrdenesTransporteDto>();
                       
             
                var objt = new OrdenesTransporteModel();
                objt.PKID = item.PKID;
                objt.NumCp = item.numcp;
                objt.Precio = item.precio;
                objt.TotalBultos = item.totalbultos;
                objt.TotalPeso = item.totalpeso;
                objt.SubTotal = item.subtotal;
                objt.ValorVenta = item.valorventa;
                objt.Total = item.total;
                if (item.guiarecojo == "" || item.guiarecojo == null)
                    objt.nroguia = item.nroguia;
                else
                    objt.nroguia = item.guiarecojo;
                objSession.Add(objt);
            }
            var listacomprobantesdetalle = new SelectList(
                oDetalleComprobante,
                "PKID",
                "numcp");
            ViewData[LISTADETALLECOMPROBATE] = listacomprobantesdetalle;

            var ots = (List<OrdenesTransporteModel>)Session["ots"];
            if (ots == null || ots.Count == 0)
            {
                ots = new List<OrdenesTransporteModel>();
                Session["ots"] = objSession;
            }


            model.Concepto = oComprobante.concepto;
            model.fechacomprobante = oComprobante.fechacomprobante.ToShortDateString();
            model.fecharegistro = oComprobante.fecharegistro.ToShortDateString();
            model.idcomprobante = oComprobante.idcomprobante;
            model.iddestino = oComprobante.iddestino;
            model.idetapa = oComprobante.idetapa;
            model.idmoneda = oComprobante.idmoneda;
            model.idorigen = oComprobante.idorigen;
            model.idproveedor = oComprobante.idproveedor;
            model.idtipocomprobante = oComprobante.idtipocomprobante;
            model.idtipotransporte = oComprobante.idtipotransporte;
            model.monto = oComprobante.monto;
            model.serienumero = oComprobante.serienumero;
            model.placa = oComprobante.placa;
            model.observacion = oComprobante.observacion;
            model.idtipofacturacion = oComprobante.idtipofacturacion;
            model.idvehiculo = oComprobante.idvehiculo;
            model.actainforme = oComprobante.actainforme;


            return View(model);


        }
       
        public JsonResult JsonGetListarOT(string numcp, string sord, int page, int rows)
        {

            var ots = (List<OrdenesTransporteModel>)Session["ots"];
            if(ots==null)
               ots = new List<OrdenesTransporteModel>();

            var preciototal =    ots.Sum(x => Convert.ToDecimal(x.TotalPeso));
            var valorizadototal = ots.Sum(x => Convert.ToDecimal(x.ValorVenta));
            var bultos = ots.Sum(x => Convert.ToDecimal(x.TotalBultos));


            var listadoTotal = ots;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.NumCp).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.NumCp).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
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
        [HttpPost]
        public JsonResult JsonGetListarComprobante(string serie, string fecinicio, string fecfinal)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = PagoData.GetListarComprobante(serie,fecinicio,fecfinal);

            if (!string.IsNullOrEmpty(searchValue))
            {
                listado = listado.Where(a => a.moneda.Contains(searchValue) ||
                                                         a.razonsocial.Contains(searchValue) ||
                                                         a.tipocomprobante.Contains(searchValue)).ToList();
            }


            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
       

        [HttpPost]
        public ActionResult ElimnarOT(string id)
        {
            var ots = (List<OrdenesTransporteModel>)Session["ots"];
            Int64 ida = Int64.Parse(id);
            try
            {
                var eliminar = ots.Where(x => x.PKID == ida).SingleOrDefault();
                ots.Remove(eliminar);
                Session["ots"] = ots;
            }
            catch (Exception ex)
            {
                
                throw;
            }



            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }
         [HttpPost]
        public ActionResult AgregarOT(string id, string guia,int? idvehiculo, int idetapa)
        {
            List<ListarOrdenesTransporteDto> resp = new List<ListarOrdenesTransporteDto>();
            ListarOrdenesTransporteDto auxiliar = new ListarOrdenesTransporteDto();
             //busca en la base de datos de Toscanos
            resp = PagoData.GetListarOT(id,guia,id).ToList();
            if (resp.Count  == 0)
            {
                //busca en la base de datos del sistema de seguimiento
               resp = new List<ListarOrdenesTransporteDto>();
               var resp2 =  OrdenData.GetListarOrdenesComprobante(id, guia, idvehiculo).ToList();
               if (resp2.Count == 0)
                    return Json(new { res = false, msj = "El dato buscado no existe" }, JsonRequestBehavior.AllowGet);
               else
               {
                   foreach (var item in resp2)
                   {
                       auxiliar.NumCp = item.numcp;
                       auxiliar.PKID = item.idordentrabajo;
                       auxiliar.Precio = item.ValorVenta;
                       auxiliar.SubTotal = item.SubTotal;
                       auxiliar.Total = item.Total;
                       auxiliar.GR = item.nroguia;
                       auxiliar.manifiesto = item.manifiesto;

                       resp.Add(auxiliar);
                   }
                  

               }
            }

            var existe = PagoData.ValidarOTS(resp[0].PKID, idetapa);

            if (existe.cantidad > 0)
                return Json(new { res = false, msj = "La OT esta siendo usado en otro comprobante" }, JsonRequestBehavior.AllowGet);

          

            var ots = (List<OrdenesTransporteModel>)Session["ots"];
            if (ots == null)
            {
                ots = new List<OrdenesTransporteModel>();

            }
            
            foreach (var item in resp)
            {
                var obj = new OrdenesTransporteModel();
                obj.PKID = item.PKID;
                obj.NumCp = item.NumCp;
                obj.Precio = item.Precio;
                obj.TotalBultos = item.TotalBultos;
                obj.TotalPeso = item.TotalPeso;
                obj.ValorVenta = item.ValorVenta;
                obj.SubTotal = item.SubTotal;
                obj.Total = item.Total;
                obj.nroguia = item.GR;
                obj.manifiesto = item.manifiesto;


                if (ots.Where(x => x.PKID.Equals(item.PKID)).SingleOrDefault() != null)
                    return Json(new { res = false, msj = "El OT ya fue ingresada" }, JsonRequestBehavior.AllowGet);


                ots.Add(obj);
            }
        

            Session["ots"] = ots;

            var pesototal = ots.Sum(x => Convert.ToDecimal(x.TotalPeso));
            var valorizadototal = ots.Sum(x => Convert.ToDecimal(x.ValorVenta));
            var bultos = ots.Sum(x => Convert.ToDecimal(x.TotalBultos));
            var cantidad = ots.Count;

            var jsonData = new
            {
                pesototal = pesototal.ToString("#.##"),
                valorizadototal = valorizadototal.ToString("#.##"),
                bultos = bultos,
                ids =  resp[0].PKID ,
                value = resp[0].NumCp ,
                cantidad = cantidad,
                res = true


            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult EditarComprobanteModal(ComprobanteModel Modelo)
        {
            var respuesta = string.Empty;

            var etapas = PagoData.GetListarEtapa("", false).Where(x => x.activo == true).ToList();
            var etapa = etapas.Where(x => x.idetapa.Equals(Modelo.idetapa)).Single();


            var existe = PagoData.ValidarSerie(Modelo.serienumero
                , Modelo.idproveedor, Modelo.idetapa, Modelo.idtipocomprobante, Modelo.idcomprobante);
            if (existe.cantidad > 0)
                return Json(new { res = false, mensaje = "El número de serie de comprobante ya existe." }, JsonRequestBehavior.AllowGet);

               



            Modelo.activo = true;
            var ots = (List<OrdenesTransporteModel>)Session["ots"];
            if (ots == null) ots = new List<OrdenesTransporteModel>();

            List<OrdenesTransporteModel> otslist = new List<OrdenesTransporteModel>();
            if (etapa.requiereot)
            {
                if (ots.Count == 0)
                {
                    return Json(new { res = false, mensaje = "Requiere el ingreso de OTS" }, JsonRequestBehavior.AllowGet);

                }
            }
            foreach (var item in ots)
            {
                otslist.Add(new OrdenesTransporteModel()
                {
                    
                    PKID = item.PKID,
                    NumCp = item.NumCp,
                    ValorVenta = item.ValorVenta,
                    TotalBultos = item.TotalBultos,
                    TotalPeso = item.TotalPeso,
                    Precio = item.Precio,
                    SubTotal = item.SubTotal,
                    Total = item.Total
                });
            }

         
            Modelo.ots = new List<OrdenesTransporteModel>();
            Modelo.ots = otslist;

            try
            {
                var proveedor = PagoData.InsertarActualizarComprobante(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult EditarComprobanteModal(int id)
        {
            var model = new ComprobanteModel();
            Session["ots"] = null;
            var oComprobante = new PagoData().GetComprobante(id);

            var ubigeos = PagoData.GetListarUbigeo();
            var listaubigeo = new SelectList(
                ubigeos,
                "idubigeo",
                "ubigeo");
            ViewData[LISTAUBIGEO] = listaubigeo;


            var proveedores = PagoData.GetListarProveedores("", false).Where(x => x.activo == true).ToList();
            var listaproveedor = new SelectList(
                proveedores,
                "idproveedor",
                "razonsocial");
            ViewData[LISTAPROVEEDOR] = listaproveedor;


            //var etapas = PagoData.GetListarEtapa("").Where(x => x.activo == true && x.idetapa.Equals(oComprobante.idetapa)).ToList();
            ////var etapas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(MaestroTablas.Etapas));
            //var listaetapa = new SelectList(
            //    etapas,
            //    "idetapa",
            //    "etapa");
            //ViewData[LISTAETAPA] = listaetapa;
            var etapas = PagoData.GetCargarEtapas(oComprobante.idproveedor);
            var listaetapa = new SelectList(
                etapas,
                "idetapa",
                "etapa");

            ViewData[LISTAETAPA] = listaetapa;


            //var tipostransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(MaestroTablas.ModoTransporte)).Where(x => x.activo == true && x.idvalortabla.Equals(oComprobante.idtipotransporte)).ToList();
            //var listatipos = new SelectList(
            //    tipostransporte,
            //    "idvalortabla",
            //    "valor");
            //ViewData[LISTATIPOTRANSPORTE] = listatipos;
            var tipostransporte = PagoData.GetCargarTipoTransporte(oComprobante.idetapa, oComprobante.idproveedor);
            var listatipos = new SelectList(
                tipostransporte,
                "idtipotransporte",
                "tipotransporte");
             ViewData[LISTATIPOTRANSPORTE] = listatipos;


            //var monedas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(MaestroTablas.Moneda)).Where(x => x.activo == true && x.idvalortabla.Equals(oComprobante.idmoneda)).ToList();
            //var listamonedas = new SelectList(
            //    monedas,
            //    "idvalortabla",
            //    "valor");
            //ViewData[LISTAMONEDA] = listamonedas;
            var monedas = PagoData.GetCargarMoneda(oComprobante.idtipocomprobante
             , oComprobante.idtipotransporte, oComprobante.idetapa, oComprobante.idproveedor);
            var listamonedas = new SelectList(
                monedas,
                "idmoneda",
                "moneda");
            ViewData[LISTAMONEDA] = listamonedas;


            //var tipocomprobantes = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(MaestroTablas.TipoComprobante)).Where(x => x.activo == true && x.idvalortabla.Equals(oComprobante.idtipocomprobante)).ToList();
            //var listatipocomprobantes = new SelectList(
            //    tipocomprobantes,
            //    "idvalortabla",
            //    "valor");
            //ViewData[LISTATIPOCOMPROBANTE] = listatipocomprobantes;

            var tipocomprobantes = PagoData.GetCargarTipoComprobante(oComprobante.idtipotransporte,oComprobante.idetapa, oComprobante.idproveedor);
            var listatipocomprobantes = new SelectList(
                tipocomprobantes,
                "idtipocomprobante",
                "tipocomprobante");
            ViewData[LISTATIPOCOMPROBANTE] = listatipocomprobantes;

            var oDetalleComprobante = PagoData.GetListarDetalleComprobante(id);
            List<OrdenesTransporteModel> objSession = new List<OrdenesTransporteModel>();

           


            foreach (var item in oDetalleComprobante)
	        {
                 var resp = PagoData.GetListarOT(item.numcp,null,null).FirstOrDefault();
                   var objt = new OrdenesTransporteModel();
                    objt.PKID = resp.PKID;
                    objt.NumCp = resp.NumCp;
                    objt.Precio = resp.Precio;
                    objt.TotalBultos = resp.TotalBultos;
                    objt.TotalPeso = resp.TotalPeso;
                    objt.SubTotal = resp.SubTotal;
                    objt.Total = resp.Total;
                    objt.ValorVenta = resp.ValorVenta;
                    objSession.Add(objt);
	        }
            var listacomprobantesdetalle = new SelectList(
                oDetalleComprobante,
                "PKID",
                "numcp");
            ViewData[LISTADETALLECOMPROBATE] = listacomprobantesdetalle;

            var ots = (List<OrdenesTransporteModel>)Session["ots"];
            if (ots == null || ots.Count == 0)
            {
                ots = new List<OrdenesTransporteModel>();
                Session["ots"] = objSession;
            }

          
            model.Concepto = oComprobante.concepto;
            model.fechacomprobante = oComprobante.fechacomprobante.ToShortDateString();
            model.fecharegistro = oComprobante.fecharegistro.ToShortDateString();
            model.idcomprobante = oComprobante.idcomprobante;
            model.iddestino = oComprobante.iddestino;
            model.idetapa = oComprobante.idetapa;
            model.idmoneda = oComprobante.idmoneda;
            model.idorigen = oComprobante.idorigen;
            model.idproveedor = oComprobante.idproveedor;
            model.idtipocomprobante = oComprobante.idtipocomprobante;
            model.idtipotransporte = oComprobante.idtipotransporte;
            model.monto = oComprobante.monto;
            model.serienumero = oComprobante.serienumero;
            model.placa = oComprobante.placa;
            model.observacion = oComprobante.observacion;
            

            return PartialView("_EditarComprobanteModal", model);

        }
       
        public PartialViewResult AgregarOTModal(string id)
        {
            
                return PartialView("_AgregarOTModal");

        }
        [HttpPost]
        public ActionResult EliminarComprobante(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new ComprobanteModel();
            Modelo.activo = false;
            Modelo.idcomprobante = id;

            try
            {
                var tipocomprobante = PagoData.InsertarActualizarComprobante(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }



        }





        #endregion


        #region ReporteGeneral
        public ActionResult BuscarReporteGeneral(string fechainicio, string fechafin, string idproveedor, string iddestino)
        {
            TempData["Reporte"] = "/Reportes TYS/Reporte General/Reporte General";
            TempData["fechaini"] = (fechainicio== "" ? null : fechainicio);
            TempData["fechafin"] = (fechafin == "" ? null : fechafin);
            TempData["idproveedor"] = (idproveedor == "" ? null : idproveedor);
            TempData["iddestino"] = (iddestino == "" ? null : iddestino); 



            return PartialView("_ReporteModal");



            
        }
        public ActionResult ReporteGeneral()
        {
            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();   //.Facturacion.FacturacionData.GetListarUbigeo();
            var listaubigeo = new SelectList(
                ubigeos,
                "iddistrito",
                "ubigeo");
            ViewData[LISTAUBIGEO] = listaubigeo;

            var proveedores = PagoData.GetListarProveedores("", false).Where(x => x.activo == true).ToList();
            var listaproveedor = new SelectList(
                proveedores,
                "idproveedor",
                "razonsocial");
            ViewData[LISTAPROVEEDOR] = listaproveedor;


            TempData["Reporte"] = "/Reportes TYS/Reporte General/Reporte General";
            TempData["Incidencia"] = null;
            //return View("_ReporteModal");



            return View();
        }
        [HttpPost]
        public JsonResult JsonGetReporteGeneral(int? idproveedor, int? iddestino, string fecinicio, string fecfinal)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = PagoData.GetListarReporteGeneral(idproveedor,iddestino,fecinicio,fecfinal);

            if (!string.IsNullOrEmpty(searchValue))
            {
                listado = listado.Where(a => a.moneda.Contains(searchValue) ||
                                                         a.razonsocial.Contains(searchValue) ||
                                                         a.tipocomprobante.Contains(searchValue)).ToList();
            }


            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ReporteGerencial
        public ActionResult BuscarReporteGerencial(string fechainicio, string fechafin, string idproveedor, string iddestino)
        {
            TempData["Reporte"] = "/Reportes TYS/Reporte Gerencial/RPGerencial";

            TempData["fechaini"] = (fechainicio == "" ? null : fechainicio);
            TempData["fechafin"] = (fechafin == "" ? null : fechafin);
            TempData["idproveedor"] = (idproveedor == "" ? null : idproveedor);
            TempData["iddestino"] = (iddestino == "" ? null : iddestino); 


            return PartialView("_ReporteModal");




        }
        public ActionResult ReporteGerencial()
        {

            var ubigeos = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();   //.Facturacion.FacturacionData.GetListarUbigeo();
            var listaubigeo = new SelectList(
                ubigeos,
                "iddistrito",
                "ubigeo");
            ViewData[LISTAUBIGEO] = listaubigeo;


            var proveedores = PagoData.GetListarProveedores("", false).Where(x => x.activo == true).ToList();
            var listaproveedor = new SelectList(
                proveedores,
                "idproveedor",
                "razonsocial");
            ViewData[LISTAPROVEEDOR] = listaproveedor;


            return View();
        }
        [HttpPost]
        public JsonResult JsonGetReporteGerencial(int? idproveedor, int? iddestino ,string fecinicio, string fecfinal,string sord, int page, int rows)
        {
            if (fecinicio == "") fecinicio = null;
            if (fecfinal == "") fecfinal = null;

            var listado = PagoData.GetListarReporteGerencial(iddestino,idproveedor, fecinicio,fecfinal);

            var listadoTotal = listado;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.etapa).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.etapa).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
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
        public ActionResult ExportarReporte(int? idproveedor, int? iddestino ,string fecinicio, string fecfinal)
        {
            if (fecinicio == "") fecinicio = null;
            if (fecfinal == "") fecfinal = null;

            var resultado = PagoData.GetExportarReporteGerencial(iddestino, idproveedor, fecinicio, fecfinal);
            var output = Utilidades.Exportar<ExportarReporteGerencialDto>(resultado);
            return File(output.ToArray(), "application/vnd.ms-excel", "Pedidos_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
        }
        #endregion

        #region combos
        public ActionResult ListarPlacas(int? idproveedor)
        {

            var clases = PagoData.GetListarPlaca(idproveedor.Value);
            var listaclases = new SelectList(
                clases,
                "idvehiculo",
                "placa");

            return Json(listaclases, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarEtapas(int? idproveedor)
        {

            var clases = PagoData.GetCargarEtapas(idproveedor.Value);
            var listaclases = new SelectList(
                clases,
                "idetapa",
                "etapa");

            return Json(listaclases, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarTipoTransporte(int? idetapa, int? idproveedor)
        {

            var clases = PagoData.GetCargarTipoTransporte(idetapa.Value,idproveedor.Value);
            var listaclases = new SelectList(
                clases,
                "idtipotransporte",
                "tipotransporte");

            return Json(listaclases, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarTipoComprobante(int? idtipotransporte, int? idetapa, int? idproveedor)
        {

            var clases = PagoData.GetCargarTipoComprobante(idtipotransporte.Value, idetapa.Value, idproveedor.Value);
            var listaclases = new SelectList(
                clases,
                "idtipocomprobante",
                "tipocomprobante");

            return Json(listaclases, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarMoneda(int? idtipocomprobante, int? idtipotransporte, int? idetapa, int? idproveedor)
        {

            var clases = PagoData.GetCargarMoneda(idtipocomprobante.Value
                , idtipotransporte.Value, idetapa.Value, idproveedor.Value);
            var listaclases = new SelectList(
                clases,
                "idmoneda",
                "moneda");

            return Json(listaclases, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarMonedaVarios()
        {

            var monedas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Moneda)).Where(x => x.activo == true).ToList();
            var listaclases = new SelectList(
                monedas,
                "idvalortabla",
                "valor");
            

            return Json(listaclases, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarTipoComprobanteVarios()
        {
            var tipocomprobante = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoComprobante)).Where(x => x.activo == true).ToList();
            var listatipocomprobante = new SelectList(
                tipocomprobante,
                "idvalortabla",
                "valor");


            return Json(listatipocomprobante, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Asignacion

        public ActionResult Asignacion()
        {
            return View();
        }
        public JsonResult JsonGetListarAsignaciones(string criterio ,string sord, int page, int rows)
        {

            var result = PagoData.GetListarAsignacion(criterio);


            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idasignacion).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idasignacion).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
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

        public ActionResult ExportarAsignacion(string criterio)
        {

            var resultado = PagoData.GetExportarAsignacion(criterio);
            var output = Utilidades.Exportar<ListarExportarAsignacionDto>(resultado);
            return File(output.ToArray(), "application/vnd.ms-excel", "Asignacion_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
        }

        public PartialViewResult GetControlDetailsGrid(string control)
        {
            if (control == "idtipotransporte")
            {

                var tipostransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte)).Where(x => x.activo == true).ToList();
                var listatipos = new SelectList(
                    tipostransporte,
                    "idvalortabla",
                    "valor");
                ViewData[LISTATIPOTRANSPORTE] = listatipos;
            }
            if(control== "idproveedor")
            {
                var proveedores = PagoData.GetListarProveedores("", false);
                var listaproveedor = new SelectList(
                    proveedores,
                    "idproveedor",
                    "razonsocial");
                ViewData[LISTAPROVEEDOR] = listaproveedor;


            }
            if (control == "idetapa")
            {

                var etapas = PagoData.GetListarEtapa("", false).Where(x => x.activo == true).ToList();
                //var etapas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(MaestroTablas.Etapas));
                var listaetapa = new SelectList(
                    etapas,
                    "idetapa",
                    "etapa");
                ViewData[LISTAETAPA] = listaetapa;

            }
            if (control == "idtipocomprobante")
            {
                var tipocomprobantes = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoComprobante)).Where(x => x.activo == true).ToList();
                var listatipocomprobantes = new SelectList(
                    tipocomprobantes,
                    "idvalortabla",
                    "valor");
                ViewData[LISTATIPOCOMPROBANTE] = listatipocomprobantes;

            }
            if(control == "idmoneda")
            {
                var monedas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Moneda)).Where(x => x.activo == true).ToList();
                var listamonedas = new SelectList(
                    monedas,
                    "idvalortabla",
                    "valor");
                ViewData[LISTAMONEDA] = listamonedas;

            }
           
            return PartialView("_controlgrid", control);

        }
        [HttpPost]
        public ActionResult GridSave(FormCollection formcollection)
        {


            var existe = PagoData.GetListarAsignacion("");


            int? id = null;
            if(formcollection["idasignacion"] != null)
                id = int.Parse(formcollection["idasignacion"].ToString());
            int idproveedor = int.Parse(formcollection["razonsocial"]);
            int idetapa = int.Parse(formcollection["etapa"]);
            int idtipotransporte = int.Parse(formcollection["tipotransporte"]);
            int idtipocomprobante = int.Parse(formcollection["tipocomprobante"]);
            int idmoneda = int.Parse(formcollection["moneda"]);


            foreach (var item in existe)
            {
                if(item.idproveedor== idproveedor
                    & item.idetapa == idetapa
                    & item.idtipotransporte == idtipotransporte
                    & item.idtipocomprobante == idtipocomprobante
                    & item.idmoneda == idmoneda)
                    return Json(new { res = false, Mensaje = "Ya existe una asignación con los mismos datos."}, JsonRequestBehavior.AllowGet); 
            }

            AsignacionModel model = new AsignacionModel();
            model.idproveedor = idproveedor;
            model.idetapa = idetapa;
            model.idmoneda = idmoneda;
            model.idtipocomprobante = idtipocomprobante;
            model.idtipotransporte = idtipotransporte;
            model.idasignacion = id;

            var result = PagoData.InsertarActualizarAsignacion(model);


            return Json(new { res = true, Mensaje = "Ya existe una asignación con los mismos datos." }, JsonRequestBehavior.AllowGet); 
        }
        [HttpPost]
        public ActionResult EliminarAsignacion(int idasignacion)
        {

            var result = new PagoData().EliminarAsignacion(idasignacion); ;


            return Json(new { msnj = "OK"}, JsonRequestBehavior.AllowGet);

        }
        #endregion


    }
}
