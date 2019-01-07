using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.Common.Extensions;

namespace Web.TYS.Areas.Seguridad.Models.Roles
{
    public class ListarRolesModel : ParameterGridDefault
    {
        public ListarRolesModel() { }

        public string SearchDefault { get; set; }

        [Display(Name = "Nombre")]
        public string NombreRol { get; set; }
               
    }
}