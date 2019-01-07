
using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.Contenedores.Parameters;
using QueryContracts.Core.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;

namespace QueryHandlers.Core.Contenedores
{
    public class ActualizarPagoQuery : IQueryHandler<ActualizarPagoParameter>
    {
        public QueryResult Handle(ActualizarPagoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdReserva", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdCabecera);
                parametros.Add("IdReservaContenedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdDetalle);

                var resultado = new ActualizarPagoResult();
                var multiquery = connection.QueryMultiple
                  (
                      commandType: CommandType.StoredProcedure,
                      sql: "usp_NOL_PagarEspacioReserva",
                      param: parametros
                  );

                return resultado;
            }
        }
    }
}
