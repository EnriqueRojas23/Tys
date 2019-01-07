
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
    public class ObtenerDetalleRutaQuery : IQueryHandler<ObtenerDetalleRutaParameters>
    {

        public QueryResult Handle(ObtenerDetalleRutaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("iddetalleruta", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddetalleruta);

                var resultado = new ObtenerDetalleRutaResult();
                resultado = connection.Query<ObtenerDetalleRutaResult>
                        (
                            "seguimiento.pa_obtenerdetalleruta",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
