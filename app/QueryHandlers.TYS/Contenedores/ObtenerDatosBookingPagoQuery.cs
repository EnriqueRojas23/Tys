using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Contenedores
{
    public class ObtenerDatosBookingPagoQuery : IQueryHandler<ObtenerDatosBookingPagoParameter>
    {
        public QueryResult Handle(ObtenerDatosBookingPagoParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_str_numero_booking", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_numero_booking);
                parametros.Add("rbp_int_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rbp_int_id);

                var resultado = connection.Query<ObtenerDatosBookingPagoResult>
                         (
                             commandType: CommandType.StoredProcedure,
                             sql: "contenedor.pa_datos_booking__pago",
                             param: parametros
                         ).LastOrDefault();

                return resultado;
            }
        }
    }
}
