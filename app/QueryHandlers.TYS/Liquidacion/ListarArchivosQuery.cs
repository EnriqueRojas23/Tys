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
    public class ListarArchivosQuery : IQueryHandler<ListarArchivosParameters>
    {

        public QueryResult Handle(ListarArchivosParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idarchivo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idarchivo);
                parametros.Add("idordentrabajo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idordentrabajo);

                var resultado = new ListarArchivosResults
                {
                    Hits = connection.Query<ListarArchivosDto>
                    (
                       "liquidacion.pa_listararchivos",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
