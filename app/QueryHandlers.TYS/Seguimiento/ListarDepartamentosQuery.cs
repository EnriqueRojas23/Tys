
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
    public class ListarDepartamentosQuery : IQueryHandler<ListarDepartamentosParameters>
    {

        public QueryResult Handle(ListarDepartamentosParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();


                var resultado = new ListarDepartamentosResult
                {
                    Hits = connection.Query<ListarDepartamentosDto>
                        (
                            "seguimiento.pa_listardepartamento",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
