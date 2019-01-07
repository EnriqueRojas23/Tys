

using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Ordenes
{
    public class ListarServiciosQuery : IQueryHandler<ListarServiciosParameter>
    {
        public QueryContracts.Common.QueryResult Handle(ListarServiciosParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("descripcion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Descripcion);
                var resultado = new ListarServiciosResult
                {
                    Hits = connection.Query<ListarServiciosDto>
                        (
                            "ordenes.pa_ListarServicios",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
