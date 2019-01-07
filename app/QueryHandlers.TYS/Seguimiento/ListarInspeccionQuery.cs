
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
    public class ListarInspeccionQuery : IQueryHandler<ListarInspeccionParameters>
    {

        public QueryResult Handle(ListarInspeccionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                //parametros.Add("idvehiculo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idvehiculo);

                var resultado = new ListarInspeccionResult
                {
                    Hits = connection.Query<ListarInspeccionDto>
                        (
                            "seguimiento.pa_listarinspeccion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
