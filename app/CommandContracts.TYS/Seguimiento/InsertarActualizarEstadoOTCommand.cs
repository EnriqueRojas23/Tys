

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarEstadoOTCommand : Command
    {
        public long? idordentrabajo { get; set; }
        public int idestado { get; set; }
       
           

    }
}
