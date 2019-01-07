

using Data.Common;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Contenedores
{
    public class InsertarBookingAuxHandler : IQueryHandler<InsertarBookingAuxParameter>
    {

        public QueryContracts.Common.QueryResult Handle(InsertarBookingAuxParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_int_identificador_terminal", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rb_int_identificador_terminal);
                parametros.Add("rb_str_cod_cliente_factura", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cod_cliente_factura);
                parametros.Add("rb_str_cliente_factura_descripcion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cliente_factura_descripcion);
                parametros.Add("rb_str_cod_consolidador", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cod_consolidador);
                parametros.Add("rb_str_consolidador_descripcion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_consolidador_descripcion);
                parametros.Add("rb_str_cod_operador_logistivo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cod_operador_logistivo);
                parametros.Add("rb_str_cod_operador_descripcion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cod_operador_descripcion);
                parametros.Add("rb_str_cod_agente_aduana", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cod_agente_aduana);
                parametros.Add("rb_str_cod_agente_aduana_descripcion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cod_agente_aduana_descripcion);
                parametros.Add("rb_str_cif_agente_aduana", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cif_agente_aduana);
                parametros.Add("rb_str_cif_agente_carga", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_cif_agente_carga);


                var resultado = connection.Query<InsertarBookingAuxResult>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_insertaraux",
                            param: parametros
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
