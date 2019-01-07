
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
    public class ObtenerClienteQuery : IQueryHandler<ObtenerClienteParameter>
    {

        public QueryResult Handle(ObtenerClienteParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.id);

                var resultado = new ObtenerClienteResult();
                resultado = connection.Query<ObtenerClienteResult>
                        (
                            "seguimiento.pa_obtenercliente",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
