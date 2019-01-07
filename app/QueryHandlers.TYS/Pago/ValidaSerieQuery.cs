
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
    public class ValidaSerieQuery : IQueryHandler<ValidaSerieParameters>
    {

        public QueryResult Handle(ValidaSerieParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("serie", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.serie);
                parametros.Add("idetapa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idetapa);
                parametros.Add("idtipocomprobante", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipocomprobante);
                parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedor);
                parametros.Add("idcomprobante", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcomprobante);

                var resultado = new ValidaSerieResult();
                resultado = connection.Query<ValidaSerieResult>
                        (
                            "pago.pa_validarserie",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
