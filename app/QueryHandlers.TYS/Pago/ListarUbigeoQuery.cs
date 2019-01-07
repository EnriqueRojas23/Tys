
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
    public class ListarUbigeoQuery : IQueryHandler<ListarUbigeoParameters>
    {

        public QueryResult Handle(ListarUbigeoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ListarUbigeoResult
                {
                    Hits = connection.Query<ListarUbigeoDto>
                        (
                            "pago.pa_listarubigeo",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
