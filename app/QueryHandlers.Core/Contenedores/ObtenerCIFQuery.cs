
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.Contenedores.Parameters;
using QueryContracts.Core.Contenedores.Results;
using QueryHandlers.Common;
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using QueryHandlers.Common.Dapper;

namespace QueryHandlers.Core.Contenedores
{
    public class ObtenerCIFQuery : IQueryHandler<ObtenerCIFParameter>
    {
        public QueryResult Handle(ObtenerCIFParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("entidad", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.entidad);

                var resultado = connection.Query<ObtenerCIFResult>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "pa_traerCIFEntidad",
                            param: parametros
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}


/*
 
 
   public QueryResult Handle(ObtenerDatosBookingResumenPagoParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_str_numero_booking", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_numero_booking);
                parametros.Add("rbd_int_id_array", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rbd_int_id_array);


                var resultado = new ObtenerDatosBookingResumenPagoResult();
                var multiquery = connection.QueryMultiple
                    (
                        commandType: CommandType.StoredProcedure,
                        sql: "contenedor.pa_datos_booking_resumen_pago",
                        param: parametros
                    );

                resultado = multiquery.Read<ObtenerDatosBookingResumenPagoResult>().LastOrDefault();
                if (resultado != null)
                {
                    var collectionbookingdetalle = multiquery.Read<DatosBookingDetalleDto>().ToList<DatosBookingDetalleDto>();
                    resultado.booking_detalle = collectionbookingdetalle;

                    var collectionbookingdetalleservicioadicional = multiquery.Read<DatosBookingDetalleServicioAdicionalDto>().ToList<DatosBookingDetalleServicioAdicionalDto>();
                    resultado.booking_detalle_servicio_adicional = collectionbookingdetalleservicioadicional;
                }
                return resultado;
            }
        }
 
 */