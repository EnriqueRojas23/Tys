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
    public class EliminarOrdenServicioQuery : IQueryHandler<EliminarOrdenServicioParameter>
    {


        public QueryContracts.Common.QueryResult Handle(EliminarOrdenServicioParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("ors", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nro);

                var resultado = new EliminarOrdenServicioResult();
                resultado = connection.Query<EliminarOrdenServicioResult>
                        (
                            "ordenes.pa_eliminarordenservicio",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
