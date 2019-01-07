

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Monitoreo
{
    public class HelpResources : Entity
    {
        [Key]
        public int idhelp { get; set; }
        public string help { get; set; }
        public int idcampo { get; set; }

    }
}
