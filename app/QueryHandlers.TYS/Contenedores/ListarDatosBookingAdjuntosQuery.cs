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
    public class ListarDatosBookingAdjuntosQuery : IQueryHandler<ListarDatosBookingAdjuntosParameter>
    {
        public QueryResult Handle(ListarDatosBookingAdjuntosParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_int_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rb_int_id);

                var resultado = new ListarDatosBookingAdjuntosResult
                {
                    Hits = connection.Query<ListarDatosBookingAdjuntosDto>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_listar_booking_detalle_adjuntos",
                            param: parametros
                        )
                };

                return resultado;
            }
        }
    }
}
