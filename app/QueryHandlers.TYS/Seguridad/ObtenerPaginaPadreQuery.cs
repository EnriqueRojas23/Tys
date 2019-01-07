

using Data.Common;
using QueryContracts.TYS.Seguridad.Parameters;
using QueryContracts.TYS.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Seguridad
{
    public class ObtenerPaginaPadreQuery : IQueryHandler<ObtenerPaginaPadreParameter>
    {


        public QueryContracts.Common.QueryResult Handle(ObtenerPaginaPadreParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idhijo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.pag_int_id);

                var resultado = new ObtenerPaginaPadreResult();
                resultado = connection.Query<ObtenerPaginaPadreResult>
                        (
                            "seguridad.pa_listarpadrepagina",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
