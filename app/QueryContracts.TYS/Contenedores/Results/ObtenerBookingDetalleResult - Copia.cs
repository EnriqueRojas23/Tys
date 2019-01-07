using System;
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.TYS.Contenedores.Results
{
    public class ObtenerBookingAuxResult : QueryResult
    {
        public long? rb_int_identificador_terminal { get; set; }
        public string rb_str_cod_cliente_factura { get; set; }
        public string rb_str_cliente_factura_descripcion { get; set; }
        public string rb_str_cod_consolidador { get; set; }
        public string rb_str_consolidador_descripcion { get; set; }
        public string rb_str_cod_operador_logistivo { get; set; }
        public string rb_str_cod_operador_descripcion { get; set; }
        public string rb_str_cod_agente_aduana { get; set; }
        public string rb_str_cod_agente_aduana_descripcion { get; set; }
     
    }

}
