
using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor
{
    public class ActualizarWSIdTerminalCommand : Command
    {
        public decimal rb_int_identificador_terminal { get; set; }
        public long rbd_int_id { get; set; }
    }
}
