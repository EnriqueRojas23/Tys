
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
    public class ListarFormulaTarifaQuery : IQueryHandler<ListarFormulaTarifaParameters>
    {

        public QueryResult Handle(ListarFormulaTarifaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idorigen", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigen);
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddepartamento);
                parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idprovincia);
                parametros.Add("iddistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddistrito);


                var resultado = new ListarFormulaTarifaResult
                {
                    Hits = connection.Query<ListarFormulaTarifaDto>
                        (
                            "seguimiento.pa_listarformulatarifas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
