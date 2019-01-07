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
    public class ObtenerDatosBookingDespachoQuery : IQueryHandler<ObtenerDatosBookingDespachoParameter>
    {
        public QueryResult Handle(ObtenerDatosBookingDespachoParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_str_numero_booking", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_numero_booking);
                parametros.Add("rbd_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.rbd_int_id);

                var resultado = new ObtenerDatosBookingDespachoResult
                {
                    Hits = connection.Query<ObtenerDatosBookingDespachoDto>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_listar_booking_detalle_despacho",
                            param: parametros
                        )
                };

                return resultado;
            }
        }
    }
}
