
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
    public class ListarPrecintosTablasQuery : IQueryHandler<ListarPrecintosParameters>
    {

        public QueryResult Handle(ListarPrecintosParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("iddespacho", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddespacho);
                var resultado = new ListarPrecintosResult
                {
                    Hits = connection.Query<ListarPrecintosDto>
                        (
                            "seguimiento.pa_listarprecintos",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
