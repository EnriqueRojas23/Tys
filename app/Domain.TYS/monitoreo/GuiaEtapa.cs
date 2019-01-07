

using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.TYS.Monitoreo
{
    public class GuiaEtapa : Entity
    {
        [Key]
        public long idguiaetapa { get; set; }
        public long idguiaremisioncliente { get; set; }
        public long idordentrabajo { get; set; }
        public int cantidad { get; set; }

    }
}
