
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
    public class ObtenerDespachoQuery : IQueryHandler<ObtenerDespachoParameters>
    {

        public QueryResult Handle(ObtenerDespachoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idvehiculo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idvehiculo);
                parametros.Add("idestado", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idestado);

                var resultado = new ObtenerDespachoResult();
                resultado = connection.Query<ObtenerDespachoResult>
                        (
                            "seguimiento.pa_obtenerdespacho",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
