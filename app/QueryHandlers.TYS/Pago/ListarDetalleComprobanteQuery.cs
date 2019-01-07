
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
    public class ListarDetalleComprobanteQuery : IQueryHandler<ListarDetalleComprobanteParameters>
    {

        public QueryResult Handle(ListarDetalleComprobanteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("id", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idcomprobante);

                var resultado = new ListarDetalleComprobanteResult
                {
                    Hits = connection.Query<ListarDetalleComprobanteDto>
                        (
                            "pago.pa_listardetallecomprobantes",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
