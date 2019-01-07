
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
    public class ListarValorxTablaQuery : IQueryHandler<ListarValorxTablaParameters>
    {

        public QueryResult Handle(ListarValorxTablaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtabla", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtabla);
                parametros.Add("valor", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.search);

                var resultado = new ListarValorxTablaResult
                {
                    Hits = connection.Query<ListarValorxTablaDto>
                        (
                            "seguimiento.pa_listarvaloresportabla",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
