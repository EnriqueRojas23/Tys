using System.Web.Mvc;
using CommandContracts.Common;
using QueryContracts.Common;
using StructureMap.Pipeline;
using Web.Common.ActionResults;
using Seguridad.Common;
using SessionWrapper = Seguridad.Common.SessionWrapper;
using log4net;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using QueryContracts.Common.Seguridad.Results;
using System.Configuration;
using System.Web.Routing;
using QueryContracts.Common.Seguridad.Parameters;
using ServiceAgents.Common;
using System.Runtime.Caching; 

namespace Web.Common.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        { 
             
            //var ctx = HttpContext.Current;

            // if (ctx.Session != null)
            // {

            //     if (ctx.Session.IsNewSession || ctx.Session["dbUser"] == null)
            //     { }
            // }
        }

        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Metodos de Log

        public void Error(String error){
            Error(new Exception(error));
        }

        public void Error(Exception ex)
        {
            log.Error(ex.Message, ex);
            ex = this.Unwrap(ex);
            log.Error(ex.Message, ex);
        }

        private Exception Unwrap(Exception ex)
        {
            while (null != ex.InnerException)
            {
                ex = ex.InnerException;
            }

            return ex;
        }

        #endregion


        public CommandActionResult Command(Command command)
        {
            return new CommandActionResult(command);
        }

        public CommandActionResult Command(Command command, string urlResultRedirect)
        {
            return new CommandActionResult(command, urlResultRedirect);
        }

        public Transaccion ExecuteCommand(ControllerContext context, Command command)
        {
            return new CommandActionResult(command).ExecuteCommand(context);
        }

        public QueryViewResult<T> Query<T>(QueryParameter parameter) where T : class
        {
            return new QueryViewResult<T>(parameter);
        }

        private List<ListarMenusxRolesDto> GetListarMenu(int? prol_int_id)
        {
            var parameter = new ListarMenusxRolesParameter() { rol_int_id = prol_int_id, sis_str_siglas = GetModuleAcronym() };
            var result = (ListarMenusxRolesResult)parameter.Execute();
            return result == null ? new List<ListarMenusxRolesDto>() : result.Hits.Where(x=>x.srp_seleccion == 1).ToList();
        }

        public Perfil Perfil
        {
            get 
            {
                if (SessionWrapper.Perfil != null && (SessionWrapper.Perfil.ListaMenuOpcion == null || SessionWrapper.Perfil.ListaMenuOpcion.Any() == false )){
                    SessionWrapper.Perfil.ListaMenuOpcion = GetMenuOpcionBd(SessionWrapper.Perfil.IdPerfil);
                }
                return SessionWrapper.Perfil; 
            }
        }

        private List<MenuOpcion> GetMenuOpcionBd(int? idperfil)
        {
            var listamenu = GetListarMenu(idperfil);
            var resmenu = ObtenerItemsMenu(null, listamenu);
            return resmenu;

        }

        private static List<MenuOpcion> ObtenerItemsMenu(string codigopadre, List<ListarMenusxRolesDto> listamenu)
        {
            var listamenures = new List<MenuOpcion>();
            foreach (var menu in listamenu.Where(x => x.pag_str_codmenu_padre == codigopadre).ToList())
            {
                MenuOpcion mnu = new MenuOpcion();
                mnu.NombreMenu = menu.pag_str_nombre;
                mnu.Url = menu.pag_str_url;
                mnu.IdMenuOpcion = menu.pag_int_id;
                mnu.Nivel = menu.pag_int_nivel;
                mnu.CodigoMenu = menu.pag_str_codmenu;
                mnu.TipoMenu = menu.pag_str_tipomenu;
                mnu.MenuItem = ObtenerItemsMenu(menu.pag_str_codmenu, listamenu);
                mnu.ItemSeleccionado = menu.srp_seleccion == 0 ? false : true;
                mnu.CodigoPermiso = menu.srp_str_codpermiso;
                mnu.ControllerName = menu.pag_str_controller;
                mnu.ActionName = menu.pag_str_action;
                mnu.AttributesRoute = menu.pag_str_attributes;
                mnu.Secuencia = menu.pag_int_secuencia;


                listamenures.Add(mnu);
            }
            return listamenures;
        }

        public Usuario Usuario
        {
            get
            { 
                return SessionWrapper.Usuario; 
            }
        }

        public string GetModuleAcronym()
        {
            if (ConfigurationManager.AppSettings["ModuleAcronym"] == null) throw new ArgumentException("No esta configurado el ModuleAcronym en el archivo de configuración.");
            var res = Convert.ToString(ConfigurationManager.AppSettings["ModuleAcronym"]);
            return res;
        }

        public JsonResult UrlAction(string action, string controller, string area = null)
        {
            var routevalue = new RouteValueDictionary { { "area", area } };
            var url = Url.Action(action, controller, routevalue);
            return Json(new { urlres = url });
        }

        public class InMemoryCache : ICacheService
        {
            public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
            {
                T item = MemoryCache.Default.Get(cacheKey) as T;
                if (item == null)
                {
                    item = getItemCallback();
                    MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
                }
                return item;
            }
        }

        interface ICacheService
        {
            T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        }
             


    }
}
