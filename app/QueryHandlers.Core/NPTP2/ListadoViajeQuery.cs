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
    public class ListadoViajeQuery : IQueryHandler<ListadoViajeParameter>
    {
        public QueryResult Handle(ListadoViajeParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserNPTP2Session())
            {
                var parametros = new DynamicParameters();
                parametros.Add("codnav08", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.codnav08);
                parametros.Add("numvia11", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numvia11);


                var resultado = new ListadoViajeResult
                {
                    Hits = connection.Query<ListadoViajeDto>
                        (
                            sql: "sp_nol_listar_viaje",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
