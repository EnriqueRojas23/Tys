using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Terminal.Contenedores.Parameters;
using QueryContracts.Terminal.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;

namespace QueryHandlers.Terminal.Contenedores
{
    public class ListarEntidadesBookingQuery : IQueryHandler<ListarEntidadesBookingParameter>
    {
        public QueryResult Handle(ListarEntidadesBookingParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_str_codigo_cliente", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_codigo_cliente);
                parametros.Add("rb_str_codigo_agente_carga", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_codigo_agente_carga);
                parametros.Add("rb_str_codigo_buque", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_codigo_buque);

                var resultado = new ListarEntidadesBookingResult
                {
                    Hits = connection.Query<ListarEntidadesBookingDto>
                        (
                            sql: "sp_nol_listar_entidad_cli_agen_buque",
                            param: parametros,
                            commandType: CommandType.StoredProcedure
                        )
                };
                return resultado;
            }
        }
    }
}
