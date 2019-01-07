
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ListarChoferQuery : IQueryHandler<ListarChoferParameters>
    {

        public QueryResult Handle(ListarChoferParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.criterio);

                var resultado = new ListarChoferResult
                {
                    Hits = connection.Query<ListarChoferDto>
                        (
                            "seguimiento.pa_listarchofer",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
