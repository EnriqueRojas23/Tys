
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
    public class ListarDistritoZonaQuery : IQueryHandler<ListarDistritoZonaParameters>
    {

        public QueryResult Handle(ListarDistritoZonaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idzona", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idzona);

                var resultado = new ListarDistritoZonaResult
                {
                    Hits = connection.Query<ListarDistritoZonaDto>
                        (
                            "seguimiento.pa_listardistritoszonas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
