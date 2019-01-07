
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
    public class ObtenerUltimaOperacionCargaQuery : IQueryHandler<ObtenerUltimaOperacionCargaParameter>
    {

        public QueryResult Handle(ObtenerUltimaOperacionCargaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                //parametros.Add("idorden", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorden);

                var resultado = new ObtenerUltimaOperacionCargaResult();
                resultado = connection.Query<ObtenerUltimaOperacionCargaResult>
                        (
                            "seguimiento.pa_obtenerultimaoperacioncarga",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
