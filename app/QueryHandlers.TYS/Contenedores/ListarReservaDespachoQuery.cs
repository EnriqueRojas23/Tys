using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.TYS.Contenedores
{
    public class ListarReservaDespachoQuery : IQueryHandler<ListarDespachoReservaParameter>
    {
        public QueryResult Handle(ListarDespachoReservaParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_str_numero_booking", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_numero_booking);

                var resultado = new ListarDespachoReservaResult
                {
                    Hits = connection.Query<ListarDespachoReservaDto>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_listar_despacho_reservas",
                            param: parametros
                        )
                };

                return resultado;
            }
        }
    }
}
