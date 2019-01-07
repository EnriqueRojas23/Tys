

using CommandContracts.Common;

namespace CommandContracts.TYS.Contenedor.Outputs
{
    public class ReservaContenedorOutput : CommandResult
    {
        public long rb_int_id { get; set; }

        public decimal? rb_int_identificador_terminal { get; set; }

        public string rb_str_resultado_ws { get; set; }
    }
}
