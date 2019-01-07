using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using QueryContracts.Core.SGCW.Parameters;
using QueryContracts.Core.SGCW.Results;


namespace QueryHandlers.Core.SGCW
{
    public class ObtenerServicioGateOutQuery : IQueryHandler<ObtenerServicioGateOutParameter>
    {
        public QueryResult Handle(ObtenerServicioGateOutParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSGCWSession())
            {
                var parametros = new DynamicParameters();
                var resultado = new ObtenerServicioGateOutResult
                {
                    Hits = connection.Query<ObtenerServicioGateOutDto>
                        (
                            sql: "FAC_ObtenerServicioGateOut",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
