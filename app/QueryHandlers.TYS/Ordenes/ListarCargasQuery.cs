using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Ordenes
{
    public class ListarCargasQuery : IQueryHandler<ListarCargasParameters>
    {
        public QueryResult Handle(ListarCargasParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("navvia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.navvia11);
                parametros.Add("nrodet", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nrodet12);
                var resultado = new ListarCargasResult
                {
                    Hits = connection.Query<ListarCargasDto>
                        (
                            "ordenes.listar_cargas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };
                return resultado;
            }
        }
    }
}
