

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarServicioOperacionCommand : Command
    {
        public long? idserviciooperacion { get; set; }
        public long idcarga { get; set; }
        public int idservicio { get; set; }
        public int cantidad { get; set; }
     
      
           

    }
}
