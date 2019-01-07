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
    public class ListarOrdenesTrabajoQuery : IQueryHandler<ListarOrdenesTrabajoParameters>
    {

        public QueryResult Handle(ListarOrdenesTrabajoParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);

                var resultado = new ListarOrdenesTrabajoResults
                {
                    Hits = connection.Query<ListarOrdenesTrabajoDto>
                    (
                       "monitoreo.pa_listarordentrabajo",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
