
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
    public class ObtenerDireccionQuery : IQueryHandler<ObtenerDireccionParameters>
    {

        public QueryResult Handle(ObtenerDireccionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("iddireccion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddireccion);

                var resultado = new ObtenerDireccionResult();
                resultado = connection.Query<ObtenerDireccionResult>
                        (
                            "seguimiento.pa_obtenerdireccion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
