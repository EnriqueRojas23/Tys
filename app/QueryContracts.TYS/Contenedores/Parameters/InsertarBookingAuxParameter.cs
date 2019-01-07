
using QueryContracts.Common;
using System;

namespace QueryContracts.TYS.Contenedores.Parameters
{
    public class InsertarBookingAuxParameter : QueryParameter
    {
        public long rb_int_identificador_terminal { get; set; }
        public string rb_str_cod_cliente_factura {get;set;}
        public string rb_str_cliente_factura_descripcion {get;set;}
        public string rb_str_cod_consolidador {get;set;}
        public string rb_str_consolidador_descripcion {get;set;}
        public string rb_str_cod_operador_logistivo {get;set;}
        public string rb_str_cod_operador_descripcion {get;set;}
        public string rb_str_cod_agente_aduana {get;set;}
        public string rb_str_cod_agente_aduana_descripcion { get; set; }
        public string rb_str_cif_agente_aduana { get;set; }
        public string rb_str_cif_agente_carga { get; set; }
   }
}
