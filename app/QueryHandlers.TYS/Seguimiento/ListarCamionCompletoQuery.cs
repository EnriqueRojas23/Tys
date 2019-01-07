
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
    public class ListarCamionCompletoQuery : IQueryHandler<ListarCamionCompletoParameters>
    {

        public QueryResult Handle(ListarCamionCompletoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("codigo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.codigo);
                parametros.Add("iddestino", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.iddestino);
                parametros.Add("idestacion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idestacion);

                var resultado = new ListarCamionCompletoResult
                {
                    Hits = connection.Query<ListarCamionCompletoDto>
                        (
                            "seguimiento.pa_listarcamioncompleto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
