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
    public class ListarIncidentesQuery : IQueryHandler<ListarIncidentesParameters>
    {

        public QueryResult Handle(ListarIncidentesParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idorden", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idorden);
                parametros.Add("idmaestroincidencia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idmaestroincidencia);

                var resultado = new ListarIncidentesResults
                {
                    Hits = connection.Query<ListarIncidentesDto>
                    (
                       "monitoreo.pa_listardetallemonitoreo",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
