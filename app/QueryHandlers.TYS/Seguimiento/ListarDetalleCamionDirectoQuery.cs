
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
    public class ListarDetalleCamionDirectoQuery : IQueryHandler<ListarDetalleCamionDirectoParameters>
    {

        public QueryResult Handle(ListarDetalleCamionDirectoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcamioncamiondirecto", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcamiondirecto);

                var resultado = new ListarDetalleCamionDirectoResult
                {
                    Hits = connection.Query<ListarDetalleCamionDirectoDto>
                        (
                            "seguimiento.pa_listardetalleCamionDirecto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
