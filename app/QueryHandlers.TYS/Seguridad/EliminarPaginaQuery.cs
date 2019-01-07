
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Account.Parameters;
using QueryContracts.TYS.Account.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


using QueryContracts.TYS.Seguridad.Parameters;
using QueryHandlers.Common;
using QueryContracts.TYS.Seguridad.Result;


namespace QueryHandlers.TYS.Seguridad
{
    public class EliminarPaginaQuery : IQueryHandler<EliminarPaginaParameter>
    {
        public QueryContracts.Common.QueryResult Handle(EliminarPaginaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pag_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdPagina);

                var resultado = new EliminarPaginaResult();
                resultado = connection.Query<EliminarPaginaResult>
                        (
                            "seguridad.pa_eliminarPagina",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
