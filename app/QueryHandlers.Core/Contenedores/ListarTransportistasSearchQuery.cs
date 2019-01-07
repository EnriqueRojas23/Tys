
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
    public class ListarTransportistasSearchQuery : IQueryHandler<ListarTransportistasSearchParameter>
    {
        public QueryResult Handle(ListarTransportistasSearchParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("coditem", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CodigoItem);
                parametros.Add("desitem", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.DescripcionItem);
                parametros.Add("tipitem", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.TipoItem);
                
                var resultado = new ListarTransportistasSearchResult
                {
                    Hits = connection.Query<ListarTransportistasSearchDto>
                        (
                            sql: "sp_nol_listar_transportistas_search",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
