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
    public class IncidenciaData
    {
        

        public static IEnumerable<MaestroIncidenteModel> GetListarMaestroIncidencia(string tipoincidencia, int idmaestroincidencia)
        {
            var parametros = new ListarMaestroIncidenteParameters { 
                tipoincidencia = tipoincidencia 
                , idmaestroincidencia = idmaestroincidencia  
            };

            var resultado = (ListarMaestroIncidenteResults)parametros.Execute();

            Mapper.CreateMap<ListarMaestroIncidenteDto, MaestroIncidenteModel>();
            var respuesta = Mapper.Map<IEnumerable<ListarMaestroIncidenteDto>
                , IEnumerable<MaestroIncidenteModel>>(resultado.Hits);
            return respuesta;
        }
        public static IEnumerable<MaestroIncidenteModel> GetListarMaestroIncidencia(string tipoincidencia)
        {
            var parametros = new ListarMaestroIncidenteParameters
            {
                tipoincidencia = tipoincidencia
            };

            var resultado = (ListarMaestroIncidenteResults)parametros.Execute();

            Mapper.CreateMap<ListarMaestroIncidenteDto, MaestroIncidenteModel>();
            var respuesta = Mapper.Map<IEnumerable<ListarMaestroIncidenteDto>
                , IEnumerable<MaestroIncidenteModel>>(resultado.Hits);
            return respuesta;
        }

        public static List<ListarIncidentesDto> ListarIncidenciasxOT(long idorden , int? idmaestroincidencia = null)
        {
            var parametros = new ListarIncidentesParameters { idorden = idorden , idmaestroincidencia = idmaestroincidencia };
            var resultado = (ListarIncidentesResults)parametros.Execute();
            return resultado == null ? new List<ListarIncidentesDto>() : resultado.Hits.ToList();
        }
        public static InsertarActualizarIncidenteOutput AnularIncidencia(long id)
        {
            var comando = new AnularIncidenteCommand { idincidencia = id };
            return (InsertarActualizarIncidenteOutput)comando.Execute();
        }
        public static long InsertarActualizarIncidenciaIndividual(IncidenciaModel model)
        {
            Mapper.CreateMap<IncidenciaModel, InsertarActualizarIncidenteCommand>();
            var command = Mapper.Map<IncidenciaModel, InsertarActualizarIncidenteCommand>(model);
            var result = (InsertarActualizarIncidenteOutput)command.Execute();
            return result.idincidencia;
        }
        public static long InsertarActualizarIncidencia(IncidenciaModel model)
        {
            string[] ordenes = model.idsorden.Split(',');
            InsertarIncidenciaParameterDto param;
            InsertarIncidenciaParameter parameters = new InsertarIncidenciaParameter();
            parameters.Hits = new List<InsertarIncidenciaParameterDto>();

            foreach (var item in ordenes)
            {
                param = new InsertarIncidenciaParameterDto();
                param.descripcion = model.descripcion;
                param.fechaincidencia = model.fechaincidencia;
                param.fecharegistro = model.fecharegistro;
                param.idordentrabajo = Convert.ToInt64(item);
                param.idusuarioregistro = model.idusuarioregistro;
                param.activo = model.activo;
                param.observacion = model.observacion;
                param.idmaestroincidencia = model.idmaestroincidencia;
                param.recurso = model.recurso;
                param.documento = model.documento;
                parameters.Hits.Add(param);
            }

            parameters.Execute();
            return 1;
            //bool existemasivo = false;

            //if (model.idmanifiesto == null || model.idmanifiesto == 0)
            //{
            //    var entrega = MonitoreoData.GetListarMaestroIncidente(null).SingleOrDefault();

            

            //    bool existe = (incidencias.Where(x => x.idmaestroincidencia.Equals(model.idmaestroincidencia)).SingleOrDefault() != null);
            //    if (existe)
            //        return -1;

            //    Mapper.CreateMap<IncidenciaModel, InsertarActualizarIncidenteCommand>();
            //    var comando =  Mapper.Map<IncidenciaModel, InsertarActualizarIncidenteCommand>(model);

            //    //Actualiza estado de la orden
            //  //  if(model.idmaestroincidencia)

            //    if (entrega != null)
            //    {
            //        if (entrega.tipo == "E")
            //            if (incidencias.Where(x => x.tipo.Equals('E')).SingleOrDefault() != null)
            //                return -1;
            //        DataAccess.Seguimiento.SeguimientoData.ActualizarEstadoOT(model.idordentrabajo.Value
            //          , Convert.ToInt32(DataAccess.Constantes.EstadoOT.PendienteRetornoDocumentario));
            //    }

            //    var incidente = (InsertarActualizarIncidenteOutput)comando.Execute();
            //    return incidente.idincidencia;

            //}
            //else
            //{
            //    var ordenes = DataAccess.Monitoreo.MonitoreoData.GetListarOrdenesxManifiesto(model.idmanifiesto.Value);
            //    foreach (var item in ordenes)
            //    {
            //        var incidencias = DataAccess.Monitoreo.MonitoreoData.GetListarDetalleMonitoreoOperaciones(item.idordentrabajo).OrderByDescending(x => x.fechainicio).ToList();

            //        var entrega = MonitoreoData.GetListarMaestroIncidente(null).SingleOrDefault();
            //        bool existe = (incidencias.Where(x => x.idmaestroincidencia.Equals(model.idmaestroincidencia)).SingleOrDefault() != null);

            //        if (existe)
            //        {
            //            existemasivo = true;
            //            continue;
            //        }
            //        try
            //        {
            //            if (entrega.tipo == "E")
            //                if (incidencias.Where(x => x.tipo == "E").SingleOrDefault() != null)
            //                {
            //                    existemasivo = true;
            //                    continue;
            //                }

            //        }
            //        catch (Exception)
            //        {
            //            throw;
            //        }

            //        var comando = new InsertarActualizarIncidenteCommand();
            //        if (model.idincidente == 0)
            //            model.idincidente = null;
            //        comando.idincidencia = model.idincidente;
            //        comando.descripcion = model.descripcion;

            //        var hm = model.horaincidencia.Split(':');
            //        TimeSpan ts = new TimeSpan(Convert.ToInt32(hm[0]), Convert.ToInt32(hm[1]), 0);
            //        comando.fechainicio = model.fechaincidencia.Value.Date + ts;
            //        comando.fecharegistro = model.fecharegistro;
            //        comando.recurso = model.recurso;
            //        comando.documento = model.documento;
            //        comando.idmaestroincidencia = model.idmaestroincidencia;
            //        comando.idordentrabajo = item.idordentrabajo.Value;
            //        comando.usuarioregistro = model.usuarioregistro;
            //        comando.visible = model.visible;
            //        comando.Execute();

            //        //Actualiza estado de la orden

            //        if (entrega != null)
            //        {
            //            DataAccess.Seguimiento.SeguimientoData.ActualizarEstadoOT(item.idordentrabajo.Value
            //            , Convert.ToInt32(DataAccess.Constantes.EstadoOT.PendienteRetornoDocumentario));
            //        }

            //    }
            //    if (existemasivo == true)
            //        return -2;
            //    else return 1;
            //}
        }

    }
}