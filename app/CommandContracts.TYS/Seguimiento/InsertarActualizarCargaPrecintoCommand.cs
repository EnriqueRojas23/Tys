

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarCargaPrecintoCommand : Command
    {
        public long? idprecinto { get; set; }
        public long? idcargaprecinto  { get; set; }
        public long? iddespacho { get; set; }
        public bool eliminar { get; set; }
     
    }
}
