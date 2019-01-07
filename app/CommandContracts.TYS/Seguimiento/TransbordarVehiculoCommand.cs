

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class TransbordarVehiculoCommand : Command
    {
        public int idvehiculo_old { get; set; }
        public int idvehiculo_new { get; set; }
      
    }
}
