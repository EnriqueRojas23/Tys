

using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Ordenes
{
    public class ListarContenedorQuery :IQueryHandler<ListarContenedorParameter>
    {

        public QueryContracts.Common.QueryResult Handle(ListarContenedorParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("navvia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.navvia);
                parametros.Add("sRucFwd", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.conso);
                parametros.Add("conte", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.conte);
                var resultado = new ListarContenedorResult
                {
                    Hits = connection.Query<ListarContenedorDto>
                        (
                            "ordenes.pa_listarContenedor",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };
                return resultado;
            }
        }
    }
}
