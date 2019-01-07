
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ObtenerOperacionCargaTotalVehiculoQuery : IQueryHandler<ObtenerOperacionCargaTotalVehiculoParameters>
    {

        public QueryResult Handle(ObtenerOperacionCargaTotalVehiculoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcarga", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idcarga);

                var resultado = new ObtenerOperacionCargaTotalVehiculoResult();
                resultado = connection.Query<ObtenerOperacionCargaTotalVehiculoResult>
                        (
                            "seguimiento.pa_obteneroperacioncarga_vehiculo",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
