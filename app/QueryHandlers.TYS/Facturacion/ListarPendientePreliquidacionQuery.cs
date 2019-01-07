

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
    public class ListarPendientePreliquidacionQuery : IQueryHandler<ListarPendientePreliquidacionParameters>
    {

        public QueryResult Handle(ListarPendientePreliquidacionParameters parameters)
        {                         
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddestino", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddestino);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);

                var resultado = new ListarPendientePreliquidacionResult
                {
                    Hits = connection.Query<ListarPendientePreliquidacionDto>
                        (
                            "facturacion.pa_listarpendientepreliquidacion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
