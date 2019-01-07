

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class ActualizarGuiaOrdenCommand : Command
    {
        public long idordentransporte { get; set; }
        public string guiatransportista { get; set; }
      
    }
}
