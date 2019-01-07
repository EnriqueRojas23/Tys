

using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Facturacion.Parameters;
using QueryContracts.TYS.Facturacion.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Facturacion
{
    public class ListarComprobanteDetallesQuery : IQueryHandler<ListarComprobanteDetallesParameters>
    {
        public QueryResult Handle(ListarComprobanteDetallesParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcomprobantepago", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcomprobante);

                var resultado = new ListarComprobanteDetallesResult
                {
                    Hits = connection.Query<ListarComprobanteDetallesDto>
                        (
                            "facturacion.pa_listardetallecomprobante",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };
                return resultado;
            }
        }
    }
}
