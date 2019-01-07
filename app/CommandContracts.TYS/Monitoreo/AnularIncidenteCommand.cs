

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class AnularIncidenteCommand : Command
    {
        public long? idincidencia { get; set; }

    }
}
