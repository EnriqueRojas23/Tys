
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CommandContracts.Common;
using System.Collections.Generic;
using Componentes.Common.Extensions;

namespace CommandContracts.TYS.Contenedor
{
    public class ReservaContenedorCommand : Command
    {

        public long? rb_int_id { get; set; }


        [Display(Name = "Booking")]
        public string rb_str_numero_booking { get; set; }

        [Display(Name = "Oficina")]
        public string rb_str_oficina { get; set; }
        public string rb_str_oficina_descripcion { get; set; }

        [Display(Name = "Depot")]
        public string rb_str_depot { get; set; }
        public string rb_str_depot_descripcion { get; set; }

        [Display(Name = "Tipo de Reserva")]
        public string rb_str_tipo_reserva { get; set; }

        public string rb_str_tipo_reserva_descripcion { get; set; }
                
        [Display(Name = "Fecha de Reserva")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? rb_dat_fecha_reserva 
        {
            get { return rb_str_fecha_reserva.ToDate(); } 
            set { rb_str_fecha_reserva = value.HasValue ? value.Value.ToString("yyyyMMdd") : null; }  
        }
        
        public string rb_str_fecha_reserva { get; set; }
        public string rb_str_hora_reserva { get; set; }


        [Display(Name = "Línea")]
        public string rb_str_codigo_cliente { get; set; }

        public string rb_str_codigo_cliente_descripcion { get; set; }


        [Display(Name = "Facturar:")]
        public string rb_str_codigo_cliente_factura { get; set; }

        public string rb_str_codigo_cliente_factura_descripcion { get; set; }

        [Display(Name = "Nave")]
        public string rb_str_codigo_buque { get; set; }
        public string rb_str_codigo_buque_descripcion { get; set; }
        
        [Display(Name = "Viaje")]
        public string rb_str_viaje { get; set; }
        

        [Display(Name = "Puerto Origen")]
        public string rb_str_codigo_puerto_origen { get; set; }

        public string rb_str_codigo_puerto_origen_descripcion { get; set; }

        [Display(Name = "Puerto Destino")]
        public string rb_str_codigo_puerto_destino { get; set; }

        public string rb_str_codigo_puerto_destino_descripcion { get; set; }

        [Display(Name = "Puerto Destino Final")]
        public string rb_str_codigo_puerto_destino_final { get; set; }
        public string rb_str_codigo_puerto_destino_final_descripcion { get; set; }

        [Display(Name = "Agente de Carga")]
        public string rb_str_codigo_agente_carga { get; set; }
        public string rb_str_codigo_agente_carga_descripcion { get; set; }

        [Display(Name = "E.T.A")]
        public DateTime? rb_str_ws_eta
        {
            get;
            set;
            //get { return rb_str_fecha_eta.ToDate(); } 
            //set { rb_str_fecha_eta = value.HasValue ? value.Value.ToString("yyyyMMdd") : null; }  

            // get { return rb_str_fecha_eta; } 
            //set { rb_str_fecha_eta = value.HasValue ? value.Value.ToString("yyyyMMdd") : null; }  
        }

        public string rb_str_fecha_eta { get; set; }
        public string rb_str_hora_eta { get; set; }

        [Display(Name = "Producto")]
        public string rb_str_producto { get; set; }

        [Display(Name = "Sub - Producto")]
        public string rb_str_subproducto { get; set; }

        [Display(Name = "Nro. Espacios")]
        public int rb_int_espacios { get; set; }
        public string rb_str_ws_estado { get; set; }

        [Display(Name = "Consolidador")]
        public string rb_str_consolidador { get; set; }

        public string rb_str_consolidador_descripcion { get; set; }


        [Display(Name = "Operador Logístico")]
        public string rb_str_operador_logistico { get; set; }

        public string rb_str_operador_logistico_descripcion { get; set; }


        [Display(Name = "Agente de Aduana")]
        public string rb_str_agente_aduana { get; set; }

        public string rb_str_agente_aduana_descripcion { get; set; }

        [Display(Name = "Mercancía")]
        public string rb_str_mercancia { get; set; }

        [Display(Name = "Peso")]
        public decimal? rb_dec_peso { get; set; }

        [Display(Name = "Check IMO")]
        public bool rb_bit_checkimo { get; set; }

        [Display(Name = "Código IMO")]
        public string rb_str_codigoimo { get; set; }

        [Display(Name = "Check Servicio Integral")]
        public bool rb_bit_servicio_integral { get; set; }

        [Display(Name = "Número de Servicio Integral")]
        public string rb_str_numero_servicio_integral { get; set; }

        [Display(Name = "Condición Origen")]
        public string rb_str_condicion_origen { get; set; }

        [Display(Name = "Local Asignado")]
        public string rb_str_local_asignado { get; set; }

        [Display(Name = "Embarque Vía")]
        public string rb_str_embarque_via { get; set; }

        [Display(Name = "Movilizado")]
        public string rb_str_movilizado { get; set; }

        [Display(Name = "Nombre Completo")]
        public string rb_str_nombre_contacto { get; set; }

        [Display(Name = "Teléfono")]
        public string rb_str_telefono_contacto { get; set; }

        [Display(Name = "Email")]
        public string rb_str_email_contacto { get; set; }
        public int mlt_int_id_estado_reserva { get; set; }

        [Display(Name = "Estado")]
        public string mlt_str_estado_reserva { get; set; }
        public DateTime rb_dat_fecha_creacion { get; set; }
        public string rb_str_usuario_creacion { get; set; }
        public decimal? rb_int_identificador_terminal { get; set; }

        public IList<ReservaContenedorDocumentosCommand> Documentos { get; set; }

    }

    public class ReservaContenedorDocumentosCommand
    {

        public int rba_int_id { get; set; }
        public long mlt_int_id_tipo_documento { get; set; }
        public string mlt_str_valor_tipo_documento { get; set; }
        public string rba_str_nombre_archivo { get; set; }
        public DateTime rba_dat_fecha_creacion { get; set; }
        public string rba_str_usuario_creacion { get; set; }
        //atributo que no va en el handler
        public string mlt_str_descripcion_tipo_documento { get; set; }

    }
}
