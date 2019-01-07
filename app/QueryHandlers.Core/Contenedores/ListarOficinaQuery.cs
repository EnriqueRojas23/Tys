

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
    public class ListarOficinaQuery : IQueryHandler<ListarOficinaParameter>
    {
        public QueryResult Handle(ListarOficinaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ListarOficinaResult
                {
                    Hits = connection.Query<ListarOficinaDto>
                        (
                            "sp_nol_listar_oficina",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
