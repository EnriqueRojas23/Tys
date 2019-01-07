
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Pago.Parameters;
using QueryContracts.TYS.Pago.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Pago
{
    public class EliminarAsignacionQuery : IQueryHandler<EliminarAsignacionParameter>
    {

        public QueryResult Handle(EliminarAsignacionParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idasignacion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idasignacion);

                var resultado = new EliminarAsignacionResult();
                resultado = connection.Query<EliminarAsignacionResult>
                        (
                            "pago.pa_eliminarasignacion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
