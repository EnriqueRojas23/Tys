
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Pago.Parameters;
using QueryContracts.TYS.Pago.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Pago
{
    public class ListarProveedorQuery : IQueryHandler<ListarProveedorParameters>
    {

        public QueryResult Handle(ListarProveedorParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.criterio);
                parametros.Add("activo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.activo);

                var resultado = new ListarProveedorResult
                {
                    Hits = connection.Query<ListarProveedorDto>
                        (
                            "pago.pa_listarproveedor",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
