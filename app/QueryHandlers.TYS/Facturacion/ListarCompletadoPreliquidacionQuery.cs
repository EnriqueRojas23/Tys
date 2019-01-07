

using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Facturacion.Parameters;
using QueryContracts.TYS.Facturacion.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Facturacion
{
    public class ListarCompletadoPreliquidacionQuery : IQueryHandler<ListarCompletadoPreliquidacionParameters>
    {

        public QueryResult Handle(ListarCompletadoPreliquidacionParameters parameters)
        {                         
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idpreliquidacion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idpreliquidacion);

                var resultado = new ListarCompletadoPreliquidacionResult
                {
                    Hits = connection.Query<ListarCompletadoPreliquidacionDto>
                        (
                            "facturacion.pa_listarordentrabajopreliquidacion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
