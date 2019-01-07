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
    public class ListarHelpResourcesQuery : IQueryHandler<ListarHelpResourcesParameters>
    {

        public QueryResult Handle(ListarHelpResourcesParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcampo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcampo);

                var resultado = new ListarHelpResourcesResults
                {
                    Hits = connection.Query<ListarHelpResourcesDto>
                    (
                       "monitoreo.pa_listarhelpresources",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
