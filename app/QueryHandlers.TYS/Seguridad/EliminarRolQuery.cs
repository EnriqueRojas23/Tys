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
    public class EliminarRolQuery : IQueryHandler<EliminarRolParameter>
    {

        public QueryContracts.Common.QueryResult Handle(EliminarRolParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rol_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdRol);

                var resultado = new EliminarRolResult();
                resultado = connection.Query<EliminarRolResult>
                        (
                            "seguridad.pa_eliminarrol",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
