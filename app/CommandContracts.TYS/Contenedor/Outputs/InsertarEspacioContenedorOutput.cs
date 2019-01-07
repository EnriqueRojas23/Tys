
using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor.Outputs
{
    public class InsertarEspacioContenedorOutput : CommandResult
    {
        public int rb_int_espacios { get; set; }
        public long[] rbd_int_id { get; set; }
        public decimal? rb_int_identificador_terminal { get; set; }
        public string mensaje_error { get; set; }
    }
}
