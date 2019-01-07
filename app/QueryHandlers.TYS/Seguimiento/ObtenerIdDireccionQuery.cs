
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
    public class ObtenerIdDireccionQuery : IQueryHandler<ObtenerIdDireccionParameters>
    {

        public QueryResult Handle(ObtenerIdDireccionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("direccion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.direccion);

                var resultado = new ObtenerIdDireccionResult();
                resultado = connection.Query<ObtenerIdDireccionResult>
                        (
                            "seguimiento.pa_traeriddireccion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
