
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
    public class ValidaOtsQuery : IQueryHandler<ValidaOtsParameters>
    {

        public QueryResult Handle(ValidaOtsParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.id);
                parametros.Add("idetapa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idetapa);

                var resultado = new ValidaOtsResult();
                resultado = connection.Query<ValidaOtsResult>
                        (
                            "pago.pa_validarots",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
