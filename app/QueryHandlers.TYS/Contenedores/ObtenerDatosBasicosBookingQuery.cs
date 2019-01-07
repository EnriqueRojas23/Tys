using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;

namespace QueryHandlers.TYS.Contenedores
{
    public class ObtenerDatosBasicosBookingQuery : IQueryHandler<ObtenerDatosBasicosBookingParameter>
    {
        public QueryResult Handle(ObtenerDatosBasicosBookingParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_int_id", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rb_int_id);

                var resultado = connection.Query<ObtenerDatosBasicosBookingResult>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_datos_basicos_booking",
                            param: parametros
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
