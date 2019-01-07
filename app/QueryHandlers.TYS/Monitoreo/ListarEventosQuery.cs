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
    public class ListarEventosQuery : IQueryHandler<ListarEventosParameters>
    {

        public QueryResult Handle(ListarEventosParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("idmaestroetapa", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idmaestroetapa);
                parametros.Add("idmaestroincidencia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idmaestroincidencia);
                parametros.Add("idorden", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idorden);

                var resultado = new ListarEventosResults
                {
                    Hits = connection.Query<ListarEventosDto>
                    (
                       "monitoreo.pa_listareventos",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
