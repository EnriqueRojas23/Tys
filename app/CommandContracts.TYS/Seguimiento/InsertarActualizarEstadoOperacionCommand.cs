

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarEstadoOperacionCommand : Command
    {
        public long? idcarga { get; set; }
        public int idestado { get; set; }
       
           

    }
}
