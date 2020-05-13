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
using Web.TYS.Areas.Monitoreo.Models;
using Web.TYS.DataAccess;
using Web.TYS.DataAccess.Pago;
using Web.TYS.DataAccess.Seguridad;
using Web.TYS.Areas.Pago.Models;
using Web.TYS.DataAccess.Facturacion;
using Web.TYS.DataAccess.Monitoreo;
using QueryContracts.TYS.Account.Results;

namespace Web.TYS.Areas.Seguimiento.Controllers
{
    public class OrdenController : BaseController
    {
        private const string LISTAPROVEEDOR = "ListaProveedor";

        #region ResultViews
       
        public ActionResult Ordenes()
        {
            ObtenerUsuarioResult1 usr;

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

            if (Usuario == null)
            {
                usr = new ObtenerUsuarioResult1();
                usr.idestacionorigen = 1;
                
            }
            else
            {
                 usr = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));
            }

            if (usr.idestacionorigen != null)
                model.idestacion = usr.idestacionorigen.Value;

            model.fechainicio = DateTime.Now.Subtract(TimeSpan.FromDays(31));
            model.fechafin = DateTime.Now;

            return View(model);
        }

        public ActionResult EditarOrdenTrabajo(int? id)
        {
            var guias = new List<GuiaRemisionModel>();
            GuiaRemisionModel item;

            var model = OrdenData.GetObtenerOrden_model(id.Value);
            var guiasbd = OrdenData.GetListarGuias(id.Value).Hits.ToList();
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            foreach (var guia in guiasbd)
            {
                item = new GuiaRemisionModel();
                item.idguiaremisioncliente = guia.idguiaremisioncliente;
                if (guia.bulto != null)
                    item.bulto = Convert.ToInt32(guia.bulto);
                item.nroguia = guia.nroguia;
                if (guia.peso != null)
                    item.peso = Convert.ToDecimal(guia.peso);
                if (guia.volumen != null)
                    item.volumen = Convert.ToDecimal(guia.volumen);
                if (guia.pesovol != null)
                    item.pesovol = Convert.ToDecimal(guia.pesovol);
                item.documento = guia.documento;
                guias.Add(item);
            }

            Session["guias"] = guias;

            //OrdenTrabajoModel model = new OrdenTrabajoModel();

            #region asignarpropiedades

            //   model.idorigen = usuario.iddistrito;
            model.razonsocialdestinatario = model.destinatario;
            model.iddestinatario_selected = model.iddestinatario;

            model.idremitente_selected = model.idremitente;
            model.pesogeneral = model.peso;
            model.volgeneral = model.volumen;
            model.bultogeneral = model.bulto;
            model.cantidad = "1";
            model.horarecojo = model.fecharecojo.Value.Hour.ToString().PadLeft(2, '0') + ":" + model.fecharecojo.Value.Minute.ToString().PadLeft(2, '0');
            model.fecharecojo = model.fecharecojo.Value.Date;

            #endregion asignarpropiedades

            #region combos

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var direcciones = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(model.idcliente);
            var listadirecciones = new SelectList(
               direcciones,
               "iddireccion",
               "direccion");
            ViewData["ListaDirecciones"] = listadirecciones;

            var direccionesremitente = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(model.idremitente);
            var listadireccionesremitente = new SelectList(
               direccionesremitente,
               "iddireccion",
               "direccion");
            ViewData["ListaDireccionesRemitente"] = listadireccionesremitente;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var formula = new OrdenData().GetListarFormulaOT(model.idcliente, model.idorigen, model.iddestino, ubigeos);
            var listaformula = new SelectList(
               formula,
               "idformula",
               "formula");
            ViewData["ListadoFormula"] = listaformula;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            if (model.idremitente == model.idcliente)
            {
                var clientesproveedor = GetListarClientes_Cache().Where(x => x.idcliente.Equals(model.idcliente));
                var listaclientesproveedor = new SelectList(
                   clientesproveedor,
                   "idcliente",
                   "razonsocial");
                ViewData["ListaClientesProveedor"] = listaclientesproveedor;
            }
            else
            {
                var clientesproveedor = DataAccess.Seguimiento.SeguimientoData.GetListarProveedorxCliente(model.idcliente);
                var listaclientesproveedor = new SelectList(
                   clientesproveedor,
                   "idproveedor",
                   "razonsocial");
                ViewData["ListaClientesProveedor"] = listaclientesproveedor;
            }

            var tipostransporte = new OrdenData().GetListarTipoTransporteOT(model.idcliente, model.idorigen, model.iddestino, ubigeos);
            var listatipos = new SelectList(
               tipostransporte,
               "idtipotransporte",
               "tipotransporte");
            ViewData["ListaModoTransporte"] = listatipos;

            var conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarConceptoCobroT(model.idcliente, model.idorigen
                , model.iddestino, model.idformula, model.idtipotransporte);

            if (conceptocobro.Count == 0)
                conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarConceptoCobroT(80, model.idorigen
                , model.iddestino, model.idformula, model.idtipotransporte);

            List<ListarConceptosTarifaDto> conceps = new List<ListarConceptosTarifaDto>();

            foreach (var item1 in conceptocobro)
            {
                if (item1.concepto != null)
                    conceps.Add(item1);
            }
            var listaconceptocobro = new SelectList(
               conceps,
               "idconceptocobro",
               "concepto");
            ViewData["ListaConceptoCobro"] = listaconceptocobro;

            var cargadaen = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.CargadaEn))).ToList(); ;
            var listacargadaen = new SelectList(
               cargadaen,
               "idvalortabla",
               "valor");
            ViewData["ListaCargadaEn"] = listacargadaen;

            var entregara = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.EntregarA))).ToList();
            var listaentregara = new SelectList(
               entregara,
               "idvalortabla",
               "valor");
            ViewData["ListaEntregarA"] = listaentregara;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var tipovehiculo = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoVehiculo)).Where(x => x.activo == true).ToList();
            var listatipovehiculo = new SelectList(
                tipovehiculo,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoVehiculo"] = listatipovehiculo;

            var tipomercaderia = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoMercaderia)).Where(x => x.activo == true).ToList();
            var listatipomercaderia = new SelectList(
                tipomercaderia,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoMercaderia"] = listatipomercaderia;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;

            #endregion combos

            return View(model);
        }

        public JsonResult JsonListarClientes()
        {
            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            return Json(new { listaclientes = listaclientes });
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
            ViewData["ListaMoneda"] = listamonedas;
            ClienteModel model = new ClienteModel();
            model.pagocontado = true;
            model.idmonedalinea = 70;

            return PartialView("_AgregarClienteModal", model);
        }

        public PartialViewResult AgregarDestinatarioModal()
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
            ViewData["ListaMoneda"] = listamonedas;
            ClienteModel model = new ClienteModel();
            model.pagocontado = true;
            model.idmonedalinea = 70;

            return PartialView("_AgregarDestinatarioModal", model);
        }

        public ActionResult CopiarOrdenTrabajo(int? id)
        {
            Session["guias"] = null;
            var orden = OrdenData.GetObtenerOrden(id.Value);
            OrdenTrabajoModel model = new OrdenTrabajoModel();

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));
            model.idestacionorigen = usuario.idestacionorigen;
            model.idorigen = usuario.iddistrito;
            model.numcp = orden.numcp;
            model.guiarecojo = orden.guiarecojo;
            model.idcargadaen = orden.idcargadaen;
            model.idcliente = orden.idcliente;
            model.idclienteconceptocobro = orden.idclienteconceptocobro;
            model.idclientetipounidad = orden.idclientetipounidad;
            model.identregara = orden.identregara;
            model.idestacionorigen = orden.idestacionorigen;
            model.idorigen = orden.idorigen;
            model.idremitente = orden.idremitente;
            model.idremitentedireccion = orden.idremitentedireccion;
            model.idtipovehiculo = orden.idtipovehiculo;
            model.idvehiculo = orden.idvehiculo;
            model.numcp = orden.numcp;
            model.numeroinicial = orden.numeroinicial;
            model.chofer = orden.chofer;
            model.dni = orden.dni;
            model.placa = orden.placa;
            model.puntopartida = orden.puntopartida;

            //if (orden.bulto != null)
            //    model.bultogeneral = Convert.ToInt32(orden.bulto);

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var direcciones = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(model.idcliente);
            var listadirecciones = new SelectList(
               direcciones,
               "iddireccion",
               "direccion");
            ViewData["ListaDirecciones"] = listadirecciones;

            var direccionesremitente = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(model.idremitente);
            var listadireccionesremitente = new SelectList(
               direccionesremitente,
               "iddireccion",
               "direccion");
            ViewData["ListaDireccionesRemitente"] = listadireccionesremitente;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            //var formula = new OrdenData().GetListarFormulaOT(model.idcliente, model.idorigen, model.iddestino, ubigeos);
            //var listaformula = new SelectList(
            //   formula,
            //   "idformula",
            //   "formula");
            //ViewData["ListadoFormula"] = listaformula;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            if (model.idremitente == model.idcliente)
            {
                var clientesproveedor = GetListarClientes_Cache().Where(x => x.idcliente.Equals(model.idcliente));
                var listaclientesproveedor = new SelectList(
                   clientesproveedor,
                   "idcliente",
                   "razonsocial");
                ViewData["ListaClientesProveedor"] = listaclientesproveedor;
            }
            else
            {
                var clientesproveedor = DataAccess.Seguimiento.SeguimientoData.GetListarProveedorxCliente(model.idcliente);
                var listaclientesproveedor = new SelectList(
                   clientesproveedor,
                   "idproveedor",
                   "razonsocial");
                ViewData["ListaClientesProveedor"] = listaclientesproveedor;
            }

            var tipostransporte = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte))).ToList();
            var listatipos = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData["ListaModoTransporte"] = listatipos;

            var entregara = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.EntregarA))).ToList();
            var listaentregara = new SelectList(
               entregara,
               "idvalortabla",
               "valor");
            ViewData["ListaEntregarA"] = listaentregara;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var tipovehiculo = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoVehiculo)).Where(x => x.activo == true).ToList();
            var listatipovehiculo = new SelectList(
                tipovehiculo,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoVehiculo"] = listatipovehiculo;

            var tipomercaderia = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoMercaderia)).Where(x => x.activo == true).ToList();
            var listatipomercaderia = new SelectList(
                tipomercaderia,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoMercaderia"] = listatipomercaderia;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;
            model.horarecojo = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fecharecojo = DateTime.Now;

            model.cantidad = "1";

            return View(model);
        }

        public ActionResult NuevaOrdenTrabajo()
        {
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            OrdenTrabajoModel model = new OrdenTrabajoModel();
            model.idestacionorigen = usuario.idestacionorigen;
            model.idorigen = usuario.iddistrito;

            Session["guias"] = null;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);  //GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var tipostransporte = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte))).ToList();
            var listatipos = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData["ListaModoTransporte"] = listatipos;

            var cargadaen = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.CargadaEn))).ToList(); ;
            var listacargadaen = new SelectList(
               cargadaen,
               "idvalortabla",
               "valor");
            ViewData["ListaCargadaEn"] = listacargadaen;

            var entregara = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.EntregarA))).ToList();
            var listaentregara = new SelectList(
               entregara,
               "idvalortabla",
               "valor");
            ViewData["ListaEntregarA"] = listaentregara;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var tipovehiculo = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.TipoVehiculo))).ToList();
            var listatipovehiculo = new SelectList(
                tipovehiculo,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoVehiculo"] = listatipovehiculo;

            var tipomercaderia = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.TipoMercaderia))).ToList();
            var listatipomercaderia = new SelectList(
                tipomercaderia,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoMercaderia"] = listatipomercaderia;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            model.horarecojo = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            model.fecharecojo = DateTime.Now;

            model.cantidad = "1";

            return View(model);
        }

        #endregion ResultViews

        #region Acciones

        [HttpPost]
        public ActionResult EliminarOrden(long id)
        {

            string respuesta = "";
            ManifiestoModel modelmanifiesto = new ManifiestoModel();
            OrdenTrabajoModel modelorden = new OrdenTrabajoModel();
            decimal peso = 0, volumen = 0;
            int idusuario = Usuario.Idusuario;

            var orden = OrdenData.GetObtenerOrden(id);
            if (orden.idcarga.HasValue)
            {
                var operacion = SeguimientoData.GetObtenerOperacion(orden.idcarga.Value);

                if(operacion.idestado ==(Int32) Constantes.EstadoCarga.EnRuta)
                {
                    return Json(new { res = true, msj = "La OT ya se encuentra en ruta o entregada" }, JsonRequestBehavior.AllowGet);
                }

                var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(operacion.idvehiculo);
                #region Actualizar Cargas

                var listadooperaciones = DataAccess.Seguimiento.SeguimientoData.GetListarOperacionDetalle(orden.idcarga);
                if (listado.Count == 0)
                {
                    //// anular manifiesto anterior
                    modelmanifiesto.idmanifiesto = orden.idmanifiesto;
                    modelmanifiesto._tipoop = 2;
                    SeguimientoData.InsertarActualizarManifiesto(modelmanifiesto, out respuesta);

                    CargaModel cargamodel = new CargaModel();
                    cargamodel.activo = false;
                    cargamodel.idcarga = orden.idcarga;
                    cargamodel._tipooperacion = 1;
                    SeguimientoData.InsertarActualizarCarga(cargamodel, out respuesta);
                }
                else
                {   // De lo contrario suma las OTs restantes de la carga y actualiza sus datos
                    foreach (var item in listado)
                    {
                        peso = peso + item.peso;
                        volumen = volumen + item.volumen;
                    }
                    CargaModel cargamodel = new CargaModel();
                    cargamodel.idcarga = orden.idcarga;
                    cargamodel._tipooperacion = 6;
                    cargamodel.peso = peso;
                    cargamodel.vol = volumen;
                    SeguimientoData.InsertarActualizarCarga(cargamodel, out respuesta);
                }

                #endregion



                if (listado.Count == 0)
                {
                    //Eliminar Despacho
                    DespachoModel modeldespacho = new DespachoModel();
                    modeldespacho._tipoop = 3;
                    modeldespacho.iddespacho = orden.iddespacho;
                    SeguimientoData.InsertarActualizarDespacho(modeldespacho);

                    //Eliminar Carga
                    CargaModel cargamodel = new CargaModel();
                    cargamodel.activo = false;
                    cargamodel.idcarga = operacion.idcarga;
                    cargamodel._tipooperacion = 1;
                    DataAccess.Seguimiento.SeguimientoData.InsertarActualizarCarga(cargamodel, out respuesta);


                    //Libera vehículo
                    VehiculoModel modelvehiculo = new VehiculoModel();
                    modelvehiculo.idvehiculo = operacion.idvehiculo;
                    modelvehiculo._tipoop = 5;
                    modelvehiculo.idestado = (Int32)DataAccess.Constantes.EstadoVehiculo.Inspeccionado;
                    SeguimientoData.InsertarActualizarVehiculo(modelvehiculo);
                }

            }

            //Desasociar la OT
            modelorden._tipoop = 5;
            modelorden.idordentrabajo = orden.idordentrabajo;
            modelorden.iddespacho = null;
            modelorden.idcarga = null;
            modelorden.activo = false;
            var resultot = OrdenData.InsertarActualizarOrdenTransporte(modelorden, null, true);

            ////generar nuevo manifiesto

            var modIncidencia = new IncidenciaModel();
            modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeElimino;
            modIncidencia.idsorden = orden.idordentrabajo.ToString();
            modIncidencia.fechaincidencia = DateTime.Now;
            modIncidencia.fecharegistro = DateTime.Now;
            modIncidencia.descripcion = "Se eliminó la orden desde el seguimiento";
            modIncidencia.idusuarioregistro = idusuario;
            modIncidencia.activo = true;
            IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

            //var ordenes = SeguimientoData.des

            


            //Actualizar Cargas
          

            //var result = OrdenData.GetObtenerOrden(id);

            //if (result.iddespacho == null && result.idcarga == null)
            //{
            //    OrdenData.EliminarOrdenTrabajo(id);
            //}
            //else return Json(new { res = false, msj = "Tiene carga asociada" }, JsonRequestBehavior.AllowGet);

            //// var result = new DataAccess.Seguimiento.SeguimientoData().EliminarTarifa(id); ;
            return Json(new { res = true, msj = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Elimnarguia(int? id)
        {
            var guias = (List<GuiaRemisionModel>)Session["guias"];

            try
            {
                foreach (var item in guias)
                {
                    if (item.idguiaremisioncliente.Value.Equals(id.Value))
                    {
                        guias.Remove(item);
                        break;
                    }
                }
                Session["guias"] = guias;
            }
            catch (Exception)
            {
                throw;
            }
            var JsonTotales = new
            {
                peso = guias.Select(x => x.peso).Sum(),
                bultos = guias.Select(x => x.bulto).Sum(),
                cantidad = guias.Count,
            };

            Session["guias"] = guias;
            return Json(new { res = true, jsondata = JsonTotales }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarGuiasNumeroInicial(string numeroinicial, string cantidad)
        {
            List<GuiaRemisionModel> listamodel = new List<GuiaRemisionModel>();
            GuiaRemisionModel model = new GuiaRemisionModel();
            int cant = Convert.ToInt32(cantidad);

            var records = (List<GuiaRemisionModel>)Session["guias"];

            if (records == null) records = new List<GuiaRemisionModel>();

            string[] ig = numeroinicial.Split('-');

            if (ig.Length > 0)
            {
                numeroinicial = ig[1].ToString();
            }
            int numero = Convert.ToInt32(numeroinicial);
            for (int i = 0; i < cant; i++)
            {
                var serepite = ig[0] + '-' + numero.ToString().PadLeft(6, '0');
                var validarepet = records.Where(x => x.nroguia.Equals(serepite.Trim())).ToList();
                var existeenbd = OrdenData.ExisteGuia(serepite.Trim(), null);
                if (existeenbd != null)
                    return Json(new { res = false, guia = serepite.Trim(), ot = existeenbd.numcp }, JsonRequestBehavior.AllowGet);

                if (validarepet.Count != 0)
                    return Json(new { res = false }, JsonRequestBehavior.AllowGet);

                numero = numero + 1;
            }

            numero = Convert.ToInt32(numeroinicial);
            long? maximo = 0;
            if (records.Count > 0)
                maximo = records.Select(x => x.idguiaremisioncliente).Max() + 1;
            else maximo = 1;

            for (int i = 0; i < cant; i++)
            {
                model = new GuiaRemisionModel();
                model.idguiaremisioncliente = maximo;
                model.nroguia = ig[0] + '-' + numero.ToString().PadLeft(6, '0');
                numero = numero + 1;
                maximo = maximo + 1;

                records.Add(model);
            }
            Session["guias"] = records;

            var peso = records.Sum(x => x.peso);
            var bulto = records.Sum(x => x.bulto);
            var cat = records.Count;

            return Json(new { res = "OK", peso = peso, bulto = bulto, cantidad = cat }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcularPago(int origen, int destino, int formula, int tipotransporte, int cliente, decimal peso, decimal volumen, int? idconceptocobro, decimal? pesovol, int? bulto)
        {
            try
            {
                var tot_result = OrdenData.GetListarTarifasOrden(origen, destino, cliente, formula, tipotransporte, idconceptocobro).ToList();
                ListarTarifaOrdenDto result = new ListarTarifaOrdenDto();

                //Calcular desde hasta
                foreach (var item in tot_result)
                {
                    if (item.hasta == 0 && item.desde == 0)
                    {
                        result = item;
                        break;
                    }

                    if (peso <= item.hasta && peso >= item.desde)
                    {
                        result = item;
                    }
                }

                if (result == null || result.precio == 0)
                    return Json(new { res = false, mensaje = "No existe una tarifa asociada" }, JsonRequestBehavior.AllowGet);

                var res_formula = new DataAccess.Seguimiento.SeguimientoData().GetFormula(formula);

                var Base = result.montobase;

                res_formula.formula = res_formula.formula.Replace("Base", result.montobase.ToString());
                res_formula.formula = res_formula.formula.Replace("Precio", result.precio.ToString());

                if (pesovol > peso)
                    res_formula.formula = res_formula.formula.Replace("KG", pesovol.ToString());
                else
                    res_formula.formula = res_formula.formula.Replace("KG", peso.ToString());

                res_formula.formula = res_formula.formula.Replace("Bulto", bulto.ToString());
                res_formula.formula = res_formula.formula.Replace("M3", volumen.ToString());
                res_formula.formula = res_formula.formula.Replace("PesoVol", pesovol.ToString());

                var minimo = result.minimo;
                var base1 = result.montobase;
                var tarifa = result.precio;

                var SubTotal = Math.Round(Evaluate(res_formula.formula), 2);
                if (result.adicional != null)
                    SubTotal = SubTotal + Convert.ToDouble(result.adicional);

                if (SubTotal < Double.Parse(result.minimo.ToString()))
                {
                    SubTotal = Double.Parse(result.minimo.ToString());
                }
                var IGV = Math.Round(SubTotal * 0.18, 2);
                var Total = Math.Round(SubTotal + IGV, 2);

                return Json(new { minimo = minimo, idtarifa = result.idtarifa, tarifa = tarifa, base1 = base1, total = Total, subtotal = SubTotal, igv = IGV }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { res = false, Errors = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        private static double Evaluate(string expression)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (double)(loDataTable.Rows[0]["Eval"]);
        }

        public JsonResult GridSaveGuias(FormCollection formcollection)
        {
            int? id = null;
            long? idguiaremisioncliente = long.Parse(formcollection["idguiaremisioncliente"]);

            int? idordentrabajo = null;
            if (Session["idordentrabajo"] != null)
                idordentrabajo = int.Parse(Session["idordentrabajo"].ToString());

            string nroguia = null;
            if (formcollection["nroguia"] != "")
                nroguia = formcollection["nroguia"];

            string[] ig = nroguia.Split('-');

            int? bulto = null;
            if (formcollection["bulto"] != "")
                bulto = int.Parse(formcollection["bulto"]);

            decimal? peso = null;
            if (formcollection["peso"] != "")
                peso = decimal.Parse(formcollection["peso"]);

            decimal? volumen = null;
            if (formcollection["volumen"] != "")
                volumen = decimal.Parse(formcollection["volumen"]);
            else
                volumen = 0;

            decimal? pesovol = null;
            if (formcollection["pesovol"] != "")
                pesovol = decimal.Parse(formcollection["pesovol"]);

            string documento = null;
            if (formcollection["documento"] != "")
                documento = formcollection["documento"];

            var guias = (List<GuiaRemisionModel>)Session["guias"];

            if (idguiaremisioncliente != 0)
            {
                //validaciones de guia modificada
                //Si existe el numero en la grilla
                if (guias.Where(x => x.nroguia.Equals(nroguia)
                    && x.idguiaremisioncliente != idguiaremisioncliente).SingleOrDefault() != null)
                    return Json(new { res = false, msj = "No se puede repetir el número de Guía." }, JsonRequestBehavior.AllowGet);
                //Si existe en la base de datos
                var existeenbd = OrdenData.ExisteGuia(nroguia.Trim(), idguiaremisioncliente);
                if (existeenbd != null)
                    return Json(new { res = false, msj = "Este número de Guía ya existe en la Base de datos." }, JsonRequestBehavior.AllowGet);

                foreach (var item in guias)
                {
                    if (item.idguiaremisioncliente == idguiaremisioncliente)
                    {
                        item.bulto = bulto;
                        item.nroguia = nroguia;
                        item.peso = peso;
                        item.volumen = volumen;
                        item.pesovol = pesovol;
                        item.documento = documento;
                    }
                }
            }
            else
            {
                if (guias != null)
                {
                    if (guias.Where(x => x.nroguia.Equals(nroguia)).SingleOrDefault() != null)
                        return Json(new { res = false }, JsonRequestBehavior.AllowGet);
                }

                GuiaRemisionModel item = new GuiaRemisionModel();
                item.bulto = bulto;

                var existeenbd = OrdenData.ExisteGuia(nroguia.Trim(), null);

                if (existeenbd != null)
                    return Json(new { res = false }, JsonRequestBehavior.AllowGet);
                item.nroguia = nroguia;
                item.peso = peso;
                item.volumen = volumen;
                item.pesovol = pesovol;
                item.documento = documento;

                if (guias == null || guias.Count == 0)
                {
                    guias = new List<GuiaRemisionModel>();
                    item.idguiaremisioncliente = 1;
                }
                else
                    item.idguiaremisioncliente = guias.Max(x => x.idguiaremisioncliente).Value + 1;
                guias.Add(item);
            }

            var JsonTotales = new
            {
                peso = guias.Select(x => x.peso).Sum(),
                bultos = guias.Select(x => x.bulto).Sum(),
                cantidad = guias.Count,
            };

            Session["guias"] = guias;
            return Json(new { res = true, jsondata = JsonTotales }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AgregarOrdenTrabajoModal(OrdenTrabajoModel model)
        {
            var respuesta = string.Empty;
            List<GuiaRemisionModel> guias = (List<GuiaRemisionModel>)Session["guias"];
            long? idpreliquidacion = null;

            #region validacion LineaCredito

            var cliente = SeguimientoData.GetObtenerCliente(model.idcliente);

            if (!cliente.pagocontado)
            {  // para los pagos con línea de crédito
                if (cliente.lineaconsumida.HasValue)
                {
                    cliente.lineaconsumida = cliente.lineaconsumida + model.subtotal;
                    if (cliente.lineacredito < cliente.lineaconsumida)
                        return Json(new { res = false, mensaje = "El cliente no tiene línea de crédito para esta operación." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    cliente.lineaconsumida = model.subtotal;
                    if (cliente.lineacredito < cliente.lineaconsumida)
                        return Json(new { res = false, mensaje = "El cliente no tiene línea de crédito para esta operación." }, JsonRequestBehavior.AllowGet);
                }
            }

            #endregion validacion LineaCredito

            #region validacionesgenerales

            if (model.total == null)
                return Json(new { res = false, mensaje = "Debe recalcular la OT" }, JsonRequestBehavior.AllowGet);
            if (guias == null)
                return Json(new { res = false, mensaje = "Debe ingresar al menos una Guía de Remisión." }, JsonRequestBehavior.AllowGet);

            #endregion validacionesgenerales

            try
            {
                //actualizar Help de Punto de Partida
                //Actualizar descripciones general

                SeguimientoData.ActualizarPuntoPartida(model.idcliente, model.puntopartida.Trim());
                if (model.descripciongeneral == 0)
                    model.descripciongeneral = SeguimientoData.ActualizarDescripcionesGRR(model.nuevadescripciongeneral);

                #region propiedades

                model._tipoop = 1;
                model.idusuarioregistro = (Int32)Usuario.Idusuario;
                model.activo = true;
                model.iddestinatario = model.iddestinatario_selected;
                model.idremitente = model.idremitente_selected;
                model.facturado = false;

                if (model.pesogeneral.HasValue)
                    model.peso = model.pesogeneral.Value;
                if (model.volgeneral.HasValue)
                    model.volumen = model.volgeneral.Value;
                if (model.bultogeneral.HasValue)
                    model.bulto = model.bultogeneral.Value;

                model.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteProgramacion;
                var hm = model.horarecojo.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
                model.fecharecojo = model.fecharecojo.Value.Date + ts;

                if (!model.idordentrabajo.HasValue)
                {
                    model.numcp = "#";
                    model.fecharegistro = DateTime.Now;
                }

                #endregion propiedades

                string resp = string.Empty;

                var idorden = OrdenData.InsertarActualizarOrdenTransporte(model, guias);

                //Actualizar la linea de crédito
                if (!cliente.pagocontado)
                {  // para los pagos con línea de crédito
                    SeguimientoData.InsertarActualizarLineaConsumidaCliente(cliente.lineaconsumida.Value, model.idcliente);
                }

                var orden = OrdenData.GetObtenerOrden_model(idorden);

                //generación de preliquidación.
                //
                if (model.idordentrabajo == null)
                {
                    if (cliente.pagocontado)
                    {
                        var igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
                        PreliquidacionModel modPreliquidacion = new PreliquidacionModel();
                        modPreliquidacion.totalpeso = orden.peso;
                        modPreliquidacion.totalvolumen = orden.volumen;
                        modPreliquidacion.subtotal = orden.subtotal.Value;
                        modPreliquidacion.total = orden.total.Value;
                        modPreliquidacion.igv = orden.igv.Value;
                        modPreliquidacion.totalbulto = orden.bulto;
                        modPreliquidacion.fecharegistro = DateTime.Now;
                        modPreliquidacion.idusuarioregistro = Convert.ToInt32(Usuario.Idusuario);
                        modPreliquidacion.idcliente = orden.idcliente;
                        modPreliquidacion.idestado = (Int32)DataAccess.Constantes.EstadoPreliquidacion.PendienteFactura;
                        modPreliquidacion.numeropreliquidacion = FacturacionData.ObtenerUltimaPreliquidacion();
                        modPreliquidacion._tipoop = 1;

                        ////insertar preliquidacion
                        idpreliquidacion =
                            new FacturacionData().InsertarActualizarPreliquidacion(modPreliquidacion);

                        new FacturacionData().ActualizarOTS(orden.idordentrabajo.Value.ToString(), idpreliquidacion);
                    }
                }

                #region registroincidencia

                if (!model.idordentrabajo.HasValue)
                {
                    IncidenciaModel modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeRegistro;
                    modIncidencia.idsorden = idorden.ToString();
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se registró la orden";
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
                }

                #endregion registroincidencia

                Session["guias"] = null;

                return Json(new
                {
                    res = true
                    ,
                    idpreliquidacion = idpreliquidacion
                    ,
                    ot = orden.numcp
                    ,
                    idorden = orden.idordentrabajo
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { res = false, mensaje = "Ocurrió un error" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Acciones

        #region Consultar

        [HttpPost]
        public JsonResult JsonListarFormulayTipoTransporteOT(int? idcliente, int? idorigen, int? iddestino)
        {
            if (idcliente == null) return Json(JsonRequestBehavior.AllowGet);
            if (idorigen == null) return Json(JsonRequestBehavior.AllowGet);
            if (iddestino == null) return Json(JsonRequestBehavior.AllowGet);

            var ubigeo = GetListarUbigeo_Cache();

            var tipotransporte = new OrdenData().GetListarTipoTransporteOT(idcliente, idorigen, iddestino, ubigeo);
            var listatipotransporte = new SelectList(
               tipotransporte,
               "idtipotransporte",
               "tipotransporte");

            var formula = new OrdenData().GetListarFormulaOT(idcliente, idorigen, iddestino, ubigeo);
            var listaformula = new SelectList(
               formula,
               "idformula",
               "formula");

            return Json(new { listatipotransporte = listatipotransporte, listaformula = listaformula }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonListarConceptoCobroT(int? idcliente, int? idorigen, int? iddestino, int? idformula, int? idtipotransporte)
        {
            if (idcliente == null) return Json(JsonRequestBehavior.AllowGet);
            if (idorigen == null) return Json(JsonRequestBehavior.AllowGet);
            if (iddestino == null) return Json(JsonRequestBehavior.AllowGet);
            if (idformula == null) return Json(JsonRequestBehavior.AllowGet);
            if (idtipotransporte == null) return Json(JsonRequestBehavior.AllowGet);

            var conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarConceptoCobroT(idcliente, idorigen, iddestino, idformula, idtipotransporte);
            if (conceptocobro.Count == 0)
                conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarConceptoCobroT(80, idorigen, iddestino, idformula, idtipotransporte);

            List<ListarConceptosTarifaDto> conceps = new List<ListarConceptosTarifaDto>();

            foreach (var item in conceptocobro)
            {
                if (item.concepto != null)
                    conceps.Add(item);
            }
            var listaconceptocobro = new SelectList(
               conceps,
               "idconceptocobro",
               "concepto");
            ViewData["ListaConceptoCobro"] = listaconceptocobro;

            return Json(listaconceptocobro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetListarOrdenes(string numcp, string fecinicio
            , string fecfin, int? idcliente, int? idestacion, string sidx, string sord, int page, int rows)
        {
            if (numcp == string.Empty) numcp = null;
            if (fecinicio == string.Empty) fecinicio = null;
            if (fecfin == string.Empty) fecfin = null;

            var listado = OrdenData.GetListarOrdenes(numcp, idcliente, fecinicio, fecfin, idestacion, page, 120);

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

        public JsonResult JsonGetListarGuias(string sord, int page, int rows)
        {
            var result = (List<GuiaRemisionModel>)Session["guias"];
            if (result == null) return Json(new { Mensaje = false }, JsonRequestBehavior.AllowGet);

            var listadoTotal = result;
            int pageindex = page - 1;
            int pagesize = rows;

            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderBy(s => s.idguiaremisioncliente).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.idguiaremisioncliente).ToList();
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

        public JsonResult JsonGetCliente(int idcliente)
        {
            var result = DataAccess.Seguimiento.SeguimientoData.GetObtenerCliente(idcliente);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDirecciones(int? idcliente)
        {
            if (idcliente == null) return Json(JsonRequestBehavior.AllowGet);

            var direcciones = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(idcliente.Value);

            var listadirecciones = new SelectList(
               direcciones,
               "iddireccion",
               "direccion");

            return Json(listadirecciones, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDireccionesAutocomplete(int? idcliente, int? iddestino)
        {
            if (idcliente == null) return Json(JsonRequestBehavior.AllowGet);

            var direcciones = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(idcliente.Value);
            direcciones = direcciones.Where(x => x.iddistrito.Equals(iddestino)).ToList();
            var listadirecciones = new SelectList(
               direcciones,
               "iddireccion",
               "direccion");

            List<string> direccioneslist = new List<string>();

            foreach (var item in listadirecciones)
            {
                direccioneslist.Add(item.Text);
            }

            return Json(direccioneslist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListarClientesProveedor(int? id)
        {
            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarProveedorxCliente(id);

            var listaclientes = new SelectList(
               clientes,
               "idproveedor",
               "razonsocial");

            return Json(listaclientes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerDatosVehiculo(int idvehiculo)
        {
            var vehiculo = OrdenData.GetObtenerVehiculo(idvehiculo);

            return Json(new { proveedor = vehiculo.proveedor, placa = vehiculo.placa, chofer = vehiculo.nombrechofer, dni = vehiculo.dni }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAutocomplete_Descripcion(int idcampo)
        {
            var helps = DataAccess.Monitoreo.MonitoreoData.ListarHelpResources(idcampo);
            List<string> helpsingle = new List<string>();
            foreach (var item in helps)
                helpsingle.Add(item.help);

            return Json(helpsingle, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonAutocomplete(int idcampo, int idcliente)
        {
            //var helps = DataAccess.Monitoreo.MonitoreoData.ListarHelpResources(idcampo);

            var puntospartida = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(idcliente);

            puntospartida = puntospartida.Where(x => x.puntopartida == true).ToList();

            List<string> helpsingle = new List<string>();
            foreach (var item in puntospartida)
                helpsingle.Add(item.direccion);

            return Json(helpsingle, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReporteOT(long idordentrabajo)
        {
            TempData["Reporte"] = "/Reportes TYS/Impresiones/RPOrdenTransporte";
            TempData["idordentrabajo"] = idordentrabajo;
            return PartialView("_ReporteOT");
        }

        #endregion Consultar

        #region combos

        public IEnumerable<ClienteModel> GetListarClientes_Cache()
        {
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

        public PartialViewResult GetControlDetailsGrid3(string control, string id)
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
                if (id == "") id = "1";
                else
                {
                    //traer tarifa
                    var tarifa = DataAccess.Seguimiento.SeguimientoData.GetObtenerTarifa(Convert.ToInt32(id));
                    var provincias = DataAccess.Seguimiento.SeguimientoData.GetListarProvincia(tarifa.iddepartamento);
                    var listaprovincias = new SelectList(
                       provincias,
                       "idprovincia",
                       "provincia");
                    ViewData["ListaProvincia"] = listaprovincias;
                }
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

                //var etapas = DataAccess.Facturacion.PagoData.GetListarEtapa("", false).Where(x => x.activo != false).ToList();
                var listaetapa = new SelectList(
                    etapas,
                    "idmaestroincidencia",
                    "descripcion");
                ViewData["ListaEtapa"] = listaetapa;
            }

            return PartialView("_controlgrid", control);
        }

        #endregion combos

        #region camioncompleto

        [HttpPost]
        public ActionResult AgregarCamionCompleto(OrdenTrabajoModel model)
        {
            var respuesta = string.Empty;
            List<GuiaRemisionModel> guias = (List<GuiaRemisionModel>)Session["guias"];

            var cliente = SeguimientoData.GetObtenerCliente(model.idcliente);

            if (cliente.lineaconsumida.HasValue)
            {
                cliente.lineaconsumida = cliente.lineaconsumida + model.subtotal;
                if (cliente.lineacredito < cliente.lineaconsumida)
                    return Json(new { res = false, mensaje = "El cliente no tiene línea de crédito para esta operación." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                cliente.lineaconsumida = model.subtotal;
                if (cliente.lineacredito < cliente.lineaconsumida)
                    return Json(new { res = false, mensaje = "El cliente no tiene línea de crédito para esta operación." }, JsonRequestBehavior.AllowGet);
            }

            if (model.total == null)
                return Json(new { res = false, mensaje = "Debe recalcular la OT" }, JsonRequestBehavior.AllowGet);

            try
            {
                //actualizar Help de Punto de Partida
                //Actualizar descripciones general

                #region propiedades

                model._tipoop = 1;
                model.idusuarioregistro = (Int32)Usuario.Idusuario;
                model.activo = true;
                model.fecharegistro = DateTime.Now;
                model.iddestinatario = model.iddestinatario_selected;
                model.idremitente = model.idcliente;
                model.fecharecojo = DateTime.Now;

                var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));
                model.idestacionorigen = usuario.idestacionorigen;

                if (model.pesogeneral.HasValue)
                    model.peso = model.pesogeneral.Value;
                if (model.volgeneral.HasValue)
                    model.volumen = model.volgeneral.Value;
                if (model.bultogeneral.HasValue)
                    model.bulto = model.bultogeneral.Value;

                model.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteFacturacion;
                if (model.idordentrabajo == null)
                    model.numcp = OrdenData.ObtenerUltimaOT();
                else
                {
                    var auxmodel = OrdenData.GetObtenerOrden_model(model.idordentrabajo.Value);
                    model.numcp = auxmodel.numcp;
                }

                #endregion propiedades

                string resp = string.Empty;
                var idorden = OrdenData.InsertarActualizarOTLigera(model, guias);
                model.idordentrabajo = idorden;

                SeguimientoData.InsertarActualizarLineaConsumidaCliente(cliente.lineaconsumida.Value, model.idcliente);

                var orden = OrdenData.GetObtenerOrden_model(idorden);

                if (model.idordentrabajo.HasValue)
                {
                    IncidenciaModel modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeRegistro;
                    modIncidencia.idsorden = idorden.ToString();
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se registró la orden";
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
                }

                Session["guias"] = null;

                return Json(new { res = true, ot = orden.numcp }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { res = false, mensaje = "Ocurrió un error" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult JsonListarFormulayTipoTransporteCamion(int? idcliente, int? idorigen, int? iddestino)
        {
            if (idcliente == null) return Json(JsonRequestBehavior.AllowGet);
            if (idorigen == null) return Json(JsonRequestBehavior.AllowGet);
            if (iddestino == null) return Json(JsonRequestBehavior.AllowGet);

            var ubigeo = GetListarUbigeo_Cache();

            var tipotransporte = new DataAccess.Seguimiento.SeguimientoData().GetListarTipoTransporteCamion(idcliente, idorigen, iddestino, ubigeo);
            var listatipotransporte = new SelectList(
               tipotransporte,
               "idtipotransporte",
               "tipotransporte");

            var formula = new DataAccess.Seguimiento.SeguimientoData().GetListarFormulaCamion(idcliente, idorigen, iddestino, ubigeo);
            var listaformula = new SelectList(
               formula,
               "idformula",
               "formula");

            return Json(new { listatipotransporte = listatipotransporte, listaformula = listaformula }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CamionCompleto()
        {
            var ubigeos = GetListarUbigeo_Cache();
            var listaubigeo = new SelectList(
                ubigeos,
                "iddistrito",
                "ubigeo");
            ViewData["ListaUbigeo"] = listaubigeo;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            CamionCompletoModel model = new CamionCompletoModel();

            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            model.idestacionorigen = usuario.idestacionorigen;

            return View(model);
        }

        [HttpPost]
        public JsonResult JsonListarUnidadCamion(int? idcliente, int? idorigen, int? iddestino)
        {
            if (idcliente == null) return Json(JsonRequestBehavior.AllowGet);
            if (idorigen == null) return Json(JsonRequestBehavior.AllowGet);
            if (iddestino == null) return Json(JsonRequestBehavior.AllowGet);

            var ubigeo = GetListarUbigeo_Cache();

            var tipounidad = new DataAccess.Seguimiento.SeguimientoData().GetListarTipoUnidadCamion(idcliente, idorigen, iddestino, ubigeo);
            var listatipounidad = new SelectList(
               tipounidad,
               "idtipounidad",
               "tipounidad");

            return Json(new { listatipounidad = listatipounidad }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonGetListarCamionCompleto(string codigo, int? iddestino, int? idestacion)
        {
            if (codigo == string.Empty) codigo = null;

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarCamionCompleto(codigo, iddestino, idestacion);

            var displayedDocumentos = listado;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = displayedDocumentos.Count();
            var data = displayedDocumentos.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CalcularPagoCamionCompleto(int? origen, int? destino, int? formula
          , int? unidad, int? cliente, int? idconceptocobro, decimal? sobreestadia)
        {
            try
            {
                var tot_result = OrdenData.GetListarTarifasOrden(origen.Value, destino.Value, cliente.Value, formula.Value, 59, idconceptocobro).ToList();
                //ListarTarifaOrdenDto result = new ListarTarifaOrdenDto();

                tot_result = tot_result.Where(x => x.idtipounidad.Equals(unidad)).ToList();

                var result = tot_result.Where(x => x.formula == "Camion").SingleOrDefault();
                if (result == null) result = tot_result.FirstOrDefault();

                if (result == null || result.precio == 0)
                    return Json(new { res = false, mensaje = "No existe una tarifa asociada" }, JsonRequestBehavior.AllowGet);

                var res_formula = new DataAccess.Seguimiento.SeguimientoData().GetFormula(formula.Value);

                var Base = result.montobase;

                res_formula.formula = res_formula.formula.Replace("Base", result.montobase.ToString());
                res_formula.formula = res_formula.formula.Replace("Precio", result.precio.ToString());
                //res_formula.formula = res_formula.formula.Replace("KG", peso.ToString());
                //res_formula.formula = res_formula.formula.Replace("M3", volumen.ToString());

                var minimo = result.minimo;
                var base1 = result.montobase;
                var tarifa = result.precio;

                //var Total = Math.Round(Evaluate(res_formula.formula),2);
                //var SubTotal = Math.Round(Total / 1.18, 2);
                //var IGV = Math.Round(SubTotal * 0.18,2);

                var SubTotal = Math.Round(Convert.ToDouble(result.precio), 2);
                if (sobreestadia != null)
                    SubTotal = Math.Round(SubTotal + Convert.ToDouble(sobreestadia.Value), 2);

                var IGV = Math.Round(SubTotal * 0.18, 2);
                var Total = Math.Round(SubTotal + IGV, 2);

                return Json(new { minimo = minimo, tarifa = tarifa, base1 = base1, total = Total, subtotal = SubTotal, igv = IGV }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { res = false, Errors = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult JsonListarConceptoCobroCamion(int? idcliente, int? idorigen, int? iddestino, int? idformula, int? idtipotransporte)
        {
            if (idcliente == null) return Json(JsonRequestBehavior.AllowGet);
            if (idorigen == null) return Json(JsonRequestBehavior.AllowGet);
            if (iddestino == null) return Json(JsonRequestBehavior.AllowGet);
            if (idformula == null) return Json(JsonRequestBehavior.AllowGet);
            if (idtipotransporte == null) return Json(JsonRequestBehavior.AllowGet);

            var conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarConceptoCobroT(idcliente, idorigen, iddestino, idformula, idtipotransporte);
            List<ListarConceptosTarifaDto> conceps = new List<ListarConceptosTarifaDto>();

            foreach (var item in conceptocobro)
            {
                if (item.concepto != null)
                    conceps.Add(item);
            }
            var listaconceptocobro = new SelectList(
               conceps,
               "idconceptocobro",
               "concepto");
            ViewData["ListaConceptoCobro"] = listaconceptocobro;

            return Json(listaconceptocobro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NuevoCamionCompleto()
        {
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            OrdenTrabajoModel model = new OrdenTrabajoModel();
            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            model.idestacionorigen = usuario.idestacionorigen;
            model.camioncompleto = true;
            model.registrorapido = false;
            model.devolucion = false;
            model.idtipotransporte = (Int32)Constantes.TipoTransporte.Terrestre;

            Session["guias"] = null;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);  //GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var tipounidad = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.Unidad))).ToList();
            var listatipounidad = new SelectList(
               tipounidad,
               "idvalortabla",
               "valor");
            ViewData["ListaTipoUnidad"] = listatipounidad;

            var ruta = DataAccess.Seguimiento.SeguimientoData.GetListarRuta("");
            var listaruta = new SelectList(
            ruta,
            "idruta",
            "nombre");
            ViewData["ListaRuta"] = listaruta;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            return View(model);
        }

        public ActionResult NuevaOrdenCamionTrabajo(long id)
        {
            var camioncompleto = OrdenData.GetObtenerCamionCompleto(id);
            var usuario = UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            OrdenTrabajoModel model = new OrdenTrabajoModel();
            model.idestacionorigen = usuario.idestacionorigen;
            model.idorigen = usuario.iddistrito;

            Session["guias"] = null;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var tipostransporte = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte))).ToList();
            var listatipos = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData["ListaModoTransporte"] = listatipos;

            var cargadaen = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.CargadaEn))).ToList(); ;
            var listacargadaen = new SelectList(
               cargadaen,
               "idvalortabla",
               "valor");
            ViewData["ListaCargadaEn"] = listacargadaen;

            var entregara = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.EntregarA))).ToList();
            var listaentregara = new SelectList(
               entregara,
               "idvalortabla",
               "valor");
            ViewData["ListaEntregarA"] = listaentregara;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var tipovehiculo = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.TipoVehiculo))).ToList();
            var listatipovehiculo = new SelectList(
                tipovehiculo,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoVehiculo"] = listatipovehiculo;

            var tipomercaderia = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.TipoMercaderia))).ToList();
            var listatipomercaderia = new SelectList(
                tipomercaderia,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoMercaderia"] = listatipomercaderia;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            model.cantidad = "1";
            model.cambioncompleto = camioncompleto.codigocamioncompleto;
            model.idcarga = camioncompleto.idcarga;
            model.idorigen = camioncompleto.idorigen;
            model.idcliente = camioncompleto.idcliente;
            model.idcamioncompleto = camioncompleto.idcamioncompleto;
            model.idvehiculo = camioncompleto.idvehiculo;
            model.idtipotransporte = camioncompleto.idtipotransporte;
            model.idtipooperacion = camioncompleto.idtipooperacion;

            return View(model);
        }

        public ActionResult EditarCamionCompleto(long id)
        {
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));
            var model = OrdenData.GetObtenerOrden_model(id);

            model.idestacionorigen = usuario.idestacionorigen;
            model.camioncompleto = true;
            model.registrorapido = false;
            model.devolucion = false;
            model.idtipotransporte = (Int32)Constantes.TipoTransporte.Terrestre;

            var ruta = SeguimientoData.GetListarRuta("");
            var listaruta = new SelectList(
            ruta,
            "idruta",
            "nombre");
            ViewData["ListaRuta"] = listaruta;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);
            var tipounidad = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.Unidad))).ToList();
            var listatipounidad = new SelectList(
               tipounidad,
               "idvalortabla",
               "valor");
            ViewData["ListaTipoUnidad"] = listatipounidad;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var placas = PagoData.GetListarPlaca(null)
                 .Where(x => x.idestado.Equals((Int32)DataAccess.Constantes.EstadoVehiculo.Inspeccionado)).ToList();

            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            return View(model);
        }

        //abre pagina de creacion de hijas de CC con el id del padre
        public ActionResult NuevaOrdenTrabajoLigeraCC(long id)
        {
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            var model = OrdenData.GetObtenerOrden_model(id);

            //OrdenTrabajoModel model = new OrdenTrabajoModel();
            model.idestacionorigen = usuario.idestacionorigen;
            model.iddestino = null;
            model.idorigenaux = model.idorigen.Value;
            model.idclienteaux = model.idcliente;
            model.camioncompleto = false;
            model.registrorapido = true;
            model.devolucion = false;
            model.numcamioncompleto = model.numcp;
            model.idcamioncompleto = id;

            Session["guias"] = null;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);  //GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var tipostransporte = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte))).ToList();
            var listatipos = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData["ListaModoTransporte"] = listatipos;

            var ruta = DataAccess.Seguimiento.SeguimientoData.GetListarRuta("");
            var listaruta = new SelectList(
            ruta,
            "idruta",
            "nombre");
            ViewData["ListaRuta"] = listaruta;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            model.cantidad = "1";
            return View(model);
        }

        //abre edicion de hijas de CC con el id del hijo a editar
        public ActionResult EditarOrdenTrabajoLigeraCC(long id)
        {
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));
            var guiasbd = OrdenData.GetListarGuias(id).Hits.ToList();
            var item = new GuiaRemisionModel();
            var guias = new List<GuiaRemisionModel>();
            foreach (var guia in guiasbd)
            {
                item = new GuiaRemisionModel();
                item.idguiaremisioncliente = guia.idguiaremisioncliente;
                if (guia.bulto != null)
                    item.bulto = Convert.ToInt32(guia.bulto);
                item.nroguia = guia.nroguia;
                if (guia.peso != null)
                    item.peso = Convert.ToDecimal(guia.peso);
                if (guia.volumen != null)
                    item.volumen = Convert.ToDecimal(guia.volumen);
                if (guia.pesovol != null)
                    item.pesovol = Convert.ToDecimal(guia.pesovol);
                item.documento = guia.documento;
                guias.Add(item);
            }

            Session["guias"] = guias;
            var model = OrdenData.GetObtenerOrden_model(id);

            //OrdenTrabajoModel model = new OrdenTrabajoModel();
            model.idestacionorigen = usuario.idestacionorigen;
            // model.iddestino = null;
            model.idorigenaux = model.idorigen.Value;
            model.idclienteaux = model.idcliente;
            model.camioncompleto = false;
            model.registrorapido = true;
            model.devolucion = false;
            model.numcamioncompleto = model.numcp;
            model.idcamioncompleto = id;
            model.razonsocialdestinatario = model.destinatario;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);  //GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var tipostransporte = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte))).ToList();
            var listatipos = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData["ListaModoTransporte"] = listatipos;

            var ruta = DataAccess.Seguimiento.SeguimientoData.GetListarRuta("");
            var listaruta = new SelectList(
            ruta,
            "idruta",
            "nombre");
            ViewData["ListaRuta"] = listaruta;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            model.cantidad = "1";
            return View(model);
        }

        #endregion camioncompleto

        #region otrapida

        public ActionResult OrdenesRapidas()
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

            return View(model);
        }

        public ActionResult NuevaOrdenTrabajoLigera()
        {
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            OrdenTrabajoModel model = new OrdenTrabajoModel();
            model.idestacionorigen = usuario.idestacionorigen;
            model.idorigen = usuario.iddistrito;
            model.camioncompleto = false;
            model.registrorapido = true;
            model.devolucion = false;

            Session["guias"] = null;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var clientes = DataAccess.Seguimiento.SeguimientoData.GetListarClientes("", false);  //GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var tipostransporte = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.ModoTransporte))).ToList();
            var listatipos = new SelectList(
                tipostransporte,
                "idvalortabla",
                "valor");
            ViewData["ListaModoTransporte"] = listatipos;

            var ruta = DataAccess.Seguimiento.SeguimientoData.GetListarRuta("");
            var listaruta = new SelectList(
            ruta,
            "idruta",
            "nombre");
            ViewData["ListaRuta"] = listaruta;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            model.cantidad = "1";
            return View(model);
        }

        public ActionResult EditarOrdenTrabajoLigera(long? id)
        {
            var guias = new List<GuiaRemisionModel>();
            GuiaRemisionModel item;

            var model = OrdenData.GetObtenerOrden_model(id.Value);
            var guiasbd = OrdenData.GetListarGuias(id.Value).Hits.ToList();
            var usuario = DataAccess.Seguridad.UsuariosData.ObtenerUsuario(Convert.ToInt32(Usuario.Idusuario));

            foreach (var guia in guiasbd)
            {
                item = new GuiaRemisionModel();
                item.idguiaremisioncliente = guia.idguiaremisioncliente;
                if (guia.bulto != null)
                    item.bulto = Convert.ToInt32(guia.bulto);
                item.nroguia = guia.nroguia;
                if (guia.peso != null)
                    item.peso = Convert.ToDecimal(guia.peso);
                if (guia.volumen != null)
                    item.volumen = Convert.ToDecimal(guia.volumen);
                if (guia.pesovol != null)
                    item.pesovol = Convert.ToDecimal(guia.pesovol);
                item.documento = guia.documento;
                guias.Add(item);
            }

            Session["guias"] = guias;

            //OrdenTrabajoModel model = new OrdenTrabajoModel();

            #region asignarpropiedades

            //model.idorigen = usuario.iddistrito;
            model.razonsocialdestinatario = model.destinatario;
            model.iddestinatario_selected = model.iddestinatario;

            model.idremitente_selected = model.idremitente;
            model.pesogeneral = model.peso;
            model.volgeneral = model.volumen;
            model.bultogeneral = model.bulto;
            model.cantidad = "1";
            model.horarecojo = model.fecharecojo.Value.Hour + ":" + model.fecharecojo.Value.Minute;
            model.fecharecojo = model.fecharecojo.Value.Date;
            model.idordentrabajo = id;

            #endregion asignarpropiedades

            #region combos

            var ListadoValores = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(null);

            var direcciones = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(model.idcliente);
            var listadirecciones = new SelectList(
               direcciones,
               "iddireccion",
               "direccion");
            ViewData["ListaDirecciones"] = listadirecciones;

            var direccionesremitente = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(model.idremitente);
            var listadireccionesremitente = new SelectList(
               direccionesremitente,
               "iddireccion",
               "direccion");
            ViewData["ListaDireccionesRemitente"] = listadireccionesremitente;

            var ubigeos = GetListarUbigeo_Cache();
            var listaUbigeos = new SelectList(
             ubigeos,
             "iddistrito",
             "ubigeo");
            ViewData["listaubigeos"] = listaUbigeos;

            var formula = new OrdenData().GetListarFormulaOT(model.idcliente, model.idorigen, model.iddestino, ubigeos);
            var listaformula = new SelectList(
               formula,
               "idformula",
               "formula");
            ViewData["ListadoFormula"] = listaformula;

            var clientes = GetListarClientes_Cache();
            var listaclientes = new SelectList(
                clientes,
                "idcliente"
                , "razonsocial"
                );
            ViewData["ListaClientes"] = listaclientes;

            if (model.idremitente == model.idcliente)
            {
                var clientesproveedor = GetListarClientes_Cache().Where(x => x.idcliente.Equals(model.idcliente));
                var listaclientesproveedor = new SelectList(
                   clientesproveedor,
                   "idcliente",
                   "razonsocial");
                ViewData["ListaClientesProveedor"] = listaclientesproveedor;
            }
            else
            {
                var clientesproveedor = DataAccess.Seguimiento.SeguimientoData.GetListarProveedorxCliente(model.idcliente);
                var listaclientesproveedor = new SelectList(
                   clientesproveedor,
                   "idproveedor",
                   "razonsocial");
                ViewData["ListaClientesProveedor"] = listaclientesproveedor;
            }

            var tipostransporte = new OrdenData().GetListarTipoTransporteOT(model.idcliente, model.idorigen, model.iddestino, ubigeos);
            var listatipos = new SelectList(
               tipostransporte,
               "idtipotransporte",
               "tipotransporte");
            ViewData["ListaModoTransporte"] = listatipos;

            var conceptocobro = DataAccess.Seguimiento.SeguimientoData.GetListarConceptoCobroT(model.idcliente, model.idorigen
                , model.iddestino, model.idformula, model.idtipotransporte);
            List<ListarConceptosTarifaDto> conceps = new List<ListarConceptosTarifaDto>();

            foreach (var item1 in conceptocobro)
            {
                if (item1.concepto != null)
                    conceps.Add(item1);
            }
            var listaconceptocobro = new SelectList(
               conceps,
               "idconceptocobro",
               "concepto");
            ViewData["ListaConceptoCobro"] = listaconceptocobro;

            var cargadaen = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.CargadaEn))).ToList(); ;
            var listacargadaen = new SelectList(
               cargadaen,
               "idvalortabla",
               "valor");
            ViewData["ListaCargadaEn"] = listacargadaen;

            var entregara = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.EntregarA))).ToList();
            var listaentregara = new SelectList(
               entregara,
               "idvalortabla",
               "valor");
            ViewData["ListaEntregarA"] = listaentregara;

            var estacion = DataAccess.Seguimiento.SeguimientoData.GetListarEstacionOrigen().ToList();
            var listaestacion = new SelectList(
               estacion,
               "idestacion",
               "estacionorigen");
            ViewData["ListaEstacion"] = listaestacion;

            var tipovehiculo = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoVehiculo)).Where(x => x.activo == true).ToList();
            var listatipovehiculo = new SelectList(
                tipovehiculo,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoVehiculo"] = listatipovehiculo;

            var tipomercaderia = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.TipoMercaderia)).Where(x => x.activo == true).ToList();
            var listatipomercaderia = new SelectList(
                tipomercaderia,
                "idvalortabla",
                "valor");
            ViewData["ListaTipoMercaderia"] = listatipomercaderia;

            var placas = PagoData.GetListarPlaca(null).ToList();
            var listaplaca = new SelectList(
                placas,
                "idvehiculo",
                "placa");
            ViewData["ListadoPlaca"] = listaplaca;

            var descripciongrr = ListadoValores.Where(x => x.idmaestrotabla.Equals(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR))).ToList();
            var listadescripciongrr = new SelectList(
                descripciongrr,
                "idvalortabla",
                "valor");
            ViewData["ListaDescripcionGRR"] = listadescripciongrr;

            var ruta = SeguimientoData.GetListarRuta("");
            var listaruta = new SelectList(
            ruta,
            "idruta",
            "nombre");
            ViewData["ListaRuta"] = listaruta;

            #endregion combos

            return View(model);
        }

        public JsonResult AutoGenerarDespacho(long id)
        {
            var model = OrdenData.GetObtenerOrden_model(id);
            model._tipoop = 1;
            model.idusuarioregistro = (Int32)Usuario.Idusuario;
            model.activo = true;
            model.fecharegistro = DateTime.Now;
            model.iddestinatario = model.iddestinatario_selected;
            model.idremitente = model.idcliente;
            if (model.pesogeneral.HasValue)
                model.peso = model.pesogeneral.Value;
            if (model.volgeneral.HasValue)
                model.volumen = model.volgeneral.Value;
            if (model.bultogeneral.HasValue)
                model.bulto = model.bultogeneral.Value;

            if (model.camioncompleto)
            {
                var ordeneshijas = OrdenData.GetListarOrdenesHijasCC(model.idordentrabajo.Value);

                foreach (var item in ordeneshijas)
                {
                    item.idruta = model.idruta;
                    item.idusuarioregistro = model.idusuarioregistro;
                    item.idvehiculo = model.idvehiculo;

                    var respuestas = OrdenData.AutogeneraOperaciones(item);

                    IncidenciaModel modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SePlanifico;
                    modIncidencia.idsorden = item.idordentrabajo.ToString();
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se planificó la Orden  - N° Carga :" + respuestas.numcarga;
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                    modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ManifiestoHR;
                    modIncidencia.idsorden = item.idordentrabajo.ToString();
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se generó el manifiesto: " + respuestas.nummanifiesto;
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                    modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeDespacho;
                    modIncidencia.idsorden = item.idordentrabajo.ToString();
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se despachó la Orden";
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
                }
            }
            else
            {
                var respuestas = OrdenData.AutogeneraOperaciones(model);
                IncidenciaModel modIncidencia = new IncidenciaModel();
                modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SePlanifico;
                modIncidencia.idsorden = id.ToString();
                modIncidencia.fechaincidencia = DateTime.Now;
                modIncidencia.fecharegistro = DateTime.Now;
                modIncidencia.descripcion = "Se planificó la Orden  - N° Carga :" + respuestas.numcarga;
                modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                modIncidencia.activo = true;
                IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                modIncidencia = new IncidenciaModel();
                modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ManifiestoHR;
                modIncidencia.idsorden = id.ToString();
                modIncidencia.fechaincidencia = DateTime.Now;
                modIncidencia.fecharegistro = DateTime.Now;
                modIncidencia.descripcion = "Se generó el manifiesto: " + respuestas.nummanifiesto;
                modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                modIncidencia.activo = true;
                IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

                modIncidencia = new IncidenciaModel();
                modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeDespacho;
                modIncidencia.idsorden = id.ToString();
                modIncidencia.fechaincidencia = DateTime.Now;
                modIncidencia.fecharegistro = DateTime.Now;
                modIncidencia.descripcion = "Se despachó la Orden";
                modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                modIncidencia.activo = true;
                IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
            }

            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarOTLigeraModal(OrdenTrabajoModel model)
        {
            List<GuiaRemisionModel> guias = (List<GuiaRemisionModel>)Session["guias"];
            string resp = string.Empty;

            var cliente = SeguimientoData.GetObtenerCliente(model.idcliente);

            if (!cliente.pagocontado)
            {
                if (cliente.lineaconsumida.HasValue)
                {
                    cliente.lineaconsumida = cliente.lineaconsumida + model.subtotal;
                    if (cliente.lineacredito < cliente.lineaconsumida)
                        return Json(new { res = false, mensaje = "El cliente no tiene línea de crédito para esta operación." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    cliente.lineaconsumida = model.subtotal;
                    if (cliente.lineacredito < cliente.lineaconsumida)
                        return Json(new { res = false, mensaje = "El cliente no tiene línea de crédito para esta operación." }, JsonRequestBehavior.AllowGet);
                }
            }

            if (model.total == null)
                return Json(new { res = false, mensaje = "Debe recalcular la OT" }, JsonRequestBehavior.AllowGet);

            if (guias == null)
                return Json(new { res = false, mensaje = "Debe ingresar al menos una Guía de Remisión." }, JsonRequestBehavior.AllowGet);
            else if (guias.Count == 0)
                return Json(new { res = false, mensaje = "Debe ingresar al menos una Guía de Remisión." }, JsonRequestBehavior.AllowGet);

            try
            {
                #region Asigna_propiedades

                //Si es nuevo registro
                if (model.idordentrabajo == null)
                {
                    model.numcp = OrdenData.ObtenerUltimaOT();
                    model.fecharegistro = DateTime.Now;
                    model.idusuarioregistro = (Int32)Usuario.Idusuario;
                    model.fecharecojo = DateTime.Now;
                    model.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteProgramacion;
                }

                model._tipoop = 1;

                model.activo = true;
                model.iddestinatario = model.iddestinatario_selected;
                model.idremitente = model.idcliente;
                if (model.pesogeneral.HasValue)
                    model.peso = model.pesogeneral.Value;
                if (model.volgeneral.HasValue)
                    model.volumen = model.volgeneral.Value;
                if (model.bultogeneral.HasValue)
                    model.bulto = model.bultogeneral.Value;

                #endregion Asigna_propiedades

                var idorden = OrdenData.InsertarActualizarOTLigera(model, guias);
                Session["guias"] = null;
                model.idordentrabajo = idorden;

                var orden = OrdenData.GetObtenerOrden_model(idorden);

                if (!cliente.pagocontado)
                {  // para los pagos con línea de crédito
                    SeguimientoData.InsertarActualizarLineaConsumidaCliente(cliente.lineaconsumida.Value, model.idcliente);
                }
                //generación de preliquidación.
                if (cliente.pagocontado)
                {
                    var igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
                    PreliquidacionModel modPreliquidacion = new PreliquidacionModel();
                    modPreliquidacion.totalpeso = orden.peso;
                    modPreliquidacion.totalvolumen = orden.volumen;
                    modPreliquidacion.subtotal = orden.subtotal.Value;
                    modPreliquidacion.total = orden.total.Value;
                    modPreliquidacion.igv = orden.igv.Value;
                    modPreliquidacion.totalbulto = orden.bulto;
                    modPreliquidacion.fecharegistro = DateTime.Now;
                    modPreliquidacion.idusuarioregistro = Convert.ToInt32(Usuario.Idusuario);
                    modPreliquidacion.idcliente = orden.idcliente;
                    modPreliquidacion.idestado = (Int32)DataAccess.Constantes.EstadoPreliquidacion.PendienteFactura;
                    modPreliquidacion.numeropreliquidacion = FacturacionData.ObtenerUltimaPreliquidacion();
                    modPreliquidacion._tipoop = 1;

                    ////insertar preliquidacion
                    var idpreliquidacion =
                        new FacturacionData().InsertarActualizarPreliquidacion(modPreliquidacion);

                    new FacturacionData().ActualizarOTS(orden.idordentrabajo.Value.ToString(), idpreliquidacion);
                }

                if (model.idordentrabajo.HasValue)
                {
                    IncidenciaModel modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeRegistro;
                    modIncidencia.idsorden = idorden.ToString();
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se registró la orden";
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
                }
                return Json(new { res = true, ot = orden.numcp }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { res = false, mensaje = "Ocurrió un error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AgregarOTLigeraModalCC(OrdenTrabajoModel model)
        {
            var respuesta = string.Empty;
            List<GuiaRemisionModel> guias = (List<GuiaRemisionModel>)Session["guias"];

            var cliente = SeguimientoData.GetObtenerCliente(model.idcliente);

            if (!model.idtipovehiculo.HasValue)
            {
                if (guias == null)
                    return Json(new { res = false, mensaje = "Debe ingresar al menos una Guía de Remisión." }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                //actualizar Help de Punto de Partida
                //Actualizar descripciones general

                #region propiedades

                model._tipoop = 1;
                model.idusuarioregistro = (Int32)Usuario.Idusuario;
                model.activo = true;
                model.fecharegistro = DateTime.Now;
                model.iddestinatario = model.iddestinatario_selected;
                model.idcliente = model.idclienteaux;
                model.idremitente = model.idcliente;

                model.idorigen = model.idorigenaux;
                model.fecharecojo = DateTime.Now;

                if (model.pesogeneral.HasValue)
                    model.peso = model.pesogeneral.Value;
                if (model.volgeneral.HasValue)
                    model.volumen = model.volgeneral.Value;
                if (model.bultogeneral.HasValue)
                    model.bulto = model.bultogeneral.Value;

                model.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteProgramacion;
                if (model.idordentrabajo == null)
                    model.numcp = OrdenData.ObtenerUltimaOT();
                model.nofacturable = true;
                model.registrorapido = true;

                #endregion propiedades

                string resp = string.Empty;
                var idorden = OrdenData.InsertarActualizarOTLigera(model, guias);
                model.idordentrabajo = idorden;

                //SeguimientoData.InsertarActualizarLineaConsumidaCliente(cliente.lineaconsumida.Value, model.idcliente);

                var orden = OrdenData.GetObtenerOrden_model(idorden);

                if (model.idordentrabajo.HasValue)
                {
                    IncidenciaModel modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.SeRegistro;
                    modIncidencia.idsorden = idorden.ToString();
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se registró la orden";
                    modIncidencia.idusuarioregistro = (Int32)Usuario.Idusuario;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
                }

                Session["guias"] = null;

                return Json(new { res = true, ot = orden.numcp }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { res = false, mensaje = "Ocurrió un error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonGetListarOrdenesrRapidas(string numcp, int? idcliente, bool? devolucion, string sidx, string sord, int page, int rows)
        {
            if (numcp == string.Empty) numcp = null;
            if (devolucion == null) devolucion = false;

            var listado = OrdenData.GetListarOrdenesLigeras(numcp, idcliente, devolucion.Value).ToList();

            listado.ForEach(x =>
            {
                if (x.camioncompleto)
                {
                    x.tipoot = "Camión Completo";
                }
                else if (x.registrorapido)
                {
                    x.tipoot = "Registro Rápido";
                }
                else x.tipoot = "Devolución";
            });

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

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonGetListarCamionCompletoDetalle(long idcamioncompleto, string sidx, string sord, int page, int rows)
        {
            var listado = OrdenData.GetListarOrdenesHijasCC(idcamioncompleto).ToList();

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

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listado
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion otrapida

        public JsonResult JsonPagoContado(long idordentrabajo)
        {
            return Json(new { res = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult PreFactura(long idpreliquidacion)
        {
            Session["detallefacturacion"] = null;


            //var comprobante = new FacturacionData().ObtenerComprobante(idpreliquidacion);
            decimal igv = Convert.ToDecimal(ConfigurationManager.AppSettings["igv"].ToString());
            var liquidacion = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion).FirstOrDefault();
            var usuario = UsuariosData.ObtenerUsuario((Int32)Usuario.Idusuario);
            var series = new FacturacionData().GetListarDocumentos((int)Constantes.TipoComprobante.Factura, usuario.usr_int_id, usuario.idestacionorigen).ToList();
            string guias = "", descripcion = "";

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

        [HttpPost]
        public ActionResult GridSaveDetalle(FormCollection formcollection)
        {
            var listado = (List<ComprobanteDetalleModel>)Session["detallefacturacion"];

            int? iddetalle = Convert.ToInt16(formcollection["iddetallecomprobante"]);


            decimal recargo = 0;
            if (formcollection["recargo"] != "")
                recargo = decimal.Parse(formcollection["recargo"]);


            if (iddetalle == 0)
            {
                int max = listado.Count;
                var detalle = new ComprobanteDetalleModel
                {
                    idcomprobantepago = 1,
                    cantidad = 1,
                    unidadmedida = DataAccess.Seguimiento.SeguimientoData
                                   .GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.Unidad))
                                   .Where(x => x.idvalortabla == 76).First().valor,
                    recargo = recargo,
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
                        item.subtotal = item.subtotal + recargo;
                        item.recargo = recargo;
                        break;
                    }
                }
            }


            Session["detallefacturacion"] = listado;

            return Json(new { res = true, Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
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
            var ordencompra = Request.Form.GetValues("ordencompra").FirstOrDefault();

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

            AutoMapper.Mapper.CreateMap<PreliquidacionModel, Web.TYS.Areas.Facturacion.Models.ComprobanteModel>();
            var model = AutoMapper.Mapper.Map<PreliquidacionModel, Web.TYS.Areas.Facturacion.Models.ComprobanteModel>(preliquidacion);

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
                return Json(new { res = false }, JsonRequestBehavior.AllowGet);
            if (numerodocumento == "")
                return Json(new { res = false }, JsonRequestBehavior.AllowGet);
            if (fecha == "")
                return Json(new { res = false }, JsonRequestBehavior.AllowGet);


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

            var preliquidacion = new FacturacionData().ObtenerPreliquidacion(idpreliquidacion.Value).FirstOrDefault();

            AutoMapper.Mapper.CreateMap<PreliquidacionModel, Web.TYS.Areas.Facturacion.Models.ComprobanteModel>();
            var model = AutoMapper.Mapper.Map<PreliquidacionModel, Web.TYS.Areas.Facturacion.Models.ComprobanteModel>(preliquidacion);

           





            #region asignarpropiedades

            if (tipocomprobante == "FACT")
                model.idtipocomprobante = (Int16)DataAccess.Constantes.TipoComprobante.Factura;
            else
                model.idtipocomprobante = (Int16)DataAccess.Constantes.TipoComprobante.Boleta;
            model.idusuarioregistro = (Int32)Usuario.Idusuario;

            int cantidadcaracteres = 0;

            var numerocomprobante = new FacturacionData().obtenerNumeroComprobante(
                Usuario.Idusuario
                , model.idtipocomprobante
                , usuario.idestacionorigen
                , null);

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
          //  var otspreliquidadas = new FacturacionData().GetListarCompletadoPreliquidacion(idpreliquidacion.Value);

            foreach (var item in listado)
            {
                modDetalle = new ComprobanteDetalleModel();
                modDetalle.idcomprobantepago = idcomprobante;
                //modDetalle.idordentrabajo = item.idordentrabajo;
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
    }
}