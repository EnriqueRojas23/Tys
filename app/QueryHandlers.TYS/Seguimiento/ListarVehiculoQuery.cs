
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
    public class ListarVehiculoQuery : IQueryHandler<ListarVehiculoParameters>
    {

        public QueryResult Handle(ListarVehiculoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("placa", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.placa);
                parametros.Add("idestado", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestado);

                var resultado = new ListarVehiculoResult
                {
                    Hits = connection.Query<ListarVehiculoDto>
                        (
                            "seguimiento.pa_listarvehiculo",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
