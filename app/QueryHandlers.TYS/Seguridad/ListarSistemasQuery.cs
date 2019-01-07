using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguridad.Parameters;
using QueryContracts.TYS.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Seguridad
{
    public class ListarSistemasQuery : IQueryHandler<ListarSistemasParameter>
    {
        public QueryResult Handle(ListarSistemasParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("psis_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.sis_int_id);

                var result = new ListarSistemasResult();
                result.Hits = connection.Query<ListarSistemasDto>(
                                    "seguridad.pa_listar_sistemas",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }
    }
}
