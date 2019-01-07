

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarHelpResourcesCommand : Command
    {
        public int idhelp { get; set; }
        public string help { get; set; }
        public int idcampo { get; set; }
  
    }
}
