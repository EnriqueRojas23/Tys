﻿using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Common.Controllers;
using Web.Common.Extensions;
using Web.TYS.Areas.Seguridad.Models.Usuarios;
using Web.TYS.DataAccess;
using Componentes.Common.Utilidades;
using Web.TYS.DataAccess.Seguridad;
using QueryContracts.TYS.Seguridad.Result;
using System.Configuration;
using QueryContracts.TYS.Account.Results;




namespace Web.TYS.Areas.Seguridad.Controllers
{
    public class UsuariosController : BaseController
    {
        #region Listado de Usuarios

        public ActionResult Index()
        {
            return RedirectToAction("ListarUsuarios", "Usuarios", new { area = "seguridad" });
        }

        public ActionResult ListarUsuarios()
        {
            var modelo = new ListarUsuariosModel(true);

            TipoUsuario oTipoUsuario = new TipoUsuario();
            List<TipoUsuario> tipos = new List<TipoUsuario>();
            oTipoUsuario.idtipo = 1;
            oTipoUsuario.tipo = "Interno";
            tipos.Add(oTipoUsuario);
            oTipoUsuario = new TipoUsuario();
            oTipoUsuario.idtipo = 2;
            oTipoUsuario.tipo = "Externo";
            tipos.Add(oTipoUsuario);
           

            var listatipos = new SelectList(
                      tipos,
                      "idtipo",
                      "tipo");
            ViewData["ListadoTipos"] = listatipos;


            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes(null,true).ToList();
            var listaclientes = new SelectList(
                   clientes,
                   "idcliente",
                   "nombrecorto");
            ViewData["ListadoClientes"] = listaclientes;




            return View(modelo);
        }

        [HttpPost]
        public ActionResult ListarUsuariosModel(ListarUsuariosModel modelo)
        {
            //if (!string.IsNullOrEmpty(modelo.SearchDefault))
            //{
            //    modelo.AliasUsuario = string.Empty;
            //    modelo.NombreCompleto = string.Empty;

            //    //analizando el filtro principal.
            //    if (!modelo.SearchDefault.IsFormatSearch())
            //    {
            //        modelo.NombreCompleto = modelo.SearchDefault;
            //        modelo.SearchDefault = "{" + Constantes.Seguridad.Usuario.listadopedido_filtro_nombrecompleto + ":" + modelo.NombreCompleto + "}";
            //    }
            //    else
            //    {
            //        var res = modelo.SearchDefault.FormatSearch();
            //        modelo.NombreCompleto = res.GetOrDefault(Constantes.Seguridad.Usuario.listadopedido_filtro_nombrecompleto);
            //        modelo.AliasUsuario = res.GetOrDefault(Constantes.Seguridad.Usuario.listadopedido_filtro_aliasusuario);
            //        modelo.IdRol = Utilidades.Cast<int?>(res.GetOrDefault(Constantes.Seguridad.Usuario.listadopedido_filtro_rol));
            //    }
            //}
            //modelo.FillSelectList();
            return View(modelo);
        }
        
      //  [HttpPost]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult JsonListarUsuarios( ListarUsuariosModel modelo, FormCollection formcollection)
        public JsonResult JsonListarUsuarios(string nom , int? rol)
        {
            var modelo = new ListarUsuariosModel() { NombreCompleto = nom, IdRol = rol };
            var listadoTotal = UsuariosData.GetListarUsuarios(modelo);
            var resjson1 = (new JqGridExtension<ListarUsuariosDto>()).DataBind(listadoTotal, listadoTotal.Count);
            return resjson1;
           
        }

        #endregion

        #region Asignar Roles a los Usuarios
        public ActionResult AsignarRolesUsuarios(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.Error(new ArgumentNullException("No se ha ingresado el id del usuario"));
                return RedirectToAction("ListarUsuarios", "Usuarios");
            }
            if (!Utilidades.IsNumeric(id))
            {
                this.Error(new InvalidCastException("El id ingresado no cumple el formato numerico requerido"));
                return RedirectToAction("ListarUsuarios", "Usuarios");
            }

            var modelo = GetAsignarRolesUsuariosModel(Convert.ToInt32(id));

            return View(modelo);
        }

        
        [HttpPost]
        public ActionResult AsignarRolesUsuarios(AsignarRolesUsuariosModel modelo, FormCollection formcollection)
        {
            var sis_str_siglas = ConfigurationManager.AppSettings["ModuleAcronym"] == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["ModuleAcronym"]);
            var rol_int_id_array = new List<Int32>();
            if (!string.IsNullOrEmpty(modelo.idsRolesDestino)){
                rol_int_id_array = modelo.idsRolesDestino.Split(',').Select(Int32.Parse).ToList();
            }
            var roles_procesados = UsuariosData.AsignarRolesUsuarios(this.ControllerContext, sis_str_siglas, modelo.usr_int_id, rol_int_id_array.ToArray());
            if (roles_procesados == 0) { ViewBag.Mensaje = "Se han activo todos los roles al usuario"; }
            else { ViewBag.Mensaje = string.Format("se han asignado {0} al usuario seleccionado", roles_procesados); }

            if (roles_procesados > 0)
                return Json(new { res = "true" }, JsonRequestBehavior.AllowGet);

            return Json(new { res = "false" }, JsonRequestBehavior.AllowGet);
        }

        private AsignarRolesUsuariosModel GetAsignarRolesUsuariosModel(int id)
        {
            var res = UsuariosData.GetdatosBasicosUsuario(id, null);
            if (res == null) return null;

            Mapper.CreateMap<ObtenerDatosBasicosUsuarioResult, AsignarRolesUsuariosModel>();
            var modelo = Mapper.Map<ObtenerDatosBasicosUsuarioResult, AsignarRolesUsuariosModel>(res);

            return modelo;
        }
        public ActionResult AsignarRolesModal(int id)
        {
            var model = GetAsignarRolesUsuariosModel(id);
            return PartialView("_AsignarRolesModal", model);
        }
        public JsonResult ListarRolesDisponiblesAsignados(string ptipo,  string pured, string pralias, string sidx, string sord, int page, int rows)
        {
            int tiporeporte = (String.IsNullOrEmpty(ptipo) || !Utilidades.IsNumeric(ptipo)) ? 0 : Convert.ToInt32(ptipo);
            var listadoTotal = RolesData.GetListarRolesAsignables(pured, pralias, tiporeporte);
            var resjson1 = (new JqGridExtension<ListarRolesAsignablesDto>()).DataBind(listadoTotal, listadoTotal.Count);
            return resjson1;
        }

        #endregion 

        public ActionResult Insertar()
        {
            return View(GetInsertarModificarUsuarioModel(null));
        }
        //public PartialViewResult InsertarModal(InsertarModificarUsuarioModel model)
        //{
        //    return PartialView("_InsertarModal");
        //}
        public ActionResult EliminarUsuario(int id)
        {
            var res = DataAccess.Seguridad.UsuariosData.EliminarUsuario(id);
            if(res=="OK")
                return Json(new { success = true, msj = res }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, msj = res }, JsonRequestBehavior.AllowGet);


 
        }

        public ActionResult Modificar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.Error(new ArgumentNullException("No se ha ingresado el id del usuario"));
                return RedirectToAction("ListarUsuarios", "Usuarios");
            }
            if (!Utilidades.IsNumeric(id))
            {
                this.Error(new InvalidCastException("El id ingresado no cumple el formato numerico requerido"));
                return RedirectToAction("ListarUsuarios", "Usuarios");
            }

            return View(GetInsertarModificarUsuarioModel(int.Parse(id)));
        }


        public ActionResult InsertarModificarAlerta(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.Error(new ArgumentNullException("No se ha ingresado el id del usuario"));
                return RedirectToAction("ListarUsuarios", "Usuarios");
            }
            if (!Utilidades.IsNumeric(id))
            {
                this.Error(new InvalidCastException("El id ingresado no cumple el formato numerico requerido"));
                return RedirectToAction("ListarUsuarios", "Usuarios");
            }

            return View(GetInsertarModificarUsuarioModel(int.Parse(id)));
        }

        [HttpPost]
        public ActionResult Modificar(InsertarModificarUsuarioModel modelo)
        {
            if (ModelState.IsValid)
            {
                var res = UsuariosData.InsertarUsuario(this.ControllerContext, modelo);
                 return Json(new { res = res }, JsonRequestBehavior.AllowGet);
            }
            return View(modelo);
        }
        [HttpPost]
        public ActionResult DesbloquearUsuario(int id)
        {


            if (ModelState.IsValid)
            {
                var rSes = UsuariosData.DesbloquearUsuario(id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }


        [HttpPost]
        public ActionResult Insertar(InsertarModificarUsuarioModel modelo)
        {
            if (ModelState.IsValid)
            {
                var res = UsuariosData.InsertarUsuario(this.ControllerContext, modelo);
                return Json(new { res = res }, JsonRequestBehavior.AllowGet);
            }
            else
            { 
                return Json(new { success = false, errors = GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet); 
            }
        }

        private InsertarModificarUsuarioModel GetInsertarModificarUsuarioModel(int? id)
        {
            string res = string.Empty;
            InsertarModificarUsuarioModel modelo = null;
            
         

            if (id.HasValue)
            {
                var resusuario = AccountData.ObtenerUsuario(id.Value, ref res);
                Mapper.CreateMap<ObtenerUsuarioResult, InsertarModificarUsuarioModel>();
                modelo = Mapper.Map<ObtenerUsuarioResult, InsertarModificarUsuarioModel>(resusuario);
              
                
            }
            else
            {
                modelo = new InsertarModificarUsuarioModel();
               // modelo.Sis_int_id = 1;
            }
            return modelo;
        }

        public JsonResult ResetearPassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {

                this.Error(new ArgumentNullException("No se ha ingresado el id del usuario"));
                return Json(new { res = "false", msj="No se ha ingresado el id del usuario" });

            }
            if (!Utilidades.IsNumeric(id))
            {
                this.Error(new InvalidCastException("El id ingresado no cumple el formato numerico requerido"));
                return Json(new { res = "false", msj = "El id ingresado no cumple el formato numerico requerido" });
            }

            var result = UsuariosData.ResetarContraseña(this.ControllerContext, int.Parse(id));
            if (string.IsNullOrEmpty(result)) return Json(new { res = "false", msj = "No se ha podido resetear la contraseña" });




            return Json(new { res = "true", msj = string.Format("La contraseña se ha generado correctamente: {0}", result) });
        }
        public PartialViewResult InsertarModal(int? id)
        {
            var modelo = new  InsertarModificarUsuarioModel();
            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes(null,false).ToList();
            var listaclientes = new SelectList(
                   clientes,
                   "idcliente",
                   "razonsocial");
            ViewData["ListadoClientes"] = listaclientes;


            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;




            //mapeando valores en el modelo
            if (id != null)
            {
                var result = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(id);
                modelo.Usr_str_nombre = result.usr_str_nombre;
                modelo.Usr_str_apellidos = result.usr_str_apellidos;
                modelo.Usr_str_email = result.usr_str_email;
                modelo.Usr_str_red = result.usr_str_red;
                modelo.Usr_int_id = result.usr_int_id;
                modelo.usr_str_tipoacceso = result.usr_str_tipoacceso;

            }

            return PartialView("_InsertarModificarUsuario", modelo);
        }
       
        public ActionResult AsignarClientesUsuarios(AsignarClientesModal modelo)
        {
            var AsignarClientes = DataAccess.Seguridad.UsuariosData.AsignarClientesUsuarios(this.ControllerContext, modelo.usuario, modelo.ClientesSeleccionados);
            return RedirectToAction("ListarUsuarios", "usuarios");
        }
        private IEnumerable<SelectListItem> GetListaClientes()
        {
            List<SelectListItem> ListaAccesorios = new List<SelectListItem>();
            return ListaAccesorios.AsEnumerable();
        }
        public PartialViewResult ConfigurarAlertasModal(int? id)
        {

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes(null, false).ToList();
            var listaclientes = new SelectList(
                   clientes,
                   "idcliente",
                   "razonsocial");
            ViewData["ListadoClientes"] = listaclientes;


            var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(null).ToList();
            var listaprovincias = new SelectList(
                   provincias,
                   "idprovincia",
                   "provincia");
            ViewData["ListadoProvincias"] = listaprovincias;


            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var modelo = new InsertarModificarAlertaModel();
            //mapeando valores en el modelo
            if (id != null)
            {
                var result = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(id);
               
                modelo.Usr_str_red = result.usr_str_red;
                modelo.usr_int_id = result.usr_int_id;
          

            }

            return PartialView("_ConfigurarAlertas", modelo);
        }

        [HttpPost]
        public ActionResult InsertarModificarAlerta(InsertarModificarAlertaModel modelo)
        {
            modelo.estados = string.Empty;

            if (ModelState.IsValid)
            {

                if (modelo.Cerrado) 
                    modelo.estados = modelo.estados + (int) Constantes.EstadoOT.Cerrado; 
                if(modelo.EnRuta)
                    modelo.estados = modelo.estados + "," + (int)Constantes.EstadoOT.PendienteEntrega;
                if (modelo.Facturado)
                    modelo.estados = modelo.estados + "," + (int)Constantes.EstadoOT.Facturado;
                if (modelo.pendienteDespacho)
                    modelo.estados = modelo.estados + "," + (int)Constantes.EstadoOT.PendienteDespacho;
                if (modelo.Entregado)
                    modelo.estados = modelo.estados + "," + (int)Constantes.EstadoOT.PendienteRetornoDocumentario;
                if (modelo.pendienteInicioCarga)
                    modelo.estados = modelo.estados + "," + (int)Constantes.EstadoOT.PendienteInicioCarga;
                if (modelo.Liquidado)
                    modelo.estados = modelo.estados + "," + (int)Constantes.EstadoOT.PendienteFacturacion;
                if (modelo.pendienteProgramacion)
                    modelo.estados = modelo.estados + "," + (int)Constantes.EstadoOT.PendienteProgramacion;
 

                var res = UsuariosData.InsertarModificarAlerta(modelo);
                return Json(new { res = true }, JsonRequestBehavior.AllowGet);
            }
            return View(modelo);
        }
        public PartialViewResult ModificarModal(int? id)
        {

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes(null, false).ToList();
            var listaclientes = new SelectList(
                   clientes,
                   "idcliente",
                   "razonsocial");
            ViewData["ListadoClientes"] = listaclientes;


            var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(null).ToList();
            var listaprovincias = new SelectList(
                   provincias,
                   "idprovincia",
                   "provincia");
            ViewData["ListadoProvincias"] = listaprovincias;


            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var modelo = new InsertarModificarUsuarioModel();
            //mapeando valores en el modelo
            if (id != null)
            {
                var result = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(id);

                if(result.idclientes != null)
                {
                    string[] ids = result.idclientes.Split(',');

                    modelo.idclientes = new string[ids.Length];
                    int index = 0;
                    foreach (var item in ids)
                    {
                        modelo.idclientes[index] = item;
                        index++;
                    }
                }

       
                modelo.clientes = result.idclientes;
                modelo.Usr_str_nombre = result.usr_str_nombre;
                modelo.Usr_str_apellidos = result.usr_str_apellidos;
                modelo.Usr_str_email = result.usr_str_email;
                modelo.Usr_str_red = result.usr_str_red;
                modelo.Usr_int_id = result.usr_int_id;
                modelo.Usr_bool_bloqueado = Convert.ToBoolean(result.usr_int_bloqueado);
                modelo.Usr_bool_aprobado = Convert.ToBoolean(result.usr_bit_aprobado);
                modelo.usr_str_tipoacceso = result.usr_str_tipoacceso;
                modelo.idestacionorigen = result.idestacionorigen;
                modelo.idcliente = result.idcliente;
                modelo.idprovincia = result.idprovincia;

            }

            return PartialView("_ModificarUsuario", modelo);
        }
        public List<string> GetModelStateErrors(ModelStateDictionary ModelState)
        {
            List<string> errorMessages = new List<string>();

            var validationErrors = ModelState.Values.Select(x => x.Errors);
            validationErrors.ToList().ForEach(ve =>
            {
                var errorStrings = ve.Select(x => x.ErrorMessage);
                errorStrings.ToList().ForEach(em =>
                {
                    errorMessages.Add(em);
                });
            });
            return errorMessages;
        }
        











    }
}
