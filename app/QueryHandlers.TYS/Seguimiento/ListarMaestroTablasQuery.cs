
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
    public class ListarMaestroTablasQuery : IQueryHandler<ListarMaestroTablasParameters>
    {

        public QueryResult Handle(ListarMaestroTablasParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ListarMaestroTablasResult
                {
                    Hits = connection.Query<ListarMaestroTablasDto>
                        (
                            "seguimiento.pa_listarmaestrotabla",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
