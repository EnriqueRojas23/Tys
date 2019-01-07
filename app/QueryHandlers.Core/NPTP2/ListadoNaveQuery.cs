using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using QueryContracts.Core.NPTP2.Parameters;
using QueryContracts.Core.NPTP2.Results;

namespace QueryHandlers.Core.NPTP2
{
    public class ListadoNaveQuery : IQueryHandler<ListadoNaveParameter>
    {
        public QueryResult Handle(ListadoNaveParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserNPTP2Session())
            {
                var parametros = new DynamicParameters();
                parametros.Add("desnave08", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.desnave08);

                var resultado = new ListadoNaveResult
                {
                    Hits = connection.Query<ListadoNaveDto>
                        (
                            sql: "sp_nol_listar_nave",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
