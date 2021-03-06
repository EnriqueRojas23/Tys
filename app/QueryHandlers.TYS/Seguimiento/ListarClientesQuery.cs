﻿
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ListarClientesQuery : IQueryHandler<ListarClientesParameters>
    {

        public QueryResult Handle(ListarClientesParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.criterio);
                parametros.Add("activo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.activo);

                var resultado = new ListarClientesResult
                {
                    Hits = connection.Query<ListarClientesDto>
                        (
                            "seguimiento.pa_listarclientes",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
