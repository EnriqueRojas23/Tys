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

namespace Web.TYS.DataAccess.Seguimiento
{
    public class OrdenData
    {
        #region Crear-editar

        public static long ActualizarCarga(OrdenTrabajoModel model)
        {
            Mapper.CreateMap<OrdenTrabajoModel, InsertarActualizarOrdenTrabajoCommand>();
            var comando = Mapper.Map<OrdenTrabajoModel, InsertarActualizarOrdenTrabajoCommand>(model);
            comando.idcarga = model.idcarga;
            comando.activo = true;
            comando.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteDespacho;

            var result = (InsertarActualizarOrdenTrabajoOutput)comando.Execute();
            return result.idordentrabajo;
        }

        public static bool EliminarOrdenTrabajo(long idorden)
        {
            var command = new InsertarActualizarOrdenTrabajoCommand { idordentrabajo = idorden, _tipoop = 4 };
            command.Execute();
            return true;
        }

        public static long DesasociarOrdenCarga(long idorden)
        {
            var comando = new InsertarActualizarOrdenTrabajoCommand();
            comando.idordentrabajo = idorden;
            comando._tipoop = 5;
            comando.idcarga = null;
            comando.idtipooperacion = null;
            comando.idagencia = null;
            comando.idcamioncompleto = null;
            comando.idmanifiesto = null;
            comando.iddespacho = null;
            comando.activo = true;
            comando.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteProgramacion;
            var result = (InsertarActualizarOrdenTrabajoOutput)comando.Execute();
            return result.idordentrabajo;
        }

        public static long ActualizarManifiestoOT(long idorden, long idmanifiesto, long iddespacho, long idcarga, int idtipooperacion)
        {
            var comando = new InsertarActualizarOrdenTrabajoCommand();
            comando.idordentrabajo = idorden;
            comando._tipoop = 3;
            comando.idmanifiesto = idmanifiesto;
            comando.iddespacho = iddespacho;
            comando.activo = true;
            comando.idcarga = idcarga;
            comando.idtipooperacion = idtipooperacion;
            comando.idestado = (Int32)Constantes.EstadoOT.PendienteDespacho;
            var result = (InsertarActualizarOrdenTrabajoOutput)comando.Execute();
            return result.idordentrabajo;
        }
        public static long InsertarActualizarOrdenTransporteSeguimiento(OrdenTrabajoSeguimiento orden)
        {
            Mapper.CreateMap<OrdenTrabajoSeguimiento, InsertarActualizarOrdenTrabajoSeguimientoCommand>();
            var command = Mapper.Map<OrdenTrabajoSeguimiento, InsertarActualizarOrdenTrabajoSeguimientoCommand>(orden);
            var result = (InsertarActualizarOrdenTrabajoOutput)command.Execute();
            return result.idordentrabajo;
        }



        public static long InsertarActualizarOrdenTransporte(OrdenTrabajoModel model, List<GuiaRemisionModel> guias, bool eliminarguias = false)
        {
            
            Mapper.CreateMap<OrdenTrabajoModel, InsertarActualizarOrdenTrabajoCommand>();
            var comando = Mapper.Map<OrdenTrabajoModel, InsertarActualizarOrdenTrabajoCommand>(model);

            var result = (InsertarActualizarOrdenTrabajoOutput)comando.Execute();

            // implementar el uso de transacciones
            if (guias != null)
            {
                var command = new EliminarGuiaRemisionClienteCommand { idordentrabajo = result.idordentrabajo };
                command.Execute();
                if (!eliminarguias)
                {
                    foreach (var item in guias)
                    {
                        item.idordentrabajo = result.idordentrabajo;
                        var existeenbd = OrdenData.ExisteGuia(item.nroguia.Trim(), null);
                        if (existeenbd == null)
                            item.idguiaremisioncliente = null;
                        InsertarActualizarGuias(item);
                    }
                }
            }
            return result.idordentrabajo;
        }

        public static AutogeneraOperacionesResult AutogeneraOperaciones(OrdenTrabajoModel model)
        {
            var parameters = new AutogeneraOperacionesParameter
            {
                idordentrabajo = model.idordentrabajo.Value,
                idruta = model.idruta,
                idusuarioregistro = model.idusuarioregistro,
                idvehiculo = model.idvehiculo,
                peso = model.peso,
                vol = model.volumen
            };
            var result = (AutogeneraOperacionesResult)parameters.Execute();

            return result;
        }

        public static long InsertarActualizarOTLigera(OrdenTrabajoModel model, List<GuiaRemisionModel> guias)
        {
            Mapper.CreateMap<OrdenTrabajoModel, InsertarActualizarOTLigeraCommand>();
            var comando = Mapper.Map<OrdenTrabajoModel, InsertarActualizarOTLigeraCommand>(model);

            var result = (InsertarActualizarOrdenTrabajoOutput)comando.Execute();

            if (guias != null)
            {
                foreach (var item in guias)
                {
                    item.idordentrabajo = result.idordentrabajo;
                    var existeenbd = OrdenData.ExisteGuia(item.nroguia.Trim(), null);
                    if (existeenbd == null)
                        item.idguiaremisioncliente = null;
                    InsertarActualizarGuias(item);
                }
            }

            return result.idordentrabajo;
        }

        public static InsertarActualizarGuiaRemisionClienteOutput InsertarActualizarGuias(GuiaRemisionModel model)
        {
            if (model.idguiaremisioncliente == 0)
                model.idguiaremisioncliente = null;
            var comando = new InsertarActualizarGuiaRemisionClienteCommand();
            comando.idguiaremisioncliente = model.idguiaremisioncliente;
            comando.idordentrabajo = model.idordentrabajo;
            comando.nroguia = model.nroguia;
            comando.bulto = model.bulto;
            comando.peso = model.peso;
            comando.pesovol = model.pesovol;
            comando.documento = model.documento;
            comando.volumen = model.volumen;

            return (InsertarActualizarGuiaRemisionClienteOutput)comando.Execute();
        }

        public static int ActualizarGuiaTransportista(long idorden, string numeroguia)
        {
            var comando = new ActualizarGuiaOrdenCommand { guiatransportista = numeroguia, idordentransporte = idorden };
            comando.Execute();
            return 1;
        }

        #endregion Crear-editar

        #region consultar

        public static string ObtenerUltimaOT()
        {
            var parameters = new ObtenerUltimaOrdenTrabajoParameter { };
            var resultado = (ObtenerUltimaOrdenTrabajoResult)parameters.Execute();
            if (resultado == null)
                return ConfigurationManager.AppSettings["numcp"].ToString();
            else return resultado.numcp.Split('-')[0].ToString() + "-" + (Convert.ToInt32(resultado.numcp.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');
        }

        public static ObtenerGuiaResult ExisteGuia(string numguia, long? idguiaremisioncliente)
        {
            var parameter = new ObtenerGuiaParameter { numguia = numguia, idguiaremisioncliente = idguiaremisioncliente };
            var result = (ObtenerGuiaResult)parameter.Execute();
            return result;
        }

        public static IEnumerable<OrdenTrabajoModel> GetListarOrdenes(string numcp, int? idcliente, string fecini, string fecfin, int? idestacion, int? pagenumber, int? pagesize)
        {
            var parametros = new ListarOrdenTrabajoParameters
            {
                numcp = numcp
                ,
                idcliente = idcliente
                ,
                fecinicio = fecini
                ,
                fecfin = fecfin
                ,
                idestacion = idestacion
                ,
                pagenumber = pagenumber
                ,
                pagesize = pagesize
            };
            var resultado = (ListarOrdenTrabajoResult)parametros.Execute();

            Mapper.CreateMap<ListarOrdenTrabajoDto, OrdenTrabajoModel>();
            return Mapper.Map<IEnumerable<ListarOrdenTrabajoDto>, IEnumerable<OrdenTrabajoModel>>(resultado.Hits);
        }
        public static IEnumerable<OrdenTrabajoModel> GetListarOrdenesCliente(string numcp, string fecini, string fecfin, string idscliente, string grr)
        {
            var parametros = new ListarOrdenTrabajoClienteParameters
            {
                numcp = numcp
                ,
                fecinicio = fecini
                ,
                fecfin = fecfin
                ,
                idscliente = idscliente ,
                grr = grr
            };
            var resultado = (ListarOrdenTrabajoClienteResult)parametros.Execute();

            Mapper.CreateMap<ListarOrdenTrabajoClienteDto, OrdenTrabajoModel>();
            return Mapper.Map<IEnumerable<ListarOrdenTrabajoClienteDto>, IEnumerable<OrdenTrabajoModel>>(resultado.Hits);
        }

        public static IEnumerable<OrdenTrabajoModel> GetListarOrdenesLigeras(string numcp, int? idcliente, bool devolucion)
        {
            var parametros = new ListarOTLigeraParameters { numcp = numcp, idcliente = idcliente, devolucion = devolucion };
            var resultado = (ListarOTLigeraResult)parametros.Execute();

            Mapper.CreateMap<ListarOTLigeraDto, OrdenTrabajoModel>();
            return Mapper.Map<IEnumerable<ListarOTLigeraDto>, IEnumerable<OrdenTrabajoModel>>(resultado.Hits);
        }

        public static IEnumerable<OrdenTrabajoModel> GetListarOrdenesHijasCC(long idcamioncompleto)
        {
            var parametros = new ListarOTHijasCCParameters { idcamioncompleto = idcamioncompleto };
            var resultado = (ListarOTHijasCCResult)parametros.Execute();

            Mapper.CreateMap<ListarOTHijasCCDto, OrdenTrabajoModel>();
            return Mapper.Map<IEnumerable<ListarOTHijasCCDto>, IEnumerable<OrdenTrabajoModel>>(resultado.Hits);
        }

        public static ObtenerVehiculoResult GetObtenerVehiculo(int? idvehiculo)
        {
            var parametros = new ObtenerVehiculoParameter { idvehiculo = idvehiculo.Value };
            var resultado = (ObtenerVehiculoResult)parametros.Execute();
            return resultado == null ? new ObtenerVehiculoResult() : resultado;
        }

        public static List<ListarOrdenTrabajoComprobanteDto> GetListarOrdenesComprobante(string numcp, string guia, int? idvehiculo)
        {
            if (numcp == "")
                numcp = null;
            if (guia == "")
            {
                idvehiculo = null;
                guia = null;
            }
            var parametros = new ListarOrdenTrabajoComprobanteParameters { numcp = numcp, guia = guia, idvehiculo = idvehiculo };
            var resultado = (ListarOrdenTrabajoComprobanteResult)parametros.Execute();
            return resultado == null ? new List<ListarOrdenTrabajoComprobanteDto>() : resultado.Hits.ToList();
        }

        public static OrdenTrabajoModel GetObtenerOrden_model(long idorden)
        {
            var parametros = new ObtenerOrdenTrabajoParameter { idorden = idorden };
            var resultado = (ObtenerOrdenTrabajoResult)parametros.Execute();
            //return resultado == null ? new ObtenerOrdenTrabajoResult() : resultado;

            Mapper.CreateMap<ObtenerOrdenTrabajoResult, OrdenTrabajoModel>();
            return Mapper.Map<ObtenerOrdenTrabajoResult, OrdenTrabajoModel>(resultado);
        }

        public static ObtenerOrdenTrabajoResult GetObtenerOrden(long idorden)
        {
            var parametros = new ObtenerOrdenTrabajoParameter { idorden = idorden };
            var resultado = (ObtenerOrdenTrabajoResult)parametros.Execute();
            return resultado == null ? new ObtenerOrdenTrabajoResult() : resultado;
        }

        public static ListarGuiasResult GetListarGuias(long idorden)
        {
            var parametros = new ListarGuiasParameters { idorden = idorden };
            var resultado = (ListarGuiasResult)parametros.Execute();
            return resultado == null ? new ListarGuiasResult() : resultado;
        }

        public static CamionCompletoModel GetObtenerCamionCompleto(long idcamioncompleto)
        {
            var parametros = new ObtenerCamionCompletoParameter { idcamioncompleto = idcamioncompleto };
            var resultado = (ObtenerCamionCompletoResult)parametros.Execute();
            //resultado == null ? new ObtenerCamionCompletoResult() : resultado;

            Mapper.CreateMap<ObtenerCamionCompletoResult, CamionCompletoModel>();
            return Mapper.Map<ObtenerCamionCompletoResult, CamionCompletoModel>(resultado);
        }

        #endregion consultar

        #region FillEvaluaciones

        public List<ListarFormulaTarifaDto> GetListarFormulaOT(int? idcliente, int? idorigen, int? iddestino, List<ListarUbigeoDto> ubigeo)
        {
            List<ListarFormulaTarifaDto> result = evaluarFormulaOT(idcliente, idorigen, iddestino, ubigeo);
            if (result.Count == 0)
                result = evaluarFormulaOT(80, idorigen, iddestino, ubigeo);
            return result;
        }

        private List<ListarFormulaTarifaDto> evaluarFormulaOT(int? idcliente, int? idorigen, int? iddestino, List<ListarUbigeoDto> ubigeo)
        {
            var parametros = new ListarFormulaTarifaParameters
            {
                idcliente = idcliente.Value,
                iddistrito = iddestino.Value,
                idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
            };
            var resultado = (ListarFormulaTarifaResult)parametros.Execute();
            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

            if (resultado.Hits.ToList().Count == 0)
            {
                parametros = new ListarFormulaTarifaParameters
                {
                    idcliente = idcliente.Value,
                    idprovincia = provincia.idprovincia,
                    idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                };
                resultado = (ListarFormulaTarifaResult)parametros.Execute();
                resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
            }
            if (resultado.Hits.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametros = new ListarFormulaTarifaParameters
                {
                    idcliente = idcliente.Value,
                    iddepartamento = provincia.iddepartamento,
                    idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                };
                resultado = (ListarFormulaTarifaResult)parametros.Execute();
                if (resultado.Hits.ToList().Count != 0) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
            }
            return resultado.Hits.ToList();
        }

        public List<ListarTipoTransporteTarifaDto> GetListarTipoTransporteOT(int? idcliente, int? idorigen, int? iddestino, List<ListarUbigeoDto> ubigeo)
        {
            List<ListarTipoTransporteTarifaDto> result = evaluarTipoTransporteOT(idcliente, idorigen, iddestino, ubigeo);
            if (result.Count == 0)
                result = evaluarTipoTransporteOT(80, idorigen, iddestino, ubigeo);
            return result;
        }

        private List<ListarTipoTransporteTarifaDto> evaluarTipoTransporteOT(int? idcliente, int? idorigen, int? iddestino, List<ListarUbigeoDto> ubigeo)
        {
            var parametros = new ListarTipoTransporteTarifaParameters
            {
                idcliente = idcliente.Value,
                iddistrito = iddestino.Value,
                idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
            };
            var resultado = (ListarTipoTransporteTarifaResult)parametros.Execute();
            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();

            if (resultado.Hits.ToList().Count == 0)
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
            if (resultado.Hits.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametros = new ListarTipoTransporteTarifaParameters
                {
                    idcliente = idcliente.Value,
                    iddepartamento = provincia.iddepartamento,
                    idorigen = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento
                };
                resultado = (ListarTipoTransporteTarifaResult)parametros.Execute();
                if (resultado.Hits.ToList().Count != 0) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
            }
            return resultado.Hits.ToList();
        }

        public static List<ListarTarifaOrdenDto> GetListarTarifasOrden(int idorigen, int iddestino, int idcliente, int idformula, int idtipotransporte, int? idconceptocobro)
        {

            var cliente = DataAccess.Seguimiento.SeguimientoData.GetObtenerCliente(idcliente);
            List<ListarTarifaOrdenDto> result;

            result = TarifaData.evaluarTarifasOrden_OrigenDistrito(idorigen, iddestino, idcliente, idformula, idtipotransporte, idconceptocobro);
            if (result.Count == 0)
                result = TarifaData.evaluarTarifasOrden_OrigenProvincias(idorigen, iddestino, idcliente, idformula, idtipotransporte, idconceptocobro);
            if (result.Count == 0)
                result = TarifaData.evaluarTarifasOrden_OrigenDepartamento(idorigen, iddestino, idcliente, idformula, idtipotransporte, idconceptocobro);

            if (result.Count > 0)
                return result;
            else
            {
                //Para clientes de pago al contado
                result = TarifaData.evaluarTarifasOrden_OrigenDistrito(idorigen, iddestino, 80, idformula, idtipotransporte, idconceptocobro);
                if (result.Count == 0)
                    result = TarifaData.evaluarTarifasOrden_OrigenProvincias(idorigen, iddestino, 80, idformula, idtipotransporte, idconceptocobro);
                if (result.Count == 0)
                    result = TarifaData.evaluarTarifasOrden_OrigenDepartamento(idorigen, iddestino, 80, idformula, idtipotransporte, idconceptocobro);
            }

            //if (!cliente.pagocontado)
            //{

            //    result = TarifaData.evaluarTarifasOrden_OrigenDistrito(idorigen, iddestino, idcliente, idformula, idtipotransporte, idconceptocobro);
            //    if (result.Count == 0)
            //        result = TarifaData.evaluarTarifasOrden_OrigenProvincias(idorigen, iddestino, idcliente, idformula, idtipotransporte, idconceptocobro);
            //    if (result.Count == 0)
            //        result = TarifaData.evaluarTarifasOrden_OrigenDepartamento(idorigen, iddestino, idcliente, idformula, idtipotransporte, idconceptocobro);
            //}
            //else
            //{
            //    //Para clientes de pago al contado
            //     result = TarifaData.evaluarTarifasOrden_OrigenDistrito(idorigen, iddestino, 80, idformula, idtipotransporte, idconceptocobro);
            //    if (result.Count == 0)
            //        result = TarifaData.evaluarTarifasOrden_OrigenProvincias(idorigen, iddestino, 80, idformula, idtipotransporte, idconceptocobro);
            //    if (result.Count == 0)
            //        result = TarifaData.evaluarTarifasOrden_OrigenDepartamento(idorigen, iddestino, 80, idformula, idtipotransporte, idconceptocobro);
            //}
            return result;
        }

        
        #endregion FillEvaluaciones
    }
}