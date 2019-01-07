

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
    public class ListarTamanyoContenedoresQuery  : IQueryHandler<ListarTamanyoContenedoresParameter>
    {
        public QueryResult Handle(ListarTamanyoContenedoresParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ListarTamanyoContenedoresResult
                {
                    Hits = connection.Query<ListarTamanyoContenedoresDto>
                        (
                            "sp_nol_listar_tamanyo_contenedores",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
