
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
    public class ListarDetalleTarifaIdsQuery : IQueryHandler<ListarDetalleTarifaIdsParameters>
    {

        public QueryResult Handle(ListarDetalleTarifaIdsParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtarifa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtarifa);

                var resultado = new ListarDetalleTarifaIdsResult
                {
                    Hits = connection.Query<ListarDetalleTarifaIdsDto>
                        (
                            "seguimiento.pa_listardetalletarifaids",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
