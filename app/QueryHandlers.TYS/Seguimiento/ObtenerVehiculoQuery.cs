
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
    public class ObtenerVehiculoQuery : IQueryHandler<ObtenerVehiculoParameter>
    {

        public QueryResult Handle(ObtenerVehiculoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idvehiculo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idvehiculo);

                var resultado = new ObtenerVehiculoResult();
                resultado = connection.Query<ObtenerVehiculoResult>
                        (
                            "seguimiento.pa_obtenervehiculo",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
