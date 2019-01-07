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
    public class ActualizarBookingCabeceraHandler : ICommandHandler<ActualizarBookingCabeceraCommand>
    {
        private readonly IRepository<ReservaBooking> _reservaBooking;
        private readonly IRepository<ReservaBookingDetalle> _reservaBookingDetalle;

        public ActualizarBookingCabeceraHandler(IRepository<ReservaBooking> pReservaBooking, IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservaBooking = pReservaBooking;
            this._reservaBookingDetalle = pReservaBookingDetalle;
        }

        public CommandResult Handle(ActualizarBookingCabeceraCommand command)
        {
            var dominio = _reservaBooking.Get(x => x.rb_str_numero_booking == command.rb_str_numero_booking).LastOrDefault();
            if (dominio == null) throw new ReservaBookingException("No se encontro el booking seleccionado");

            dominio.rb_str_numero_booking = command.rb_str_numero_booking;
            dominio.rb_str_oficina = command.rb_str_oficina;
            dominio.rb_str_oficina_descripcion = command.rb_str_oficina_descripcion;
            dominio.rb_str_depot = command.rb_str_depot;
            dominio.rb_str_depot_descripcion = command.rb_str_depot_descripcion;
            dominio.rb_str_tipo_reserva = command.rb_str_tipo_reserva;
            dominio.rb_str_tipo_reserva_descripcion = command.rb_str_tipo_reserva_descripcion;
            dominio.rb_str_fecha_reserva = command.rb_str_fecha_reserva;
            dominio.rb_str_hora_reserva = command.rb_str_hora_reserva;
            dominio.rb_str_codigo_cliente = command.rb_str_codigo_cliente;
            dominio.rb_str_codigo_cliente_cif = command.rb_str_codigo_cliente_cif;
            dominio.rb_str_codigo_cliente_descripcion = command.rb_str_codigo_cliente_descripcion;
            dominio.rb_str_codigo_agente_carga = command.rb_str_codigo_agente_carga;
            dominio.rb_str_codigo_agente_carga_descripcion = command.rb_str_codigo_agente_carga_descripcion;
            dominio.rb_str_codigo_buque = command.rb_str_codigo_buque;
            dominio.rb_str_codigo_buque_descripcion = command.rb_str_codigo_buque_descripcion;
            dominio.rb_str_viaje = command.rb_str_viaje;
            dominio.rb_str_codigo_puerto_origen = command.rb_str_codigo_puerto_origen;
            dominio.rb_str_codigo_puerto_origen_descripcion = command.rb_str_codigo_puerto_origen_descripcion;
            dominio.rb_str_codigo_puerto_destino = command.rb_str_codigo_puerto_destino;
            dominio.rb_str_codigo_puerto_destino_descripcion = command.rb_str_codigo_puerto_destino_descripcion;
            dominio.rb_str_codigo_puerto_destino_final = command.rb_str_codigo_puerto_destino_final;
            dominio.rb_str_codigo_puerto_destino_final_descripcion = command.rb_str_codigo_puerto_destino_final_descripcion;
            dominio.rb_str_fecha_eta = command.rb_str_fecha_eta;
            dominio.rb_str_hora_eta = command.rb_str_hora_eta;
            dominio.rb_str_producto = command.rb_str_producto;
            dominio.rb_str_subproducto = command.rb_str_subproducto;
            dominio.rb_str_ws_estado = command.rb_str_ws_estado;
            dominio.rb_str_consolidador = command.rb_str_consolidador;
            dominio.rb_str_operador_logistico = command.rb_str_operador_logistico;
            dominio.rb_str_agente_aduana = command.rb_str_agente_aduana;
            dominio.rb_str_mercancia = command.rb_str_mercancia;
            dominio.rb_dec_peso = command.rb_dec_peso;
            dominio.rb_bit_checkimo = command.rb_bit_checkimo;
            dominio.rb_str_codigoimo = command.rb_str_codigoimo;
            dominio.rb_bit_servicio_integral = command.rb_bit_servicio_integral;
            dominio.rb_str_numero_servicio_integral = command.rb_str_numero_servicio_integral;
            dominio.rb_str_condicion_origen = command.rb_str_condicion_origen;
            dominio.rb_str_condicion_origen_descripcion = command.rb_str_condicion_origen_descripcion;
            dominio.rb_str_local_asignado = command.rb_str_local_asignado;
            dominio.rb_str_embarque_via = command.rb_str_embarque_via;
            dominio.rb_str_movilizado = command.rb_str_movilizado;
            dominio.rb_int_identificador_terminal = command.rb_int_identificador_terminal;

            var contador = 0;
            foreach(var booking_detalle in command.LIstabookingDetalle)
            {
                var existe_detalle = true;
                var dominio_detalle = _reservaBookingDetalle.Get(x => x.rb_int_identificador_terminal == booking_detalle.rb_int_identificador_terminal).LastOrDefault();
                if (dominio_detalle == null){
                    existe_detalle = false;
                    dominio_detalle = new ReservaBookingDetalle();
                }

                dominio_detalle.rb_int_identificador_terminal = booking_detalle.rb_int_identificador_terminal;
                dominio_detalle.rb_int_id = dominio.rb_int_id;
                dominio_detalle.rbd_dat_fecha_creacion = booking_detalle.rbd_dat_fecha_creacion;
                dominio_detalle.rbd_int_tamanyo = booking_detalle.rbd_int_tamanyo;
                dominio_detalle.rbd_str_ent_cif_transportista = booking_detalle.rbd_str_ent_cif_transportista;
                //dominio_detalle.rbd_str_ent_cif_transportista_descripcion = booking_detalle.rbd_str_ent_cif_transportista_descripcion;
                //dominio_detalle.rbd_str_estado_bookingdet = booking_detalle.rbd_str_estado_bookingdet;
                dominio_detalle.rbd_str_fecha_estimada = booking_detalle.rbd_str_fecha_estimada;
                dominio_detalle.rbd_str_hora_estimada = booking_detalle.rbd_str_hora_estimada;
                dominio_detalle.rbd_str_fecha_recojo = booking_detalle.rbd_str_fecha_recojo;
                dominio_detalle.rbd_str_hora_recojo = booking_detalle.rbd_str_hora_recojo;
                dominio_detalle.rbd_str_humedad = booking_detalle.rbd_str_humedad;
                dominio_detalle.rbd_str_precintos = booking_detalle.rbd_str_precintos;
                dominio_detalle.rbd_str_reefers = booking_detalle.rbd_str_reefers;
                dominio_detalle.rbd_str_temperatura = booking_detalle.rbd_str_temperatura;
                dominio_detalle.rbd_str_tipo = booking_detalle.rbd_str_tipo;
                dominio_detalle.rbd_str_tipo_carga = booking_detalle.rbd_str_tipo_carga;
                dominio_detalle.rbd_str_trans_matricula_camion = booking_detalle.rbd_str_trans_matricula_camion;
                dominio_detalle.rbd_str_trans_nif_conductor = booking_detalle.rbd_str_trans_nif_conductor;
                dominio_detalle.rbd_str_trans_nombre_conductor = booking_detalle.rbd_str_trans_nombre_conductor;
                dominio_detalle.rbd_str_unidad_temperatura = booking_detalle.rbd_str_unidad_temperatura;
                dominio_detalle.rbd_str_usuario_creacion = booking_detalle.rbd_str_usuario_creacion;
                dominio_detalle.rbd_str_ventilacion = booking_detalle.rbd_str_ventilacion;

                if (existe_detalle == false) _reservaBookingDetalle.Add(dominio_detalle);
                contador++;
            
            }
            
            try 
            {
                _reservaBooking.Commit();
                _reservaBookingDetalle.Commit();

                return new ActualizarBookingCabeceraOutput() { booking_actualizado = true, rb_int_identificador_terminal = dominio.rb_int_identificador_terminal,filas_afectadas = contador };

            }
            catch (Exception ex){
                return new ActualizarBookingCabeceraOutput() { booking_actualizado = false };

            }
        }
    }
}
