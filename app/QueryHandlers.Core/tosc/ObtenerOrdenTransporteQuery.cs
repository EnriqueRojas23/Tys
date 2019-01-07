

using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.CRM.Parameters;
using QueryContracts.Core.CRM.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.Core.CRM
{
    public class ObtenerOrdenTransporteQuery : IQueryHandler<ObtenerOrdenTransporteParameters>
    {
        public QueryResult Handle(ObtenerOrdenTransporteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserCRMSession())
            {
                var parametros = new DynamicParameters();

                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);

                var resultado = new ObtenerOrdenTransporteResult();
                resultado = connection.Query<ObtenerOrdenTransporteResult>
                    (
                        sql: "pa_obtenerot",
                        param: parametros,
                        commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                return resultado;
            }
        }
    }
}
