using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.CRM.Parameters;
using QueryContracts.Core.CRM.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.Core.CRM
{
    public class ListarOrdenesTransporteQuery : IQueryHandler<ListarOrdenesTransporteParameters>
    {
        public QueryResult Handle(ListarOrdenesTransporteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("guia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.guia);
                parametros.Add("manifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.manifiesto);

                var resultado = new ListarOrdenesTransporteResult
                {
                    Hits = connection.Query<ListarOrdenesTransporteDto>
                        (
                            sql: "seguimiento.pa_listaroots",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}