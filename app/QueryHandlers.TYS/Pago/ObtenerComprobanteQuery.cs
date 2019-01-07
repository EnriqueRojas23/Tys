
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
    public class ObtenerComprobanteQuery : IQueryHandler<ObtenerComprobanteParameter>
    {

        public QueryResult Handle(ObtenerComprobanteParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.id);

                var resultado = new ObtenerComprobanteResult();
                resultado = connection.Query<ObtenerComprobanteResult>
                        (
                            "pago.pa_obtenercomprobante",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
