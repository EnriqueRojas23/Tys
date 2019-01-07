using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceAgents.Common;
using Web.TYS.Areas.Liquidacion.Models;
using CommandContracts.TYS.Seguimiento;
using AutoMapper;
using CommandContracts.TYS.Seguimiento.Output;
using Web.TYS.Areas.Monitoreo.Models;
using QueryContracts.TYS.Liquidacion.Results;
using QueryContracts.TYS.Liquidacion.Parameters;

namespace Web.TYS.DataAccess.Liquidacion
{
    public class LiquidacionData
    {

        public static IEnumerable<ArchivoModel> GetListarArchivos(long? idordentrabajo, long? idarchivo)
        {
            var parametros = new ListarArchivosParameters
            {
                idordentrabajo = idordentrabajo,
                idarchivo = idarchivo,
            };
            var resultado = (ListarArchivosResults)parametros.Execute();

            Mapper.CreateMap<ListarArchivosDto,ArchivoModel >();
            return  Mapper.Map<IEnumerable<ListarArchivosDto>, IEnumerable<ArchivoModel>>(resultado.Hits);

        }
        public static List<ListarLiquidacionOperacionesDto> GetListarLiquidacionOperaciones(int? idcliente, int? iddestinatario, string numcp, string grr, string fechainicio, string fechafin, int? diastranscurridos)
        {
            var parametros = new ListarLiquidacionOperacionesParameters
            {
                fechainicio = fechainicio,
                fechafin = fechafin,
                idcliente = idcliente,
                iddestinatario = iddestinatario,
                grr = grr,
                numcp = numcp,
                diastranscurridos = diastranscurridos
            };
            var resultado = (ListarLiquidacionOperacionesResults)parametros.Execute();
            return resultado == null ? new List<ListarLiquidacionOperacionesDto>() : resultado.Hits.ToList();
        }

        public long InsertarArchivo(ArchivoModel model)
        {
              Mapper.CreateMap<ArchivoModel, InsertarArchivoCommand>();
              var command = Mapper.Map<ArchivoModel,InsertarArchivoCommand>(model);

              var resp = (InsertarArchivoOutput) command.Execute();
              return resp.idarchivo;
            

        }
        public static long InsertarIncidencia(IncidenciaModel model)
        {

                Mapper.CreateMap<IncidenciaModel, InsertarActualizarIncidenteCommand>();
                var comando = Mapper.Map<IncidenciaModel, InsertarActualizarIncidenteCommand>(model);


                var incidente = (InsertarActualizarIncidenteOutput)comando.Execute();
                return incidente.idincidencia;
           
        }


        internal static long ActualizarLiquidacion(LiquidacionModel model)
        {
            Mapper.CreateMap<LiquidacionModel, ActualizarLiquidacionCommand>();
            var command =   Mapper.Map<LiquidacionModel, ActualizarLiquidacionCommand>(model);

            var result =  (ActualizarLiquidacionOutput) command.Execute();
            return result.idordentrabajo;
        }

       public static IEnumerable<ListarDiasTranscurridosDto> GetListarDiasTranscurridos()
       {
           var parameters = new ListarDiasTranscurridosParameters { };
            var result =  (ListarDiasTranscurridosResults) parameters.Execute();
            return result.Hits;


       }
    }
}
