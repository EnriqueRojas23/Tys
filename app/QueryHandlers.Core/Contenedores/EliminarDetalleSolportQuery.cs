
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
    public class EliminarDetalleSolportQuery : IQueryHandler<EliminarDetalleSolportParameter>
    {
        public QueryResult Handle(EliminarDetalleSolportParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdReservaContenedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdDetalle);

                var resultado = new EliminarDetalleSolportResult();
                var multiquery = connection.QueryMultiple
                  (
                      commandType: CommandType.StoredProcedure,
                      sql: "usp_NOL_EliminarEspacioContenedor",
                      param: parametros
                  );

                return resultado;
            }
        }
    }
}
