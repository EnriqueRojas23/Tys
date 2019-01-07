using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Monitoreo
{
    public class ListarMonitoreoOperacionesExportarQuery : IQueryHandler<ListarMonitoreoOperacionesExportarParameters>
    {

        public QueryResult Handle(ListarMonitoreoOperacionesExportarParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("fechainicio", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechainicio);
                parametros.Add("fechafin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechafin);
                parametros.Add("idruta", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idruta);
                parametros.Add("idestacion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idestacion);
                parametros.Add("guia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.guia);
                parametros.Add("chofer", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.chofer);
                parametros.Add("placa", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.placa);

                var resultado = new ListarMonitoreoOperacionesExportarResults
                {
                    Hits = connection.Query<ListarMonitoreoOperacionesExportarDto>
                    (
                       "monitoreo.pa_listarmonitoreooperaciones_exportar",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
