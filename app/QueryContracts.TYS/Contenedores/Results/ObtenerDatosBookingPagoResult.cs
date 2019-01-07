using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Contenedores.Results
{
    public class ObtenerDatosBookingPagoResult : QueryResult
    {
       public long rb_int_id { get; set; }
       public string rb_str_numero_booking { get; set; }
       public long rbp_int_id { get; set; }
       public string rbp_str_cip { get; set; }
       public string rb_str_codigo_cliente_operacion { get; set; }
       public string rb_str_codigo_cliente_operacion_cif { get; set; }
       public string rb_str_codigo_cliente_operacion_descripcion { get; set; }
       public string rb_str_codigo_cliente_factura { get; set; }
       public string rb_str_codigo_cliente_factura_cif { get; set; }
       public string rb_str_codigo_cliente_factura_descripcion { get; set; }
       public string rb_str_codigo_cliente_tarifa { get; set; }
       public string rb_str_codigo_cliente_tarifa_cif { get; set; }
       public string rb_str_codigo_cliente_tarifa_descripcion { get; set; }
       public decimal rbp_dec_importe_final { get; set; }
       public string rpb_str_estado_pago { get; set; }
       public DateTime rbp_dat_fecha_creacion { get; set; }
       public string rbp_str_usuario_creacion { get; set; }
    }
}
