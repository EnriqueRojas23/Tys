
using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.TYS.Ordenes
{
    public class ListarConsolidadorQuery : IQueryHandler<ListarConsolidadorParameter>
    {
        public QueryContracts.Common.QueryResult Handle(ListarConsolidadorParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("descrip", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.descripcion);
                var resultado = new ListarConsolidadorResult
                {
                    Hits = connection.Query<ListarConsolidadorDto>
                        (
                            "ordenes.pa_listarConsolidadores",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };
                return resultado;
            }
        }
    }
}