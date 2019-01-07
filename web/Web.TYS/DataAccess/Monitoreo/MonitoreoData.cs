using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Componentes.Common.Utilidades;
using ServiceAgents.Common;
using CommandContracts.TYS.Seguimiento.Output;
using Web.TYS.Areas.Monitoreo.Models;
using CommandContracts.TYS.Seguimiento;
using AutoMapper;
using Web.TYS.Areas.Seguimiento.Models.Seguimiento;
using System.Configuration;
using Web.TYS.DataAccess.Seguimiento;

namespace Web.TYS.DataAccess.Monitoreo
{
    public class MonitoreoData
    {
        public static List<ListarMonitoreoOperacionesDto> GetListarMonitoreoOperaciones(Int64 idmanifiesto)
        {
            var parametros = new ListarMonitoreoOperacionesParameters
            {
                idmanifiesto = idmanifiesto
            };
            var resultado = (ListarMonitoreoOperacionesResults)parametros.Execute();
            return resultado == null ? new List<ListarMonitoreoOperacionesDto>() : resultado.Hits.ToList();
        }

        public static List<ListarOtsMonitoreoDto> OtsMonitoreo(int? idcliente
        , int? iddestino
        , string numcp
        , string nummanifiesto
        , string numhojaruta
        , string grr, string documento, string tienda)
        {
            var parametros = new ListarOtsMonitoreoParameters
            {
                grr = grr,
                idcliente = idcliente,
                iddestino = iddestino,
                numcp = numcp,
                nummanifiesto = nummanifiesto,
                numhojaruta = numhojaruta,
                documento = documento,
                tienda = tienda
            };
            var resultado = (ListarOtsMonitoreoResults)parametros.Execute();
            return resultado == null ? new List<ListarOtsMonitoreoDto>() : resultado.Hits.ToList();
        }
        public static List<ListarOtsMonitoreoEntregaDto> OtsMonitoreoEntrega(int? idcliente
      , int? iddestino
      , string numcp
      , string nummanifiesto
            , int? idestado, int idtipotransporte)
      
        {
            var parametros = new ListarOtsMonitoreoEntregaParameters
            {
                idcliente = idcliente,
                iddestino = iddestino,
                numcp = numcp,
                nummanifiesto = nummanifiesto,
                idestado = idestado,
                idtipotransporte = idtipotransporte
            };
            var resultado = (ListarOtsMonitoreoEntregaResults)parametros.Execute();
            return resultado == null ? new List<ListarOtsMonitoreoEntregaDto>() : resultado.Hits.ToList();
        }

        public static List<ListarOtsFluvialDto> OtsFluvial(int? idcliente
       , int? iddestino
       , int? idestado
       , string numcp
       , string nummanifiesto
       , string numhojaruta
       , string documento)
        {
            var parametros = new ListarOtsFluvialParameters
            {
                documento = documento,
                idcliente = idcliente,
                iddestino = iddestino,
                idestado = idestado,
                numcp = numcp,
                nummanifiesto = nummanifiesto,
                numhojaruta = numhojaruta
            };
            var resultado = (ListarOtsFluvialResults)parametros.Execute();
            return resultado == null ? new List<ListarOtsFluvialDto>() : resultado.Hits.ToList();
        }

        public static List<ListarMonitoreoOperacionesExportarDto> GetListarMonitoreoOperaciones_Exportar(string fechainicio
      , string fechafin
      , int? idestacion
      , int? idruta
      , string guia
      , string chofer
      , string placa)
        {
            var parametros = new ListarMonitoreoOperacionesExportarParameters
            {
                fechainicio = fechainicio,
                fechafin = fechafin,
                idruta = idruta,
                idestacion = idestacion,
                guia = guia,
                chofer = chofer,
                placa = placa
            };
            var resultado = (ListarMonitoreoOperacionesExportarResults)parametros.Execute();
            return resultado == null ? new List<ListarMonitoreoOperacionesExportarDto>() : resultado.Hits.ToList();
        }

       



        public static List<OrdenTrabajoModel> GetListarOrdenesxManifiesto(long idmanifiesto)
        {
            var parametros = new ListarOrdenesxManifiestoParameters { idmanifiesto = idmanifiesto };
            var resultado = (ListarOrdenesxManifiestoResults)parametros.Execute();
            var todo = (ListarOrdenesxManifiestoDto[])resultado.Hits;

            Mapper.CreateMap<ListarOrdenesxManifiestoDto, OrdenTrabajoModel>();
            var respuesta = Mapper.Map<IEnumerable<ListarOrdenesxManifiestoDto>
                , IEnumerable<OrdenTrabajoModel>>(resultado.Hits);
            return respuesta.ToList();
        }

        

        public static InsertarHelpResourcesOutput InsertarHelpResources(string help, int idcampo)
        {
            var comando = new InsertarHelpResourcesCommand { idcampo = idcampo, help = help };
            return (InsertarHelpResourcesOutput)comando.Execute();
        }

        public static List<ListarHelpResourcesDto> ListarHelpResources(int id)
        {
            var parameters = new ListarHelpResourcesParameters { idcampo = id };
            var result = (ListarHelpResourcesResults)parameters.Execute();
            return result.Hits.ToList();
        }

        public long InsertarActualizarEmbarqueFluvial(EmbarqueModel model)
        {
            Mapper.CreateMap<EmbarqueModel, InsertarActualizarEmbarqueFluvialCommand>();
            var command = Mapper.Map<EmbarqueModel, InsertarActualizarEmbarqueFluvialCommand>(model);

            var result = (InsertarActualizarEmbarqueFluvialOutput)command.Execute();
            return result.idembarque;
        }

      

        internal static bool CerrarOperaciones(IEnumerable<OrdenTrabajoModel> ordenes)
        {
            try
            {
                foreach (var orden in ordenes)
                {
                    var command = new CerrarOperacionCommand { cierreoperativo = true, idorden = orden.idordentrabajo };
                    command.Execute();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static int CerrarManifiesto(long idmanifiesto)
        {
            CerrarOperacionCommand command = new CerrarOperacionCommand();
            command.idmanifiesto = idmanifiesto;
            command.Execute();
            return 1;
        }

        internal static int ValidarOtsCerradas(long idmanifiesto)
        {
            var parameters = new ValidarOtsCerradasParameter { idmanifiesto = idmanifiesto };
            var result = (ValidarOtsCerradasResult)parameters.Execute();
            return result.cantidad;
        }

        internal static IEnumerable<MaestroEtapaModel> GetListarMaestraEtapa(string tipoetapa)
        {
            var parameters = new ListarEtapasMonitoreoParameters { tipoetapa = tipoetapa };
            var result = (ListarEtapasMonitoreoResults)parameters.Execute();

            Mapper.CreateMap<ListarEtapasMonitoreoDto, MaestroEtapaModel>();
            return Mapper.Map<IEnumerable<ListarEtapasMonitoreoDto>, IEnumerable<MaestroEtapaModel>>(result.Hits);
        }

        public static List<ManifiestoModel> GetListarManifiesto(long idembarque)
        {
            var parameters = new ListarManifiestosSinEmbarqueParameters { idembarque = idembarque };
            var result = (ListarManifiestosSinEmbarqueResults)parameters.Execute();

            Mapper.CreateMap<ListarManifiestosSinEmbarqueDto, ManifiestoModel>();
            return Mapper.Map<IEnumerable<ListarManifiestosSinEmbarqueDto>, IEnumerable<ManifiestoModel>>(result.Hits).ToList();
        }

        public static List<ManifiestoModel> GetListarAsignadosManifiesto(long idembarque)
        {
            var parameters = new ListarManifiestosAsignadosEmbarqueParameters { idembarque = idembarque };
            var result = (ListarManifiestosAsignadosEmbarqueResults)parameters.Execute();

            Mapper.CreateMap<ListarManifiestosAsignadosEmbarqueDto, ManifiestoModel>();
            return Mapper.Map<IEnumerable<ListarManifiestosAsignadosEmbarqueDto>, IEnumerable<ManifiestoModel>>(result.Hits).ToList();
        }

        #region Etapa

        public static long  InsertarEtapa(EtapaModel model, List<IncidenciaGuiasRechazadas> guias)
        {
            string[] ordenes = model.idsorden.Split(',');
            InsertarEtapaParameterDto param;
            InsertarEtapaParameter parameters = new InsertarEtapaParameter();
            parameters.Hits = new List<InsertarEtapaParameterDto>();

            foreach (var item in ordenes)
            {
                var orden = Seguimiento.OrdenData.GetObtenerOrden_model(Convert.ToInt64(item));
                if (orden.idtipooperacion == 124) // estacion a estacion
                //Desasociar la OT
                {
                    if (model.idmaestroetapa == 4) //Confirmar Recibo
                    {
                        orden._tipoop = 9;
                        orden.iddespacho = null;
                        orden.idmanifiesto = null;
                        orden.idcarga = null;
                        orden.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteProgramacion;
                        orden.activo = true;
                        orden.idestacionorigen = orden.idestaciondestino;
                        var resultot = OrdenData.InsertarActualizarOrdenTransporte(orden, null);
                    }
                }
                else
                {
                    if (model.tipoetapa.Trim() == "E")
                    {
                        orden._tipoop = 8;
                        orden.idordentrabajo = orden.idordentrabajo;
                        orden.idestado = (Int32)DataAccess.Constantes.EstadoOT.PendienteRetornoDocumentario;
                        orden.fechaentrega = model.fechaetapa;
                        orden.activo = true;
                        orden.idusuarioregistro = model.idusuarioregistro;
                        orden.idusuarioentrega = model.idusuarioregistro;
                        var resultot = OrdenData.InsertarActualizarOrdenTransporte(orden, null);
                    }
                }

                param = new InsertarEtapaParameterDto();
                param.descripcion = model.descripcion;
                param.documento = model.documento;
                param.fechaetapa = model.fechaetapa.Value;
                param.fecharegistro = model.fecharegistro;
                param.idordentrabajo = Convert.ToInt64(item);
                param.idusuarioregistro = model.idusuarioregistro;
                param.recurso = model.recurso;
                param.visible = model.visible;
                param.idmaestroetapa = model.idmaestroetapa;
                parameters.Hits.Add(param);
            }

            if (guias != null)
            {
                //insertar ot nueva por el rechazo
                var cantidad = guias.Select(x => x.cantidad).Sum();
                long idordennueva = duplicarOT(Convert.ToInt64(ordenes[0]), model.idusuarioregistro, cantidad, false);

                foreach (var item in guias)
                {
                    item.idordentrabajo = Convert.ToInt64(ordenes[0]);
                    DataAccess.Monitoreo.MonitoreoData.InsertarGuiaRechazada(item);
                    duplicarGuia(item.idordentrabajo, false, item.idguia, idordennueva);
                }
            }
            else if (model.idmaestroetapa.Equals((Int32)Constantes.MaestroEtapas.EntregaRechazoTotal))
            {
                long idordennueva = duplicarOT(Convert.ToInt64(ordenes[0]), model.idusuarioregistro, 0, true);
                long resp = duplicarGuia(Convert.ToInt64(ordenes[0]), true, 0, idordennueva);
            }

            parameters.Execute();
            return 1;
        }

        public static long duplicarOT(long idordentrabajo, int idusuarioregistro, int bultos, bool rechazototal)
        {
            var parameter = new DuplicarOTParameters
            {
                idordentrabajo = idordentrabajo,
                idusuarioregistro = idusuarioregistro,
                cantidad = bultos,
                rechazototal = rechazototal
            };
            var result = (DuplicarOTResults)parameter.Execute();
            return result.idordentrabajo;
        }

        public static long duplicarGuia(long idordentrabajo, bool rechazototal, long idguiaremisioncliente, long idordennueva)
        {
            var parameter = new DuplicarGuiaParameters
            {
                idordentrabajo = idordentrabajo,
                rechazototal = rechazototal,
                idguiaremisioncliente = idguiaremisioncliente,
                idordennueva = idordennueva
            };
            var result = (DuplicarGuiaResults)parameter.Execute();
            return result.idguia;
        }

        #endregion Etapa

        internal static IEnumerable<OrdenTrabajoModel> obtenerOt(string numcp)
        {
            var parameter = new ListarOrdenesTrabajoParameters { numcp = numcp };
            var result = (ListarOrdenesTrabajoResults)parameter.Execute();

            Mapper.CreateMap<ListarOrdenesTrabajoDto, OrdenTrabajoModel>();
            return Mapper.Map<IEnumerable<ListarOrdenesTrabajoDto>, IEnumerable<OrdenTrabajoModel>>(result.Hits);
        }

        public static IEnumerable<EmbarqueModel> GetListarDetalleEmbarqueFluvial(long idembarque)
        {
            var parametros = new ListarDetalleEmbarqueParameters { idembarque = idembarque };
            var resultado = (ListarDetalleEmbarqueResults)parametros.Execute();

            Mapper.CreateMap<ListarDetalleEmbarqueDto, EmbarqueModel>();
            return Mapper.Map<IEnumerable<ListarDetalleEmbarqueDto>, IEnumerable<EmbarqueModel>>(resultado.Hits);
        }

        public static IEnumerable<EventoModel> GetListarEventos(string numcp, int? idmaestroincidencia, int? idmaestroetapa, long? idorden)
        {
            var parametros = new ListarEventosParameters { numcp = numcp, idmaestroincidencia = idmaestroincidencia, idmaestroetapa = idmaestroetapa, idorden = idorden };
            var resultado = (ListarEventosResults)parametros.Execute();

            Mapper.CreateMap<ListarEventosDto, EventoModel>();
            return Mapper.Map<IEnumerable<ListarEventosDto>, IEnumerable<EventoModel>>(resultado.Hits);
        }

        public static IEnumerable<EmbarqueModel> GetListarEmbarqueFluvial(string numeroembarque, int? idtransporte)
        {
            var parametros = new ListarEmbarqueFluvialParameters { numeroembarque = numeroembarque, idvehiculo = idtransporte };
            var resultado = (ListarEmbarqueFluvialResults)parametros.Execute();

            Mapper.CreateMap<ListarEmbarqueFluvialDto, EmbarqueModel>();
            return Mapper.Map<IEnumerable<ListarEmbarqueFluvialDto>, IEnumerable<EmbarqueModel>>(resultado.Hits);
        }

        internal static void InsertarGuiaRechazada(IncidenciaGuiasRechazadas item)
        {
            var command = new InsertarGuiaEtapaCommand
            {
                cantidad = item.cantidad
                ,
                idordentrabajo = item.idordentrabajo
                ,
                idguiaremisioncliente = item.idguia
            };
            command.Execute();
        }

        public static string ObtenerUltimoNumeroEmbarque()
        {
            var parameters = new ObtenerUltimoNumeroEmbarqueParameters { };
            var resultado = (ObtenerUltimoNumeroEmbarqueResults)parameters.Execute();
            if (resultado == null)
                return ConfigurationManager.AppSettings["numemb"].ToString();
            else return resultado.numeroembarque.Split('-')[0].ToString() + "-" + (Convert.ToInt32(resultado.numeroembarque.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');
        }

        public static EmbarqueModel obtenerEmbarque(long idembarque)
        {
            Mapper.CreateMap<ObtenerEmbarqueFluvialResults, EmbarqueModel>();
            var parameter = new ObtenerEmbarqueFluvialParameters { idembarque = idembarque };
            var result = (ObtenerEmbarqueFluvialResults)parameter.Execute();
            return Mapper.Map<ObtenerEmbarqueFluvialResults, EmbarqueModel>(result);
        }

        public static int EliminarDetalleEmbarque(long idorden)
        {
            var command = new EliminarDetalleEmbarqueCommand { idordentrabajo = idorden };
            command.Execute();
            return 1;
        }

        public static int InsertarDetalleEmbarque(EmbarqueModel model, IEnumerable<ManifiestoModel> ordenes)
        {
            //      string[] ordenes = model.idsorden.Split(',');
            InsertarDetalleEmbarqueDto param;
            InsertarDetalleEmbarqueParameter parameters = new InsertarDetalleEmbarqueParameter();
            parameters.Hits = new List<InsertarDetalleEmbarqueDto>();
            string ids = string.Empty;
            foreach (var item in ordenes)
            {
                param = new InsertarDetalleEmbarqueDto();
                param.fechacontrolsunat = model.fechacontrolsunat;
                param.fechadescarga = model.fechadescarga;
                param.idembarque = model.idembarque.Value;
                param.idmanifiesto = model.idmanifiesto;
                param.idordentrabajo = Convert.ToInt64(item.idordentrabajo);
                param.idpuerto = model.idpuerto;
                param.idusuariocontrolsunat = model.idusuariocontrolsunat;
                param.idusuariodescarga = model.idusuariodescarga;
                parameters.Hits.Add(param);
                ids = ids + "," + item.idordentrabajo.ToString();
            }
            parameters.Execute();
            ids = ids.Substring(1, ids.Length - 1);

            EtapaModel modEtapa = new EtapaModel();
            modEtapa.fecharegistro = DateTime.Now;
            modEtapa.idusuarioregistro = model.idusuarioregistro;
            modEtapa.visible = true;
            modEtapa.fechaetapa = DateTime.Now;
            modEtapa.idmaestroetapa = (Int32)Constantes.MaestroEtapas.AsignaciondeEmbarque;
            modEtapa.descripcion = "Conocimiento de Embarque : " + model.conocimientoembarque;
            modEtapa.tipoetapa = "M";
            modEtapa.idsorden = ids;
            var idetapa = InsertarEtapa(modEtapa, null);

            return 1;
        }

        public static int DescargaFluvial(List<ManifiestoModel> model)
        {
            foreach (var item in model)
            {
                var parameter = new ActualizarDetalleEmbarqueCommand
                {
                    idordentrabajo = item.idordentrabajo
                    ,
                    fechadescarga = item.fechadescarga
                    ,
                    idusuariodescarga = item.idusuariodescarga
                    ,
                    embarquecompleto = item.embarquecompleto
                    ,
                    idpuerto = item.idpuerto
                    ,
                    fechacontrolsunat = item.fechacontrolsunat
                    ,
                    idusuariocontrolsunat = item.idusuariodescarga
                };
                parameter.Execute();
            }

            return 1;
        }

        internal static bool eliminarEmbarque(long idembarque)
        {
            var parameter = new EliminarEmbarqueFluvialParameters { idembarque = idembarque };
            parameter.Execute();
            return true;
        }

        public static bool ActualizarEmbarque(EmbarqueModel model)
        {
            var comando = new InsertarActualizarEmbarqueFluvialCommand
            {
                conocimientoembarque = model.conocimientoembarque
                ,
                embarquecompleto = model.embarquecompleto
                ,
                fechaatraque = model.fechaatraque
                ,
                fechafincarga = model.fechafincarga
                ,
                fechainiciocarga = model.fechainiciocarga
                ,
                fechallegada = model.fechallegada
                ,
                fecharegistro = model.fecharegistro
                ,
                fechazarpe = model.fechazarpe
                ,
                idembarque = model.idembarque
                ,
                idpuerto = model.idpuerto
                ,
                idtransporte = model.idtransporte
                ,
                idusuarioregistro = model.idusuarioregistro
                ,
                numeroembarque = model.numeroembarque
            };
            comando.Execute();
            return true;
        }

        #region embarque

        public static bool ExisteCE(string ce, int idtransporte, long? idembarque)
        {
            var parameter = new ObtenerExisteCEParameters { ce = ce, idtransporte = idtransporte, idembarque = idembarque };
            var result = (ObtenerExisteCEResults)parameter.Execute();
            if (result.cantidad > 0) return true; else return false;
        }

        #endregion embarque
    }
}