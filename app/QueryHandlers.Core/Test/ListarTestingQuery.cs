
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Terminal.Test.Parameters;
using QueryContracts.Terminal.Test.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Terminal.Test
{
    public class ListarTestingQuery : IQueryHandler<ListarTestingParameter>
    {
        public QueryResult Handle(ListarTestingParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ListarTestingResult();
                resultado.Hits = connection.Query<ListarTestingDto>
                        (
                            "pa_listar_testing",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        );
                return resultado;
            }
        }
    }
}
