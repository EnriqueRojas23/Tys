
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
    public class ObtenerUltimoManifiestoQuery : IQueryHandler<ObtenerUltimoManifiestoParameter>
    {

        public QueryResult Handle(ObtenerUltimoManifiestoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                //parametros.Add("idorden", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorden);

                var resultado = new ObtenerUltimoManifiestoResult();
                resultado = connection.Query<ObtenerUltimoManifiestoResult>
                        (
                            "seguimiento.pa_obtenerultimomanifiesto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
