
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ListarDetalleRutaQuery : IQueryHandler<ListarDetalleRutaParameters>
    {

        public QueryResult Handle(ListarDetalleRutaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idruta", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idruta);

                var resultado = new ListarDetalleRutaResult
                {
                    Hits = connection.Query<ListarDetalleRutaDto>
                        (
                            "seguimiento.pa_listardetalleruta",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
