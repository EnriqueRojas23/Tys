
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
    public class ListarOperacionCargaVehiculoQuery : IQueryHandler<ListarOperacionCargaVehiculoParameters>
    {

        public QueryResult Handle(ListarOperacionCargaVehiculoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idoperacion", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idcarga);
                parametros.Add("idestado", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestado);

                var resultado = new ListarOperacionCargaVehiculoResult
                {
                    Hits = connection.Query<ListarOperacionCargaVehiculoDto>
                        (
                            "seguimiento.pa_listaroperacionescargavehiculo",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}

