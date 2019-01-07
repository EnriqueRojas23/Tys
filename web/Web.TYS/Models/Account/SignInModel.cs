

namespace Web.TYS.Models.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SignInModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name = "Código Usuario")]
        public string CodigoUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public SelectList ListaModoAutenticacion
        {
            get 
            {
                var lista = new SelectList(new List<SelectListItem>() 
                { 
                    new SelectListItem() { Selected = true, Text = "Autenticación Externa", Value = "EX" },
                    new SelectListItem() { Selected = true, Text = "Autenticación Coorporativa", Value = "AD" }

                }, "Value" , "Text");

                return lista;
            }
        }

        [Required]
        [Display(Name = "Modo Autenticación")]
        public string ModoAutenticacion { get; set; }

    }
}