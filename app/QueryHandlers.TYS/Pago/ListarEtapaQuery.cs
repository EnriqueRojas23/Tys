
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
    public class ListarEtapaQuery : IQueryHandler<ListarEtapaParameters>
    {

        public QueryResult Handle(ListarEtapaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.criterio);
                parametros.Add("activo", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: parameters.activo);

                var resultado = new ListarEtapaResult
                {
                    Hits = connection.Query<ListarEtapaDto>
                        (
                            "pago.pa_listaretapa",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
