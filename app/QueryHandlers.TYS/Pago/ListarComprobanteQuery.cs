
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Pago.Parameters;
using QueryContracts.TYS.Pago.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Pago
{
    public class ListarComprobanteQuery : IQueryHandler<ListarComprobanteParameters>
    {

        public QueryResult Handle(ListarComprobanteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("serie", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.serie);
                parametros.Add("fechaini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechaini);
                parametros.Add("fechafin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechafin);

                var resultado = new ListarComprobanteResult
                {
                    Hits = connection.Query<ListarComprobanteDto>
                        (
                            "pago.pa_listarcomprobantes",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
