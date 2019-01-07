using System;
using System.Linq;
using CommandContracts.Common;
using CommandContracts.TYS.Contenedor;
using CommandContracts.TYS.Contenedor.Outputs;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.contenedor;
using Domain.TYS.contenedor.Exceptions;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.TYS.Contenedores;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Configuracion.Results;
using QueryHandlers.TYS.Configuracion;
using QueryContracts.TYS.Configuracion.Parameters;
using System.Collections.Generic;
using Componentes.Common.Extensions;

namespace CommandHandlers.TYS.Contenedor
{
    public class InsertarEspacioContenedorHandler : ICommandHandler<InsertarEspacioContenedorCommand>
    {
        private List<ListarMultiusoPorTipoDto> multiusoresult = null;

        private readonly string PROCESO = "M";  
        private readonly IRepository<ReservaBookingDetalle> _reservaBookingDetalle;

        public InsertarEspacioContenedorHandler(IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservaBookingDetalle = pReservaBookingDetalle;
            SetListaMensajeMultiuso(PROCESO);
        }

        public CommandResult Handle(InsertarEspacioContenedorCommand command)
        {
            //validando los parametros de entrada.
            if (command == null) throw new ArgumentNullException("Se requiere el command");

            var datbook = GetDatosBasicosBooking(command.rb_int_id);
            if (datbook.rb_int_espacios_disponibles == 0) throw new ReservaBookingException(GetMensaje("ERR_ESPACIO1"));
            if (datbook.rb_int_espacios_disponibles < command.rb_int_nro_contenedores) throw new ReservaBookingException(GetMensaje("ERR_ESPACIO2"));

            //long[] rbd_int_id_array = new long[command.rb_int_nro_contenedores];
            List<long> rbd_int_id_array = new List<long>();
            for (var i = 0; i < command.rb_int_nro_contenedores; i++)
            {
                var dominiobookdet = MapBookingDetalle(new ReservaBookingDetalle(), command);
                _reservaBookingDetalle.Add(dominiobookdet);
                 _reservaBookingDetalle.Commit();

                rbd_int_id_array.Add(dominiobookdet.rbd_int_id);
            }

            //actualizar estado del detalle

            var handler = new ActualizarEstadoReservaDetalleHandler(this._reservaBookingDetalle);
            var output = (ActualizarEstadoReservaDetalleOutput)handler.Handle(new ActualizarEstadoReservaDetalleCommand()
            {
                lst_rbd_int_id = rbd_int_id_array,
                rbd_str_estado_bookingdet = ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Registrado,
                UsuarioCreacion = command.rbd_str_usuario_creacion,
                DescripcionOpcional = "Reserva de espacio"
            });


            return new InsertarEspacioContenedorOutput() 
            {
                rb_int_espacios = command.rb_int_nro_contenedores, 
                rbd_int_id = rbd_int_id_array.ToArray(),
                rb_int_identificador_terminal = datbook.rb_int_identificador_terminal
            };
        }

        private static ReservaBookingDetalle MapBookingDetalle(ReservaBookingDetalle domain, InsertarEspacioContenedorCommand command)
        {
            domain.rb_int_id = command.rb_int_id;
            domain.rbd_dat_fecha_creacion = DateTime.Now;
            domain.rbd_int_tamanyo = command.rbd_int_tamanyo;
            domain.rbd_str_fecha_estimada = command.rbd_str_formato_fecha_estimada;
            domain.rbd_str_hora_estimada = command.rbd_str_formato_hora_estimada;
            //domain.rbd_str_fecha_estimada = command.rbd_str_fecha_estimada.ToString("yyyyMMdd");
            domain.rbd_str_humedad = command.rbd_str_humedad;
            domain.rbd_str_temperatura = command.rbd_str_temperatura;
            domain.rbd_str_tipo = command.rbd_str_tipo;
            domain.rbd_str_tipo_carga = command.rbd_str_tipo_carga;
            domain.rbd_str_unidad_temperatura = command.rbd_str_unidad_temperatura;
            domain.rbd_str_usuario_creacion = command.rbd_str_usuario_creacion;
            domain.rbd_str_ventilacion = command.rbd_str_ventilacion;
            domain.rbd_str_co2 = command.rbd_str_co2;
            domain.rbd_str_o2 = command.rbd_str_o2;
            
            domain.rbd_str_estado_bookingdet = "R";

            return domain;
        }

        public static ObtenerDatosBasicosBookingResult GetDatosBasicosBooking(long prb_int_id)
        {
            var query = new ObtenerDatosBasicosBookingQuery();
            var resultado = (ObtenerDatosBasicosBookingResult)query.Handle(new ObtenerDatosBasicosBookingParameter() { rb_int_id = prb_int_id });
            return resultado;
        }

        public void SetListaMensajeMultiuso(string pmlt_str_tipo)
        {
            var query = new ListarMultiusoPorTipoQuery();
            var result = (ListarMultiusoPorTipoResult)query.Handle(new ListarMultiusoPorTipoParameter() { mlt_str_tipo = pmlt_str_tipo });
            multiusoresult = result != null ? result.Hits.ToList() : new List<ListarMultiusoPorTipoDto>();
        }

        public string GetMensaje(string codigomensaje)
        {
            if (multiusoresult == null) return string.Empty;
            var objeto = multiusoresult.LastOrDefault(x => x.mlt_str_valor == codigomensaje);
            return objeto == null ? string.Empty : objeto.mlt_str_descripcion;
        }
    }
}



     