
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
    public class ListarRutaQuery : IQueryHandler<ListarRutaParameters>
    {

        public QueryResult Handle(ListarRutaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                //parametros.Add("idorigen", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigen);
                //parametros.Add("iddestino", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddestino);
                //parametros.Add("idruta", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idruta);
                parametros.Add("ruta", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.criterio);

                var resultado = new ListarRutaResult
                {
                    Hits = connection.Query<ListarRutaDto>
                        (
                            "seguimiento.pa_listarrutas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
