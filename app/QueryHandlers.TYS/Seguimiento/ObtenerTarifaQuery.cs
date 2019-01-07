
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
    public class ObtenerTarifaQuery : IQueryHandler<ObtenerTarifaParameter>
    {

        public QueryResult Handle(ObtenerTarifaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtarifa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtarifa);

                var resultado = new ObtenerTarifaResult();
                resultado = connection.Query<ObtenerTarifaResult>
                        (
                            "seguimiento.pa_obtenertarifa",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
