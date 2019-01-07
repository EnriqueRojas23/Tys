
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
    public class ListarDetalleTarifaQuery : IQueryHandler<ListarDetalleTarifaParameters>
    {

        public QueryResult Handle(ListarDetalleTarifaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtarifa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtarifa);

                var resultado = new ListarDetalleTarifaResult
                {
                    Hits = connection.Query<ListarDetalleTarifaDto>
                        (
                            "seguimiento.pa_listardetalletarifa",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
