
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
    public class ListarExportarAsignacionQuery : IQueryHandler<ListarExportarAsignacionParameters>
    {

        public QueryResult Handle(ListarExportarAsignacionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.criterio);

                var resultado = new ListarExportarAsignacionResult
                {
                    Hits = connection.Query<ListarExportarAsignacionDto>
                        (
                            "pago.pa_listarasignacion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
