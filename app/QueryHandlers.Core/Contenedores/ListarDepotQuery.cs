

using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.Contenedores.Parameters;
using QueryContracts.Core.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;

namespace QueryHandlers.Core.Contenedores
{
    public class ListarDepotQuery : IQueryHandler<ListarDepotParameter>
    {
        public QueryResult Handle(ListarDepotParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("id_oficina", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.IdOficina);
                
                var resultado = new ListarDepotResult
                {
                    Hits = connection.Query<ListarDepotDto>
                        (
                            "sp_nol_listar_oficina_almacenes",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
