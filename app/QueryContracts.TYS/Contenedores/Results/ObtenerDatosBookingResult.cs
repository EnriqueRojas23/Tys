
using System;
using QueryContracts.Common;
using System.Collections.Generic;
using Componentes.Common.Extensions;

namespace QueryContracts.TYS.Contenedores.Results
{
	public class ObtenerDatosBookingResult : QueryResult
	{
		public long? rb_int_id { get; set; }
		public string rb_str_numero_booking  { get; set; }
		public string rb_str_oficina  { get; set; }
		public string rb_str_depot  { get; set; }
		public string rb_str_tipo_reserva { get; set; }
        public string rb_str_tipo_reserva_descripcion { get; set; }
        public DateTime? rb_dat_fecha_reserva 
        { 
            get { return rb_str_fecha_reserva.ToDate(); }
            set { rb_str_fecha_reserva = value.HasValue ? value.Value.ToString("yyyyMMdd") : null; }
        }
        public string rb_str_fecha_reserva { get; set; }
		public string rb_str_codigo_cliente { get; set; }
        public string rb_str_codigo_cliente_descripcion { get; set; }

        public string rb_str_codigo_cliente_factura { get; set; }
        public string rb_str_codigo_cliente_factura_descripcion { get; set; }



        public string rb_str_cod_consolidador { get; set; }
	    public string rb_str_consolidador_descripcion { get; set; }
        public string rb_str_operador_logistico { get; set; }
        public string rb_str_operador_logistico_descripcion { get; set; }
        public string rb_str_agente_aduana { get; set; }
        public string rb_str_agente_aduana_descripcion { get; set; }


        public string rb_str_codigo_buque { get; set; }
        public string rb_str_codigo_buque_descripcion { get; set; }
        public string rb_str_viaje { get; set; }
		public string rb_str_codigo_puerto_origen { get; set; }
        public string rb_str_codigo_puerto_origen_descripcion { get; set; }
		public string rb_str_codigo_puerto_destino { get; set; }
        public string rb_str_codigo_puerto_destino_descripcion { get; set; }
		public string rb_str_codigo_puerto_destino_final { get; set; }
        public string rb_str_codigo_puerto_destino_final_descripcion { get; set; }
        public string rb_str_codigo_agente_carga { get; set; }
        public string rb_str_codigo_agente_carga_descripcion { get; set; }

        public DateTime? rb_str_ws_eta
        {

            get {

                string hora = rb_str_hora_eta.Substring(0, 2) + ":" + rb_str_hora_eta.Substring(2,2);
                return Convert.ToDateTime(rb_str_fecha_eta.ToDate().Value.ToShortDateString() + " " + hora);
            }
            set { rb_str_fecha_eta = value.HasValue ? value.Value.ToString("yyyyMMdd") : null; }
        }
        public string rb_str_fecha_eta { get; set; }
        public string rb_str_hora_eta { get; set; }
        public string rb_str_producto { get; set; }
		public string rb_str_subproducto { get; set; }
		public int rb_int_espacios { get; set; }
		public string rb_str_ws_estado { get; set; }
		public string rb_str_consolidador { get; set; }
        //public string rb_str_operador_logistico { get; set; }
        //public string rb_str_agente_aduana { get; set; }
		public string rb_str_mercancia { get; set; }
		public decimal rb_dec_peso { get; set; }
		public bool rb_bit_checkimo { get; set; }
		public string rb_str_codigoimo { get; set; }
		public bool rb_bit_servicio_integral { get; set; }
		public string rb_str_numero_servicio_integral { get; set; }
		public string rb_str_condicion_origen { get; set; }
		public string rb_str_local_asignado { get; set; }
		public string rb_str_embarque_via { get; set; }
		public string rb_str_movilizado { get; set; }
		public string rb_str_nombre_contacto { get; set; }
		public string rb_str_telefono_contacto { get; set; }
		public string rb_str_email_contacto { get; set; }
		public int mlt_int_id_estado_reserva { get; set; }
		public string mlt_str_estado_reserva { get; set; }
        public string mlt_str_estado_reserva_descripcion { get; set; }
		public DateTime rb_dat_fecha_creacion { get; set; }
		public string rb_str_usuario_creacion { get; set; }

        public IEnumerable<ObtenerBookingDocumentoDto> Documentos { get; set; }
		 
	}

    public class ObtenerBookingDocumentoDto
    {
         public long? rba_int_id  { get; set; }
         public long? rb_int_id  { get; set; }
         public long? mlt_int_id_tipo_documento { get; set; }
         public string mlt_str_valor_tipo_documento { get; set; }
         public string mlt_str_descripcion_tipo_documento { get; set; }
         public string rba_str_nombre_archivo  { get; set; }
         public DateTime rba_dat_fecha_creacion { get; set; }
         public string rba_str_usuario_creacion  { get; set; }
        
    }
}
