using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using QueryContracts.Core.SGCW.Parameters;
using QueryContracts.Core.SGCW.Results;

namespace QueryHandlers.Core.SGCW
{
    public class ListarProductosQuery : IQueryHandler<ListarProductosParameter>
    {
        public QueryResult Handle(ListarProductosParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                var resultado = new ListarProductosResult
                {
                    Hits = connection.Query<ListarProductosDto>
                        (
                            sql: "sp_nol_listar_productos",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
