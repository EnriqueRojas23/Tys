using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Ordenes
{
    public class ListarViajeQuery : IQueryHandler<ListarViajeParameter>
    {
        public QueryContracts.Common.QueryResult Handle(ListarViajeParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("descrip", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.descripcion);
                parametros.Add("nave", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nave);
                var resultado = new ListarViajeResult
                {
                    Hits = connection.Query<ListarViajeDto>
                        (
                            "ordenes.pa_listarViajes",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };
                return resultado;
            }
        }
    }
}
