
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Core.Contenedores.Results
{
    public class ObtenerReservaCabeceraResult : QueryResult
    {
        public string rb_str_numero_booking { get; set; } //booking
        public string rb_str_oficina { get; set; } //idoficina
        public string rb_str_oficina_descripcion { get; set; }
        public string rb_str_depot { get; set; } //idalmacen
        public string rb_str_depot_descripcion { get; set; }
        public string rb_str_tipo_reserva { get; set; } //tiporeserva
        public string rb_str_tipo_reserva_descripcion { get; set; }
        public string rb_str_fecha_reserva { get; set; } //fechareserva
        public string rb_str_hora_reserva { get; set; } //horareserva
        public string rb_str_codigo_cliente { get; set; } //idcliente
        public string rb_str_codigo_cliente_cif { get; set; } //idcliente

        public string rb_str_codigo_cliente_descripcion { get; set; }
        public string rb_str_codigo_agente_carga { get; set; } //idagentecarga
        public string rb_str_codigo_agente_carga_descripcion { get; set; }
        public string rb_str_codigo_buque { get; set; } //idbuque
        public string rb_str_codigo_buque_descripcion { get; set; }
        public string rb_str_viaje { get; set; } //viaje
        public string rb_str_codigo_puerto_origen { get; set; } //idpuertoorigen
        public string rb_str_codigo_puerto_origen_descripcion { get; set; }
        public string rb_str_codigo_puerto_destino { get; set; } //idpuertodestino
        public string rb_str_codigo_puerto_destino_descripcion { get; set; }
        public string rb_str_codigo_puerto_destino_final { get; set; } //idpuertodestinofinal
        public string rb_str_codigo_puerto_destino_final_descripcion { get; set; }
        public string rb_str_fecha_eta { get; set; } //fechaeta
        public string rb_str_hora_eta { get; set; } //horaeta
        public string rb_str_producto { get; set; } //producto
        public string rb_str_subproducto { get; set; } //subproducto
        public string rb_str_ws_estado { get; set; } //estado
        public string rb_str_consolidador { get; set; } //idconsolidador
        public string rb_str_operador_logistico { get; set; } //idoperador
        public string rb_str_agente_aduana { get; set; } //idagenteaduana
        public string rb_str_mercancia { get; set; } //mercancia
        public decimal? rb_dec_peso { get; set; } //peso
        public bool rb_bit_checkimo { get; set; } //snimo
        public string rb_str_codigoimo { get; set; } //numeroimo
        public bool rb_bit_servicio_integral { get; set; } //snserviciointegral
        public string rb_str_numero_servicio_integral { get; set; } //serviciointegral
        public string rb_str_condicion_origen { get; set; } //condicionorigen
        public string rb_str_condicion_origen_descripcion { get; set; } //condicionorigen
        public string rb_str_local_asignado { get; set; } //localasignado
        public string rb_str_embarque_via { get; set; } //embarquevia
        public string rb_str_movilizado { get; set; } //movilizadoa
        public decimal? rb_int_identificador_terminal { get; set; } //id

        public IEnumerable<ListarReservaDetalleDto> ListaReservaDetalle { get; set; }

    }

    public class ListarReservaDetalleDto
    {
        public string rbd_str_matricula_equipamiento { get; set; }
        public string rbd_str_fecha_estimada { get; set; }
        public string rbd_str_hora_estimada { get; set; }
        public int rbd_int_tamanyo { get; set; }
        public string rbd_str_tipo { get; set; }
        public string rbd_str_precintos { get; set; }
        public string rbd_str_temperatura { get; set; }
        public string rbd_str_unidad_temperatura_descripcion { get; set; }
        public string rbd_str_unidad_temperatura { get; set; }
        public string rbd_str_humedad { get; set; }
        public string rbd_str_ventilacion { get; set; }
        public string rbd_str_tipo_carga { get; set; }
        public string rbd_str_reefers { get; set; }
        public string rbd_str_estado_bookingdet { get; set; }
        public string rbd_str_ent_cif_transportista { get; set; }
        //public string rbd_str_ent_nombre_transportista { get; set; }
        public string rbd_str_trans_matricula_camion { get; set; }
        public string rbd_str_trans_nif_conductor { get; set; }
        public string rbd_str_trans_nombre_conductor { get; set; }
        public string rbd_str_fecha_recojo { get; set; }
        public string rbd_str_hora_recojo { get; set; }
        public decimal? rb_int_identificador_terminal { get; set; }
        public DateTime? rbd_dat_fecha_creacion { get; set; }
        public string rbd_str_usuario_creacion { get; set; }
        public string rbd_str_co2 { get; set; }
        public string rbd_str_o2 { get; set; }

    }

}
