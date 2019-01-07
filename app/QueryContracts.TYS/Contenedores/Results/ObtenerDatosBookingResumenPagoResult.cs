using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Contenedores.Results
{
    public class ObtenerDatosBookingResumenPagoResult : QueryResult
    {
        public long rb_int_id { get; set; }
        public string rb_str_numero_booking { get; set; }
        public string rb_str_codigo_buque { get; set; }
        public string rb_str_codigo_buque_descripcion { get; set; }
        public string rb_str_viaje { get; set; }
        public string rb_str_tipo_facturacion { get; set; }
        public string rb_str_unidad_negocio { get; set; }
        public string rb_str_concepto { get; set; }
        public string rb_str_depot { get; set; }
        public string rb_str_depot_descripcion { get; set; }

        public string rb_str_codigo_cliente { get; set; }
        public string rb_str_codigo_cliente_descripcion { get; set; }

        public string rb_str_codigo_cliente_factura { get; set; }
        public string rb_str_codigo_cliente_factura_descripcion { get; set; }

        public string rb_str_codigo_cliente_tarifa { get; set; }
        public string rb_str_codigo_cliente_tarifa_descripcion { get; set; }

        public IEnumerable<DatosBookingDetalleDto> booking_detalle { get; set; }
        public IEnumerable<DatosBookingDetalleServicioAdicionalDto> booking_detalle_servicio_adicional { get; set; }


    }

    public class DatosBookingDetalleDto
    {
        public long rb_int_id { get; set; }
        public long rbd_int_id { get; set; }
        public string rbd_str_matricula_equipamiento { get; set; }
        public int rbd_int_tamanyo { get; set; }
        public string rbd_str_tipo { get; set; }
        public decimal rb_int_identificador_terminal { get; set; }

    }

    public class DatosBookingDetalleServicioAdicionalDto
    {
        public long rbd_int_id { get; set; }
        public long rbsa_int_id { get; set; }
        public string rbsa_str_codigo_servicio_adicional { get; set; }
        public string rbsa_str_codigo_servicio_adicional_descripcion { get; set; }
        public double rbsa_dec_importe_descuento { get; set; }
        public double rbsa_dec_importe_final { get; set; }
        public double rbsa_dec_importe_formula { get; set; }
        public double rbsa_dec_importe_tarifa { get; set; }
        public string rbsa_str_usuario_creacion { get; set; }
        public DateTime rbsa_dat_fecha_creacion { get; set; }

    }
}
