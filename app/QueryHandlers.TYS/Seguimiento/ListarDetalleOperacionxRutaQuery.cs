
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
    public class ListarDetalleOperacionxRutaQuery : IQueryHandler<ListarDetalleOperacionxRutaParameters>
    {

        public QueryResult Handle(ListarDetalleOperacionxRutaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idruta", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idruta);
                parametros.Add("idorigen", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigen);
                parametros.Add("iddistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddistrito);
                parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idprovincia);
                parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddepartamento);

                var resultado = new ListarDetalleOperacionxRutaResult
                {
                    Hits = connection.Query<ListarDetalleOperacionxRutaDto>
                        (
                            "seguimiento.pa_listardetallerutaxdestino",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}

