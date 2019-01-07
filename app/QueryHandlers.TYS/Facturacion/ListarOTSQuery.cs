

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
    public class ListarOTSxIdsQuery : IQueryHandler<ListarOTSxIdsParameters>
    {

        public QueryResult Handle(ListarOTSxIdsParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("ots", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.ots);

                var resultado = new ListarOTSxIdsResult
                {
                    Hits = connection.Query<ListarOTSxIdsDto>
                        (
                            "facturacion.pa_listarots",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
