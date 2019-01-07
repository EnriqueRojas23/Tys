


using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Ordenes
{
    public class ListarNaveQuery : IQueryHandler<ListarNavesParameter>
    {

        public QueryContracts.Common.QueryResult Handle(ListarNavesParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("descrip", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.descripcion);
                var resultado = new ListarNaveResult
                {
                    Hits = connection.Query<ListarNaveDto>
                        (
                            "ordenes.pa_listarNaves",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
