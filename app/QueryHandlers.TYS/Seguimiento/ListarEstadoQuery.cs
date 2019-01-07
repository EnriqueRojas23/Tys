
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
    public class ListarEstadoQuery : IQueryHandler<ListarEstadoParameters>
    {

        public QueryResult Handle(ListarEstadoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtabla", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idtabla);

                var resultado = new ListarEstadoResult
                {
                    Hits = connection.Query<ListarEstadoDto>
                        (
                            "seguimiento.pa_listarestados",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
