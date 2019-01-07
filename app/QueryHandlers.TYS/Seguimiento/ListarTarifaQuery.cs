
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
    public class ListarTarifaQuery : IQueryHandler<ListarTarifaParameters>
    {

        public QueryResult Handle(ListarTarifaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);

                var resultado = new ListarTarifaResult
                {
                    Hits = connection.Query<ListarTarifaDto>
                        (
                            "seguimiento.pa_listartarifas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
