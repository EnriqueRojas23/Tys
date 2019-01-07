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
    public class ListarSubProductosQuery : IQueryHandler<ListarSubProductosParameter>
    {
        public QueryResult Handle(ListarSubProductosParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdProducto", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdProducto);

                var resultado = new ListarSubProductosResult
                {
                    Hits = connection.Query<ListarSubProductosDto>
                        (
                            sql: "sp_nol_listar_subproductos",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
