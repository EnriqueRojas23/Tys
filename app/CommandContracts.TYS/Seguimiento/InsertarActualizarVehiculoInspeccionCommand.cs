

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarVehiculoInspeccionCommand : Command
    {
        public int? idvehiculoinspeccion { get; set; }
        public int idinspeccion { get; set; }
        public int idvehiculo { get; set; }
        public string observacion { get; set; }
        public bool checkeado { get; set; }
       
           

    }
}
