
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
    public class EliminarPrecintoQuery : IQueryHandler<EliminarPrecintoParameter>
    {

        public QueryResult Handle(EliminarPrecintoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idprecinto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idprecinto);

                var resultado = new EliminarPrecintoResult();
                resultado = connection.Query<EliminarPrecintoResult>
                        (
                            "seguimiento.pa_eliminarprecinto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
