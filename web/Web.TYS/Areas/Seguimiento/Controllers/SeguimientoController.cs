using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Common.Controllers;
using Componentes.Common.Utilidades;
using Web.Common.Extensions;
using Web.Common.HtmlHelpers;
using Web.TYS.Areas.Seguimiento.Models.Seguimiento;
using Web.TYS.Areas.Facturacion.Models;
using System.Data;
using QueryContracts.TYS.Seguimiento.Results;
using System.Web.Caching;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Configuration;
using Web.TYS.DataAccess.Seguimiento;
using Web.TYS.Areas.Pago.Models;
using Web.TYS.DataAccess.Pago;
using Web.TYS.DataAccess;
using Web.TYS.Areas.Monitoreo.Models;
using AutoMapper;
using System.Collections;
using Web.TYS.DataAccess.Monitoreo;

namespace Web.TYS.Areas.Seguimiento.Controllers
{
    public class SeguimientoController : BaseController
    {
        //
        // GET: /Seguimiento/Seguimiento/
        private const string LISTAMAESTROTABLA = "ListaMaestroTabla";

        private const string LISTARDIRECCIONES = "ListaDirecciones";
        private const string LISTAMONEDA = "ListaMoneda";
        private const string LISTAPROVEEDOR = "ListaProveedor";

        public ActionResult Index()
        {
            return View();
        }

        #region GestionarCodigos

        public ActionResult ValorTabla()
        {
            var maestrotablas = DataAccess.Seguimiento.SeguimientoData.GetListarMaestroTabla();
            var listamaestrotablas = new SelectList(
                maestrotablas,
                "idmaestrotabla",
                "tabla");
            ViewData[LISTAMAESTROTABLA] = listamaestrotablas;

            return View();
        }

        [HttpPost]
        public JsonResult JsonGetListarValorTabla(int? idMaestroTabla, string valor)
        {
            if (idMaestroTabla == null) idMaestroTabla = 0;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(idMaestroTabla, valor);

            if (sortColumn != "" && sortColumnDir != "")
            {
                if (sortColumnDir.ToUpper() == "DESC")
                {
                    var parametro = sortColumn;
                    var propertyInfo = typeof(ListarValorxTablaDto).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sortColumn;
                    var propertyInfo = typeof(ListarValorxTablaDto).GetProperty(parametro);
                    listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }

            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult AgregarValorTablaModal()
        {
            var maestrotablas = DataAccess.Seguimiento.SeguimientoData.GetListarMaestroTabla();
            var listamaestrotablas = new SelectList(
                maestrotablas,
                "idmaestrotabla",
                "tabla");
            ViewData[LISTAMAESTROTABLA] = listamaestrotablas;

            return PartialView("_AgregarValorTablaModal");
        }

        [HttpPost]
        public ActionResult AgregarValorTablaModal(Seguimiento.Models.Seguimiento.ValorTablaModel Modelo)
        {
            var datos =  DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Modelo.idmaestrotabla);
            if(datos.Where(x=>x.valor.Equals(Modelo.valor.Trim())).SingleOrDefault() != null )
            {
                return Json(new { res = false , msj = "El valor ya existe." }, JsonRequestBehavior.AllowGet);
            }

            var respuesta = string.Empty;
            Modelo.activo = true;
            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarValorTabla(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EditarValorTablaModal(Seguimiento.Models.Seguimiento.ValorTablaModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;

            var datos = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Modelo.idmaestrotabla);
            if (datos.Where(x => x.valor.Equals(Modelo.valor.Trim()) && x.idvalortabla!= Modelo.idvalortabla).SingleOrDefault() != null)
            {
                return Json(new { res = false, msj = "El valor ya existe." }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarValorTabla(Modelo, out respuesta);
                return Json(new { res = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult EditarValorTablaModal(int id)
        {
            var model = new ValorTablaModel();

            var maestrotablas = DataAccess.Seguimiento.SeguimientoData.GetListarMaestroTabla();
            var listamaestrotablas = new SelectList(
                maestrotablas,
                "idmaestrotabla",
                "tabla");
            ViewData[LISTAMAESTROTABLA] = listamaestrotablas;

            var ovalortabla = new DataAccess.Seguimiento.SeguimientoData().GetValorTabla(id);

            model.idmaestrotabla = ovalortabla.idmaestrotabla;
            model.valor = ovalortabla.valor;
            model.idvalortabla = ovalortabla.idvalortabla;
            model.activo = ovalortabla.activo;
            model.orden = ovalortabla.orden;

            return PartialView("_EditarValorTablaModal", model);
        }

        [HttpPost]
        public ActionResult EliminarValorTabla(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new ValorTablaModel();
            Modelo.activo = false;
            Modelo.idvalortabla = id;

            try
            {
                //var proveedor = PagoData.InsertarActualizarProveedor(Modelo, out respuesta);
                var valortabla = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarValorTabla(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion GestionarCodigos

        #region Formula

        public ActionResult Formula()
        {
            return View();
        }

        [HttpPost]
        public JsonResult JsonGetListarFormula(string criterio, bool inactivo)
        {
            if (criterio == string.Empty) criterio = null;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarFormula(criterio,inactivo);

            if (sortColumn != "" && sortColumnDir != "")
            {
                if (sortColumnDir.ToUpper() == "DESC")
                {
                    var parametro = sortColumn;
                    var propertyInfo = typeof(ListarFormulasDto).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sortColumn;
                    var propertyInfo = typeof(ListarFormulasDto).GetProperty(parametro);
                    listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
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
        public ActionResult AgregarFormulaModal(Seguimiento.Models.Seguimiento.FormulaModel Modelo)
        {
            var respuesta = string.Empty;
            Modelo.activo = true;
            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarFormula(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult AgregarFormulaModal()
        {
            //ListaDirecciones
            operacionModel oOperacion = new operacionModel();
            List<operacionModel> operaciones = new List<operacionModel>();
            oOperacion.id = 1;
            oOperacion.operador = "+";
            operaciones.Add(oOperacion);
            oOperacion = new operacionModel();
            oOperacion.id = 2;
            oOperacion.operador = "-";
            operaciones.Add(oOperacion);
            oOperacion = new operacionModel();
            oOperacion.id = 3;
            oOperacion.operador = "*";
            operaciones.Add(oOperacion);
            oOperacion = new operacionModel();
            oOperacion.id = 4;
            oOperacion.operador = "/";
            operaciones.Add(oOperacion);

            var listaoperaciones = new SelectList(
                      operaciones,
                      "id",
                      "operador");
            ViewData["ListadoOperacion"] = listaoperaciones;
            //operaciones.

            var conceptos = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ConceptoFormula)).Where(x => x.activo == true).ToList();
            var listaconcepto = new SelectList(
                conceptos,
                "idvalortabla",
                "valor");
            ViewData["ListadoConcepto"] = listaconcepto;

            return PartialView("_AgregarFormulaModal");
        }

        [HttpPost]
        public ActionResult EditarFormulaModal(Seguimiento.Models.Seguimiento.FormulaModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarFormula(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult EditarFormulaModal(int id)
        {
            int cant = 0;
            var model = new FormulaModel();

            //ListaDirecciones
            operacionModel oOperacion = new operacionModel();
            List<operacionModel> operaciones = new List<operacionModel>();
            oOperacion.id = 1;
            oOperacion.operador = "+";
            operaciones.Add(oOperacion);
            oOperacion = new operacionModel();
            oOperacion.id = 2;
            oOperacion.operador = "-";
            operaciones.Add(oOperacion);
            oOperacion = new operacionModel();
            oOperacion.id = 3;
            oOperacion.operador = "*";
            operaciones.Add(oOperacion);
            oOperacion = new operacionModel();
            oOperacion.id = 4;
            oOperacion.operador = "/";
            operaciones.Add(oOperacion);
            oOperacion = new operacionModel();
            oOperacion.id = 5;
            oOperacion.operador = "=";
            operaciones.Add(oOperacion);

            var listaoperaciones = new SelectList(
                      operaciones,
                      "id",
                      "operador");
            ViewData["ListadoOperacion"] = listaoperaciones;
            //operaciones.

            var conceptos = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ConceptoFormula)).Where(x => x.activo == true).ToList();
            var listaconcepto = new SelectList(
                conceptos,
                "idvalortabla",
                "valor");
            ViewData["ListadoConcepto"] = listaconcepto;

            var ovalortabla = new DataAccess.Seguimiento.SeguimientoData().GetFormula(id);

            model.idformula = ovalortabla.idformula;
            model.nombre = ovalortabla.nombre;
            model.formula = ovalortabla.formula;
            model.activo = ovalortabla.activo;

            return PartialView("_EditarFormulaModal", model);
        }

        [HttpPost]
        public ActionResult EliminarFormula(int id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new FormulaModel();
            Modelo.activo = false;
            Modelo.idformula = id;

            try
            {
                //var proveedor = PagoData.InsertarActualizarProveedor(Modelo, out respuesta);
                var valortabla = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarFormula(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Formula



        #region cliente

        public ActionResult Cliente()
        {
            return View();
        }

        public PartialViewResult AgregarClienteModal()
        {
            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var monedas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Moneda)).Where(x => x.activo == true).ToList();
            var listamonedas = new SelectList(
                monedas,
                "idvalortabla",
                "valor");
            ViewData[LISTAMONEDA] = listamonedas;

            return PartialView("_AgregarClienteModal");
        }

        public JsonResult JsonGetDatosDireccion(int? iddireccion)
        {
            var DatosDireccion = DataAccess.Seguimiento.SeguimientoData.GetObtenerDireccion(iddireccion);

            if (DatosDireccion != null)
            {
                return Json(DatosDireccion, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult JsonGetListarCliente(string criterio, bool inactivo)
        {
            if (criterio == string.Empty) criterio = null;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarClientes(criterio, inactivo);
            if (sortColumn != "" && sortColumnDir != "")
            {
                if (sortColumnDir.ToUpper() == "DESC")
                {
                    var parametro = sortColumn;
                    var propertyInfo = typeof(ClienteModel).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sortColumn;
                    var propertyInfo = typeof(ClienteModel).GetProperty(parametro);
                    listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }

            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetDirecciones(int idcliente, string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(idcliente);

            //Session["idcliente"] = idcliente;

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.iddireccion).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.iddireccion).ToList();
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

        public JsonResult JsonGetListarProveedorxCliente(int? idcliente, string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarProveedorxCliente(idcliente);
            Session["idcliente"] = idcliente;

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idproveedor).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idproveedor).ToList();
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
        public ActionResult AgregarClienteModal(ClienteModel Modelo)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            //Modelo.activo = true;
            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCliente(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EliminarCliente(int? id)
        {
            var respuesta = string.Empty;
            //Modelo.idproveedor = Usuario.Idusuario;
            var Modelo = new ClienteModel();
            Modelo.activo = false;
            Modelo.idcliente = id;

            try
            {
                //var proveedor = PagoData.InsertarActualizarProveedor(Modelo, out respuesta);
                var valortabla = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCliente(Modelo, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult AgregarDireccionModal(int id)
        {
            var model = new DireccionModel();
            Session["idcliente"] = id;
            return PartialView("_AgregarDireccionModal", model);
        }

        public PartialViewResult EditarClienteModal(int id)
        {
            int cant = 0;
            var model = new ClienteModel();

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var monedas = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Moneda)).Where(x => x.activo == true).ToList();
            var listamonedas = new SelectList(
                monedas,
                "idvalortabla",
                "valor");
            ViewData[LISTAMONEDA] = listamonedas;

            var ocliente = DataAccess.Seguimiento.SeguimientoData.GetObtenerCliente(id);

            model.activo = ocliente.activo;
            model.idcliente = ocliente.idcliente;
            model.iddireccion = ocliente.iddireccion;
            model.idmonedalinea = ocliente.idmonedalinea;
            model.idubigeo = ocliente.idubigeo;

            model.lineacredito = ocliente.lineacredito;
            model.nombrecorto = ocliente.nombrecorto;
            model.razonsocial = ocliente.razonsocial;
            model.ruc = ocliente.ruc;
            model.ubigeo = ocliente.ubigeo;
            model.rutalogo = ocliente.rutalogo;

            model.direccion = ocliente.direccion;
            model.codigodireccion = ocliente.codigo;
            model.iddistrito = ocliente.idubigeo;
            model.pagocontado = ocliente.pagocontado;

            return PartialView("_EditarClienteModal", model);
        }

          [HttpPost]
          public ActionResult GridSaveDireccion(FormCollection formcollection)
          {
              var idcliente = Convert.ToInt32(Session["idcliente"]);

              int? iddireccion = int.Parse(formcollection["iddireccion"]);
              string codigo = formcollection["codigo"].ToString();
              string distrito = formcollection["distrito"].ToString();
              string direccion = formcollection["direccion"].ToString();

              if (iddireccion == 0) iddireccion = null;

              DireccionModel model = new DireccionModel();
              model.iddireccion = iddireccion;
              model.codigo = codigo;
              model.direccion = direccion;
              model.iddistrito = Convert.ToInt32(distrito);
              model.idcliente   = idcliente;
              model.activo = true;
              model.principal = false;

              if (codigo != "" && idcliente != 0)
              {
                  var validar = DataAccess.Seguimiento.SeguimientoData.GetValidarDireccion(idcliente, codigo);
                  if(validar.iddireccion > 0)
                  return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
              }

              var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDireccion(model);

              return Json(new { res = true, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
          }

          [HttpPost]
          public ActionResult GridSave(FormCollection formcollection)
          {
              int? id = null;
              int idproveedor = int.Parse(formcollection["razonsocial"]);
              int? idcliente = null;
              if(Session["idcliente"] != null )
              idcliente = Convert.ToInt32(Session["idcliente"].ToString());

              ProveedorClienteModel model = new ProveedorClienteModel();
              model.idproveedor = idproveedor;
              model.idcliente = idcliente.Value;

              var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarProveedorCliente(model);

              return Json(new { res = true, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
          }

          [HttpPost]
          public ActionResult EliminarProveedor(int idproveedor)
          {
              var result = new DataAccess.Seguimiento.SeguimientoData().EliminarProveedor(idproveedor); ;

              return Json(new { msnj = "OK" }, JsonRequestBehavior.AllowGet);
          }

          public PartialViewResult DireccionesModal(int idcliente)
          {
              DireccionModel model = new DireccionModel();

              Session["idcliente"] = idcliente;

              var result = DataAccess.Seguimiento.SeguimientoData.GetObtenerCliente(idcliente);
              model.cliente = result.razonsocial;
              return PartialView("_AgregarDireccionModal", model);
          }

          public ActionResult EliminarDireccion(int iddireccion)
          {
              try
              {
                  var idcliente = Convert.ToInt32(Session["idcliente"]);

                  DireccionModel model = new DireccionModel();
                  model.iddireccion = iddireccion;
                  model.activo = false;

                  var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDireccion(model);
                  return Json(new { res = true }, JsonRequestBehavior.AllowGet);
              }
              catch (Exception ex)
              {
                  ModelState.AddModelError("", ex.InnerException.ToString());
                  return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
              }
          }

        #endregion cliente

        #region Zonas

          public ActionResult Zona()
          {
              return View();
          }

          public JsonResult JsonGetListarDistritoxZonaEditar(int? idprovincia, int? idzona, string sord, int page, int rows)
          {
              var result = DataAccess.Seguimiento.SeguimientoData.GetListarDistritoZonaEditar(idzona, idprovincia);
              Session["idzona"] = idzona;

              var listadoTotal = result;
              int pageindex = page - 1;
              int pagesize = rows;

              int totalrecord = listadoTotal.Count();
              var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

              if (sord.ToUpper() == "DESC")
              {
                  listadoTotal = listadoTotal.OrderByDescending(s => s.distrito).ToList();
                  listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
              }
              else
              {
                  listadoTotal = listadoTotal.OrderBy(s => s.distrito).ToList();
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

          public JsonResult JsonGetListarDistritoxZona(int? idzona, string sord, int page, int rows)
          {
              var result = DataAccess.Seguimiento.SeguimientoData.GetListarDistritoZona(idzona);
              Session["idzona"] = idzona;
              List<string> distritos = new List<string>();
              foreach (var item in result)
              {
                  distritos.Add(item.iddistrito.ToString());
              }
              Session["distritos"] = distritos;

              var listadoTotal = result;
              int pageindex = page - 1;
              int pagesize = rows;

              int totalrecord = listadoTotal.Count();
              var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

              if (sord.ToUpper() == "DESC")
              {
                  listadoTotal = listadoTotal.OrderByDescending(s => s.iddistrito).ToList();
                  listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
              }
              else
              {
                  listadoTotal = listadoTotal.OrderByDescending(s => s.iddistrito).ToList();
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
          public JsonResult JsonGetListarZona(string criterio)
          {
              if (criterio == string.Empty) criterio = null;
              var draw = Request.Form.GetValues("draw").FirstOrDefault();
              var start = Request.Form.GetValues("start").FirstOrDefault();
              var length = Request.Form.GetValues("length").FirstOrDefault();
              var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
              var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
              var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

              var listado = DataAccess.Seguimiento.SeguimientoData.GetListarZona(criterio);

              if (sortColumn != "" && sortColumnDir != "")
              {
                  if (sortColumnDir.ToUpper() == "DESC")
                  {
                      var parametro = sortColumn;
                      var propertyInfo = typeof(ListarZonasDto).GetProperty(parametro);
                      listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                  }
                  else
                  {
                      var parametro = sortColumn;
                      var propertyInfo = typeof(ListarZonasDto).GetProperty(parametro);
                      listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                  }
              }

              var displayedDocumentos = listado;
              int pageSize = length != null ? Convert.ToInt32(length) : 0;
              int skip = start != null ? Convert.ToInt32(start) : 0;
              int recordsTotal = 0;
              recordsTotal = displayedDocumentos.Count();
              var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

              return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
          }

          public PartialViewResult AgregarZonaModal()
          {
              Session["distritos"] = null;
              operacionModel oOperacion = new operacionModel();
              var departamentos = GetListarDepartamento_Cache();
              var listadepartamentos = new SelectList(
                  departamentos,
                  "iddepartamento",
                  "departamento");
              ViewData["ListadoDepartamento"] = listadepartamentos;
              return PartialView("_AgregarZonaModal");
          }

          [HttpPost]
          public ActionResult AgregarZonaModal(Seguimiento.Models.Seguimiento.ZonaModel Modelo )
          {
              var respuesta = string.Empty;
              List<string> distritos = new List<string>();
              if(Modelo.distritos != null)
              {
                  string[] distritosaux = Modelo.distritos.Split(',');
                  distritos = distritosaux.ToList();
              }

              try
              {
                  var distritosseleccionados = distritos; //(List<string>)Session["distritos"];
                  var validar = DataAccess.Seguimiento.SeguimientoData.validarZona(Modelo.zona, Modelo.idzona);

                  if (Modelo.idzona == null)
                  {
                      if (validar.idzona != null)
                          return Json(new { res = false, mensaje = "Ya existe una zona con este nombre." }, JsonRequestBehavior.AllowGet);
                  }

                  var zona = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarZona(Modelo, distritosseleccionados);
                  return Json(new { res = true }, JsonRequestBehavior.AllowGet);
              }
              catch (Exception ex)
              {
                  ModelState.AddModelError("", ex.InnerException.ToString());
                  return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
              }
          }

          public ActionResult ListarProvincias(int? iddepartamento)
          {
              var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(iddepartamento.Value).ToList();
              var listaprovincias = new SelectList(
                  provincias,
                  "idprovincia",
                  "provincia");
              ViewData["ListadoProvincias"] = listaprovincias;
              return Json(listaprovincias, JsonRequestBehavior.AllowGet);
          }

          public ActionResult ListarDistritos(int? idprovincia)
          {
              var distritos = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(idprovincia.Value).ToList();
              var listadistritos = new SelectList(
                  distritos,
                  "iddistrito",
                  "distrito");
              ViewData["ListaDistrito"] = listadistritos;
              return Json(listadistritos, JsonRequestBehavior.AllowGet);
          }

          public JsonResult JsonGetListarDistritos(int? idprovincia, string sord, int page, int rows)
          {
              var result = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(idprovincia);
              Session["iddistrito"] = idprovincia;

              var listadoTotal = result;
              int pageindex = page - 1;
              int pagesize = rows;

              int totalrecord = listadoTotal.Count();
              var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

              if (sord.ToUpper() == "DESC")
              {
                  listadoTotal = listadoTotal.OrderByDescending(s => s.distrito).ToList();
                  listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
              }
              else
              {
                  listadoTotal = listadoTotal.OrderBy(s => s.distrito).ToList();
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

          public ActionResult AgregarDistrito(int? iddistrito)
          {
              List<string> distritos ;
              if( (List<string>) Session["distritos"] == null)
              {
                  distritos= new List<string>();
                  distritos.Add(iddistrito.ToString());
              }
              else
              {
                   distritos = (List<string>)Session["distritos"];
                   var res = distritos.Where(x => x.Equals(iddistrito.Value.ToString())).SingleOrDefault();
                   if (res == null)
                       distritos.Add(iddistrito.ToString());
                   else
                       distritos.Remove(iddistrito.Value.ToString());
              }
              Session["distritos"] = distritos;

              return Json(distritos, JsonRequestBehavior.AllowGet);
          }

          public PartialViewResult EditarZonaModal(int id)
          {
              var result = DataAccess.Seguimiento.SeguimientoData.GetListarDistritoZona(id);
              List<string> distritos = new List<string>();
              foreach (var item in result)
              {
                  distritos.Add(item.iddistrito.ToString());
              }
              Session["distritos"] = distritos;

              var departamentos = GetListarDepartamento_Cache();
              var listadepartamentos = new SelectList(
                  departamentos,
                  "iddepartamento",
                  "departamento");
              ViewData["ListadoDepartamento"] = listadepartamentos;

              var model = new ZonaModel();
              var ozona = new DataAccess.Seguimiento.SeguimientoData().GetZona(id);
              model.idzona = ozona.idzona;
              model.zona = ozona.zona;

              return PartialView("_EditarZonaModal", model);
          }

        #endregion Zonas

        #region combos

          public IEnumerable<ClienteModel> GetListarClientes_Cache()
          {
              //var clientes = HttpContext.Cache.Get("Clientes") as List<ListarClientesDto>;
              //if (HttpContext.Cache["Clientes"] == null)
              //{
              //    clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
              //    HttpContext.Cache.Insert("Clientes", clientes, null, DateTime.Now.AddSeconds(1500), Cache.NoSlidingExpiration);
              //}
              //else
              //    clientes = (List<ListarClientesDto>)HttpContext.Cache["Clientes"];

              return DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
          }

        public List<ListarDepartamentosDto> GetListarDepartamento_Cache()
          {
              var departamentos = HttpContext.Cache.Get("Departamentos") as List<ListarDepartamentosDto>;
              if (HttpContext.Cache["Departamentos"] == null)
              {
                  departamentos = DataAccess.Seguimiento.SeguimientoData.GetListarDepartamento();
                  HttpContext.Cache.Insert("Departamentos", departamentos, null, DateTime.Now.AddSeconds(1500), Cache.NoSlidingExpiration);
              }
              else
                  departamentos = (List<ListarDepartamentosDto>)HttpContext.Cache["Departamentos"];

              return departamentos;
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

        public PartialViewResult GetControlDetailsGrid_Direccion(string control, string id)
        {
            if (control == "departamento")
            {
                var departamentos = GetListarDepartamento_Cache();
                var listadepartamentos = new SelectList(
                   departamentos,
                   "iddepartamento",
                   "departamento");
                ViewData["ListaDepartamento"] = listadepartamentos;
            }
            if (control == "provincia")
            {
                //var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerDireccion(Convert.ToInt32(id));

                if (id == "")
                    id = "1";
                var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(tarifa.iddepartamento);
                var listaprovincias = new SelectList(
                    provincias,
                    "idprovincia",
                    "provincia");
                ViewData["ListaProvincia"] = listaprovincias;

                //traer tarifa
                //var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
            }
            if (control == "distrito")
            {
                if (id == "") id = "1";
                var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerDireccion(Convert.ToInt32(id));
                if (id == "")
                    id = "1";
                //var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                var distritos = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(tarifa.idprovincia);
                var listadistritos = new SelectList(
                   distritos,
                   "iddistrito",
                   "distrito");
                ViewData["ListaDistrito"] = listadistritos;
            }
            return PartialView("_controlgrid", control);
        }

          public PartialViewResult GetControlDetailsGrid(string control, string id)
          {
              if (control == "razonsocial")
              {
                  int idcliente = Convert.ToInt32(Session["idcliente"]);
                  var proveedores = DataAccess.Seguimiento.SeguimientoData.GetClienteProveedorPendientes(idcliente);
                  var listaproveedor = new SelectList(
                      proveedores,
                      "idcliente",
                      "razonsocial");
                  ViewData[LISTAPROVEEDOR] = listaproveedor;
              }
              if (control == "departamento")
              {
                  var departamentos = GetListarDepartamento_Cache();
                  var listadepartamentos = new SelectList(
                     departamentos,
                     "iddepartamento",
                     "departamento");
                  ViewData["ListaDepartamento"] = listadepartamentos;
              }
              if (control == "provincia")
              {
                  var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                  if (id == "")
                      id = "1";
                  var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(tarifa.iddepartamento);
                    var listaprovincias = new SelectList(
                        provincias,
                        "idprovincia",
                        "provincia");
                    ViewData["ListaProvincia"] = listaprovincias;

                  //traer tarifa
                  //var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
              }
              if (control == "origenprovincia")
              {
                  if (id == "") id = "1";
                  else
                  {
                      //traer tarifa
                      var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                      var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(tarifa.idorigendepartamento);
                      var listaprovincias = new SelectList(
                         provincias,
                         "idprovincia",
                         "provincia");
                      ViewData["ListaProvincia"] = listaprovincias;
                  }
              }
              if (control == "distrito")
              {
                  if (id == "") id = "1";
                  var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                  if (id == "")
                      id = "1";
                  //var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                  var distritos = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(tarifa.idprovincia);
                  var listadistritos = new SelectList(
                     distritos,
                     "iddistrito",
                     "distrito");
                  ViewData["ListaDistrito"] = listadistritos;
              }
              if (control == "origendistrito")
              {
                  if (id == "") id = "1";
                  var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));

                  var distritos = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(tarifa.idorigenprovincia);
                  var listadistritos = new SelectList(
                     distritos,
                     "iddistrito",
                     "distrito");
                  ViewData["ListaDistrito"] = listadistritos;
              }
              if (control == "idcobrarpor")
              {
                  var conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ConceptoCobro)).Where(x => x.activo == true).ToList();
                  var listaconceptocobro = new SelectList(
                     conceptocobro,
                     "idvalortabla",
                     "valor");
                  ViewData["ListaConceptoCobro"] = listaconceptocobro;
              }
              if (control == "idservicio")
              {
                  var servicios = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ServicioComplementario)).Where(x => x.activo == true).ToList();
                  var listaservicios = new SelectList(
                     servicios,
                     "idvalortabla",
                     "valor");
                  ViewData["ListaServicio"] = listaservicios;
              }
              if(control == "zona")
              {
                  var zonas = DataAccess.Seguimiento.SeguimientoData.GetListarZona("").ToList();
                  var listazonas = new SelectList(
                     zonas,
                     "idzona",
                     "zona");
                  ViewData["ListaZona"] = listazonas;
              }
              if (control == "formula")
              {
                  var formulas = DataAccess.Seguimiento.SeguimientoData.GetListarFormula("",false).ToList();
                  var listaformula = new SelectList(
                     formulas,
                     "idformula",
                     "nombre");
                  ViewData["ListaFormula"] = listaformula;
              }
              if (control == "tipotransporte")
              {
                  var modotransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte)).Where(x => x.activo == true).ToList();
                  var listamodotransporte = new SelectList(
                     modotransporte,
                     "idvalortabla",
                     "valor");
                  ViewData["ListaTipoTransporte"] = listamodotransporte;
              }
              if (control == "tipounidad")
              {
                  var tipounidad = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Unidad)).Where(x => x.activo == true).ToList();
                  var listatipounidad = new SelectList(
                     tipounidad,
                     "idvalortabla",
                     "valor");
                  ViewData["ListaTipoUnidad"] = listatipounidad;
              }

              if (control == "moneda")
              {
                  var moneda = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Moneda)).Where(x => x.activo == true).ToList();
                  var listamoneda = new SelectList(
                     moneda,
                     "idvalortabla",
                     "valor");
                  ViewData["ListaMoneda"] = listamoneda;
              }

              if (control == "idetapa")
              {
                  var etapas = IncidenciaData.GetListarMaestroIncidencia(null)
                      .Where(x => x.tipo.Equals('M')).ToList();

                  //var etapas = PagoData.GetListarEtapa("", false).Where(x => x.activo != false).ToList();
                  var listaetapa = new SelectList(
                      etapas,
                      "idmaestroincidencia",
                      "descripcion");
                  ViewData["ListaEtapa"] = listaetapa;
              }

              return PartialView("_controlgrid", control);
          }

        #endregion combos

        #region ruta

          public ActionResult Ruta()
          {
              return View();
          }

          [HttpPost]
          public JsonResult JsonGetListarDetalleRuta(int? idruta, string sord, int page, int rows)
          {
              var result = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleRuta(idruta);

              Session["idruta"] = idruta.Value;

              var listadoTotal = result;
              int pageindex = page - 1;
              int pagesize = rows;

              int totalrecord = listadoTotal.Count();
              var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

              if (sord.ToUpper() == "DESC")
              {
                  listadoTotal = listadoTotal.OrderByDescending(s => s.idorden).ToList();
                  listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
              }
              else
              {
                  listadoTotal = listadoTotal.OrderBy(s => s.idorden).ToList();
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
          //public JsonResult JsonGetListarRuta(int? idorigen , int? iddestino, int? idruta)
          public JsonResult JsonGetListarRuta(string criterio)
          {
              if (criterio == string.Empty) criterio = null;

              var draw = Request.Form.GetValues("draw").FirstOrDefault();
              var start = Request.Form.GetValues("start").FirstOrDefault();
              var length = Request.Form.GetValues("length").FirstOrDefault();
              var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
              var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
              var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

              var listado = DataAccess.Seguimiento.SeguimientoData.GetListarRuta(criterio);

              if (sortColumn != "" && sortColumnDir != "")
              {
                  if (sortColumnDir.ToUpper() == "DESC")
                  {
                      var parametro = sortColumn;
                      var propertyInfo = typeof(ListarRutaDto).GetProperty(parametro);
                      listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                  }
                  else
                  {
                      var parametro = sortColumn;
                      var propertyInfo = typeof(ListarRutaDto).GetProperty(parametro);
                      listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                  }
              }

              var displayedDocumentos = listado;
              int pageSize = length != null ? Convert.ToInt32(length) : 0;
              int skip = start != null ? Convert.ToInt32(start) : 0;
              int recordsTotal = 0;
              recordsTotal = displayedDocumentos.Count();
              var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

              return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
          }

          public PartialViewResult EditarRutaModal(int? idruta)
          {
              RutaModel model = new RutaModel();
              var result = DataAccess.Seguimiento.SeguimientoData.GetObtenerRuta(idruta.Value);
              model.idruta = result.idruta;
              model.nombre = result.nombre;

              return PartialView("_EditarRutaModal", model);
          }

          public PartialViewResult AgregarRutaModal()
          {
              return PartialView("_AgregarRutaModal");
          }

          public PartialViewResult AgregarRutaDetalleModal(int? idruta)
          {
              Session["distritos"] = null;

              operacionModel oOperacion = new operacionModel();
              var departamentos = GetListarDepartamento_Cache();
              var listadepartamentos = new SelectList(
                  departamentos,
                  "iddepartamento",
                  "departamento");
              ViewData["ListadoDepartamento"] = listadepartamentos;

              var tipostransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte)).Where(x => x.activo == true).ToList();
              var listatipos = new SelectList(
                  tipostransporte,
                  "idvalortabla",
                  "valor");
              ViewData["ListaTipoTransporte"] = listatipos;

              var etapas = PagoData.GetListarEtapa("", false).ToList();
              var listaetapas = new SelectList(
                  etapas,
                  "idetapa",
                  "etapa");
              ViewData["ListaEtapas"] = listaetapas;

              var ruta = DataAccess.Seguimiento.SeguimientoData.GetObtenerRuta(idruta.Value);

              RutaModel model = new RutaModel();
              model.nombre = ruta.nombre;

              return PartialView("_AgregarRutaDetalleModal",model);
          }

          [HttpPost]
          public ActionResult AgregarRutaModal(Models.Seguimiento.RutaModel Modelo)
          {
              var respuesta = string.Empty;
              //Modelo.idproveedor = Usuario.Idusuario;
              try
              {
                  var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarRuta(Modelo);
                  return Json(new { res = true }, JsonRequestBehavior.AllowGet);
              }
              catch (Exception ex)
              {
                  ModelState.AddModelError("", ex.InnerException.ToString());
                  return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
              }
          }

          [HttpPost]
          public ActionResult AgregarDetalleRutaModal(Models.Seguimiento.RutaModel Modelo)
          {
              var respuesta = string.Empty;

              //Modelo.idproveedor = Usuario.Idusuario;
              try
              {
                  var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDetalleRuta(Modelo);
                  return Json(new { res = true }, JsonRequestBehavior.AllowGet);
              }
              catch (Exception ex)
              {
                  ModelState.AddModelError("", ex.InnerException.ToString());
                  return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
              }
          }

        public ActionResult JsonCargarCombosGrillas()
        {
            var departamentos = GetListarDepartamento_Cache();
              var listadepartamentos = new SelectList(
                  departamentos,
                  "iddepartamento",
                  "departamento");
              ViewData["ListadoDepartamento"] = listadepartamentos;

              var tipostransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte)).Where(x => x.activo == true).ToList();
              var listatipos = new SelectList(
                  tipostransporte,
                  "idvalortabla",
                  "valor");
              ViewData["ListaTipoTransporte"] = listatipos;

              var etapas = PagoData.GetListarEtapa("", false).ToList();
              var listaetapas = new SelectList(
                  etapas,
                  "idetapa",
                  "etapa");
              ViewData["ListaEtapa"] = listaetapas;

              return Json(new { resA = listadepartamentos, resB = listatipos, resC = listaetapas }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarDetalleRuta(int? id)
        {
            try
            {
                var result = DataAccess.Seguimiento.SeguimientoData.EliminarDetalleRuta(id.Value);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveDetalles(string iddetalleruta
            , string iddistrito
            , string idorigen
            ,string km
            , string idtipotransporte
            ,string lida
            ,string lvuelta
            ,string ldocu
            , string idruta, string etapas)
        {
            var algo = etapas;

            RutaModel Modelo = new RutaModel();
            Modelo.iddetalleruta =Convert.ToInt32(iddetalleruta);
            Modelo.iddistrito = Convert.ToInt32(iddistrito);
            Modelo.idorigen = Convert.ToInt32(idorigen);
            Modelo.idtipotransporte = Convert.ToInt32(idtipotransporte);
            Modelo.idtipotransporte = Convert.ToInt32(idtipotransporte);
            Modelo.leadida = lida;
            Modelo.leadretorno = lvuelta;
            Modelo.leaddocumentario = ldocu;
            Modelo.km = km;
            Modelo.idruta = Convert.ToInt32(idruta);

            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDetalleRuta(Modelo);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarRuta(int id)
        {
            var result = new DataAccess.Seguimiento.SeguimientoData().EliminarRuta(id); ;
            return Json(new { msnj = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GridSaveRuta(FormCollection formcollection)
        {
            string id = "";
            id = Session["idruta"].ToString();

            int iddetalleruta = int.Parse(formcollection["iddetalleruta"]);
            int orden = int.Parse(formcollection["idorden"]);
            int? origen = null;
            if (formcollection["origen"] != "")
                origen = int.Parse(formcollection["origen"]);

            var algo = formcollection["etapas"];

            int? departamento = null;
            if (formcollection["departamento"] != "")
                departamento = int.Parse(formcollection["departamento"]);

            int? provincia = null;
            if (formcollection["provincia"] != "")
                provincia = int.Parse(formcollection["provincia"]);

            int? distrito = null;
            if (formcollection["distrito"] != "")
                distrito = int.Parse(formcollection["distrito"]);

            string km = formcollection["km"];

            int? tipotransporte = null;
            if (formcollection["tipotransporte"] != "")
                tipotransporte = int.Parse(formcollection["tipotransporte"]);

            //string urgente = formcollection["urgente"].ToString();
            string leadida = formcollection["leadida"].ToString();
            string leadretorno = formcollection["leadretorno"].ToString();
            string leaddocumentario = formcollection["leaddocumentario"].ToString();
            //string etapas = formcollection["etapas"].ToString();

            RutaModel model = new RutaModel();
            //comando.etapas = comando.etapas.Substring(0, comando.etapas.Length - 1);
            model.idorigen = origen.Value ;
            model.km = km;
            model.iddistrito = distrito;
            model.idorden =  orden;
            model.idtipotransporte = tipotransporte.Value;
            model.leaddocumentario = leaddocumentario;
            model.leadida = leadida;
            model.leadretorno = leadretorno;
            model.iddetalleruta = iddetalleruta;
            model.idruta =Convert.ToInt32(id);
            model.idprovincia = provincia;
            model.iddepartamento = departamento.Value;

            var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDetalleRuta(model);

            return Json(new { res = true, Errors = ModelState.Errors() });
            //return View();
        }

        public PartialViewResult GetControlDetailsGrid_ruta(string control, string id)
        {
            if (control == "razonsocial")
            {
                int idcliente = Convert.ToInt32(Session["idcliente"]);
                var proveedores = DataAccess.Seguimiento.SeguimientoData.GetClienteProveedorPendientes(idcliente);
                var listaproveedor = new SelectList(
                    proveedores,
                    "idcliente",
                    "razonsocial");
                ViewData[LISTAPROVEEDOR] = listaproveedor;
            }
            if (control == "departamento")
            {
                var departamentos = GetListarDepartamento_Cache();
                var listadepartamentos = new SelectList(
                   departamentos,
                   "iddepartamento",
                   "departamento");
                ViewData["ListaDepartamento"] = listadepartamentos;
            }
            if (control == "provincia")
            {
                var tarifa = DataAccess.Seguimiento.SeguimientoData.ObtenerDetalleRutaResult(Convert.ToInt32(id));
                if (id == "0")
                    id = "1";
                else
                    id = tarifa.iddepartamento.ToString();

                var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(Convert.ToInt32(id));
                var listaprovincias = new SelectList(
                    provincias,
                    "idprovincia",
                    "provincia");
                ViewData["ListaProvincia"] = listaprovincias;

                //traer tarifa
             //   var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
            }
            if (control == "origenprovincia")
            {
                if (id == "") id = "1";
                else
                {
                    //traer tarifa
                    var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                    var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(tarifa.idorigendepartamento);
                    var listaprovincias = new SelectList(
                       provincias,
                       "idprovincia",
                       "provincia");
                    ViewData["ListaProvincia"] = listaprovincias;
                }
            }
            if (control == "distrito")
            {
                var tarifa = DataAccess.Seguimiento.SeguimientoData.ObtenerDetalleRutaResult(Convert.ToInt32(id));
                if (id == "0")
                    id = "1";
                else
                    id = tarifa.idprovincia.ToString();

                var distritos = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(Convert.ToInt32(id));
                var listadistritos = new SelectList(
                   distritos,
                   "iddistrito",
                   "distrito");
                ViewData["ListaDistrito"] = listadistritos;
            }
            if (control == "origendistrito")
            {
                if (id == "") id = "1";
                var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));

                var distritos = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(tarifa.idorigenprovincia);
                var listadistritos = new SelectList(
                   distritos,
                   "iddistrito",
                   "distrito");
                ViewData["ListaDistrito"] = listadistritos;
            }
            if (control == "idcobrarpor")
            {
                var conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ConceptoCobro)).Where(x => x.activo == true).ToList();
                var listaconceptocobro = new SelectList(
                   conceptocobro,
                   "idvalortabla",
                   "valor");
                ViewData["ListaConceptoCobro"] = listaconceptocobro;
            }
            if (control == "idservicio")
            {
                var servicios = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ServicioComplementario)).Where(x => x.activo == true).ToList();
                var listaservicios = new SelectList(
                   servicios,
                   "idvalortabla",
                   "valor");
                ViewData["ListaServicio"] = listaservicios;
            }
            if (control == "zona")
            {
                var zonas = DataAccess.Seguimiento.SeguimientoData.GetListarZona("").ToList();
                var listazonas = new SelectList(
                   zonas,
                   "idzona",
                   "zona");
                ViewData["ListaZona"] = listazonas;
            }
            if (control == "formula")
            {
                var formulas = DataAccess.Seguimiento.SeguimientoData.GetListarFormula("", false).ToList();
                var listaformula = new SelectList(
                   formulas,
                   "idformula",
                   "nombre");
                ViewData["ListaFormula"] = listaformula;
            }
            if (control == "tipotransporte")
            {
                var modotransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte)).Where(x => x.activo == true).ToList();
                var listamodotransporte = new SelectList(
                   modotransporte,
                   "idvalortabla",
                   "valor");
                ViewData["ListaTipoTransporte"] = listamodotransporte;
            }
            if (control == "tipounidad")
            {
                var tipounidad = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Unidad)).Where(x => x.activo == true).ToList();
                var listatipounidad = new SelectList(
                   tipounidad,
                   "idvalortabla",
                   "valor");
                ViewData["ListaTipoUnidad"] = listatipounidad;
            }

            if (control == "moneda")
            {
                var moneda = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Moneda)).Where(x => x.activo == true).ToList();
                var listamoneda = new SelectList(
                   moneda,
                   "idvalortabla",
                   "valor");
                ViewData["ListaMoneda"] = listamoneda;
            }

            if (control == "idetapa")
            {
                var etapas = IncidenciaData.GetListarMaestroIncidencia(null)
                    .Where(x => x.tipo.Equals('M')).ToList();

                //var etapas = PagoData.GetListarEtapa("", false).Where(x => x.activo != false).ToList();
                var listaetapa = new SelectList(
                    etapas,
                    "idmaestroincidencia",
                    "descripcion");
                ViewData["ListaEtapa"] = listaetapa;
            }

            return PartialView("_controlgrid", control);
        }

        #endregion ruta

        #region Tarifa

        //[OutputCache(Duration = 10, VaryByParam = "none",Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Tarifa()
        {
            var clientes = GetListarClientes_Cache();

            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View();
        }

        public JsonResult JsonGetListarTarifas(int? id, string sidx,string sord, int page, int rows)
        {
            if(!id.HasValue)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            Session["idcliente"] = id;

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarTarifas(id.Value);
            Session["idcliente"] = id;

            var listadoTotal = listado;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sidx != "" && sord != "")
            {
                if (sord.ToUpper() == "DESC")
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(ListarTarifaDto).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(ListarTarifaDto).GetProperty(parametro);
                    listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }

            listado = listado.Skip(pageindex * pagesize).Take(pagesize).ToList();

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GridSaveTarifa(FormCollection formcollection)
        {
            int? id = null;
            int idtarifa = int.Parse(formcollection["idtarifa"]);
            int origen = int.Parse(formcollection["origen"]);

            int? origenprovincia = null;
            if (formcollection["origenprovincia"] != "")
                origenprovincia = int.Parse(formcollection["origenprovincia"]);

            int? origendistrito = null;
            if (formcollection["origendistrito"] != "")
                origendistrito = int.Parse(formcollection["origendistrito"]);

            int? zona = null;
            if(formcollection["zona"] != "")
              zona = int.Parse(formcollection["zona"]);

            int? provincia = null;
            if (formcollection["provincia"] != "")
                provincia = int.Parse(formcollection["provincia"]);

            int? departamento = null;
            if (formcollection["departamento"] != "")
                departamento = int.Parse(formcollection["departamento"]);

            int? distrito = null;
            if(formcollection["distrito"] != "")
                distrito = int.Parse(formcollection["distrito"]);

            int formula = int.Parse(formcollection["formula"]);

            //string urgente = formcollection["urgente"].ToString();
            var concepto = formcollection["conceptos"];
            //string val = formcollection["val"].ToString();

            int tipotransporte = int.Parse(formcollection["tipotransporte"]);
            int tipounidad = int.Parse(formcollection["tipounidad"]);
            int moneda = int.Parse(formcollection["moneda"]);

            decimal? montobase = null;
            if(formcollection["montobase"]!= "")
                montobase = decimal.Parse(formcollection["montobase"]);

            decimal? minimo = null;
            if (formcollection["minimo"] != "")
                minimo = decimal.Parse(formcollection["minimo"]);

            decimal? desde = null;
            if (formcollection["desde"] != "")
                desde = decimal.Parse(formcollection["desde"]);

            decimal? hasta = null;
            if (formcollection["hasta"] != "")
                hasta = decimal.Parse(formcollection["hasta"]);

            decimal? adicional = null;
            if(formcollection["adicional"] != "")
                adicional = decimal.Parse(formcollection["adicional"]);

            decimal precio = decimal.Parse(formcollection["precio"]);
            int? idcliente = null;
            if (Session["idcliente"] != null)
                idcliente = Convert.ToInt32(Session["idcliente"].ToString());

            TarifaModel model = new TarifaModel();
            model.idcliente = idcliente.Value;
            model.idzona = zona;

            model.desde = desde;
            model.hasta = hasta;
            model.idprovincia = provincia;
            model.idorigenprovincia = origenprovincia;
            model.idorigendistrito = origendistrito;
            model.iddepartamento = departamento;
            model.iddistrito = distrito;
            model.idformula = formula;
            model.idmoneda = moneda;
            model.idorigen = origen;
            model.idtarifa = idtarifa;
            model.idtipotransporte = tipotransporte;
            model.idtipounidad = tipounidad;
            model.minimo = minimo;
            model.montobase = montobase;
            model.precio = precio;
            model.conceptos = new List<TarifaDetalleModel>();
            model.adicional = adicional;

            string[] aux = concepto.Split(',');
            foreach (var item in aux)
	        {
                if(item!="")
                model.conceptos.Add(new TarifaDetalleModel() {  idconceptocobro = Convert.ToInt32(item)  });
	        }

            //model.val =Convert.ToBoolean(val);

            var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarTarifa(model);

            return Json(new { res = true, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            //return View();
        }

        [HttpPost]
        public ActionResult EliminarTarifa(int id)
        {
            var result = new DataAccess.Seguimiento.SeguimientoData().EliminarTarifa(id); ;
            return Json(new { msnj = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CopiarTarifa(int id, int idoriginal)
        {
            var result =  DataAccess.Seguimiento.SeguimientoData.CopiarTarifa(id,idoriginal);
            return Json(new { msnj = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CopiarTarifaIndividual ( int id )
        {
            try
            {
                var result = DataAccess.Seguimiento.SeguimientoData.CopiarTarifaIndividual(id);
                return Json(new { mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { mensaje = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Tarifa

        #region operacioncarga

        public ActionResult Operaciones()
        {
            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            CargaModel model = new CargaModel();

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            if (usuario.idestacionorigen != null)
                model.idestacion = usuario.idestacionorigen.Value;

            return View(model);
        }

        //GetListarPlanificarOrden

        public ActionResult PlanificarCarga()
        {
            Session["otsa"] = null;
            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var modotransporte = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte)).Where(x => x.activo == true).ToList();
            var listamodotransporte = new SelectList(
               modotransporte,
               "idvalortabla",
               "valor");
            ViewData["ListaTipoTransporte"] = listamodotransporte;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var tipooperacion = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoOperacion)).Where(x => x.activo == true).ToList();
            var listatipooperacion = new SelectList(
               tipooperacion,
               "idvalortabla",
               "valor");
            ViewData["ListaTipoOperacion"] = listatipooperacion;

            var agencia = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Agencia)).Where(x => x.activo == true).ToList();
            var listaagencia = new SelectList(
               agencia,
               "idvalortabla",
               "valor");
            ViewData["ListaAgencia"] = listaagencia;

            var ruta = DataAccess.Seguimiento.SeguimientoData.GetListarRuta("");
            var listaruta = new SelectList(
            ruta,
            "idruta",
            "nombre");
            ViewData["ListaRuta"] = listaruta;

            var ubigeos = GetListarUbigeo_Cache();
            var listaubigeo = new SelectList(
                ubigeos,
                "iddistrito",
                "ubigeo");
            ViewData["ListaUbigeo"] = listaubigeo;

            CargaModel model = new CargaModel();

             var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

             if (usuario.idestacionorigen!=null)
             model.idestacion = usuario.idestacionorigen.Value;

             return View(model);
        }

        public JsonResult GetListarPlanificarOrden(int? idestacionorigen
            , int? idcliente
            , int? iddestino
            , int? idtipotransporte,
            string sidx, string sord, int page, int rows)
        {
            int idestado =(Int32) DataAccess.Constantes.EstadoOT.PendienteProgramacion;

            List<ObtenerOrdenTrabajoResult> ordenes = new List<ObtenerOrdenTrabajoResult>();
            if (Session["otsa"] != null)
                ordenes = (List<ObtenerOrdenTrabajoResult>)Session["otsa"];

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarPlanificarOrden(idestacionorigen, idcliente, iddestino, idestado, idtipotransporte);
            List<long> ids = new List<long>();

                if (ordenes.Count != 0)
                {
                    foreach (var item in listado)
                    {
                        if (ordenes.Where(x => x.idordentrabajo.Equals(item.idordentrabajo)).ToList().Count != 0)
                            ids.Add(item.idordentrabajo);
                    }
                }
                foreach (var item in ids)
	            {
                    var eliminar = listado.Where(x => x.idordentrabajo.Equals(item)).SingleOrDefault();
                    listado.Remove(eliminar);
	            }

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
                    var propertyInfo = typeof(ListarPlanificarOrdenDto).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(ListarPlanificarOrdenDto).GetProperty(parametro);
                    listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }

            listado = listado.Skip(pageindex * pagesize).Take(pagesize).ToList();

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarOrdenAgregada(long id)
        {
            int cantidad = 0, bultos = 0;
            decimal peso = 0 ,subtotal = 0;
            List<ObtenerOrdenTrabajoResult> ordenes = new List<ObtenerOrdenTrabajoResult>();

            if (Session["otsa"] != null)
                ordenes = (List<ObtenerOrdenTrabajoResult>)Session["otsa"];

            ordenes.Remove(ordenes.Where(x => x.idordentrabajo.Equals(id)).SingleOrDefault());
            Session["otsa"] = ordenes;

            foreach (var item in ordenes)
            {
                cantidad = cantidad + 1;
                bultos = bultos + (item.bulto == null ? 0 : Convert.ToInt32(item.bulto));
                peso = peso + (item.peso == null ? 0 : Convert.ToDecimal(item.peso));
                subtotal = subtotal + (item.subtotal == null ? 0 : Convert.ToDecimal(item.subtotal));
            }

            return Json(new { subtotal = subtotal, cantidad = cantidad, peso = peso, bultos = bultos, resp = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult validarordenes(string ids,int idtipooperacion
            , int? idagencia , int? idestacion, int? idruta,string operacion)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetAgregarOrdenesPendientes(ids);

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            foreach (var item in result)
            {
                if (idruta != null)
                {
                    var rutas = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleRuta(idruta.Value);

                    var coherencia = rutas.Where(x => x.iddistrito.Equals(item.iddestino)).SingleOrDefault();
                    if(coherencia == null)
                    {
                        var ubigeo = GetListarUbigeo_Cache().Where(x => x.iddistrito.Equals(item.iddestino)).SingleOrDefault();
                        coherencia = rutas.Where(x => x.idprovincia.Equals(ubigeo.idprovincia)).FirstOrDefault();
                        if (coherencia == null)
                        {
                            try
                            {
                                coherencia = rutas.Where(x => x.iddepartamento == ubigeo.iddepartamento).FirstOrDefault();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                            if(coherencia == null)
                              return Json(new { msj = "La ruta original de la orden y la seleccionada no tiene coherencia - Orden: " + item.numcp, resp = false }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (usuario.idestacionorigen != null)
                {
                    if (item.idestacionorigen != usuario.idestacionorigen)
                    {
                        return Json(new { msj = "Una o mas órdenes seleccionadas no corresponde a su estación origen - Orden: " + item.numcp, resp = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            //Calcular totales
            List<ObtenerOrdenTrabajoResult> ordenes = new List<ObtenerOrdenTrabajoResult>();
            int cantidad = 0, bultos = 0;
            decimal peso = 0, subtotal = 0;
            if (Session["otsa"] != null)
                ordenes = (List<ObtenerOrdenTrabajoResult>)Session["otsa"];
            foreach (var item in result)
            {
                var existe = ordenes.Where(x => x.idordentrabajo.Equals(item.idordentrabajo)).SingleOrDefault();
                if (existe == null)
                {
                    item.idtipooperacion = idtipooperacion;
                    item.idagencia = idagencia;
                    item.idestaciondestino = idestacion;
                    item.idruta = idruta;
                    item.operacion = operacion;

                    ordenes.Add(item);
                }
            }

            Session["otsa"] = ordenes;

            foreach (var item in ordenes)
            {
                cantidad = cantidad + 1;
                bultos = bultos + (item.bulto == null ? 0 : Convert.ToInt32(item.bulto));
                peso = peso + (item.peso == null ? 0 : Convert.ToDecimal(item.peso));
                subtotal = subtotal + (item.subtotal == null ? 0 : Convert.ToDecimal(item.subtotal));
            }
            return Json(new {subtotal = subtotal, cantidad = cantidad ,  peso = peso, bultos = bultos, resp = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListarPlanificarOrdenAgregadas( string sord, int page, int rows)
        {
            List<ObtenerOrdenTrabajoResult> ordenes = new List<ObtenerOrdenTrabajoResult>();

            if (Session["otsa"] != null)
                ordenes = (List<ObtenerOrdenTrabajoResult>)Session["otsa"];

            var listadoTotal = ordenes;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idordentrabajo).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idordentrabajo).ToList();
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
        public JsonResult PlanificarCargaOp(CargaModel Modelo)
        {
            Modelo.activo = true;
            string ids = string.Empty;
            try
            {
                if (Session["otsa"] == null)
                {
                    return Json(new { res = false, mensaje = "No existen ordenes para planificar." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ots = (List<ObtenerOrdenTrabajoResult>)Session["otsa"];
                    Modelo.ordenes = new List<OrdenTrabajoModel>();
                    OrdenTrabajoModel orden;
                    ;
                    foreach (var item in ots)
                    {
                        orden = new OrdenTrabajoModel();
                        orden.idordentrabajo = item.idordentrabajo;

                        orden.idestado = (Int32) DataAccess.Constantes.EstadoOT.PendienteInicioCarga;
                        orden.idtipooperacion = item.idtipooperacion;
                        orden.idestaciondestino = item.idestaciondestino;
                        orden.idruta = item.idruta;
                        orden.idagencia = item.idagencia;
                        //uso de variables auxiliares (Solo para calcular el total - No actualiza valores)
                        orden.pesogeneral = item.peso;
                        orden.volgeneral = item.volumen;
                        orden.fechaplanificacion = DateTime.Now;
                        ////////////////////////////////////////////////////////////////////////////

                        orden.activo = true;
                        ids = ids + "," + item.idordentrabajo;

                        Modelo.ordenes.Add(orden);
                    }
                    ids = ids.Substring(1, ids.Length - 1);
                }
                Modelo._tipooperacion = 3;
                Modelo.idplanificador =(Int32)Usuario.Idusuario;
                string numcarga = "";

                   var carga = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(Modelo, out numcarga);

                    IncidenciaModel modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SePlanifico;
                    modIncidencia.idsorden = ids;
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se planificó la Orden  - N° Carga :" + numcarga;
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                return Json(new { res = true , idcarga = carga.idcarga}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        public JsonResult JsonGetListarOperacionCarga(string numcp, string numcarga, int? idestacion,string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionCarga(numcp, numcarga, (Int32)DataAccess.Constantes.EstadoCarga.Pendiente, idestacion);

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.fecharegistro).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.fecharegistro).ToList();
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

            //if (numcp == string.Empty) numcp = null;
            //if (numcarga == string.Empty) numcarga = null;
            //var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //var start = Request.Form.GetValues("start").FirstOrDefault();
            //var length = Request.Form.GetValues("length").FirstOrDefault();
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            //var listado = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionCarga(numcp, numcarga, (Int32)DataAccess.Constantes.EstadoCarga.Pendiente, idestacion);

            //var displayedDocumentos = listado;
            //int pageSize = length != null ? Convert.ToInt32(length) : 0;
            //int skip = start != null ? Convert.ToInt32(start) : 0;
            //int recordsTotal = 0;
            //recordsTotal = displayedDocumentos.Count();
            //var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            //return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetListarOperacionDetalleVehiculo(int idcarga,string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionesVehiculoDetalle(idcarga);

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.fechacreacion).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.fechacreacion).ToList();
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

        //[HttpPost]
        public JsonResult JsonGetListarOperacionDetalle(long? idcarga, string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(idcarga);

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderBy(s => s.idordentrabajo).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.idordentrabajo).ToList();
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

        public JsonResult AnularOperacion (long? idcarga)
        {
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);
            string respuesta = "";
            CargaModel modelcarga;
            var detalle = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(idcarga);
            foreach (var item in detalle)
            {
                OrdenData.DesasociarOrdenCarga(item.idordentrabajo);
            }
            modelcarga = new CargaModel();
            modelcarga.idcarga = idcarga.Value;
            modelcarga._tipooperacion = 1;
            var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(modelcarga, out respuesta);
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AsignarVehiculo(CargaModel model)
        {
            try
            {
                string respuesta = "";
                model.activo = true;
                model._tipooperacion = 2;
                var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(model, out respuesta);

                Web.TYS.Areas.Seguimiento.Models.Seguimiento.VehiculoModel vmodel = new VehiculoModel();
                vmodel._tipoop = 3;
                vmodel.usuarioasignado = Convert.ToInt32(Usuario.Idusuario);
                vmodel.idvehiculo = model.idvehiculo;

                var res = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarVehiculo(vmodel);

                var operaciones = SeguimientoData.GetListarOperacionxVehiculo(model.idvehiculo.Value);
                string cargas = "";
                foreach (var item in operaciones)
                {
                    cargas = cargas + "," + item.numcarga + " - ("  +  item.origen +   ")";
                }
                if (cargas.Length > 0)
                    cargas = cargas.Substring(1, cargas.Length - 1);
                else cargas = "Sin asignaciones";

                return Json(new { res = true
                    , idcarga = model.idcarga
                    , cargas = cargas
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult AsignarVehiculo(long? idcarga)
        {
            var placas = PagoData.GetListarPlaca(null)
                .Where(x => x.idestado.Equals((Int32)DataAccess.Constantes.EstadoVehiculo.Inspeccionado)).ToList();

            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var muelles = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.Muelle))).ToList();
            var listamuelles = new SelectList(
                muelles,
                "idvalortabla",
                "valor");
            ViewData["ListaMuelles"] = listamuelles;

            var result  = DataAccess.Seguimiento.SeguimientoData.GetObtenerOperacion(idcarga.Value);
            CargaModel model = new CargaModel();
            model.idcarga = result.idcarga;
            model.numcarga = result.numcarga;
            model.peso = result.peso;
            model.vol = result.vol;

            return PartialView("_AsignarCargaVehiculo", model);
        }

        [HttpPost]
        public JsonResult AsignarVehiculoManifiesto(int idvehiculo)
        {
            List<string> TOTAL = new List<string>();
            var ots_reg = new List<OrdenTrabajoModel>();
            var modelorden = new OrdenTrabajoModel();
            var ots_agr = new List<ObtenerOrdenTrabajoResult>();
            var detalles = new List<OrdenTrabajoModel>();
            string respuesta, numhojaruta = string.Empty, ids = string.Empty;

            if (Session["otsa"] == null)
                return Json(new { res = false, mensaje = "No existen ordenes para planificar." }, JsonRequestBehavior.AllowGet);
            else
                ots_agr.AddRange((List<ObtenerOrdenTrabajoResult>)Session["otsa"]);

            var cargas = SeguimientoData.GetListarOperacionxVehiculo(idvehiculo);

            foreach (var item in cargas)
            {
                detalles.AddRange(SeguimientoData.GetListarOperacionesDetalle_Model(item.idcarga));
            }
            var manifiestos = detalles.GroupBy(x => x.idmanifiesto.Value).ToList();

            foreach (var manifiesto in manifiestos)
            {
                var obj = SeguimientoData.GetObtenerManifiesto(manifiesto.Key);
                numhojaruta= obj.numhojaruta;

                ots_reg = new List<OrdenTrabajoModel>();
                var primeragrupado = detalles.Where(x => x.idtipooperacion == obj.idtipooperacion
                    && x.iddestino == obj.iddestino).ToList();

                ots_reg.AddRange(primeragrupado);

               var pen_agregar = ots_agr.Where(x => x.idtipooperacion == obj.idtipooperacion
                && x.iddestino == obj.iddestino).ToList();

                foreach (var item in pen_agregar)
                {
                    ots_agr.Remove(item);
                }

                //agregar a ots_reg a pen_agre
               if (pen_agregar.Count > 0)
               {
                   Mapper.CreateMap<ObtenerOrdenTrabajoResult, OrdenTrabajoModel>();
                   var ots_agr_aux = Mapper.Map<IEnumerable<ObtenerOrdenTrabajoResult>, IEnumerable<OrdenTrabajoModel>>(pen_agregar);
                   long idcarga = ots_reg[0].idcarga.Value;
                   int idtipooperacion = ots_reg[0].idtipooperacion.Value;
                   long iddespacho = ots_reg[0].iddespacho.Value;

                   ots_agr_aux.ToList().ForEach(x => x.iddespacho = iddespacho);

                   ots_reg.AddRange(ots_agr_aux);

                   long new_id =   new SeguimientoData().GenerarManifiestosOts(ots_reg, idvehiculo, idcarga, Usuario.Idusuario, obj.numhojaruta, idtipooperacion);
                   ids = ids + "," + new_id.ToString();

                    ManifiestoModel modelmanifiesto = new ManifiestoModel();
                    modelmanifiesto.idmanifiesto = manifiesto.Key ;
                    modelmanifiesto._tipoop = 2;
                    SeguimientoData.InsertarActualizarManifiesto(modelmanifiesto, out respuesta);
               }
            }
            if(ots_agr.Count >0)
            {
                Mapper.CreateMap<ObtenerOrdenTrabajoResult, OrdenTrabajoModel>();
                var ots_agr_aux = Mapper.Map<IEnumerable<ObtenerOrdenTrabajoResult>, IEnumerable<OrdenTrabajoModel>>(ots_agr);
                long idcarga = ots_reg[0].idcarga.Value;
                int idtipooperacion = ots_reg[0].idtipooperacion.Value;
                long iddespacho = ots_reg[0].iddespacho.Value;

                ots_agr_aux.ToList().ForEach(x => x.iddespacho = iddespacho);

               long new_manifiesto =  new SeguimientoData().GenerarManifiestosOts(ots_agr_aux
                    , idvehiculo
                    , idcarga
                    , Usuario.Idusuario
                    , numhojaruta
                    , idtipooperacion);
               ids = ids + "," + new_manifiesto.ToString();
            }
            ids = ids.Substring(1, ids.Length - 1);

            var ids_aux =  ids.Split(',');
            List<long> ids_final = new List<long>();
            foreach (var item in ids_aux)
            {
                ids_final.Add(Convert.ToInt64(item));
            }

            return Json(new { res = true, ids = ids_final, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AsignarServicios(CargaModel model)
        {
            try
            {
                string respuesta = "";
                model.activo = true;
                model._tipooperacion = 2;
                var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(model, out respuesta);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult AsignarServicios(long? idcarga)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetObtenerOperacion(idcarga.Value);
            CargaModel model = new CargaModel();
            model.idcarga = result.idcarga;
            model.numcarga = result.numcarga;
            model.peso = result.peso;
            model.vol = result.vol;

            Session["idcarga"] = idcarga;

            return PartialView("_AsignarServicios", model);
        }

        [HttpPost]
        public JsonResult ConfirmarCarga(CargaModel model)
        {
            string ids = string.Empty;
            try
            {
                   string respuesta = "";
                   var usuario =  DataAccess.Seguridad.UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);

                    //Traer operacion
                    var operacion = DataAccess.Seguimiento.SeguimientoData.GetObtenerOperacion(model.idcarga.Value);
                    //Validar si tiene vehiculo asignado
                    if (operacion.idvehiculo == null)
                        return Json(new { res = false, mensaje = "Debe asignar un vehiculo" }, JsonRequestBehavior.AllowGet);

                    //Traer Ordenes de Transporte asociadas
                    var detalle = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(model.idcarga.Value);
                    List<ListarDetalleOperacionDto> detallefinal = new List<ListarDetalleOperacionDto>();

                    model.ordenes = new List<OrdenTrabajoModel>();
                    string[] ids_confirmados = model.ids.Split(',');

                    foreach (var item in ids_confirmados)
                    {
                        var remove = detalle.Where(x => x.idordentrabajo.Equals(Convert.ToInt64(item))).Single();

                        detallefinal.Add(remove);
                        detalle.Remove(remove);
                    }
                    foreach (var item in detalle)
                    {
                        OrdenData.DesasociarOrdenCarga(item.idordentrabajo);
                    }
                    decimal peso = 0, volumen = 0;
                    foreach (var item in detallefinal)
                    {
                        peso = peso + item.peso;
                        volumen = volumen + item.volumen;
                    }
                    CargaModel cargamodel = new CargaModel();
                    cargamodel.activo = false;
                    cargamodel.idcarga = model.idcarga.Value;
                    cargamodel._tipooperacion = 6;
                    cargamodel.peso = peso;
                    cargamodel.vol = volumen;
                    DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(cargamodel, out respuesta);

                   // foreach (var item in detalle)
                    foreach (var item in ids_confirmados)
                    {
                        model.ordenes.Add(new OrdenTrabajoModel {
                              idordentrabajo = Convert.ToInt64(item)
                            , fechaconfirmacion = DateTime.Now
                        });
                        ids = ids + "," + item;
                    }
                    ids = ids.Substring(1, ids.Length - 1);
                    //Actualizar carga
                    model._tipooperacion = 4;
                    model.usrregistrocarga = (Int32)Usuario.Idusuario;
                    DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(model, out respuesta);

                    IncidenciaModel modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeConfirmoCarga;
                    modIncidencia.idsorden = ids;
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se confirmó la carga";
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult ConfirmarCarga(long? idcarga, string ids)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetObtenerOperacion(idcarga.Value);
            CargaModel model = new CargaModel();
            model.idcarga = result.idcarga;
            model.numcarga = result.numcarga;
            model.peso = result.peso;
            model.vol = result.vol;
            model.placa = result.placa;
            model.proveedor = result.proveedor;
            model.nombrechofer = result.chofer;
            model.ids = ids;

            model.horaconfirmacion = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechaconfirmacion = DateTime.Now;

            Session["idcarga"] = idcarga;

            return PartialView("_ConfirmarCarga", model);
        }

        public JsonResult JsonGetServicios(long? idcarga, string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarServicios(idcarga.Value);

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idserviciooperacion).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.idserviciooperacion).ToList();
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

        public JsonResult EliminarServicio(long idcarga , int idserviciooperacion)
        {
            ServicioModel model = new ServicioModel();
            model.idserviciooperacion = idserviciooperacion;
            model.idcarga = idcarga;

            var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarServicio(model);

            return Json(new { res = true, msj = "Se realizó la operación con exito" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GridSaveServicios(FormCollection formcollection)
        {
            var idserviciooperacion = Convert.ToInt64(Session["idserviciooperacion"]);

            int idservicio = int.Parse(formcollection["servicio"]);
            int cantidad = int.Parse(formcollection["cantidad"]);

            ServicioModel model = new ServicioModel();
            model.idserviciooperacion = idserviciooperacion;
            model.idservicio = idservicio;
            model.idcarga = (long)Session["idcarga"];
            model.cantidad = cantidad;

            var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarServicio(model);

            return Json(new { res = true, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult VerDetalleOperacion(long? idcarga)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetObtenerOperacionTotalVehiculo(idcarga.Value);
            CargaModel model = new CargaModel();

            model.idcarga = result.idcarga;
            model.numcarga = result.numcarga;
            model.peso = result.peso;
            model.vol = result.vol;
            model.proveedor = result.proveedor;
            model.placa = result.placa;
            model.tipo = result.tipo;
            model.pesoporcentaje = result.pesoporcentaje.ToString("#.##");
            model.volporcentaje = result.volporcentaje.ToString("#.##");
            model.nombrechofer = result.chofer;
            model.planificador = result.planificador;
            model.cargautil = result.cargautil;
            model.volumenvehiculo = result.volumenvehiculo;
            model.serviciosadicionales = result.serviciosadicionales;

            return PartialView("_VerDetalle", model);
        }

        public PartialViewResult AsociarManifiestoVehiculo()
        {
            var placas = PagoData.GetListarPlaca(null)
               .Where(x => x.idestado.Equals((Int32)DataAccess.Constantes.EstadoVehiculo.Asignado)).ToList();

            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            return PartialView("_AsignarManifiestoVehiculo");
        }

        public bool desasociarOrdenes(string ordenes, long idcarga, out string respuesta)
        {
            var objordenes = ordenes.Split(',');
            decimal peso = 0, volumen = 0;

            //Desasociar las ordenes de la carga.
            foreach (var item in objordenes)
            {
                OrdenData.DesasociarOrdenCarga(Convert.ToInt64(item));
            }
            //Obtener el listado de las ordenes de la carga restantes.
            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(idcarga);

            //Si no restan Ots en la carga, elimina la carga.
            if (listado.Count == 0)
            {
                CargaModel cargamodel = new CargaModel();
                cargamodel.activo = false;
                cargamodel.idcarga = idcarga;
                cargamodel._tipooperacion = 1;
                DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(cargamodel, out respuesta);
                respuesta = "Se la carga al no tener mas OTs asociadas.";
                return false;
            }
            else
            {   // De lo contrario suma las OTs restantes de la carga y actualiza sus datos
                foreach (var item in listado)
                {
                    peso = peso + item.peso;
                    volumen = volumen + item.volumen;
                }
                CargaModel cargamodel = new CargaModel();
                cargamodel.idcarga = idcarga;
                cargamodel._tipooperacion = 6;
                cargamodel.peso = peso;
                cargamodel.vol = volumen;
                DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(cargamodel, out respuesta);
            }
            respuesta = "Se eliminó satisfactoriamente";
            return true;
        }
        [HttpPost]
        public JsonResult DesasociarOrdenes (string ordenes, long idcarga)
        {
            string respuesta = "";
            desasociarOrdenes(ordenes, idcarga, out respuesta);
            return Json(new { res = true, msj = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsongenerarguiatransportistaxOT(string guia, long idorden)
        {
            //Traer Ordenes de Transporte asociadas
            //var detalle = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(idcarga);
            string[] guia_aux = guia.Split('-');
            long numero = Convert.ToInt64(guia_aux[1]);
            OrdenData.ActualizarGuiaTransportista(idorden, guia_aux[0] + '-' + numero);
            //foreach (var item in detalle)
            //{
            //    var result = DataAccess.Seguimiento.SeguimientoData.ListarDetalleOperacionxRuta(item.idruta, item.idorigen, item.iddestino, null, null);
            //    if (result.Count == 0)
            //    {
            //        var ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
            //        var provincia = ubigeo.Where(x => x.iddistrito.Equals(item.iddestino)).SingleOrDefault().idprovincia;

            //        result = DataAccess.Seguimiento.SeguimientoData.ListarDetalleOperacionxRuta(item.idruta, item.idorigen, null, provincia, null);
            //        if (result.Count == 0)
            //        {
            //            var departamento = ubigeo.Where(x => x.iddistrito.Equals(item.iddestino)).SingleOrDefault().iddepartamento;
            //            result = DataAccess.Seguimiento.SeguimientoData.ListarDetalleOperacionxRuta(item.idruta, item.idorigen, null, null, departamento);
            //        }
            //    }
            //    if (result.Count != 0)
            //        item.idposicion = result[0].idorden;
            //    else
            //        item.idposicion = 1;
            //}
            //List<string> guias = new List<string>();

            //foreach (var item in detalle.OrderBy(x => x.idposicion))
            //{
            //    guias.Add(guia_aux[0] + '-' + numero);

            //    numero = numero + 1;

            //}

            return Json(new { res = true, guias = guia_aux[0] + '-' + numero }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Jsongenerarguiastransportista(string guia, long idcarga)
        {
            //Traer Ordenes de Transporte asociadas
            var detalle = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(idcarga);
            string[] guia_aux = guia.Split('-');
            long numero = Convert.ToInt64(guia_aux[1]);
            foreach (var item in detalle)
            {
                 var result = DataAccess.Seguimiento.SeguimientoData.ListarDetalleOperacionxRuta(item.idruta, item.idorigen, item.iddestino,null,null);
                 if(result.Count == 0)
                 {
                     var ubigeo =  DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();
                     var provincia = ubigeo.Where(x => x.iddistrito.Equals(item.iddestino)).SingleOrDefault().idprovincia;

                     result = DataAccess.Seguimiento.SeguimientoData.ListarDetalleOperacionxRuta(item.idruta, item.idorigen, null, provincia, null);
                     if(result.Count == 0)
                     {
                         var departamento = ubigeo.Where(x => x.iddistrito.Equals(item.iddestino)).SingleOrDefault().iddepartamento;
                         result = DataAccess.Seguimiento.SeguimientoData.ListarDetalleOperacionxRuta(item.idruta, item.idorigen, null, null, departamento);
                     }
                 }
                 if (result.Count != 0)
                     item.idposicion = result[0].idorden;
                 else
                     item.idposicion = 1;
            }
            List<string> guias = new List<string>();
            IncidenciaModel modIncidencia = null;
            foreach (var item in detalle.OrderBy(x=>x.idposicion))
            {
                guias.Add(guia_aux[0] + '-' + numero);
                OrdenData.ActualizarGuiaTransportista(item.idordentrabajo, guia_aux[0] + '-' + numero);

                //insertar incidencia
                modIncidencia = new IncidenciaModel();
                modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.GRTGenerada;
                modIncidencia.idsorden = item.idordentrabajo.ToString();
                modIncidencia.fechaincidencia = DateTime.Now;
                modIncidencia.fecharegistro = DateTime.Now;
                modIncidencia.descripcion = "Se asoció la GRT: " + guia_aux[0] + '-' + numero;
                modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                modIncidencia.activo = true;
                IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                numero = numero + 1;
            }

            return Json(new { res = true, guias = guias.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        #endregion operacioncarga

        #region Despacho

        public ActionResult Despacho()
        {
            Session["ordenesvalija"] = null;
            return View();
        }

        public JsonResult ValidarCargasPendientes(int idvehiculo)
        {
            //validar si existen pendientes de confirmación.

            var result = SeguimientoData.GetListarCargasPendientes(idvehiculo);
            if(result.Count > 0)
                return Json(new { res = false, msj = "Existen cargas pendientes para este vehículo" }, JsonRequestBehavior.AllowGet);

            return Json(new { res = true, msj = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarExistePrecintosValijas(int idvehiculo)
        {
            //validar si existe manifiesto que es necesario para los precintos y las valijas

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDespacho(idvehiculo
                , (Int32)DataAccess.Constantes.EstadoCarga.Confirmada, null,null );

            if (listado[0].iddespacho  == 0)
                return Json(new { res = false, msj = "No existe manifiesto, no puede continuar" }, JsonRequestBehavior.AllowGet);
            else
            {
                if(listado[0].cantidadprecintos == 0)
                return Json(new { res = false, msj = "No asignó precintos, no puede continuar" }, JsonRequestBehavior.AllowGet);
                if(listado[0].cantidadvalija == 0)
                    return Json(new { res = false, msj = "No tiene valija, no puede continuar" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { res = true, msj = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarExisteManifiesto2(int?  idvehiculo)
        {
            //validar si existe manifiesto que es necesario para los precintos y las valijas

           var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDespacho(idvehiculo, (Int32)DataAccess.Constantes.EstadoCarga.Confirmada, null,null);

            if (listado[0].iddespacho  == 0)
                return Json(new { res = false, msj = "No existe manifiesto, no puede continuar" }, JsonRequestBehavior.AllowGet);

            return Json(new { res = true, msj = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarExisteManifiesto(long idorden)
        {
            //validar si existe manifiesto que es necesario para los precintos y las valijas

            var orden = OrdenData.GetObtenerOrden(idorden);

            if (orden.idmanifiesto == null)
                return Json(new { res = false, msj = "No existe manifiesto, no puede continuar" }, JsonRequestBehavior.AllowGet);

            return Json(new { res = true, msj = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAnularManifiesto(long idorden)
        {
            new SeguimientoData().AnularManifiesto(idorden, Usuario.Idusuario);
            return Json(new { res = true}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGenerarManifiesto(int idvehiculo)
        {
            try
            {
                var despacho = DataAccess.Seguimiento.SeguimientoData.GetObtenerDespacho((int)DataAccess.Constantes.EstadoDespacho.Creado, idvehiculo);
                if (despacho != null)
                    return Json(new { res = false, msj = "Ya se generó el Manifiesto" }, JsonRequestBehavior.AllowGet);

                var ids = new SeguimientoData().GenerarManifiesto(idvehiculo, Usuario.Idusuario);
                return Json(new { res = true , ids = ids.ToArray() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReporteManifiesto(long idmanifiesto)
        {
            TempData["Reporte"] = "/Reportes TYS/Impresiones/RPManifiesto";
            TempData["idmanifiesto"] = idmanifiesto;
            //TempData["fechafin"] = fechafin;
            //TempData["idproveedor"] = idproveedor;
            //TempData["iddestino"] = iddestino;

            return PartialView("_ReporteManifiesto");
        }

        public PartialViewResult AsignarPrecinto(int? idvehiculo)
        {
            var despacho = DataAccess.Seguimiento.SeguimientoData.GetObtenerDespacho(Convert.ToInt32(DataAccess.Constantes.EstadoDespacho.Creado), idvehiculo);

            PrecintoModel model = new PrecintoModel();
            model.idvehiculo = idvehiculo.Value;
            model.ListaAccesorios = GetListaAccesorios(despacho.iddespacho);

            model.AccesoriosSeleccionados = GetListarSeleccionados(idvehiculo);

            return PartialView("_AsignarPrecintos", model);
        }

        public PartialViewResult TransbordoVehiculo(int? idvehiculo)
        {
            var placas = PagoData.GetListarPlaca(null)
                 .Where(x => x.idestado.Equals((Int32)DataAccess.Constantes.EstadoVehiculo.Inspeccionado)).ToList();

            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            DespachoModel model = new DespachoModel();
            model.idvehiculo_old = idvehiculo.Value;

            return PartialView("_TransbordoVehiculo", model);
        }

        [HttpPost]
        public JsonResult TransbordoVehiculo(DespachoModel model)
        {
            var resp =  DataAccess.Seguimiento.SeguimientoData.Transbordar(model.idvehiculo_new, model.idvehiculo_old);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        private string[] GetListarSeleccionados(int? idvehiculo)
        {
            var despacho = DataAccess.Seguimiento.SeguimientoData.GetObtenerDespacho((int)DataAccess.Constantes.EstadoDespacho.Creado, idvehiculo);

            List<SelectListItem> ListaAccesorios = new List<SelectListItem>();
            var Lista = DataAccess.Seguimiento.SeguimientoData.GetListarPrecintosCarga(despacho.iddespacho);

            string[] listado = new string[Lista.Count];
            int i = 0;
            foreach (var Accesorio in Lista)
            {
                //ListaAccesorios.Add(new SelectListItem { Value = Accesorio.idprecinto.ToString(), Text = Accesorio.precinto });

                listado[i] = Accesorio.idprecinto.ToString();
                i = i + 1;
            }
            return listado;
        }

        public PartialViewResult AsignarValijas(int? idvehiculo)
        {
                Session["ordenesvalija"] = null;
                var despacho = DataAccess.Seguimiento.SeguimientoData.GetListarDespacho(idvehiculo, (Int32)DataAccess.Constantes.EstadoCarga.Confirmada, null,null).First();
                valijaModel model = new valijaModel();

                model.cantidadordenes = despacho.cantidadvalija;
                model.idvehiculo = despacho.idvehiculo;
                return PartialView("_AgregarValijas", model);
        }

        private IEnumerable<SelectListItem> GetListaAccesorios(long iddespacho)
        {
            List<SelectListItem> ListaAccesorios = new List<SelectListItem>();
            var Lista = DataAccess.Seguimiento.SeguimientoData.GetListarPrecintos(iddespacho);

            foreach (var Accesorio in Lista)
            {
                ListaAccesorios.Add(new SelectListItem { Value = Accesorio.idprecinto.ToString(), Text = Accesorio.precinto });
            }
            return ListaAccesorios.AsEnumerable();
        }

        public JsonResult GuardarAsignarPrecinto(int idvehiculo, string seleccionados)
        {
            var despacho = DataAccess.Seguimiento.SeguimientoData.GetObtenerDespacho((int)DataAccess.Constantes.EstadoDespacho.Creado, idvehiculo);

            DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCargaPrecinto(idvehiculo, seleccionados, despacho.iddespacho);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult SalidaVehiculo(int? idvehiculo)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(idvehiculo.Value);

            DespachoModel model = new DespachoModel();
            model.idvehiculo = idvehiculo.Value;
            model.peso = 0;
            model.vol = 0;
            foreach (var item in result)
            {
                model.peso = model.peso + item.peso;
                model.vol = model.vol + item.volumen;
            }

            model.horasalida = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fechasalida = DateTime.Now;

            Session["idvehiculo"] = idvehiculo;

            return PartialView("_SalidaVehiculo", model);
        }

        //[HttpPost]
        public JsonResult JsonGetListarOperacionDespacho(string numcp, string numcarga, string sidx,string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListarDespacho(null
                , (Int32)DataAccess.Constantes.EstadoCarga.Confirmada, numcarga, numcp);

            var listadoTotal = result;
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
                    var propertyInfo = typeof(ListarDespachoDto).GetProperty(parametro);
                    listadoTotal = listadoTotal.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(ListarDespachoDto).GetProperty(parametro);
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

        [HttpPost]
        public JsonResult SalidaVehiculo (DespachoModel model)
        {
            string ids = string.Empty;
            try
            {
                string respuesta = "";
                var detalle = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(model.idvehiculo);
                //Actualizar despacho
                model.iddespacho = detalle[0].iddespacho;
                model._tipoop = 2; // salidavehiculo
                model.idusuariosalida = Convert.ToInt32(Usuario.Idusuario);

                var hm = model.horasalida.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
                model.fechasalida = model.fechasalida.Value.Date + ts;

                DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDespacho(model);

                //Traer Ordenes de Transporte asociadas
                //var detalles = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(model.idcarga.Value);
                VehiculoModel modelvehiculo = new VehiculoModel();
                modelvehiculo._tipoop = 5;
                modelvehiculo.idvehiculo = model.idvehiculo;
                modelvehiculo.idestado = (int)DataAccess.Constantes.EstadoVehiculo.EnRuta;

                DataAccess.Seguimiento.SeguimientoData.InsertarActualizarVehiculo(modelvehiculo);

                var operaciones = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionxVehiculo(model.idvehiculo);
                    //.Where(x => x.idestado == (Int32)Constantes.EstadoCarga.Confirmada);

                CargaModel modelcarga;
                foreach (var operacion in operaciones)
                {
                    modelcarga = new CargaModel();

                    modelcarga.fechasalida = model.fechasalida;
                    modelcarga.idcarga = operacion.idcarga;
                    modelcarga.idestado = (Int32)DataAccess.Constantes.EstadoCarga.EnRuta;

                    modelcarga.ordenes = new List<OrdenTrabajoModel>();
                    var detoperacion = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(modelcarga.idcarga);

                    foreach (var item in detoperacion)
                    {
                        modelcarga.ordenes.Add(new OrdenTrabajoModel
                        {
                            idordentrabajo = item.idordentrabajo
                            ,
                            fechaconfirmacion = DateTime.Now
                        });
                        ids = ids + "," + item.idordentrabajo;
                    }

                    ids = ids.Substring(1, ids.Length - 1);
                    modelcarga._tipooperacion = 5;
                    var resultoperacion = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(modelcarga, out respuesta);
                }

                IncidenciaModel modIncidencia = new IncidenciaModel();
                modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeDespacho;
                modIncidencia.idsorden = ids;
                modIncidencia.fechaincidencia = DateTime.Now;
                modIncidencia.fecharegistro = DateTime.Now;
                modIncidencia.descripcion = "Se despachó la Orden";
                modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                modIncidencia.activo = true;
                IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                return Json(new { res = true, idvehiculo = model.idvehiculo, iddespacho = model.iddespacho }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonGetListarDespachoDetalle(int? idvehiculo, string sidx ,string sord, int page, int rows)
        {
            var despacho = DataAccess.Seguimiento.SeguimientoData.GetListarDespacho(idvehiculo, (Int32)DataAccess.Constantes.EstadoCarga.Confirmada,null,null).FirstOrDefault();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(idvehiculo);

            if (despacho != null)
            {
                if (despacho.cantidadvalija > 0)
                    listado.ForEach(x => { x.valija = true; });
                else
                {
                    var ordenes = (List<string>)Session["ordenesvalija"];
                    if (ordenes != null)
                    {
                        foreach (var item in ordenes)
                        {
                            listado.Where(x => x.numcp == item).Single().valija = true;
                        }
                    }
                }
            }
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
                    var propertyInfo = typeof(ListarDetalleDespachoDto).GetProperty(parametro);
                    listadoTotal = listadoTotal.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(ListarDetalleDespachoDto).GetProperty(parametro);
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

        #endregion Despacho

        #region precintos

        public ActionResult Precintos()
        {
            return View();
        }

        [HttpPost]
        public JsonResult JsonGetListarPrecintos()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarPrecintos(1);

            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarPrecintos(PrecintoModel model)
        {
            string precinto = model.precinto;
            int cantidad = model.cantidad;

            var result = DataAccess.Seguimiento.SeguimientoData.InsertarPrecinto(precinto, cantidad);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarPrecinto(long idprecinto)
        {
            var result =  SeguimientoData.EliminarPrecinto(idprecinto);
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion precintos

        #region vehiculo

        public ActionResult Vehiculo()
        {
            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados(Convert.ToInt16(Constantes.MaestroTablas.Vehiculo));
            var listaestados = new SelectList(
                estados,
                "idestado",
                "estado");
            ViewData["ListadoEstados"] = listaestados;

            return View();
        }

        public JsonResult JsonGetListarInspeccion(int? idvehiculo, string sord, int page, int rows)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetListaInspeccion(idvehiculo.Value);

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderBy(s => s.idinspeccion).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.idinspeccion).ToList();
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
        public ActionResult JsonGetListarVehiculo(string placa, int? idestado)
        {
            if (placa == string.Empty) placa = null;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarVehiculos(placa,idestado);

            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult  AgregarVehiculoModal()
        {
            var proveedores = PagoData.GetListarProveedores("", false);
            var listaproveedor = new SelectList(
                proveedores,
                "idproveedor",
                "razonsocial");
            ViewData["ListadoProveedores"] = listaproveedor;

            var tipovehiculo = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoVehiculo)).Where(x => x.activo == true).ToList();
            var listatipovehiculo = new SelectList(
                tipovehiculo,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoVehiculo"] = listatipovehiculo;

            var marca = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.MarcaVehiculo)).Where(x => x.activo == true).ToList();
            var listamarca = new SelectList(
                marca,
                "idvalortabla",
                "valor");
            ViewData["ListadoMarca"] = listamarca;

            var modelo = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModeloVehiculo)).Where(x => x.activo == true).ToList();
            var listamodelo = new SelectList(
                modelo,
                "idvalortabla",
                "valor");
            ViewData["ListadoModelo"] = listamodelo;

            var color = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ColorVehiculo)).Where(x => x.activo == true).ToList();
            var listacolor = new SelectList(
                color,
                "idvalortabla",
                "valor");
            ViewData["ListadoColor"] = listacolor;

            var combustible = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Combustible)).Where(x => x.activo == true).ToList();
            var listacombustible = new SelectList(
                combustible,
                "idvalortabla",
                "valor");
            ViewData["ListadoCombustible"] = listacombustible;

            return PartialView("_AgregarVehiculoModal");
        }

        [HttpPost]
        public ActionResult AgregarVehiculoModal(Web.TYS.Areas.Seguimiento.Models.Seguimiento.VehiculoModel model)
        {
            var respuesta = string.Empty;
            model._tipoop = 1;
            model.usuarioinspeccion = Convert.ToInt32(Usuario.Idusuario);
            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarVehiculo(model);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult VincularChoferModal(int? id)
        {
            var chofer = DataAccess.Seguimiento.SeguimientoData.GetListarChofer(null);
            var listachofer = new SelectList(
                chofer,
                "idchofer",
                "nombrechofer");
            ViewData["ListadoChofer"] = listachofer;

            ChoferModel model = new ChoferModel();
            model.idvehiculo = id;

            return PartialView("_VincularVehiculoModal", model);
        }

        [HttpPost]
        public JsonResult VincularChoferModal(ChoferModel model)
        {
            VehiculoModel vehiculo = new VehiculoModel();
            vehiculo.idchofer = model.idchofer;
            vehiculo.idvehiculo = model.idvehiculo;
            vehiculo._tipoop = 4;

            var respuesta = string.Empty;
            try
            {
                var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarVehiculo(vehiculo);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.ToString());
                return Json(new { res = false, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult InspeccionarVehiculoModal(int? id)
        {
            var vehiculo = OrdenData.GetObtenerVehiculo(id);
            VehiculoModel model = new VehiculoModel();
            model.placa = vehiculo.placa;
            model.idvehiculo = id;
            model.inspecciones = vehiculo.inspecciones;

            return PartialView("_InspeccionarVehiculoModal", model);
        }

        public PartialViewResult EditarVehiculoModal(int? id)
        {
            var vehiculo = OrdenData.GetObtenerVehiculo(id);

            var proveedores = PagoData.GetListarProveedores("", false);
            var listaproveedor = new SelectList(
                proveedores,
                "idproveedor",
                "razonsocial");
            ViewData["ListadoProveedores"] = listaproveedor;

            var tipovehiculo = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoVehiculo)).Where(x => x.activo == true).ToList();
            var listatipovehiculo = new SelectList(
                tipovehiculo,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoVehiculo"] = listatipovehiculo;

            var marca = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.MarcaVehiculo)).Where(x => x.activo == true).ToList();
            var listamarca = new SelectList(
                marca,
                "idvalortabla",
                "valor");
            ViewData["ListadoMarca"] = listamarca;

            var modelo = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ModeloVehiculo)).Where(x => x.activo == true).ToList();
            var listamodelo = new SelectList(
                modelo,
                "idvalortabla",
                "valor");
            ViewData["ListadoModelo"] = listamodelo;

            var color = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.ColorVehiculo)).Where(x => x.activo == true).ToList();
            var listacolor = new SelectList(
                color,
                "idvalortabla",
                "valor");
            ViewData["ListadoColor"] = listacolor;

            var combustible = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Combustible)).Where(x => x.activo == true).ToList();
            var listacombustible = new SelectList(
                combustible,
                "idvalortabla",
                "valor");
            ViewData["ListadoCombustible"] = listacombustible;

            VehiculoModel model = new VehiculoModel();
            model.idvehiculo = vehiculo.idvehiculo;
            model.idproveedor = vehiculo.idproveedor;
            //model.activo = vehiculo.activo;
            model.cargautil = vehiculo.cargautil;
            model.confveh = vehiculo.confveh;
            model.idanio = vehiculo.idanio;
            model.idchofer = vehiculo.idchofer;
            model.idcolor = vehiculo.idcolor;
            model.idcombustible = vehiculo.idcombustible;
            model.idestado = vehiculo.idestado;
            model.idmarca = vehiculo.idmarca;
            model.idmodelo = vehiculo.idmodelo;
            model.idorigen = vehiculo.idorigen;
            model.idproveedor = vehiculo.idproveedor;
            model.idtipo = vehiculo.idtipo;
            model.kilometraje = vehiculo.kilometraje;
            model.pesobruto = vehiculo.pesobruto;
            model.placa = vehiculo.placa;
            model.regmtc = vehiculo.regmtc;
            model.seriemotor = vehiculo.seriemotor;

            return PartialView("_EditarVehiculoModal", model);
        }

        public JsonResult EliminarVehiculo (int idvehiculo)
        {
            VehiculoModel model = new VehiculoModel();
            model.idvehiculo = idvehiculo;
            model._tipoop = 6;
            var proveedor = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarVehiculo(model);
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarInspeccionarVehiculo(string inspeccionados, int idvehiculo)
        {
            VehiculoModel model = new VehiculoModel();
            model.idvehiculo = idvehiculo;
            model.inspecciones = inspeccionados;
            model._tipoop = 2;
            model.usuarioinspeccion = Convert.ToInt32(Usuario.Idusuario);

            var inspecciones =  model.inspecciones.Substring(1,model.inspecciones.Length-1).Split(',');

            var res = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarVehiculo(model);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonLiberarVehiculo(int idvehiculo)
        {
            var operaciones = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionxVehiculo(idvehiculo);
            //.Where(x => x.idestado.Equals((Int32)Constantes.EstadoCarga.Confirmada)).ToList();
            VehiculoModel model = new VehiculoModel();
            model._tipoop = 5; //actualizar estado
            model.activo = true;
            model.idestado = (Int32)Constantes.EstadoVehiculo.Creado;
            model.idvehiculo = idvehiculo;

            if (operaciones.Count > 0)
                return Json(new { res = true, pendiente = true }, JsonRequestBehavior.AllowGet);
            else
            {
                SeguimientoData.InsertarActualizarVehiculo(model);
                return Json(new { res = true, pendiente = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion vehiculo

        #region Monitoreo

        public ActionResult Monitoreo()
        {
            return View();
        }

        #endregion Monitoreo

        #region valijas

        public JsonResult ValidarAsignarValijas(string idorden, int? idvehiculo)
        {
            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(idvehiculo);

            if(listado.Where(x=>x.numcp==idorden).SingleOrDefault() == null)
                return Json(new { res = false, msj = "Esta orden no se encuentra asociada a este vehiculo" }, JsonRequestBehavior.AllowGet);

            var ordenes =   (List<string>) Session["ordenesvalija"] ;
            if (ordenes != null)
            {
                var aux = ordenes.Where(x => x == idorden).SingleOrDefault();
                if (aux == null)
                {
                    ordenes.Add(idorden);
                }
            }
            else
            {
                ordenes = new List<string>();
                ordenes.Add(idorden);
            }
            Session["ordenesvalija"] = ordenes;

            return Json(new { res = true, msj = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarAsignarValijas(int? idvehiculo)
        {
            var despacho = DataAccess.Seguimiento.SeguimientoData.GetObtenerDespacho((int)DataAccess.Constantes.EstadoDespacho.Creado, idvehiculo);
            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(idvehiculo);

            List<string> ids = new List<string>();
            var ordenes = (List<string>)Session["ordenesvalija"];

            if(ordenes == null)
                return Json(new { res = false, msj = "No se ha escaneado ninguna orden" }, JsonRequestBehavior.AllowGet);

            if(ordenes.Count != listado.Count)
                return Json(new { res = false, msj = "No se ha escaneado el total de ordenes, no se puede armar la valija." }, JsonRequestBehavior.AllowGet);

            foreach (var item in ordenes)
            {
                ids.Add(listado.Where(x => x.numcp == item).Single().idordentrabajo.ToString());
            }

            DataAccess.Seguimiento.SeguimientoData.InsertarActualizarValija(ids, despacho.iddespacho);

            return Json(new { res = true, msj = "" }, JsonRequestBehavior.AllowGet);
        }

        #endregion valijas

        #region vistareimpresion

        public ActionResult VistaDespacho()
        {
            ReimpresionModel model = new ReimpresionModel();
            model.fechafin = DateTime.Now.AddDays(1);
            model.fechainicio = DateTime.Now.AddDays(-5);

            return View(model);
        }

        public JsonResult JsonGetListarImpresionCarga(string  numhojaruta, string numcarga ,string sidx, string sord, int page, int rows)
        {
            if (numhojaruta == string.Empty) numhojaruta = null;
            if (numcarga == string.Empty) numcarga = null;

            var result = SeguimientoData.GetListarImpresionCarga(numhojaruta, numcarga);

            var listadoTotal = result;
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
                    var propertyInfo = typeof(ListarDespachoDto).GetProperty(parametro);
                    listadoTotal = listadoTotal.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(ListarDespachoDto).GetProperty(parametro);
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

        public JsonResult JsonGetListarImpresionManifiesto(string fecinicio, string fecfin, string  numhojaruta, string numcarga, string nummanifiesto, string numcp, string numgrt, string sidx, string sord, int page, int rows)
        {
            if (nummanifiesto == string.Empty) nummanifiesto = null;
            if (numcp == string.Empty) numcp = null;
            if (numgrt == string.Empty) numgrt = null;
            if (numhojaruta == string.Empty) numhojaruta = null;
            if (numcarga == string.Empty) numcarga = null;
            if (fecinicio == string.Empty) fecinicio = null;
            if (fecfin == string.Empty) fecfin = null;

            var result = SeguimientoData.GetListarImpresionManifiesto(nummanifiesto, numcp, numgrt,numcarga, numhojaruta, fecinicio, fecfin );

            var listadoTotal = result;
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
                    var propertyInfo = typeof(ListarDespachoDto).GetProperty(parametro);
                    listadoTotal = listadoTotal.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(ListarDespachoDto).GetProperty(parametro);
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

        #endregion vistareimpresion



        #region Chofer

        public PartialViewResult ModalNuevoChofer()
        {
            return PartialView("_AgregarChofer");
        }

        public JsonResult AgregarChofer(ChoferModel model)
        {
            model.activo = true;
            int id = SeguimientoData.AgregarChofer(model);

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }



        #endregion Chofer

        #region SeguimientoCliente
        public ActionResult SeguimientoCliente()
        {
            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            OrdenTrabajoModel model = new OrdenTrabajoModel();

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            if (usuario.idestacionorigen != null)
                model.idestacion = usuario.idestacionorigen.Value;

            model.fechainicio = DateTime.Now.Subtract(TimeSpan.FromDays(31));
            model.fechafin = DateTime.Now;

            return View(model);
        }

        public JsonResult JsonGetListarOrdenes(string numcp, string fecinicio
          , string fecfin,string grr, string sidx, string sord, int page, int rows)
        {
            if (numcp == string.Empty) numcp = null;
            if (fecinicio == string.Empty) fecinicio = null;
            if (fecfin == string.Empty) fecfin = null;

            string idscliente = "62,63";

            var listado = OrdenData.GetListarOrdenesCliente(numcp, fecinicio, fecfin, idscliente,grr);

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
                    var propertyInfo = typeof(OrdenTrabajoModel).GetProperty(parametro);
                    listado = listado.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    var parametro = sidx;
                    var propertyInfo = typeof(OrdenTrabajoModel).GetProperty(parametro);
                    listado = listado.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }

            listado = listado.Skip(pageindex * pagesize).Take(pagesize).ToList();

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion 
    }
}