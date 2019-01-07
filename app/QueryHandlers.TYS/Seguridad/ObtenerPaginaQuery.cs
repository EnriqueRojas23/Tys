

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
    public class ObtenerPaginaQuery : IQueryHandler<ObtenerPaginaParameter>
    {


        public QueryContracts.Common.QueryResult Handle(ObtenerPaginaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pag_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.pag_int_id);

                var resultado = new ObtenerPaginaResult();
                resultado = connection.Query<ObtenerPaginaResult>
                        (
                            "seguridad.pa_obtenerpagina",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
