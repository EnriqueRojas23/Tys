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
    public class ActualizarEstadoReservaDetalleHandler : ICommandHandler<ActualizarEstadoReservaDetalleCommand>
    {
        private readonly IRepository<ReservaBookingDetalle> _reservaBookingDetalle;

        public ActualizarEstadoReservaDetalleHandler(IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservaBookingDetalle = pReservaBookingDetalle;
        }

        public CommandResult Handle(ActualizarEstadoReservaDetalleCommand command)
        {
            var sNroBooking = string.Empty;
            var nrofilasafectadas = 0;
            var lst_ids = command.lst_rbd_int_id.Select(s => s).Distinct().ToList();


            foreach (var rbd_int_id in lst_ids)
            {
                var dominio_reserva_detalle = _reservaBookingDetalle.Get(x => x.rbd_int_id == rbd_int_id).LastOrDefault();
                if (dominio_reserva_detalle == null) throw new ReservaBookingException("No se encontro el detalle de reserva");

                dominio_reserva_detalle.rbd_str_estado_bookingdet = ObtenerCodigoEstado(command.rbd_str_estado_bookingdet);
                if (dominio_reserva_detalle.lista_reserva_status_historial == null) dominio_reserva_detalle.lista_reserva_status_historial = new List<ReservaStatusHistorial>();
                dominio_reserva_detalle.lista_reserva_status_historial.Add(new ReservaStatusHistorial() { 
                    rb_str_numero_booking = sNroBooking,
                    rbd_str_estado_bookingdet = ObtenerCodigoEstado(command.rbd_str_estado_bookingdet),
                    rsh_dat_fecha_creacion = DateTime.Now,
                    rsh_str_descripcion = command.DescripcionOpcional,
                    rsh_str_usuario_creacion = command.UsuarioCreacion
                });
                nrofilasafectadas ++;
            }

            _reservaBookingDetalle.Commit();
            return new ActualizarEstadoReservaDetalleOutput() { filas_actualizadas = nrofilasafectadas, lst_rbd_int_id = command.lst_rbd_int_id.ToArray() };

        }

        /// <summary>
        /// R = Registrado; E = Enviado a Pago Efectivo; P = Pendiente de Pagar; G = Pagado; T = Asignado a Transportista; C = Cerrado; D = Eliminado
        /// </summary>
        /// <param name="pEstado"></param>
        /// <returns></returns>
        internal static string ObtenerCodigoEstado(ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle pEstado)
        {
            switch(pEstado)
            {
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Registrado: return "RE";
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.EnviadoPagoEfectivo: return "E";
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.PendientePagar: return "PP";
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Pagado: return "PG";
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.AsignadoTransportista: return "T";
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Cerrado: return "C";
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Eliminado: return "D";
                //
                // --| MGZP - Inicio
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Expirado: return "EX";
                case ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Extornado: return "XT";
                // --| MGZP - Fin
            }
            return string.Empty;
        }
    }
}
