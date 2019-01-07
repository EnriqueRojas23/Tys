

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class CerrarOperacionCommand : Command
    {
        public long? idorden { get; set; }
        public long? idmanifiesto { get; set; }
        public bool cierreoperativo { get; set; }

    }
}
