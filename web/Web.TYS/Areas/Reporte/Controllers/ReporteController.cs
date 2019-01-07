using Componentes.Common.Utilidades;
using QueryContracts.TYS.Monitoreo.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Common.Controllers;
using Web.TYS.Areas.Monitoreo.Models;
using Web.TYS.Areas.Seguimiento.Models.Seguimiento;
using Web.TYS.DataAccess;
using Web.TYS.DataAccess.Monitoreo;
using Web.TYS.DataAccess.Seguimiento;
using Web.Common.HtmlHelpers;
using System.Globalization;
using QueryContracts.TYS.Seguimiento.Results;
using System.Web.Caching;
using Web.TYS.Areas.Models;

namespace Web.TYS.Areas.Monitoreo.Controllers
{
    public class ReporteController : BaseController
    {
        //
        // GET: /Monitoreo/Default1/


        #region operacionesmonitoreo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RepProdvsDestino()
        {
            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var departamentos = GetListarDepartamento_Cache();
            var listadepartamentos = new SelectList(
                departamentos,
                "iddepartamento",
                "departamento");
            ViewData["ListadoDepartamento"] = listadepartamentos;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);



            var tipostransporte = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte))).ToList();
            var listatransporte = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData["ListaModoTransporte"] = listatransporte;


            var tipo = new TipoUnidadMedidaModel();

            List<TipoUnidadMedidaModel> tipos = new List<TipoUnidadMedidaModel>();
            tipo.IdTipoUnidadMedida = 1;
            tipo.TipoUnidadMedida = "Bulto";
            tipos.Add(tipo);
            tipo = new TipoUnidadMedidaModel();
            tipo.IdTipoUnidadMedida = 2;
            tipo.TipoUnidadMedida = "Peso";
            tipos.Add(tipo);
            tipo = new TipoUnidadMedidaModel();
            tipo.IdTipoUnidadMedida = 3;
            tipo.TipoUnidadMedida = "Valor";
            tipos.Add(tipo);

            var tiposunidad = tipos;
            var listatipo = new SelectList(
             tiposunidad,
             "IdTipoUnidadMedida",
             "TipoUnidadMedida");
            ViewData["ListaTipo"] = listatipo;


            //var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(iddepartamento);
            //var listaprovincias = new SelectList(
            //    provincias,
            //    "idprovincia",
            //    "provincia");
            //ViewData["ListaProvincia"] = listaprovincias;


            return View();
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
        public ActionResult RepPendienteFacturar()
        {
            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados((Int32)Constantes.MaestroTablas.Preliquidacion);
            var listaestados = new SelectList(
                estados,
                "idestado"
                , "estado"
                );
            ViewData["ListaEstados"] = listaestados;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View();

        }
        public ActionResult RepProduccionCliente()
        {
            var tipo = new TipoUnidadMedidaModel();

            List<TipoUnidadMedidaModel> tipos = new List<TipoUnidadMedidaModel>();
            tipo.IdTipoUnidadMedida = 1;
            tipo.TipoUnidadMedida = "Bulto";
            tipos.Add(tipo);
            tipo = new TipoUnidadMedidaModel();
            tipo.IdTipoUnidadMedida = 2;
            tipo.TipoUnidadMedida = "Peso";
            tipos.Add(tipo);
            tipo = new TipoUnidadMedidaModel();
            tipo.IdTipoUnidadMedida = 3;
            tipo.TipoUnidadMedida = "Valor";
            tipos.Add(tipo);
            
            var tiposunidad = tipos;
            var listatipo = new SelectList(
             tiposunidad,
             "IdTipoUnidadMedida",
             "TipoUnidadMedida");
            ViewData["ListaTipo"] = listatipo;




            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View();

        }
        public ActionResult PendientePorDespachar()
        {


            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;


            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);
        }
        public ActionResult ReporteComercial()
        {
            var model = new ReporteModel();

             model.idusuario =(Int32)Usuario.Idusuario;



            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);
        }
        public ActionResult ReporteGeneral()
        {
            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);
        }
        public ActionResult PendienteRecepcion()
        {
            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View();
        }
        public ActionResult PendienteEntrega()
        {
            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;

            return View(model);
        }
        public ActionResult PendienteDespacho()
        
        {
            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);
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


            return DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);
        }

        public ActionResult RepConciliacionEntrega()
        {

            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;



            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);



        
        }

        public ActionResult Rechazo()
        {

            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;



            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);


        }

        public ActionResult RepLiquidacionDocumentaria ()
        {

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["ListaUbigeo"] = listaUbigeos;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;

            return View(model);
        }


        public ActionResult RepProdvsfacturacion()
        {
            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;



            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);
        }

        public ActionResult RepMonitoreoFluvial()
        {
            var estados = DataAccess.Seguimiento.SeguimientoData.GetListarEstados((Int32)Constantes.MaestroTablas.OT);
            var listaestados = new SelectList(
                estados,
                "idestado"
                , "estado"
                );
            ViewData["ListaEstados"] = listaestados;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;
            var model = new ReporteModel();

            model.idusuario = (Int32)Usuario.Idusuario;

            return View(model);

        }



        #endregion
        public JsonResult JsonGetListarDistritos(int? idprovincia)
        {

            var result = DataAccess.Seguimiento.SeguimientoData.GetListarDistritos(idprovincia);
            Session["iddistrito"] = idprovincia;

            var listaprovincias = new SelectList(
            result,
            "iddistrito",
            "distrito");
            return Json(listaprovincias, JsonRequestBehavior.AllowGet);


        }

    }
}
