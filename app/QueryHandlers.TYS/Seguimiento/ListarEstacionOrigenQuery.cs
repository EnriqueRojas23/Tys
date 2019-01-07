
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
    public class ListarEstacionOrigenQuery : IQueryHandler<ListarEstacionOrigenParameters>
    {

        public QueryResult Handle(ListarEstacionOrigenParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ListarEstacionOrigenResult
                {
                    Hits = connection.Query<ListarEstacionOrigenDto>
                        (
                            "seguimiento.pa_listarestacionorigen",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
