

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarManifiestoCommand : Command
    {
        public long? idmanifiesto { get; set; }
        public string numhojaruta { get; set; }
        public string nummanifiesto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int? idusuarioregistro { get; set; }
        public long iddespacho { get; set; }
        public bool? activo { get; set; }
        public int _tipoop { get; set; }
        public int idvehiculo { get; set; }
        public int idtipooperacion { get; set; }
        public int? iddestino { get; set; }
           
            
    }
}
