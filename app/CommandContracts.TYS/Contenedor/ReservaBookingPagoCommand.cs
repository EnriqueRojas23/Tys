
using CommandContracts.Common;
using System;
using System.Collections.Generic;
namespace CommandContracts.TYS.Contenedor
{
    public class ReservaBookingPagoCommand : Command
    {
        public long? rb_int_id { get; set; }
        public string rb_str_codigo_cliente_operacion { get; set; }
        public string rb_str_codigo_cliente_operacion_descripcion { get; set; }
        public string rb_str_codigo_cliente_factura { get; set; }
        public string rb_str_codigo_cliente_factura_descripcion { get; set; }
        public string rb_str_codigo_cliente_tarifa { get; set; }
        public string rb_str_codigo_cliente_tarifa_descripcion { get; set; }
        public string rpb_str_estado_pago { get; set; }
        public DateTime rbp_dat_fecha_creacion { get; set; }
        public string rbp_str_usuario_creacion { get; set; }
        public IList<RBookServicioAdicionalCommand> lista_servicios_adicionales { get; set; }

        public decimal rb_dec_montoservicio { get; set; }
        public decimal rb_dec_montoigv { get; set; }
        public decimal rb_dec_montodetraccion { get; set; }
        public decimal rb_dec_montototal { get; set; }
        public decimal rb_dec_montopagar { get; set; }
    }

    public class RBookServicioAdicionalCommand 
    {
        public long rbd_int_id { get; set; }
        public string rbsa_str_codigo_servicio_adicional { get; set; }
        public string rbsa_str_codigo_servicio_adicional_descripcion { get; set; }
        public decimal rbsa_dec_importe_tarifa { get; set; }
        public DateTime rbsa_dat_fecha_creacion { get; set; }
        public string rbsa_str_usuario_creacion { get; set; }
    }
}
