
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
    public class ObtenerZonaQuery : IQueryHandler<ObtenerZonaParameter>
    {

        public QueryResult Handle(ObtenerZonaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idzona", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.id);

                var resultado = new ObtenerZonaResult();
                resultado = connection.Query<ObtenerZonaResult>
                        (
                            "seguimiento.pa_obtenerzona",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
