
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
    public class ListarTiposContenedoresQuery : IQueryHandler<ListarTiposContenedoresParameter>
    {
        public QueryResult Handle(ListarTiposContenedoresParameter parameters)
        {
            using (var connection = (SqlConnection) ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ListarTiposContenedoresResult
                {
                    Hits = connection.Query<ListarTiposContenedoresDto>
                        (
                            "sp_nol_listar_tipo_contenedores",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
