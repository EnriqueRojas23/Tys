
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
    public class ReservaContenedorHandler : ICommandHandler<ReservaContenedorCommand>
    {
        private readonly IRepository<ReservaBooking> _reservaBooking;
        public ReservaContenedorHandler(IRepository<ReservaBooking> pReservaBooking)
        {
            this._reservaBooking = pReservaBooking;
        }

        public CommandResult Handle(ReservaContenedorCommand command)
        {
            //validando los parametros de entrada.
            if(command == null) throw new ArgumentNullException("Se requiere el command");
            ValidarBooking(command);
            
            //extrañendo los datos del dominio
            var dominioReservaBooking = command.rb_int_id.HasValue ? _reservaBooking.Get(x => x.rb_int_id == command.rb_int_id).LastOrDefault() : new ReservaBooking();
            //validando que el dominio tenga datos
            if(dominioReservaBooking == null) throw new ReservaBookingException("No se encontro el booking solicitado.");

            dominioReservaBooking = MapBooking(dominioReservaBooking, command);
            if (!command.rb_int_id.HasValue){
                _reservaBooking.Add(dominioReservaBooking);
            }
            _reservaBooking.Commit();
            return new ReservaContenedorOutput() { rb_int_id = dominioReservaBooking.rb_int_id, rb_int_identificador_terminal = dominioReservaBooking.rb_int_identificador_terminal };
        }

        private static ReservaBooking MapBooking(ReservaBooking domain, ReservaContenedorCommand command)
        {
            domain.rb_str_numero_booking = command.rb_str_numero_booking;
            domain.rb_str_oficina = command.rb_str_oficina;
            domain.rb_str_oficina_descripcion = command.rb_str_oficina_descripcion;
            domain.rb_str_depot = command.rb_str_depot;
            domain.rb_str_depot_descripcion = command.rb_str_depot_descripcion;
            domain.rb_str_tipo_reserva = command.rb_str_tipo_reserva;
            domain.rb_str_tipo_reserva_descripcion = command.rb_str_tipo_reserva_descripcion;
            domain.rb_str_fecha_reserva = command.rb_str_fecha_reserva;
//            domain.rb_str_codigo_cliente = command.rb_str_codigo_cliente;
            domain.rb_str_codigo_cliente = command.rb_str_codigo_cliente_factura;
            domain.rb_str_codigo_buque = command.rb_str_codigo_buque;
            domain.rb_str_codigo_buque_descripcion = command.rb_str_codigo_buque_descripcion;
            domain.rb_str_viaje = command.rb_str_viaje;
            domain.rb_str_codigo_puerto_origen = command.rb_str_codigo_puerto_origen;
            domain.rb_str_codigo_puerto_origen_descripcion = command.rb_str_codigo_puerto_origen_descripcion;
            domain.rb_str_codigo_puerto_destino = command.rb_str_codigo_puerto_destino;
            domain.rb_str_codigo_puerto_destino_descripcion = command.rb_str_codigo_puerto_destino_descripcion;
            domain.rb_str_codigo_puerto_destino_final = command.rb_str_codigo_puerto_destino_final;
            domain.rb_str_codigo_puerto_destino_final_descripcion = command.rb_str_codigo_puerto_destino_final_descripcion;
            domain.rb_str_codigo_agente_carga = command.rb_str_codigo_agente_carga;
            domain.rb_str_codigo_agente_carga_descripcion = command.rb_str_codigo_agente_carga_descripcion;
            domain.rb_str_fecha_eta = command.rb_str_fecha_eta;
            domain.rb_str_hora_eta = command.rb_str_hora_eta;
            domain.rb_str_producto = command.rb_str_producto;
            domain.rb_str_subproducto = command.rb_str_subproducto;
            domain.rb_int_espacios = command.rb_int_espacios;
            domain.rb_str_ws_estado = command.rb_str_ws_estado;
            domain.rb_str_consolidador = command.rb_str_consolidador;
            domain.rb_str_operador_logistico = command.rb_str_operador_logistico;
            domain.rb_str_agente_aduana = command.rb_str_agente_aduana;
            domain.rb_str_mercancia = command.rb_str_mercancia;
            domain.rb_dec_peso = command.rb_dec_peso;
            domain.rb_bit_checkimo = command.rb_bit_checkimo;
            domain.rb_str_codigoimo = command.rb_str_codigoimo;
            domain.rb_bit_servicio_integral = command.rb_bit_servicio_integral;
            domain.rb_str_numero_servicio_integral = command.rb_str_numero_servicio_integral;
            domain.rb_str_condicion_origen = command.rb_str_condicion_origen;
            domain.rb_str_local_asignado = command.rb_str_local_asignado;
            domain.rb_str_embarque_via = command.rb_str_embarque_via;
            domain.rb_str_movilizado = command.rb_str_movilizado;
            domain.rb_str_nombre_contacto = command.rb_str_nombre_contacto;
            domain.rb_str_telefono_contacto = command.rb_str_telefono_contacto;
            domain.rb_str_email_contacto = command.rb_str_email_contacto;
            domain.mlt_int_id_estado_reserva = command.mlt_int_id_estado_reserva;
            domain.mlt_str_estado_reserva = command.mlt_str_estado_reserva;
            domain.rb_dat_fecha_creacion = DateTime.Now;
            domain.rb_str_usuario_creacion = command.rb_str_usuario_creacion;
            
            if (command.Documentos != null || command.Documentos.Any()){
                if (domain.lista_documentos_adjuntos == null) domain.lista_documentos_adjuntos = new List<ReservaBookingAdjuntos>();
                foreach (var documento in command.Documentos)
                {
                    var existe_documento = true;
                    ReservaBookingAdjuntos dominio_documento = domain.lista_documentos_adjuntos.LastOrDefault(x=>x.mlt_str_valor_tipo_documento == documento.mlt_str_valor_tipo_documento); // == null : new ReservaBookingAdjuntos();
                    if (dominio_documento == null){
                        dominio_documento = new ReservaBookingAdjuntos();
                        existe_documento = false;
                    }

                    dominio_documento.rba_str_nombre_archivo = documento.rba_str_nombre_archivo;
                    if (existe_documento == false) {
                        dominio_documento.rba_dat_fecha_creacion = DateTime.Now;
                        dominio_documento.rba_str_usuario_creacion = command.rb_str_usuario_creacion;
                        dominio_documento.mlt_int_id_tipo_documento = documento.mlt_int_id_tipo_documento;
                        dominio_documento.mlt_str_valor_tipo_documento = documento.mlt_str_valor_tipo_documento;

                        domain.lista_documentos_adjuntos.Add(dominio_documento);
                    } 
                }
            }
            return domain;
        }

        private void ValidarBooking(ReservaContenedorCommand command)
        {
            ReservaBooking dominio = _reservaBooking.Get(x=>x.rb_str_numero_booking.Equals(command.rb_str_numero_booking, StringComparison.InvariantCulture)).LastOrDefault();
            
            if (!command.rb_int_id.HasValue && dominio != null){
                throw new ReservaBookingException("El número de booking ya ha sido ingresado");
            }

            if (command.Documentos == null || command.Documentos.Any() == false) {
                throw new ReservaBookingException("Tiene que adjuntar por lo menos un documento.");
            }

            var documento_booking = command.Documentos.LastOrDefault(x=>x.mlt_str_valor_tipo_documento == "DOC1");
            if(documento_booking == null){
                throw new ReservaBookingException("Se tiene que adjuntar la hoja de booking.");
            }

            var documento_precinto = command.Documentos.LastOrDefault(x=>x.mlt_str_valor_tipo_documento == "DOC3");
            if(documento_precinto == null){
                throw new ReservaBookingException("Se tiene que adjuntar la carta precinto.");
            }


        }
    }
}
