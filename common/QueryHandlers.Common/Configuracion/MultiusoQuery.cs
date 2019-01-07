using Data.Common;
using QueryContracts.Common;
using QueryContracts.Common.Configuracion.Parameters;
using QueryContracts.Common.Configuracion.Results;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.Common.Configuracion
{
    public class MultiusoQuery : IQueryHandler<MultiusoParameter>
    {
        public QueryResult Handle(MultiusoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var result = new MultiusoResult();
                result.Hits = connection.Query<MultiusoDto>("configuracion.pa_listar_multiuso", parametros, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
