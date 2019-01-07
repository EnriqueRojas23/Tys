

using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Ordenes
{
    public class ListarDespachadorQuery : IQueryHandler<ListarDespachadorParameter>
    {
        public QueryContracts.Common.QueryResult Handle(ListarDespachadorParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
//                parametros.Add("descrip", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters..);
                var resultado = new ListarDespachadorResult();
                resultado = connection.Query<ListarDespachadorResult>
                        (
                            "ordenes.pa_listarDespachador",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
