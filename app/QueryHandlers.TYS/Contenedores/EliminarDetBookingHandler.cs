

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
    public class EliminarDetBookingHandler : IQueryHandler<EliminarDetBookingParameter>
    {

        public QueryContracts.Common.QueryResult Handle(EliminarDetBookingParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rbd_int_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rbd_int_id);

                var resultado = connection.Query<EliminarDetBookingResult>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_eliminardetallebooking",
                            param: parametros
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
