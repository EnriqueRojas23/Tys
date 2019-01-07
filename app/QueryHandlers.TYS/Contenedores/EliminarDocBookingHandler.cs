

using Data.Common;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Contenedores
{
    public class EliminarDocBookingHandler : IQueryHandler<EliminarDocBookingParameter>
    {

        public QueryContracts.Common.QueryResult Handle(EliminarDocBookingParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rba_int_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rba_int_id);

                var resultado = connection.Query<EliminarDocBookingResult>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_eliminardocumentobooking",
                            param: parametros
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
