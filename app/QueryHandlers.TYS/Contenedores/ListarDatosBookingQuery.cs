

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
    public class ListarDatosBookingQuery : IQueryHandler<ListarDatosBookingParameter>
    {
        public QueryResult Handle(ListarDatosBookingParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_str_numero_booking", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_str_numero_booking);
                parametros.Add("rb_dat_fecha_reserva_inicio", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: parameters.rb_dat_fecha_reserva_inicio);
                parametros.Add("rb_dat_fecha_reserva_fin", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: parameters.rb_dat_fecha_reserva_fin);
                parametros.Add("mlt_str_estado_reserva", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.mlt_str_estado_reserva);
                
                //
                // --| Inicio
                // --| Autor         : MGZP 
                // --| Fecha         : 20/09/2016 03:37 p.m.
                // --| Requerimiento : ClientexUSuario
                // --| Motivo        : Pasar parametro de usuario para filtrar registros por usuario, Agente de aduana y agente de carga
                // --| 
                parametros.Add("sp_str_Usuario", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.qp_str_Usuario);
                //
                var resultado = new ListarDatosBookingResult
                {
                    Hits = connection.Query<ListarDatosBookingDto>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "contenedor.pa_listar_datos_booking",
                            //sql: "contenedor.pa_listar_datos_booking_mg",
                            param: parametros
                        )
                };
                // --| 
                // --| Fin
                //

                return resultado;
            }
        }
    }
}
