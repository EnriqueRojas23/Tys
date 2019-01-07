
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
    public class ObtenerManifiestoQuery : IQueryHandler<ObtenerManifiestoParameter>
    {

        public QueryResult Handle(ObtenerManifiestoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idmanifiesto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idmanifiesto);

                var resultado = new ObtenerManifiestoResult();
                resultado = connection.Query<ObtenerManifiestoResult>
                        (
                            "seguimiento.pa_obtenermanfiesto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
