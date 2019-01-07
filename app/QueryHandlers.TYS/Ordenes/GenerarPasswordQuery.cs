
using Data.Common;
using QueryContracts.TYS.Seguridad.Parameters;
using QueryContracts.TYS.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Ordenes
{
    public class GenerarPasswordQuery : IQueryHandler<GenerarPasswordParameter>
    {
        public QueryContracts.Common.QueryResult Handle(GenerarPasswordParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("usr_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdUsuario);

                var resultado = new GenerarPasswordResult();
                resultado = connection.Query<GenerarPasswordResult>
                        (
                            "seguridad.pa_regenerarapassusuario",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
