
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
    public class ListarProvinciasQuery : IQueryHandler<ListarProvinciasParameters>
    {

        public QueryResult Handle(ListarProvinciasParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddepartamento);

                var resultado = new ListarProvinciasResult
                {
                    Hits = connection.Query<ListarProvinciasDto>
                        (
                            "seguimiento.pa_listarprovincia",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
