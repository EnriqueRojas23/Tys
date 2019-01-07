
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
    public class ListarDistritosQuery : IQueryHandler<ListarDistritosParameters>
    {

        public QueryResult Handle(ListarDistritosParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idprovincia);
                var resultado = new ListarDistritosResult
                {
                    Hits = connection.Query<ListarDistritosDto>
                        (
                            "seguimiento.pa_listardistritos",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
