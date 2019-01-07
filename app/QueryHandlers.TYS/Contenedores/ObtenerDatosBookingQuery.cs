
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
    public class ObtenerDatosBookingQuery : IQueryHandler<ObtenerDatosBookingParameter>
    {
        public QueryResult Handle(ObtenerDatosBookingParameter parameters)
        {
            if(parameters == null) throw new ArgumentException("Se requiere el parametro");

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("numerobooking", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NumeroBooking);

                var resultado = new ObtenerDatosBookingResult();
                var multiquery = connection.QueryMultiple
                    (
                        commandType: CommandType.StoredProcedure,
                        sql: "contenedor.pa_obtener_datos_booking",
                        param: parametros
                    );


                resultado = multiquery.Read<ObtenerDatosBookingResult>().LastOrDefault();
                if (resultado != null)
                {
                    var collectiondocumentos = multiquery.Read<ObtenerBookingDocumentoDto>().ToList<ObtenerBookingDocumentoDto>();
                    resultado.Documentos = collectiondocumentos;
                }
                return resultado;
            }

        }
    }
}
