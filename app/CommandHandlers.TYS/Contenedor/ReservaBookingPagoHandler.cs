using System;
using System.Linq;
using CommandContracts.Common;
using CommandContracts.TYS.Contenedor;
using CommandContracts.TYS.Contenedor.Outputs;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.contenedor;
using Domain.TYS.contenedor.Exceptions;
using System.Collections.Generic;

namespace CommandHandlers.TYS.Contenedor
{
    public class ReservaBookingPagoHandler : ICommandHandler<ReservaBookingPagoCommand>
    {
        private readonly IRepository<ReservaBooking> _reservabooking;
        private readonly IRepository<ReservaBookingPago> _reservabookinpago;
        private readonly IRepository<ReservaBookingDetalle> _reservabookingdetalle;

        public ReservaBookingPagoHandler(IRepository<ReservaBooking> pReservaBooking, IRepository<ReservaBookingPago> pReservaBookinPago, IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservabooking = pReservaBooking;
            this._reservabookinpago = pReservaBookinPago;
            this._reservabookingdetalle = pReservaBookingDetalle;
        }

        public CommandResult Handle(ReservaBookingPagoCommand command)
        {
            if (command.lista_servicios_adicionales.Any() == false) throw new ReservaBookingException("No se han ingresado servicios adicionales para el pago.");

            //var dominio_reserva_detalle = _reservabookinpago.Get(x => x.rb_int_id == rbd_int_id).LastOrDefault();
            
            var lst_rbd_int_id = new List<long>();
            var dominio_pago = new ReservaBookingPago();
            //dominio_pago.rbp_dec_importe_final = CalcularImporteFinal(command.lista_servicios_adicionales);
            dominio_pago.rbp_dec_importe_final = command.rb_dec_montopagar;
            dominio_pago.rb_int_id = command.rb_int_id;
            dominio_pago.rb_str_codigo_cliente_factura = command.rb_str_codigo_cliente_factura;
            dominio_pago.rb_str_codigo_cliente_factura_descripcion = command.rb_str_codigo_cliente_factura_descripcion;
            dominio_pago.rb_str_codigo_cliente_operacion = command.rb_str_codigo_cliente_operacion;
            dominio_pago.rb_str_codigo_cliente_operacion_descripcion = command.rb_str_codigo_cliente_operacion_descripcion;
            dominio_pago.rb_str_codigo_cliente_tarifa = command.rb_str_codigo_cliente_tarifa;
            dominio_pago.rb_str_codigo_cliente_tarifa_descripcion = command.rb_str_codigo_cliente_tarifa_descripcion;
            dominio_pago.rbp_dat_fecha_creacion = DateTime.Now;
            //dominio_pago.rbp_int_id
            dominio_pago.rbp_str_cip = string.Empty;
            dominio_pago.rbp_str_usuario_creacion = command.rbp_str_usuario_creacion;
            dominio_pago.rpb_str_estado_pago = command.rpb_str_estado_pago;

            dominio_pago.lista_servicios_adicionales = new List<ReservaBookingServicioAdicional>();



            foreach (var servicio_adicional in command.lista_servicios_adicionales)
            {
                var dominio_servicio_adicional = new ReservaBookingServicioAdicional();
                dominio_servicio_adicional.rbsa_dat_fecha_creacion = DateTime.Now;
                dominio_servicio_adicional.rbsa_dec_importe_tarifa = servicio_adicional.rbsa_dec_importe_tarifa;
                dominio_servicio_adicional.rbd_int_id = servicio_adicional.rbd_int_id;
                dominio_servicio_adicional.rbsa_str_codigo_servicio_adicional = servicio_adicional.rbsa_str_codigo_servicio_adicional;
                dominio_servicio_adicional.rbsa_str_codigo_servicio_adicional_descripcion = servicio_adicional.rbsa_str_codigo_servicio_adicional_descripcion;
                dominio_servicio_adicional.rbsa_str_usuario_creacion = command.rbp_str_usuario_creacion;
                dominio_pago.lista_servicios_adicionales.Add(dominio_servicio_adicional);
                lst_rbd_int_id.Add(servicio_adicional.rbd_int_id);
            }

            _reservabookinpago.Add(dominio_pago);
            _reservabookinpago.Commit();

            //actualizar estado del detalle
            
            var handler = new ActualizarEstadoReservaDetalleHandler(this._reservabookingdetalle);
            var output = (ActualizarEstadoReservaDetalleOutput)handler.Handle(new ActualizarEstadoReservaDetalleCommand() 
                                {   
                                    lst_rbd_int_id = lst_rbd_int_id, 
                                    //rbd_str_estado_bookingdet = ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.PendientePagar, 
                                    rbd_str_estado_bookingdet = ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.Registrado, 
                                    UsuarioCreacion = command.rbp_str_usuario_creacion, 
                                    DescripcionOpcional = "Generando Resumen pago" 
                                }); 
            return new ReservaBookingPagoOutput() { rbp_int_id = dominio_pago.rbp_int_id };

        }

        public decimal CalcularImporteFinal(IList<RBookServicioAdicionalCommand> lista_servicios_adicionales)
        {
            decimal importe_final = 0;
            foreach (var servicio_adicional in lista_servicios_adicionales){
                importe_final += servicio_adicional.rbsa_dec_importe_tarifa;
            }
            return importe_final;
        }


    }
}
