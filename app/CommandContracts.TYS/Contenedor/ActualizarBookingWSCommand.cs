
using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor
{
    public class ActualizarBookingWSCommand : Command
    {
        public long rb_int_id { get; set; }
        public string rb_int_identificador_terminal { get; set; }
        
    }
}
