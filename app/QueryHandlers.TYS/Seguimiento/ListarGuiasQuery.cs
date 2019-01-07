
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
    public class ListarGuiasQuery : IQueryHandler<ListarGuiasParameters>
    {

        public QueryResult Handle(ListarGuiasParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idorden", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idorden);

                var resultado = new ListarGuiasResult
                {
                    Hits = connection.Query<ListarGuiasDto>
                        (
                            "seguimiento.pa_listarguias",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
