using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Componentes.Common.Utilidades;
using ServiceAgents.Common;
using CommandContracts.TYS.Pago;
using QueryContracts.TYS.Seguimiento.Results;
using QueryContracts.TYS.Seguimiento.Parameters;
using CommandContracts.TYS.Seguimiento.Output;
using Web.TYS.Areas.Seguimiento.Models.Seguimiento;
using CommandContracts.TYS.Seguimiento;
using System.Configuration;
using Web.TYS.Areas.Monitoreo.Models;
using AutoMapper;
using Web.TYS.Areas.Facturacion.Models;
using Web.TYS.Areas.Pago.Models;
using Web.TYS.DataAccess.Monitoreo;

namespace Web.TYS.DataAccess.Seguimiento
{
    public class SeguimientoData
    {
        #region formula

        public static List<ListarFormulasDto> GetListarFormula(string search, bool inactivo)
        {
            var parametros = new ListarFormulasParameters { criterio = search, activo = (inactivo == true ? false : true) };
            var resultado = (ListarFormulasResult)parametros.Execute();
            return resultado == null ? new List<ListarFormulasDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarFormulaOutput InsertarActualizarFormula(FormulaModel model, out string res)
        {
            if (model.idformula == 0)
                model.idformula = null;
            var comando = new InsertarActualizarFormulaCommand();
            comando.activo = model.activo;
            comando.formula = model.formula;
            comando.nombre = model.nombre;
            comando.idformula = model.idformula;
            res = "OK";

            var respuesta = (InsertarActualizarFormulaOutput)comando.Execute();
            return respuesta;
        }

        public static InsertarActualizarFormulaOutput InsertarActualizarProveedorCliente(FormulaModel model)
        {
            var comando = new InsertarActualizarFormulaCommand();
            comando.activo = true;
            comando.formula = model.formula;
            comando.nombre = model.nombre;

            var respuesta = (InsertarActualizarFormulaOutput)comando.Execute();
            return respuesta;
        }

        public ObtenerFormulaResult GetFormula(int id)
        {
            var parametros = new ObtenerFormulaParameters { idformula = id };
            var resultado = (ObtenerFormulaResult)parametros.Execute();
            return resultado == null ? new ObtenerFormulaResult() : resultado;
        }

        #endregion formula

        #region clientes

        //public static List<ListarClientesDto> GetListarClientes(string search, bool inactivo)
        //{
        //    var parametros = new ListarClientesParameters { criterio = search , activo = inactivo==true?false:true };
        //    var resultado = (ListarClientesResult)parametros.Execute();
        //    return resultado == null ? new List<ListarClientesDto>() : resultado.Hits.ToList();

        //}
        public static IEnumerable<ClienteModel> GetListarClientes(string search, bool inactivo)
        {
            var parametros = new ListarClientesParameters { criterio = search, activo = inactivo == true ? false : true };
            var resultado = (ListarClientesResult)parametros.Execute();

            Mapper.CreateMap<ListarClientesDto, ClienteModel>();
            return Mapper.Map<IEnumerable<ListarClientesDto>, IEnumerable<ClienteModel>>(resultado.Hits);
        }

        public static bool InsertarActualizarLineaConsumidaCliente(decimal lineaconsumida, int idcliente)
        {
            var comando = new InsertarActualizarLineaConsumidaClienteCommand { idcliente = idcliente, lineaconsumida = lineaconsumida };
            comando.Execute();
            return true;
        }

        public static InsertarActualizarClienteOutput InsertarActualizarCliente(ClienteModel model, out string res)
        {
            if (model.idcliente == 0 || model.idcliente == null)
            {
                model.idcliente = null;
                model.activo = true;
            }
            var comando = new InsertarActualizarClienteCommand();
            comando.idcliente = model.idcliente;
            comando.activo = model.activo;
            comando.iddireccion = model.iddireccion;
            comando.idmonedalinea = model.idmonedalinea;
            comando.idubigeo = model.idubigeo;
            comando.lineacredito = model.lineacredito;
            comando.nombrecorto = model.nombrecorto;
            comando.razonsocial = model.razonsocial;
            comando.ruc = model.ruc;
            comando.rutalogo = model.rutalogo;
            comando.pagocontado = model.pagocontado;
            res = "OK";

            var respuesta = (InsertarActualizarClienteOutput)comando.Execute();

            if (model.razonsocial == null)
            { return respuesta; }

            if (model.iddireccion == 0)
                model.iddireccion = null;
            var comandodireccion = new InsertarActualizarDireccionCommand();
            comandodireccion.direccion = model.direccion;
            comandodireccion.codigo = model.codigodireccion;
            comandodireccion.iddistrito = model.iddistrito;
            comandodireccion.principal = true;
            comandodireccion.idcliente = respuesta.idcliente;
            comandodireccion.iddireccion = model.iddireccion;
            comandodireccion.activo = true;

            var respuesta2 = (InsertarActualizarDireccionOutput)comandodireccion.Execute();

            return respuesta;
        }

        public static InsertarActualizarClienteOutput InsertarActualizarProveedorCliente(ProveedorClienteModel model)
        {
            var comando = new InsertarActualizarProveedorClienteCommand();
            comando.idcliente = model.idcliente;
            comando.idproveedor = model.idproveedor;
            var respuesta = (InsertarActualizarClienteOutput)comando.Execute();
            return respuesta;
        }

        public static ObtenerClienteResult GetObtenerCliente(int idcliente)
        {
            var parametros = new ObtenerClienteParameter { id = idcliente };
            var resultado = (ObtenerClienteResult)parametros.Execute();
            return resultado == null ? new ObtenerClienteResult() : resultado;
        }

        public static List<ListarProveedorxClienteDto> GetListarProveedorxCliente(int? idcliente)
        {
            var parametros = new ListarProveedorxClienteParameters { idcliente = idcliente.Value };
            var resultado = (ListarProveedorxClienteResult)parametros.Execute();
            return resultado == null ? new List<ListarProveedorxClienteDto>() : resultado.Hits.ToList();
        }

        public static List<ListarClienteProveedorPendientesDto> GetClienteProveedorPendientes(int? idcliente)
        {
            var parametros = new ListarClienteProveedorPendientesParameters { idcliente = idcliente.Value };
            var resultado = (ListarClienteProveedorPendientesResult)parametros.Execute();
            return resultado == null ? new List<ListarClienteProveedorPendientesDto>() : resultado.Hits.ToList();
        }

        public EliminarProveedorClienteResult EliminarProveedor(int id)
        {
            var parametros = new EliminarProveedorClienteParameter { idproveedorcliente = id };
            var resultado = (EliminarProveedorClienteResult)parametros.Execute();
            return resultado == null ? new EliminarProveedorClienteResult() : resultado;
        }

        #endregion clientes

        #region ubigeo

        public static List<ListarDireccionDto> GetListarDirecciones(string search)
        {
            var parametros = new ListarDireccionParameters { criterio = search };
            var resultado = (ListarDireccionResult)parametros.Execute();
            return resultado == null ? new List<ListarDireccionDto>() : resultado.Hits.ToList();
        }

        public static ObtenerDireccionResult GetObtenerDireccion(int? iddireccion)
        {
            var parametros = new ObtenerDireccionParameters { iddireccion = iddireccion.Value };
            var resultado = (ObtenerDireccionResult)parametros.Execute();
            return resultado == null ? new ObtenerDireccionResult() : resultado;
        }

        public static List<ListarDepartamentosDto> GetListarDepartamento()
        {
            var parametros = new ListarDepartamentosParameters();
            var resultado = (ListarDepartamentosResult)parametros.Execute();
            return resultado == null ? new List<ListarDepartamentosDto>() : resultado.Hits.ToList();
        }

        public static List<ListarProvinciasDto> GetListarProvincia(int iddepartamento)
        {
            var parametros = new ListarProvinciasParameters { iddepartamento = iddepartamento };
            var resultado = (ListarProvinciasResult)parametros.Execute();
            return resultado == null ? new List<ListarProvinciasDto>() : resultado.Hits.ToList();
        }

        public static List<ListarDistritosDto> GetListarDistritos(int? idprovincia)
        {
            var parametros = new ListarDistritosParameters { idprovincia = idprovincia };
            var resultado = (ListarDistritosResult)parametros.Execute();
            return resultado == null ? new List<ListarDistritosDto>() : resultado.Hits.ToList();
        }

        public static List<ListarUbigeoDto> GetListarUbigeo()
        {
            var parametros = new ListarUbigeoParameters();
            var resultado = (ListarUbigeoResult)parametros.Execute();
            return resultado == null ? new List<ListarUbigeoDto>() : resultado.Hits.ToList();
        }

        #endregion ubigeo

        #region Valores

        public static List<ListarValorxTablaDto> GetListarValoresxTabla(int? idTabla, string search)
        {
            var parametros = new ListarValorxTablaParameters { idtabla = idTabla, search = search };
            var resultado = (ListarValorxTablaResult)parametros.Execute();
            return resultado == null ? new List<ListarValorxTablaDto>() : resultado.Hits.ToList();
        }

        public static int InsertarActualizarValorTabla(ValorTablaModel model, out string res)
        {
            if (model.idvalortabla == 0)
                model.idvalortabla = null;
            var comando = new InsertarActualizarValorTablaCommand();
            comando.idmaestrotabla = model.idmaestrotabla;
            comando.valor = model.valor;
            comando.activo = model.activo;
            comando.idvalortabla = model.idvalortabla;
            comando.orden = model.orden;

            res = "OK";

            var respuesta = (InsertarActualizarValorTablaOutput)comando.Execute();
            return respuesta.idvalortabla;
        }

        public ObtenerValorTablaResult GetValorTabla(int id)
        {
            var parametros = new ObtenerValorTablaParameter { idvalortabla = id };
            var resultado = (ObtenerValorTablaResult)parametros.Execute();
            return resultado == null ? new ObtenerValorTablaResult() : resultado;
        }

        #endregion Valores

        #region Direccion

        public static List<ListarDireccionClienteDto> GetListarDireccionesxCliente(int idcliente)
        {
            var parametros = new ListarDireccionClienteParameters() { idcliente = idcliente };
            var resultado = (ListarDireccionClienteResult)parametros.Execute();
            return resultado == null ? new List<ListarDireccionClienteDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarDireccionOutput InsertarActualizarDireccion(DireccionModel model)
        {
            Mapper.CreateMap<DireccionModel, InsertarActualizarDireccionCommand>();
            var comando = Mapper.Map<DireccionModel, InsertarActualizarDireccionCommand>(model);

            var respuesta = (InsertarActualizarDireccionOutput)comando.Execute();
            return respuesta;
        }

        public static ValidarDireccionResult GetValidarDireccion(int idcliente, string codigo)
        {
            var parametros = new ValidarDireccionParameter() { codigo = codigo, idcliente = idcliente };
            var resultado = (ValidarDireccionResult)parametros.Execute();
            return resultado == null ? new ValidarDireccionResult() : resultado;
        }

        #endregion Direccion

        #region ruta

        //public static List<ListarRutaDto> GetListarRuta(int? idorigen , int? iddestion, int? idruta)
        public static List<ListarRutaDto> GetListarRuta(string ruta)
        {
            var parametros = new ListarRutaParameters { criterio = ruta };
            var resultado = (ListarRutaResult)parametros.Execute();
            return resultado == null ? new List<ListarRutaDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarRutaOutput InsertarActualizarRuta(RutaModel model)
        {
            if (model.idruta == 0)
                model.idruta = null;
            var comando = new InsertarActualizarRutaCommand();
            comando.iddestino = model.iddestino;
            comando.idorigen = model.idorigen;
            comando.km = model.km;
            comando.nombre = model.nombre;
            comando.ruta = model.ruta;
            comando.idruta = model.idruta;

            var respuesta = (InsertarActualizarRutaOutput)comando.Execute();
            return respuesta;
        }

        public static List<ListarDetalleRutaDto> GetListarDetalleRuta(int? idruta)
        {
            var parametros = new ListarDetalleRutaParameters { idruta = idruta };
            var resultado = (ListarDetalleRutaResult)parametros.Execute();
            return resultado == null ? new List<ListarDetalleRutaDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarDetalleRutaOutput InsertarActualizarDetalleRuta(RutaModel model)
        {
            var comando = new InsertarActualizarDetalleRutaCommand();
            if (model.iddetalleruta == 0)
                model.iddetalleruta = null;

            comando.idorigen = model.idorigen;
            comando.km = model.km;
            comando.iddistrito = model.iddistrito;
            comando.idorden = model.idorden;
            comando.idtipotransporte = model.idtipotransporte;
            comando.leaddocumentario = model.leaddocumentario;
            comando.leadida = model.leadida;
            comando.leadretorno = model.leadretorno;
            comando.iddetalleruta = model.iddetalleruta;
            comando.idruta = model.idruta.Value;
            comando.idprovincia = model.idprovincia;
            comando.iddepartamento = model.iddepartamento;

            var respuesta = (InsertarActualizarDetalleRutaOutput)comando.Execute();
            return respuesta;
        }

        public static ObtenerRutaResult GetObtenerRuta(int idruta)
        {
            var parametros = new ObtenerRutaParameters { idruta = idruta };
            var resultado = (ObtenerRutaResult)parametros.Execute();
            return resultado == null ? new ObtenerRutaResult() : resultado;
        }

        public static ObtenerDetalleRutaResult ObtenerDetalleRutaResult(int iddetalleruta)
        {
            var parametros = new ObtenerDetalleRutaParameters { iddetalleruta = iddetalleruta };
            return (ObtenerDetalleRutaResult)parametros.Execute();
        }

        public static InsertarActualizarDetalleRutaOutput EliminarDetalleRuta(int iddetalleruta)
        {
            var comando = new EliminarDetalleRutaCommand();
            comando.iddetalleruta = iddetalleruta;
            var respuesta = (InsertarActualizarDetalleRutaOutput)comando.Execute();
            return respuesta;
        }

        public InsertarActualizarRutaOutput EliminarRuta(int id)
        {
            var comando = new EliminarRutaCommand();
            comando.idruta = id;
            var respuesta = (InsertarActualizarRutaOutput)comando.Execute();
            return respuesta;
        }

        #endregion ruta

        #region Combos

        public static List<ListarDetalleTarifaIdsDto> GetListarTarifasIds(int idtarifa)
        {
            var parameters = new ListarDetalleTarifaIdsParameters { idtarifa = idtarifa };
            var resultado = (ListarDetalleTarifaIdsResult)parameters.Execute();
            return resultado == null ? new List<ListarDetalleTarifaIdsDto>() : resultado.Hits.ToList();
        }

        public static List<ListarMaestroTablasDto> GetListarMaestroTabla()
        {
            var parametros = new ListarMaestroTablasParameters();
            var resultado = (ListarMaestroTablasResult)parametros.Execute();
            return resultado == null ? new List<ListarMaestroTablasDto>() : resultado.Hits.ToList();
        }

        public static List<ListarValorxTablaDto> GetListarValoresxTabla(int? id)
        {
            var parametros = new ListarValorxTablaParameters() { idtabla = id };
            var resultado = (ListarValorxTablaResult)parametros.Execute();
            return resultado == null ? new List<ListarValorxTablaDto>() : resultado.Hits.ToList();
        }

        public static List<ListarConceptosTarifaDto> GetListarConceptoCobroT(int? idcliente, int? idorigen, int? iddestino
            , int? idformula, int? idtipotransporte)
        {
            var ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();

            var parametros = new ListarConceptosTarifaParameters
            {
                idcliente = idcliente.Value,
                idformula = idformula.Value,
                idtipotransporte = idtipotransporte.Value,
                iddistrito = iddestino.Value,
                idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
            };
            try
            {
                var resultado = (ListarConceptosTarifaResult)parametros.Execute();
                var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

                if (resultado.Hits.ToList().Count == 0)
                {
                    parametros = new ListarConceptosTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        idformula = idformula.Value,
                        idtipotransporte = idtipotransporte.Value,
                        idprovincia = provincia.idprovincia,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarConceptosTarifaResult)parametros.Execute();
                    resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
                    //if (resultado != null) return resultado.Hits.Where(x => x.iddistrito == 0).ToList();
                }
                if (resultado.Hits.ToList().Count == 0)
                {
                    var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                    parametros = new ListarConceptosTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        iddepartamento = provincia.iddepartamento,
                        idformula = idformula.Value,
                        idtipotransporte = idtipotransporte.Value,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarConceptosTarifaResult)parametros.Execute();
                    if (resultado != null) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
                }

                return resultado == null ? new List<ListarConceptosTarifaDto>() : resultado.Hits.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ListarEstacionOrigenDto> GetListarEstacionOrigen()
        {
            var parametros = new ListarEstacionOrigenParameters();
            var resultado = (ListarEstacionOrigenResult)parametros.Execute();
            return resultado == null ? new List<ListarEstacionOrigenDto>() : resultado.Hits.ToList();
        }

        #endregion Combos

        #region Zona

        public static List<ListarZonasDto> GetListarZona(string search)
        {
            var parametros = new ListarZonasParameters { zona = search };
            var resultado = (ListarZonasResult)parametros.Execute();
            return resultado == null ? new List<ListarZonasDto>() : resultado.Hits.ToList();
        }

        public static ValidarZonaResult validarZona(string zona, int? idzona)
        {
            var parametros = new ValidarZonaParameter { zona = zona, idzona = idzona };
            var resultado = (ValidarZonaResult)parametros.Execute();
            return resultado == null ? new ValidarZonaResult() : resultado;
        }

        public static InsertarActualizarZonaOutput InsertarActualizarZona(ZonaModel model, List<string> distritosseleccionados)
        {
            if (model.idzona == 0)
                model.idzona = null;
            var comando = new InsertarActualizarZonaCommand();
            comando.zona = model.zona;
            comando.idzona = model.idzona;
            var respuesta = (InsertarActualizarZonaOutput)comando.Execute();

            var comandodistrito = new InsertarActualizarZonaDistritoCommand();
            comandodistrito.idsdistritos = distritosseleccionados;
            comandodistrito.idzona = respuesta.idzona;
            var resp = (InsertarActualizarZonaDistritoOutput)comandodistrito.Execute();

            return respuesta;
        }

        public static List<ListarDistritoZonaDto> GetListarDistritoZona(int? idzona)
        {
            var parametros = new ListarDistritoZonaParameters { idzona = idzona };
            var resultado = (ListarDistritoZonaResult)parametros.Execute();
            return resultado == null ? new List<ListarDistritoZonaDto>() : resultado.Hits.ToList();
        }

        public static List<ListarDistritoZonaEditarDto> GetListarDistritoZonaEditar(int? idzona, int? idprovincia)
        {
            var parametros = new ListarDistritoZonaEditarParameters { idzona = idzona, idprovincia = idprovincia };
            var resultado = (ListarDistritoZonaEditarResult)parametros.Execute();
            return resultado == null ? new List<ListarDistritoZonaEditarDto>() : resultado.Hits.ToList();
        }

        public ObtenerZonaResult GetZona(int id)
        {
            var parametros = new ObtenerZonaParameter { id = id };
            var resultado = (ObtenerZonaResult)parametros.Execute();
            return resultado == null ? new ObtenerZonaResult() : resultado;
        }

        #endregion Zona

        #region tarifas

        public static CopiarTarifaIndividualResult CopiarTarifaIndividual(int idtarifa)
        {
            var parametros = new CopiarTarifaIndividualParameter { idtarifa = idtarifa };
            var resultado = (CopiarTarifaIndividualResult)parametros.Execute();
            return resultado == null ? new CopiarTarifaIndividualResult() : resultado;
        }

        public static bool CopiarTarifa(int idcliente, int idoriginal)
        {
            var tarifas = GetListarTarifas(idoriginal);
            foreach (var item in tarifas)
            {
                var parametros = new CopiarTarifaParameter { idtarifa = item.idtarifa, idcliente = idcliente };
                var resultado = (CopiarTarifaResult)parametros.Execute();
            }

            return true;
        }

        public static List<ListarTarifaDto> GetListarTarifas(int idcliente)
        {
            var parametros = new ListarTarifaParameters { idcliente = idcliente };
            var resultado = (ListarTarifaResult)parametros.Execute();
            return resultado == null ? new List<ListarTarifaDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarTarifaOutput InsertarActualizarTarifa(TarifaModel model)
        {
            if (model.idtarifa == 0)
                model.idtarifa = null;
            var comando = new InsertarActualizarTarifaCommand();
            comando.idtarifa = model.idtarifa;

            comando.desde = model.desde;
            comando.hasta = model.hasta;
            comando.idcliente = model.idcliente;
            comando.idprovincia = model.idprovincia;
            comando.iddepartamento = model.iddepartamento;
            comando.iddistrito = model.iddistrito;
            comando.idformula = model.idformula;
            comando.idmoneda = model.idmoneda;
            comando.idorigen = model.idorigen;
            comando.idtarifa = model.idtarifa;
            comando.idtipotransporte = model.idtipotransporte;
            comando.idtipounidad = model.idtipounidad;
            comando.idzona = model.idzona;
            comando.idorigendistrito = model.idorigendistrito;
            comando.idorigenprovincia = model.idorigenprovincia;

            comando.minimo = model.minimo;
            comando.montobase = model.montobase;
            comando.precio = model.precio;
            comando.adicional = model.adicional;

            var respuesta = (InsertarActualizarTarifaOutput)comando.Execute();

            foreach (var item in model.conceptos)
            {
                var comandoDet = new InsertarActualizarDetalleTarifaCommand();
                comandoDet.idconceptocobro = item.idconceptocobro;
                comandoDet.idtarifa = respuesta.idtarifa;
                comandoDet.Execute();
            }

            return respuesta;
        }

        public InsertarActualizarTarifaOutput EliminarTarifa(int id)
        {
            var comando = new EliminarTarifaCommand();
            comando.idtarifa = id;
            var respuesta = (InsertarActualizarTarifaOutput)comando.Execute();
            return respuesta;
        }

        public static ObtenerTarifaResult GetObtenerTarifa(int? idtarifa)
        {
            var parametros = new ObtenerTarifaParameter { idtarifa = idtarifa.Value };
            var resultado = (ObtenerTarifaResult)parametros.Execute();
            return resultado == null ? new ObtenerTarifaResult() : resultado;
        }

        #endregion tarifas

        #region operaciones

        public static List<ListarPlanificarOrdenDto> GetListarPlanificarOrden(int? idestacionorigen, int? idcliente, int? iddestino, int idestado, int? idtipotransporte)
        {
            var parametros = new ListarPlanificarOrdenParameters
            {
                iddestino = iddestino,
                idestacionorigen = idestacionorigen
                ,
                idcliente = idcliente,
                idestado = idestado,
                idtipotransporte = idtipotransporte
            };
            var resultado = (ListarPlanificarOrdenResult)parametros.Execute();
            return resultado == null ? new List<ListarPlanificarOrdenDto>() : resultado.Hits.ToList();
        }

        public static List<ObtenerOrdenTrabajoResult> GetAgregarOrdenesPendientes(string ids)
        {
            List<ObtenerOrdenTrabajoResult> ordenes = new List<ObtenerOrdenTrabajoResult>();

            string[] prm = ids.Split(',');
            foreach (var item in prm)
            {
                var parametros = new ObtenerOrdenTrabajoParameter { idorden = Convert.ToInt32(item.ToString()) };
                var resultado = (ObtenerOrdenTrabajoResult)parametros.Execute();
                ordenes.Add(resultado == null ? new ObtenerOrdenTrabajoResult() : resultado);
            }

            return ordenes;
        }

        public static int ActualizarDetalleOperacion(List<ListarDetalleOperacionDto> detalle)
        {
            foreach (var item in detalle)
            {
                var comandoOT = new InsertarActualizarOrdenTrabajoCommand();
                comandoOT.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteDespacho;

                comandoOT.Execute();
            }
            return 1;
        }

        public static InsertarActualizarOperacionCargaOutput InsertarActualizarCarga(CargaModel model, out string res)
        {
            if (model.idcarga == 0)
                model.idcarga = null;
            var comando = new InsertarActualizarOperacionCargaCommand();

            res = "OK";
            switch (model._tipooperacion)
            {
                case 1: // Anular
                    comando.activo = model.activo;
                    comando.idcarga = model.idcarga;
                    comando.tipooperacion = 1;
                    var respuesta = (InsertarActualizarOperacionCargaOutput)comando.Execute();
                    return respuesta;

                case 2: // Asignar Vehiculo
                    comando.activo = model.activo;
                    comando.idvehiculo = model.idvehiculo;
                    comando.idcarga = model.idcarga;
                    comando.idmuelle = model.idmuelle;
                    comando.tipooperacion = 2;
                    var respuesta2 = (InsertarActualizarOperacionCargaOutput)comando.Execute();
                    return respuesta2;

                case 3: //Actualizar
                    comando.idcarga = model.idcarga;
                    comando.fechacreacion = model.fechacreacion;
                    comando.idcliente = model.idcliente;
                    comando.idplanificador = model.idplanificador;
                    comando.idproveedor = model.idproveedor;
                    comando.numcarga = model.numcarga;
                    comando.observacion = model.observacion;
                    comando.peso = 0;
                    comando.idvehiculo = model.idvehiculo;
                    comando.vol = 0;
                    comando.activo = model.activo;
                    comando.idestado = (Int32)DataAccess.Constantes.EstadoCarga.Pendiente;
                    comando.idtipooperacion = model.idtipooperacion;
                    comando.idruta = model.idruta;
                    comando.tipooperacion = 3;

                    foreach (var item in model.ordenes)
                    {
                        comando.peso = comando.peso + Convert.ToDecimal(item.pesogeneral);
                        comando.vol = comando.vol + Convert.ToDecimal(item.volgeneral);
                    }

                    var parameters = new ObtenerUltimaOperacionCargaParameter { };
                    var resultado = (ObtenerUltimaOperacionCargaResult)parameters.Execute();
                    if (resultado == null)
                        comando.numcarga = ConfigurationManager.AppSettings["numcarga"];
                    else comando.numcarga = "OC01-" + (Convert.ToInt32(resultado.numcarga.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');

                    res = comando.numcarga;

                    var respuesta3 = (InsertarActualizarOperacionCargaOutput)comando.Execute();

                    //Agrupación para inserción masiva.
                    List<List<OrdenTrabajoModel>> grupos = new List<List<OrdenTrabajoModel>>();

                    //var algo = model.ordenes.Where(x => x.idtipooperacion.Value == 111).ToList();
                    bool encontrado = false;
                    foreach (var item in model.ordenes)
                    {
                        encontrado = false;
                        foreach (var item1 in grupos)
                        {
                            var existe = item1.Where(y => y.idordentrabajo.Equals(item.idordentrabajo)).SingleOrDefault();
                            if (existe != null)
                            {
                                encontrado = true;
                                break;
                            }
                        }

                        if (!encontrado)
                        {
                            List<OrdenTrabajoModel> grupo = new List<OrdenTrabajoModel>();
                            grupo = model.ordenes.Where(x => x.idtipooperacion.Value.Equals(item.idtipooperacion)
                            && x.idagencia == item.idagencia
                            && x.idestaciondestino == item.idestaciondestino
                            && x.idruta == item.idruta
                            ).ToList();

                            grupos.Add(grupo);
                        }
                    }

                    foreach (var item in grupos)
                    {
                        string ots = "";
                        foreach (var aux in item)
                        {
                            ots = ots + "," + aux.idordentrabajo;
                        }
                        ots = ots.Substring(1, ots.Length - 1);
                        var param = new ActualizarOTPlanificacionParameters
                        {
                            idcarga = respuesta3.idcarga,
                            idestado = item[0].idestado,
                            idtipooperacion = item[0].idtipooperacion.Value,
                            fechaplanificacion = item[0].fechaplanificacion.Value,
                            idruta = item[0].idruta,
                            idestaciondestino = item[0].idestaciondestino,
                            idagencia = item[0].idagencia,
                            idsordentrabajo = ots
                        };
                        param.Execute();
                    }
                    return respuesta3;

                case 4: //Confirmar
                    comando.fechaconfirmacion = model.fechaconfirmacion;
                    comando.horaconfirmacion = model.horaconfirmacion;
                    comando.idcarga = model.idcarga;
                    comando.idestado = (Int32)DataAccess.Constantes.EstadoCarga.Confirmada;
                    comando.tipooperacion = 4;

                    var respconfirmar = (InsertarActualizarOperacionCargaOutput)comando.Execute();

                    string ots1 = "";
                    foreach (var aux in model.ordenes)
                    {
                        ots1 = ots1 + "," + aux.idordentrabajo;
                    }
                    ots1 = ots1.Substring(1, ots1.Length - 1);
                    var param1 = new ActualizarOTConfirmacionParameters
                    {
                        idestado = comando.idestado,
                        fechaconfirmacion = DateTime.Now,
                        idsordentrabajo = ots1
                    };
                    param1.Execute();
                    return respconfirmar;

                case 5: //Salida vehiculo
                    comando.fechasalida = model.fechasalida;
                    comando.horasalida = model.horasalida;
                    comando.idcarga = model.idcarga;
                    comando.idestado = (Int32)DataAccess.Constantes.EstadoCarga.EnRuta;
                    comando.tipooperacion = 5;
                    foreach (var item in model.ordenes)
                    {
                        var comandoOT = new InsertarActualizarOrdenTrabajoCommand();

                        var ot = OrdenData.GetObtenerOrden_model(item.idordentrabajo.Value);
                        comandoOT.idordentrabajo = item.idordentrabajo;
                        if (ot.idtipooperacion == Convert.ToInt32(Constantes.TipoOperacion.Transferencia))
                            comandoOT.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteRecepcionDestino;
                        else
                            comandoOT.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteEntrega;
                        comandoOT.fechadespacho = DateTime.Now;
                        comandoOT._tipoop = 7;
                        comandoOT.activo = true;
                        comandoOT.Execute();
                    }
                    var respsalida = (InsertarActualizarOperacionCargaOutput)comando.Execute();
                    return respsalida;

                case 6: //actualizar pesos y volumen con las ots asociadas
                    comando.idcarga = model.idcarga;
                    comando.tipooperacion = 6;
                    comando.peso = model.peso;
                    comando.vol = model.vol;
                    return (InsertarActualizarOperacionCargaOutput)comando.Execute();

                default: return null;
            }
        }

        public static List<ListarDetalleOperacionxRutaDto> ListarDetalleOperacionxRuta(int idruta, int idorigen, int? iddistrito, int? idprovincia, int? iddepartamento)
        {
            var parametros = new ListarDetalleOperacionxRutaParameters
            {
                iddepartamento = iddepartamento,
                iddistrito = iddistrito,
                idorigen = idorigen,
                idprovincia = idprovincia,
                idruta = idruta
            };
            var result = (ListarDetalleOperacionxRutaResult)parametros.Execute();
            return result.Hits.ToList();
        }

        public static List<ListarOperacionCargaDto> GetListarOperacionCarga(string numcp, string numcarga, int idestado, int? idestacion)
        {
            var parametros = new ListarOperacionCargaParameters { numcarga = numcarga, numcp = numcp, idestado = idestado, idestacion = idestacion };
            var resultado = (ListarOperacionCargaResult)parametros.Execute();
            return resultado == null ? new List<ListarOperacionCargaDto>() : resultado.Hits.ToList();
        }

        public static List<ListarDespachoDto> GetListarDespacho(int? idvehiculo, int idestado, string numcarga, string numcp)
        {
            var parametros = new ListarDespachoParameters { idvehiculo = idvehiculo, idestado = idestado, numcarga = numcarga, numcp = numcp };
            var resultado = (ListarDespachoResult)parametros.Execute();
            return resultado == null ? new List<ListarDespachoDto>() : resultado.Hits.ToList();
        }

        public static IEnumerable<ReimpresionModel> GetListarImpresionCarga(string numcarga, string numhojaruta)
        {
            var parametros = new ListarImpresionCargaParameters { numcarga = numcarga, numhojaruta = numhojaruta };
            var resultado = (ListarImpresionCargaResult)parametros.Execute();

            Mapper.CreateMap<ListarImpresionCargaDto, ReimpresionModel>();
            return Mapper.Map<IEnumerable<ListarImpresionCargaDto>, IEnumerable<ReimpresionModel>>(resultado.Hits);
        }

        public static IEnumerable<ReimpresionModel> GetListarImpresionManifiesto(string nummanifiesto, string numcp, string numgrt, string numcarga, string numhojaruta, string fecini, string fecfin)
        {
            var parametros = new ListarImpresionManifiestoParameters
            {
                nummanifiesto = nummanifiesto
                ,
                numcp = numcp
                ,
                numgrt = numgrt
                ,
                numcarga = numcarga
                ,
                numhojaruta = numhojaruta
                ,
                fecinicio = fecini
                ,
                fecfin = fecfin
            };
            var resultado = (ListarImpresionManifiestoResult)parametros.Execute();
            Mapper.CreateMap<ListarImpresionManifiestoDto, ReimpresionModel>();
            return Mapper.Map<IEnumerable<ListarImpresionManifiestoDto>, IEnumerable<ReimpresionModel>>(resultado.Hits);
        }

        public static List<ListarOperacionCargaVehiculoDto> GetListarOperacionesVehiculoDetalle(long? idcarga)
        {
            var parametros = new ListarOperacionCargaVehiculoParameters { idestado = (Int32)Constantes.EstadoCarga.Pendiente, idcarga = idcarga };
            var resultado = (ListarOperacionCargaVehiculoResult)parametros.Execute();
            return resultado == null ? new List<ListarOperacionCargaVehiculoDto>() : resultado.Hits.ToList();
        }

        public static List<ListarDetalleOperacionDto> GetListarOperacionDetalle(long? idcarga)
        {
            var parametros = new ListarDetalleOperacionParameters { idcarga = idcarga };
            var resultado = (ListarDetalleOperacionResult)parametros.Execute();
            return resultado == null ? new List<ListarDetalleOperacionDto>() : resultado.Hits.ToList();
        }

        public static IEnumerable<OrdenTrabajoModel> GetListarOperacionesDetalle_Model(long? idcarga)
        {
            var parametros = new ListarDetalleOperacionParameters { idcarga = idcarga };
            var resultado = (ListarDetalleOperacionResult)parametros.Execute();

            Mapper.CreateMap<ListarDetalleOperacionDto, OrdenTrabajoModel>();
            return Mapper.Map<IEnumerable<ListarDetalleOperacionDto>, IEnumerable<OrdenTrabajoModel>>(resultado.Hits);
        }

        public static List<ListarOperacionxVehiculoDto> GetListarOperacionxVehiculo(int idvehiculo)
        {
            var parametros = new ListarOperacionxVehiculoParameters { idvehiculo = idvehiculo };
            var resultado = (ListarOperacionxVehiculoResult)parametros.Execute();
            return resultado == null ? new List<ListarOperacionxVehiculoDto>() : resultado.Hits.ToList();
        }

        public static ObtenerManifiestoResult GetObtenerManifiesto(long idmanifiesto)
        {
            var parametros = new ObtenerManifiestoParameter { idmanifiesto = idmanifiesto };
            var resultado = (ObtenerManifiestoResult)parametros.Execute();
            return resultado;
        }

        public static ObtenerOperacionCargaResult GetObtenerOperacion(long idcarga)
        {
            var parametros = new ObtenerOperacionCargaParameters { idcarga = idcarga };
            var resultado = (ObtenerOperacionCargaResult)parametros.Execute();
            return resultado == null ? new ObtenerOperacionCargaResult() : resultado;
        }

        public static ObtenerOperacionCargaTotalVehiculoResult GetObtenerOperacionTotalVehiculo(long idcarga)
        {
            var parametros = new ObtenerOperacionCargaTotalVehiculoParameters { idcarga = idcarga };
            var resultado = (ObtenerOperacionCargaTotalVehiculoResult)parametros.Execute();
            return resultado == null ? new ObtenerOperacionCargaTotalVehiculoResult() : resultado;
        }

        public static List<ListarServiciosDto> GetListarServicios(long idcarga)
        {
            var parametros = new ListarServiciosParameters() { idcarga = idcarga };
            var resultado = (ListarServiciosResult)parametros.Execute();
            return resultado == null ? new List<ListarServiciosDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarServicioOperacionOutput InsertarActualizarServicio(ServicioModel model)
        {
            if (model.idserviciooperacion == 0)
                model.idserviciooperacion = null;
            var comando = new InsertarActualizarServicioOperacionCommand();
            comando.idserviciooperacion = model.idserviciooperacion;
            comando.idservicio = model.idservicio;
            comando.idcarga = model.idcarga;
            comando.cantidad = model.cantidad;

            var respuesta = (InsertarActualizarServicioOperacionOutput)comando.Execute();
            return respuesta;
        }

        public static int ActualizarEstadoOT(long idorden, int idestado)
        {
            var command = new InsertarActualizarEstadoOTCommand();
            command.idestado = idestado;
            command.idordentrabajo = idorden;
            command.Execute();
            return 1;
        }

        public static int ActualizarEstadoOperacion(long idcarga, int idestado)
        {
            var command = new InsertarActualizarEstadoOperacionCommand();
            command.idestado = idestado;
            command.idcarga = idcarga;
            command.Execute();
            return 1;
        }

        public static string obtenerUltimoManifiesto()
        {
            var parameters = new ObtenerUltimoManifiestoParameter { };
            var resultado = (ObtenerUltimoManifiestoResult)parameters.Execute();
            if (resultado == null)
                return ConfigurationManager.AppSettings["numman"].ToString();
            else return resultado.nummanifiesto.Split('-')[0].ToString() + "-" + (Convert.ToInt32(resultado.nummanifiesto.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');
        }

        public static string obtenerUltimaHojaRuta()
        {
            var parameters = new ObtenerUltimaHojaRutaParameter { };
            var resultado = (ObtenerUltimaHojaRutaResult)parameters.Execute();
            if (resultado == null)
                return ConfigurationManager.AppSettings["numhojaruta"].ToString();
            else return resultado.numhojaruta.Split('-')[0].ToString() + "-" + (Convert.ToInt32(resultado.numhojaruta.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');
        }

        public static InsertarActualizarManifiestoOutput InsertarActualizarManifiesto(ManifiestoModel model, out string nummanifiesto)
        {
            var comando = new InsertarActualizarManifiestoCommand();
            comando._tipoop = model._tipoop;
            nummanifiesto = "";
            switch (model._tipoop)
            {
                case 1: //Nuevo
                    comando.activo = model.activo;
                    comando.fecharegistro = model.fecharegistro;
                    comando.iddespacho = model.iddespacho;
                    comando.nummanifiesto = obtenerUltimoManifiesto();
                    comando.numhojaruta = model.numhojaruta;
                    comando.idusuarioregistro = model.idusuarioregistro;
                    comando.idvehiculo = model.idvehiculo;
                    nummanifiesto = comando.nummanifiesto;
                    comando.iddestino = model.iddestino;
                    comando.idtipooperacion = model.idtipooperacion;

                    break;

                case 2: // Anular
                    comando.activo = false;
                    comando.idmanifiesto = model.idmanifiesto;
                    nummanifiesto = "";
                    break;

                default:
                    break;
            }

            return (InsertarActualizarManifiestoOutput)comando.Execute();
        }

        #endregion operaciones

        #region CamionCompleto

        public static List<ListarCamionCompletoDto> GetListarCamionCompleto(string codigo, int? iddestino, int? idestacion)
        {
            var parametros = new ListarCamionCompletoParameters { iddestino = iddestino, codigo = codigo, idestacion = idestacion };
            var resultado = (ListarCamionCompletoResult)parametros.Execute();
            return resultado == null ? new List<ListarCamionCompletoDto>() : resultado.Hits.ToList();
        }

        public static long ActualizarCamionCompleto(CamionCompletoModel model)
        {
            Mapper.CreateMap<CamionCompletoModel, InsertarActualizarCamionCompletoCommand>();
            var comando = Mapper.Map<CamionCompletoModel, InsertarActualizarCamionCompletoCommand>(model);

            var respuesta = (InsertarActualizarCamionCompletoOutput)comando.Execute();

            return respuesta.idcamioncompleto;
        }

        public static long InsertarCamionCompleto(CamionCompletoModel model)
        {
            Mapper.CreateMap<CamionCompletoModel, InsertarActualizarCamionCompletoCommand>();
            var comando = Mapper.Map<CamionCompletoModel, InsertarActualizarCamionCompletoCommand>(model);

            ///optener ultimo codigo de camion completo
            var parameters = new ObtenerUltimaOrdenCamionCompletoParameter { };
            var resultado = (ObtenerUltimaOrdenCamionCompletoResult)parameters.Execute();

            if (resultado == null)
                comando.codigocamioncompleto = ConfigurationManager.AppSettings["numcc"].ToString();
            else comando.codigocamioncompleto = resultado.codigocamioncompleto.Split('-')[0].ToString() + "-" + (Convert.ToInt32(resultado.codigocamioncompleto.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');

            ///crea carga por defecto
            var comandocarga = new InsertarActualizarOperacionCargaCommand();
            comandocarga.fechacreacion = DateTime.Now;
            comandocarga.idcliente = model.idcliente;
            comandocarga.idtipooperacion = 112; // entrega directa
            comandocarga.activo = true;
            comandocarga.tipooperacion = 3;
            comandocarga.idestado = (Int32)DataAccess.Constantes.EstadoCarga.Pendiente;

            var parameterscarga = new ObtenerUltimaOperacionCargaParameter { };
            var resultadocarga = (ObtenerUltimaOperacionCargaResult)parameterscarga.Execute();
            if (resultadocarga == null)
                comandocarga.numcarga = ConfigurationManager.AppSettings["numcarga"];
            else comandocarga.numcarga = "OC01-" + (Convert.ToInt32(resultadocarga.numcarga.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');

            //Registrar Carga
            var respuesta3 = (InsertarActualizarOperacionCargaOutput)comandocarga.Execute();

            //Asignar vehiculo
            comandocarga.idvehiculo = model.idvehiculo;
            comandocarga.tipooperacion = 2;
            comandocarga.idcarga = respuesta3.idcarga;
            comandocarga.Execute();
            comando.idcarga = respuesta3.idcarga;

            var respuesta = (InsertarActualizarCamionCompletoOutput)comando.Execute();

            return respuesta.idcamioncompleto;
        }

        public List<ListarTipoTransporteTarifaDto> GetListarTipoTransporteCamion(int? idcliente, int? idorigen, int? iddestino, List<ListarUbigeoDto> ubigeo)
        {
            var parametros = new ListarTipoTransporteTarifaParameters
            {
                idcliente = idcliente.Value,
                iddistrito = iddestino.Value,
                idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
            };
            try
            {
                var resultado = (ListarTipoTransporteTarifaResult)parametros.Execute();

                var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

                if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
                {
                    parametros = new ListarTipoTransporteTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        idprovincia = provincia.idprovincia,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarTipoTransporteTarifaResult)parametros.Execute();
                    resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
                }
                if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
                {
                    var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                    parametros = new ListarTipoTransporteTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        iddepartamento = provincia.iddepartamento,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarTipoTransporteTarifaResult)parametros.Execute();
                    if (resultado != null) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
                }

                return resultado == null ? new List<ListarTipoTransporteTarifaDto>() : resultado.Hits.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ListarTipoUnidadTarifaDto> GetListarTipoUnidadCamion(int? idcliente, int? idorigen, int? iddestino, List<ListarUbigeoDto> ubigeo)
        {
            var parametros = new ListarTipoUnidadTarifaParameters
            {
                idcliente = idcliente.Value,
                iddistrito = iddestino.Value,
                idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
            };
            try
            {
                var resultado = (ListarTipoUnidadTarifaResult)parametros.Execute();

                var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

                if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
                {
                    parametros = new ListarTipoUnidadTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        idprovincia = provincia.idprovincia,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarTipoUnidadTarifaResult)parametros.Execute();
                    resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
                }
                if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
                {
                    var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                    parametros = new ListarTipoUnidadTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        iddepartamento = provincia.iddepartamento,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarTipoUnidadTarifaResult)parametros.Execute();
                    if (resultado != null) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
                }

                return resultado == null ? new List<ListarTipoUnidadTarifaDto>() : resultado.Hits.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ListarFormulaTarifaDto> GetListarFormulaCamion(int? idcliente, int? idorigen, int? iddestino, List<ListarUbigeoDto> ubigeo)
        {
            //var ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();

            var parametros = new ListarFormulaTarifaParameters
            {
                idcliente = idcliente.Value
                ,
                iddistrito = iddestino.Value
                ,
                idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
            };
            var resultado = (ListarFormulaTarifaResult)parametros.Execute();
            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

            if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
            {
                parametros = new ListarFormulaTarifaParameters
                {
                    idcliente = idcliente.Value,
                    idprovincia = provincia.idprovincia,
                    idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                };
                resultado = (ListarFormulaTarifaResult)parametros.Execute();
                resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
                //if (resultado != null) return
            }
            if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametros = new ListarFormulaTarifaParameters
                {
                    idcliente = idcliente.Value,
                    iddepartamento = provincia.iddepartamento,
                    idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                };
                resultado = (ListarFormulaTarifaResult)parametros.Execute();
                if (resultado != null) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
            }

            return resultado == null ? new List<ListarFormulaTarifaDto>() : resultado.Hits.ToList();
        }

        public static List<ListarConceptosTarifaDto> GetListarConceptoCobroCamion(int? idcliente, int? idorigen, int? iddestino
         , int? idformula, int? idtipotransporte)
        {
            var ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();

            var parametros = new ListarConceptosTarifaParameters
            {
                idcliente = idcliente.Value,
                idformula = idformula.Value,
                idtipotransporte = idtipotransporte.Value,
                iddistrito = iddestino.Value,
                idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
            };
            try
            {
                var resultado = (ListarConceptosTarifaResult)parametros.Execute();
                var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

                if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
                {
                    parametros = new ListarConceptosTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        idformula = idformula.Value,
                        idtipotransporte = idtipotransporte.Value,
                        idprovincia = provincia.idprovincia,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarConceptosTarifaResult)parametros.Execute();
                    resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
                    //if (resultado != null) return resultado.Hits.Where(x => x.iddistrito == 0).ToList();
                }
                if (resultado.Hits.Where(x => x.formula == "Camion").ToList().Count == 0)
                {
                    var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                    parametros = new ListarConceptosTarifaParameters
                    {
                        idcliente = idcliente.Value,
                        iddepartamento = provincia.iddepartamento,
                        idformula = idformula.Value,
                        idtipotransporte = idtipotransporte.Value,
                        idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                    };
                    resultado = (ListarConceptosTarifaResult)parametros.Execute();
                    if (resultado != null) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
                }

                return resultado == null ? new List<ListarConceptosTarifaDto>() : resultado.Hits.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion CamionCompleto

        #region Vehiculo

        public static List<ListarVehiculoDto> GetListarVehiculos(string placa, int? idestado)
        {
            var parametros = new ListarVehiculoParameters { placa = placa, idestado = idestado };
            var resultado = (ListarVehiculoResult)parametros.Execute();
            return resultado == null ? new List<ListarVehiculoDto>() : resultado.Hits.ToList();
        }

        public static List<ListarInspeccionDto> GetListaInspeccion(int? idvehiculo)
        {
            var parametros = new ListarInspeccionParameters() { idvehiculo = idvehiculo };
            var resultado = (ListarInspeccionResult)parametros.Execute();
            return resultado == null ? new List<ListarInspeccionDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarVehiculoOutput InsertarActualizarVehiculo(VehiculoModel model)
        {
            if (model.idvehiculo == 0)
                model.idvehiculo = null;
            var comando = new InsertarActualizarVehiculoCommand();

            comando._tipoop = model._tipoop;
            comando.idvehiculo = model.idvehiculo;

            switch (model._tipoop)
            {
                case 1:
                    comando.activo = true;
                    comando.cargautil = model.cargautil;
                    comando.confveh = model.confveh;
                    comando.idanio = model.idanio;
                    comando.idchofer = model.idchofer;
                    comando.idcolor = model.idcolor;
                    comando.idcombustible = model.idcombustible;
                    comando.idestado = model.idestado;
                    comando.idmarca = model.idmarca;
                    comando.idmodelo = model.idmodelo;
                    comando.idorigen = model.idorigen;
                    comando.idproveedor = model.idproveedor;
                    comando.idtipo = model.idtipo;
                    comando.idvehiculo = model.idvehiculo;
                    comando.kilometraje = model.kilometraje;
                    comando.pesobruto = model.pesobruto;
                    comando.placa = model.placa;
                    comando.regmtc = model.regmtc;
                    comando.seriemotor = model.seriemotor;
                    comando.idestado = (Int32)DataAccess.Constantes.EstadoVehiculo.Creado;

                    break;

                case 2:
                    comando.inspecciones = model.inspecciones;
                    comando.idestado = (Int32)DataAccess.Constantes.EstadoVehiculo.Inspeccionado;
                    comando.fechainspeccion = DateTime.Now;
                    comando.usuarioinspeccion = model.usuarioinspeccion;

                    break;

                case 3: //ASIGNADO A UNA CARGA
                    comando.idestado = (Int32)DataAccess.Constantes.EstadoVehiculo.Inspeccionado;
                    comando.fechaasignado = DateTime.Now;
                    comando.usuarioasignado = model.usuarioasignado;
                    break;

                case 4:

                    comando.idchofer = model.idchofer;
                    break;

                case 5:
                    comando.idestado = model.idestado;
                    break;

                case 6:
                    comando.activo = false;
                    break;

                default:
                    break;
            }

            var respuesta = (InsertarActualizarVehiculoOutput)comando.Execute();
            return respuesta;
        }

        public static List<ListarChoferDto> GetListarChofer(string criterio)
        {
            var parametros = new ListarChoferParameters { criterio = criterio };
            var resultado = (ListarChoferResult)parametros.Execute();
            return resultado == null ? new List<ListarChoferDto>() : resultado.Hits.ToList();
        }

        public static int InsertarActualizarInspeccionarVehiculo(Areas.Seguimiento.Models.Seguimiento.VehiculoModel model, out string res)
        {
            var inspecciones = model.inspecciones.Split(',');
            var comando = new InsertarActualizarVehiculoInspeccionCommand();
            try
            {
                foreach (var item in inspecciones)
                {
                    if (item == "") continue;
                    comando.idvehiculoinspeccion = null;
                    comando.idvehiculo = model.idvehiculo.Value;
                    comando.idinspeccion = Convert.ToInt32(item);

                    var respuesta = (InsertarActualizarVehiculoOutput)comando.Execute();
                    res = "OK";
                    return 1;
                }
            }
            catch (Exception ex)
            {
                res = "Error";
                return 0;
            }

            res = "No existen elementos para agregar";
            return 1;

            ////if (model.idformula == 0)
            ////    model.idformula = null;
            ////var comando = new InsertarActualizarFormulaCommand();
            ////comando.activo = model.activo;
            ////comando.formula = model.formula;
            ////comando.nombre = model.nombre;
            ////comando.idformula = model.idformula;
            ////res = "OK";

            //var respuesta = (InsertarActualizarFormulaOutput)comando.Execute();
            //return respuesta;
        }

        #endregion Vehiculo

        #region estados

        public static List<ListarEstadoDto> GetListarEstados(int idmaestro)
        {
            var parametros = new ListarEstadoParameters { idtabla = idmaestro };
            var resultado = (ListarEstadoResult)parametros.Execute();
            return resultado == null ? new List<ListarEstadoDto>() : resultado.Hits.ToList();
        }

        #endregion estados

        #region Precintos

        public static List<ListarPrecintosDto> GetListarPrecintos(long iddespacho)
        {
            var parametros = new ListarPrecintosParameters { iddespacho = iddespacho };
            var resultado = (ListarPrecintosResult)parametros.Execute();
            return resultado == null ? new List<ListarPrecintosDto>() : resultado.Hits.ToList();
        }

        public static List<ListarPrecintosCargaDto> GetListarPrecintosCarga(long iddespacho)
        {
            var parametros = new ListarPrecintosCargaParameters { iddespacho = iddespacho };
            var resultado = (ListarPrecintosCargaResult)parametros.Execute();
            return resultado == null ? new List<ListarPrecintosCargaDto>() : resultado.Hits.ToList();
        }

        public static int InsertarPrecinto(string precinto, int cantidad)
        {
            int inicio = Convert.ToInt32(precinto);
            for (int i = 0; i < cantidad; i++)
            {
                inicio = inicio + 1;
                var command = new InsertarActualizarPrecintoCommand();
                command.precinto = inicio.ToString();
                command.Execute();
            }

            return 1;
        }

        public static int InsertarActualizarCargaPrecinto
            (int idvehiculo, string seleccionados, long iddespacho)
        {
            //eliminar todos los precintos
            var eliminarcommand = new InsertarActualizarCargaPrecintoCommand();
            eliminarcommand.iddespacho = iddespacho;
            eliminarcommand.eliminar = true;
            eliminarcommand.Execute();

            //Insertar nuevas.
            var er = seleccionados.Split(',');

            foreach (var item in er)
            {
                if (item == "null")
                    continue;
                var comando = new InsertarActualizarCargaPrecintoCommand();
                comando.idprecinto = int.Parse(item);
                comando.iddespacho = iddespacho;
                eliminarcommand.eliminar = false;
                comando.Execute();
            }

            return 1;
        }

        public static long EliminarPrecinto(long idprecinto)
        {
            var parameter = new EliminarPrecintoParameter { idprecinto = idprecinto };
            var result = (EliminarPrecintoResult)parameter.Execute();
            return result.idprecinto;
        }

        #endregion Precintos

        #region despacho

        public static List<ListarCargasPendientesDto> GetListarCargasPendientes(int idvehiculo)
        {
            var parametros = new ListarCargasPendientesParameters { idvehiculo = idvehiculo };
            var resultado = (ListarCargasPendientesResult)parametros.Execute();
            return resultado == null ? new List<ListarCargasPendientesDto>() : resultado.Hits.ToList();
        }

        public static List<ListarDetalleDespachoDto> GetListarDetalleDespacho(int? idvehiculo)
        {
            var parametros = new ListarDetalleDespachoParameters { idvehiculo = idvehiculo };
            var resultado = (ListarDetalleDespachoResult)parametros.Execute();
            return resultado == null ? new List<ListarDetalleDespachoDto>() : resultado.Hits.ToList();
        }

        public static InsertarActualizarDespachoOutput InsertarActualizarDespacho(DespachoModel model)
        {
            if (model.iddespacho == 0)
                model.iddespacho = null;
            var comando = new InsertarActualizarDespachoCommand();
            comando._tipoop = model._tipoop;

            switch (model._tipoop)
            {
                case 1:
                    comando.iddespacho = model.iddespacho;
                    comando.idvehiculo = model.idvehiculo;
                    comando.idestado = (int)DataAccess.Constantes.EstadoDespacho.Creado;
                    break;

                case 2:
                    comando.iddespacho = model.iddespacho;
                    comando.fechasalida = model.fechasalida;
                    comando.idvehiculo = model.idvehiculo;
                    comando.idusuariosalida = model.idusuariosalida;
                    comando.idestado = (int)DataAccess.Constantes.EstadoDespacho.EnRuta;
                    break;

                case 3:
                    comando.iddespacho = model.iddespacho;
                    break;

                default:
                    break;
            }

            return (InsertarActualizarDespachoOutput)comando.Execute();
        }

        public static ObtenerDespachoResult GetObtenerDespacho(int idestado, int? idvehiculo)
        {
            var parametros = new ObtenerDespachoParameters { idestado = idestado, idvehiculo = idvehiculo.Value };
            return (ObtenerDespachoResult)parametros.Execute();
        }

        public static TransbordarVehiculoOutput Transbordar(int idvehiculo_new, int idvehiculo_old)
        {
            var comando = new TransbordarVehiculoCommand
            {
                idvehiculo_new = idvehiculo_new,
                idvehiculo_old = idvehiculo_old
            };
            return (TransbordarVehiculoOutput)comando.Execute();
        }

        #endregion despacho

        #region valija

        public static int InsertarActualizarValija(List<string> ordenes, long iddespacho)
        {
            //if (model.iddespacho == 0)
            //    model.iddespacho = null;
            var comando = new InsertarActualizarDetalleValijaCommand();

            foreach (var item in ordenes)
            {
                comando.iddespacho = iddespacho;
                comando.idordentransporte = Convert.ToInt64(item);
                comando.Execute();
            }

            return 1;
        }

        #endregion valija

        internal static void ActualizarPuntoPartida(int idcliente, string puntopartida)
        {
            var direcciones = DataAccess.Seguimiento.SeguimientoData.GetListarDireccionesxCliente(idcliente);
            direcciones = direcciones.Where(x => x.direccion.Equals(puntopartida)).ToList();
            if (direcciones.Count == 0)
            {
                DireccionModel modDireccion = new DireccionModel();
                modDireccion.activo = true;
                modDireccion.idcliente = idcliente;
                modDireccion.direccion = puntopartida;
                modDireccion.puntopartida = true;
                DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDireccion(modDireccion);
            }
        }

        internal static int ActualizarDescripcionesGRR(string nuevadescripciongeneral)
        {
            var listadodescripciones = DataAccess.Seguimiento.SeguimientoData.GetListarValoresxTabla(Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR));
            var existedescripcion =
                  listadodescripciones.Where(x => x.valor.Equals(nuevadescripciongeneral)).SingleOrDefault();
            if (existedescripcion != null)
                return existedescripcion.idvalortabla;
            else
            {
                string mensaje;
                ValorTablaModel mod_valortabla = new ValorTablaModel();
                mod_valortabla.activo = true;
                mod_valortabla.idmaestrotabla = Convert.ToInt32(Constantes.MaestroTablas.DescripcionGRR);
                mod_valortabla.orden = 1;
                mod_valortabla.valor = nuevadescripciongeneral;
                return DataAccess.Seguimiento.SeguimientoData.InsertarActualizarValorTabla(mod_valortabla, out mensaje);
            }
        }

        internal static int AgregarChofer(ChoferModel model)
        {
            Mapper.CreateMap<ChoferModel, InsertarActualizarChoferCommand>();
            var command = Mapper.Map<ChoferModel, InsertarActualizarChoferCommand>(model);
            var resp = (InsertarActualizarChoferOutput)command.Execute();
            return resp.idchofer;
        }

        #region manifiesto

        public long GenerarManifiestosOts(IEnumerable<OrdenTrabajoModel> listado
            , int idvehiculo, long idcarga, int idusuario, string numhojaruta, int idtipooperacion)
        {
            #region variables

            ManifiestoModel model = new ManifiestoModel();
            OrdenTrabajoModel modelorden = new OrdenTrabajoModel();
            string nummanifiesto;
            IncidenciaModel modIncidencia = null;
            string ids = string.Empty;
            long idsmanifiesto; ;

            #endregion variables

            var vehiculo = OrdenData.GetObtenerVehiculo(idvehiculo);
            var variables_orden = listado.ToList();

            model.activo = true;
            model.fecharegistro = DateTime.Now;
            model.idhojaruta = null;
            model.idmanifiesto = null;
            model.idusuarioregistro = null;
            model._tipoop = 1;
            model.numhojaruta = "";
            model.idusuarioregistro = idusuario;
            model.numhojaruta = numhojaruta;
            model.iddespacho = variables_orden[0].iddespacho.Value;
            model.idvehiculo = idvehiculo;
            model.iddestino = variables_orden[0].iddestino;
            model.idtipooperacion = variables_orden[0].idtipooperacion.Value;
            var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarManifiesto(model, out nummanifiesto);
            idsmanifiesto = result.idmanifiesto;
            foreach (var item in listado)
            {
                modelorden = new OrdenTrabajoModel();
                modelorden._tipoop = 3;
                modelorden.idordentrabajo = item.idordentrabajo;
                modelorden.idmanifiesto = result.idmanifiesto;
                modelorden.iddespacho = item.iddespacho;
                modelorden.idtipooperacion = item.idtipooperacion;
                modelorden.activo = true;
                modelorden.idcarga = idcarga;
                modelorden.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteDespacho;
                var resultot = OrdenData.InsertarActualizarOrdenTransporte(modelorden, null);
                ids = ids + "," + item.idordentrabajo;
            }
            ids = ids.Substring(1, ids.Length - 1);

            modIncidencia = new IncidenciaModel();
            modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ManifiestoHR;
            modIncidencia.idsorden = ids;
            modIncidencia.fechaincidencia = DateTime.Now;
            modIncidencia.fecharegistro = DateTime.Now;
            modIncidencia.descripcion = "Se generó el manifiesto: " + nummanifiesto + " y la HR: " + numhojaruta;
            modIncidencia.idusuarioregistro = idusuario;
            modIncidencia.activo = true;
            modIncidencia.recurso = vehiculo.placa;
            modIncidencia.documento = vehiculo.nombrechofer;
            IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

            return idsmanifiesto;
        }

        public List<long> ReGenerarManifiesto(long idmanifiesto, int idvehiculo, int idusuario)
        {
            #region variables

            ManifiestoModel model = new ManifiestoModel();
            OrdenTrabajoModel modelorden = new OrdenTrabajoModel();
            List<string> TOTAL = new List<string>();
            DespachoModel modeldespacho = new DespachoModel();
            List<OrdenTrabajoModel> objAgregados = new List<OrdenTrabajoModel>();
            string nummanifiesto;
            IncidenciaModel modIncidencia = null;
            string ids = string.Empty;
            List<long> idsmanifiesto = new List<long>();

            #endregion variables

            var listado = GetListarDetalleDespacho(idvehiculo);

            listado = listado.Where(x => x.idmanifiesto.Equals(idmanifiesto)).ToList();

            var numhojaruta = SeguimientoData.obtenerUltimaHojaRuta();

            var vehiculo = OrdenData.GetObtenerVehiculo(idvehiculo);

            #region distribucionlocal

            var listadodistribucion = listado.Where(x => x.idtipooperacion == (Int32)DataAccess.Constantes.TipoOperacion.DistribucionLocal).ToList();
            if (listadodistribucion.Count > 0)
            {
                listado = listado.Where(x => x.idtipooperacion != (Int32)DataAccess.Constantes.TipoOperacion.DistribucionLocal).ToList();

                // crea manifiesto

                model = new ManifiestoModel();
                model.activo = true;
                model.fecharegistro = DateTime.Now;
                model.idhojaruta = null;
                model.idmanifiesto = null;
                model.idusuarioregistro = idusuario;
                model.iddespacho = listado[0].iddespacho;
                model._tipoop = 1;
                model.numhojaruta = numhojaruta;
                model.idvehiculo = idvehiculo;
                //model.iddestino = item2.iddestino;
                model.idtipooperacion = (Int32)DataAccess.Constantes.TipoOperacion.DistribucionLocal;

                var resultmanifiesto = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarManifiesto(model, out nummanifiesto);
                //idsdespacho.Add(resultmanifiesto.idmanifiesto);
                idsmanifiesto.Add(resultmanifiesto.idmanifiesto);
                foreach (var item3 in listadodistribucion)
                {
                    TOTAL.Add(item3.destino);
                    var resultot = OrdenData.ActualizarManifiestoOT(item3.idordentrabajo, resultmanifiesto.idmanifiesto, listado[0].iddespacho, item3.idcarga, item3.idtipooperacion);
                    ids = ids + "," + item3.idordentrabajo;
                }
                ids = ids.Substring(1, ids.Length - 1);

                modIncidencia = new IncidenciaModel();
                modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ManifiestoHR;
                modIncidencia.idsorden = ids;
                modIncidencia.fechaincidencia = DateTime.Now;
                modIncidencia.fecharegistro = DateTime.Now;
                modIncidencia.descripcion = "Se generó el manifiesto: " + nummanifiesto + " y la HR: " + numhojaruta;
                modIncidencia.idusuarioregistro = idusuario;
                modIncidencia.activo = true;
                modIncidencia.recurso = vehiculo.placa;
                modIncidencia.documento = vehiculo.nombrechofer;
                IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
            }

            #endregion distribucionlocal

            foreach (var item in listado)
            {
                if (TOTAL.Count != 0)
                {
                    if (TOTAL.Where(x => x.Equals(item.destino)).ToList().Count > 0)
                        continue;
                }
                var tipoop = item.tipooperacion;
                var primeragrupado = listado.Where(x => x.tipooperacion == tipoop).ToList();
                foreach (var item2 in primeragrupado)
                {
                    var destino = item2.destino;
                    var segundoagrupado = primeragrupado.Where(x => x.destino == destino).ToList();

                    if (TOTAL.Count != 0)
                    {
                        if (TOTAL.Where(x => x.Equals(item2.destino)).ToList().Count > 0)
                            continue;
                    }

                    model = new ManifiestoModel();
                    model.activo = true;
                    model.fecharegistro = DateTime.Now;
                    model.idhojaruta = null;
                    model.idmanifiesto = null;
                    model.idusuarioregistro = null;
                    model._tipoop = 1;
                    model.numhojaruta = "";
                    model.idusuarioregistro = idusuario;
                    model.numhojaruta = item.numhojaruta;
                    model.iddespacho = item.iddespacho;
                    model.idvehiculo = idvehiculo;
                    model.iddestino = item2.iddestino;
                    model.idtipooperacion = item2.idtipooperacion;
                    var result = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarManifiesto(model, out nummanifiesto);
                    idsmanifiesto.Add(result.idmanifiesto);
                    foreach (var item3 in segundoagrupado)
                    {
                        modelorden = new OrdenTrabajoModel();
                        TOTAL.Add(item3.destino);
                        var resultot = OrdenData.ActualizarManifiestoOT(item3.idordentrabajo, result.idmanifiesto, listado[0].iddespacho, item3.idcarga, item3.idtipooperacion);
                        ids = ids + "," + item3.idordentrabajo;
                    }
                    ids = ids.Substring(1, ids.Length - 1);

                    modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ManifiestoHR;
                    modIncidencia.idsorden = ids;
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se generó el manifiesto: " + nummanifiesto + " y la HR: " + numhojaruta;
                    modIncidencia.idusuarioregistro = idusuario;
                    modIncidencia.activo = true;
                    modIncidencia.recurso = vehiculo.placa;
                    modIncidencia.documento = vehiculo.nombrechofer;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
                }
            }
            return idsmanifiesto;
        }

        public bool AnularManifiesto(long idorden, int idusuario)
        {
            string respuesta = "";
            ManifiestoModel modelmanifiesto = new ManifiestoModel();
            OrdenTrabajoModel modelorden = new OrdenTrabajoModel();
            decimal peso = 0, volumen = 0;

            var orden = OrdenData.GetObtenerOrden(idorden);
            var operacion = SeguimientoData.GetObtenerOperacion(orden.idcarga.Value);

        

            //Desasociar la OT
            modelorden._tipoop = 5;
            modelorden.idordentrabajo = orden.idordentrabajo;
            modelorden.idusuarioregistro = idusuario;
            modelorden.osembarque = operacion.numcarga;
            modelorden.iddespacho = null;
            modelorden.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteProgramacion;
            modelorden.activo = true;

            var resultot = OrdenData.InsertarActualizarOrdenTransporte(modelorden, null);

            ////generar nuevo manifiesto
            //var result = new SeguimientoData().ReGenerarManifiesto(orden.idmanifiesto.Value, operacion.idvehiculo.Value, idusuario);

            var modIncidencia = new IncidenciaModel();
            modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.DesasocioManifiesto;
            modIncidencia.idsorden = orden.idordentrabajo.ToString();
            modIncidencia.fechaincidencia = DateTime.Now;
            modIncidencia.fecharegistro = DateTime.Now;
            modIncidencia.descripcion = "Se desasoció el manifiesto";
            modIncidencia.idusuarioregistro = idusuario;
            modIncidencia.activo = true;
            IncidenciaData.InsertarActualizarIncidencia(modIncidencia);

            //var ordenes = SeguimientoData.des

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(operacion.idvehiculo);


            //Actualizar Cargas
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

            return 
                true;
        }

        public List<long> GenerarManifiesto(int idvehiculo, int idusuario)
        {
            string ids = string.Empty;
            ManifiestoModel model = new ManifiestoModel();
            OrdenTrabajoModel modelorden = new OrdenTrabajoModel();
            List<string> TOTAL = new List<string>();
            List<string> TOTAL_OP = new List<string>();
            List<OrdenTrabajoModel> objAgregados = new List<OrdenTrabajoModel>();
            DespachoModel modeldespacho = new DespachoModel();
            List<long> idsmanifiesto = new List<long>();

            var listado = DataAccess.Seguimiento.SeguimientoData.GetListarDetalleDespacho(idvehiculo);

            //Asignar Vehiculo
            VehiculoModel modelvehiculo = new VehiculoModel();
            modelvehiculo._tipoop = 5;
            modelvehiculo.idvehiculo = idvehiculo;
            modelvehiculo.idestado = (int)DataAccess.Constantes.EstadoVehiculo.Asignado;

            var respvehiculo = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarVehiculo(modelvehiculo);

            var vehiculo = OrdenData.GetObtenerVehiculo(idvehiculo);

            //Crear Despacho
            modeldespacho.idvehiculo = idvehiculo;
            modeldespacho._tipoop = 1;
            modeldespacho.idvehiculo = idvehiculo;

            var respdespacho = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarDespacho(modeldespacho);

            var numhojaruta = SeguimientoData.obtenerUltimaHojaRuta();

            //Tiene distribución local.

            #region distribucionlocal

            var listadodistribucion = listado.Where(x => x.idtipooperacion == (Int32)DataAccess.Constantes.TipoOperacion.DistribucionLocal).ToList();
            if (listadodistribucion.Count > 0)
            {
                listado = listado.Where(x => x.idtipooperacion != (Int32)DataAccess.Constantes.TipoOperacion.DistribucionLocal).ToList();

                // crea manifiesto
                string nummanifiesto;
                model = new ManifiestoModel();
                model.activo = true;
                model.fecharegistro = DateTime.Now;
                model.idhojaruta = null;
                model.idmanifiesto = null;
                model.idusuarioregistro = idusuario;
                model.iddespacho = respdespacho.iddespacho;
                model._tipoop = 1;
                model.numhojaruta = numhojaruta;
                model.idvehiculo = idvehiculo;
                //model.iddestino = item2.iddestino;
                model.idtipooperacion = (Int32)DataAccess.Constantes.TipoOperacion.DistribucionLocal;

                var resultmanifiesto = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarManifiesto(model, out nummanifiesto);
                idsmanifiesto.Add(resultmanifiesto.idmanifiesto);
                IncidenciaModel modIncidencia = null;
                foreach (var item3 in listadodistribucion)
                {
                    TOTAL.Add(item3.destino);
                    var resultot = OrdenData.ActualizarManifiestoOT(item3.idordentrabajo, resultmanifiesto.idmanifiesto, respdespacho.iddespacho, item3.idcarga, item3.idtipooperacion);
                    ids = ids + "," + item3.idordentrabajo;
                }
                ids = ids.Substring(1, ids.Length - 1);

                modIncidencia = new IncidenciaModel();
                modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ManifiestoHR;
                modIncidencia.idsorden = ids;
                modIncidencia.fechaincidencia = DateTime.Now;
                modIncidencia.fecharegistro = DateTime.Now;
                modIncidencia.descripcion = "Se generó el manifiesto: " + nummanifiesto + " y la HR: " + numhojaruta;
                modIncidencia.idusuarioregistro = idusuario;
                modIncidencia.activo = true;
                modIncidencia.recurso = vehiculo.placa;
                modIncidencia.documento = vehiculo.nombrechofer;
                IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
            }

            #endregion distribucionlocal

            foreach (var item in listado)
            {
                if (TOTAL.Count != 0)
                {
                    if (objAgregados.Where(x => x.destino.Equals(item.destino)
                        && x.tipooperacion.Equals(item.tipooperacion)).ToList().Count > 0)
                        continue;
                }
                var tipoop = item.tipooperacion;
                var primeragrupado = listado.Where(x => x.tipooperacion == tipoop).ToList();
                foreach (var item2 in primeragrupado)
                {
                    ids = string.Empty;
                    var destino = item2.destino;
                    var segundoagrupado = primeragrupado.Where(x => x.destino == destino).ToList();

                    if (TOTAL.Count != 0)
                    {
                        if (objAgregados.Where(x => x.destino.Equals(item2.destino)
                            && x.tipooperacion.Equals(item2.tipooperacion)).ToList().Count > 0)
                            continue;
                    }
                    string nummanifiesto;
                    model = new ManifiestoModel();
                    model.activo = true;
                    model.fecharegistro = DateTime.Now;
                    model.idhojaruta = null;
                    model.idmanifiesto = null;
                    model._tipoop = 1;
                    model.numhojaruta = numhojaruta;
                    model.idusuarioregistro = idusuario;
                    model.iddespacho = respdespacho.iddespacho;
                    model.idvehiculo = idvehiculo;
                    model.iddestino = item2.iddestino;
                    model.idtipooperacion = item2.idtipooperacion;

                    var resultmanifiesto = DataAccess.Seguimiento.SeguimientoData.InsertarActualizarManifiesto(model, out nummanifiesto);

                    idsmanifiesto.Add(resultmanifiesto.idmanifiesto);
                    IncidenciaModel modIncidencia = null;
                    foreach (var item3 in segundoagrupado)
                    {
                        TOTAL.Add(item3.destino);
                        var resultot = OrdenData.ActualizarManifiestoOT(item3.idordentrabajo
                            , resultmanifiesto.idmanifiesto, respdespacho.iddespacho, item3.idcarga, item3.idtipooperacion);

                        TOTAL.Add(item3.destino);
                        objAgregados.Add(new OrdenTrabajoModel() { destino = item3.destino, tipooperacion = item3.tipooperacion });
                        ids = ids + "," + item3.idordentrabajo;
                    }
                    ids = ids.Substring(1, ids.Length - 1);

                    modIncidencia = new IncidenciaModel();
                    modIncidencia.idmaestroincidencia = (Int32)Constantes.MaestroIncidentes.ManifiestoHR;
                    modIncidencia.idsorden = ids;
                    modIncidencia.fechaincidencia = DateTime.Now;
                    modIncidencia.fecharegistro = DateTime.Now;
                    modIncidencia.descripcion = "Se generó el manifiesto: " + nummanifiesto + " y la HR: " + numhojaruta;
                    modIncidencia.idusuarioregistro = idusuario;
                    modIncidencia.recurso = vehiculo.placa;
                    modIncidencia.documento = vehiculo.nombrechofer;
                    modIncidencia.activo = true;
                    IncidenciaData.InsertarActualizarIncidencia(modIncidencia);
                }
            }

            return idsmanifiesto;
        }

        #endregion manifiesto
    }
}