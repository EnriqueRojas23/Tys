

using QueryContracts.Common;
namespace QueryContracts.Terminal.Contenedores.Parameters
{

    public class ListarEntidadesBookingParameter : QueryParameter
    {
        public string rb_str_codigo_cliente { get; set; }
        public string rb_str_codigo_agente_carga { get; set; }
        public string rb_str_codigo_buque { get; set; }
    }
}
