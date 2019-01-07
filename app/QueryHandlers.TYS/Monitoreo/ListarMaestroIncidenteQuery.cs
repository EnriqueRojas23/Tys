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
    public class ListarMaestroIncidenteQuery : IQueryHandler<ListarMaestroIncidenteParameters>
    {

        public QueryResult Handle(ListarMaestroIncidenteParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("tipoincidencia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.tipoincidencia);
                parametros.Add("idmaestroincidencia", dbType: DbType.Int16, direction: ParameterDirection.Input, value: parameters.idmaestroincidencia);

                var resultado = new ListarMaestroIncidenteResults
                {
                    Hits = connection.Query<ListarMaestroIncidenteDto>
                    (
                       "monitoreo.pa_listamaestroincidencia",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
