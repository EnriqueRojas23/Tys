
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.TYS.DataAccess.Seguridad;

namespace Web.TYS.Areas.Seguridad.Models.Usuarios
{
    public class InsertarModificarAlertaModel
    {

        public int? idalerta { get; set; }
        public int? usr_int_id { get; set; }
        public int idperiodicidad { get; set; }
        public int idmedio { get; set; }
        public string estados { get; set; }
        public string Usr_str_red { get; set; }


        public bool pendienteProgramacion { get; set; }
        public bool pendienteInicioCarga { get; set; }
        public bool pendienteDespacho { get; set; }
        public bool EnRuta { get; set; }
        public bool Entregado { get; set; }
        public bool Liquidado { get; set; }
        public bool Facturado { get; set; }
        public bool Cerrado { get; set; }

        public SelectList Medio
        {
            get
            {
                var lista = new SelectList(new List<SelectListItem>() 
                { 
                    new SelectListItem() { Selected = true, Text = "Correo", Value = "1" },
                    new SelectListItem() { Selected = true, Text = "Correo + Adjunto", Value = "2" },
                    new SelectListItem() { Selected = true, Text = "Mensaje de Texto", Value = "3" }

                }, "Value", "Text");

                return lista;
            }
        }


        public SelectList Peridiocidad
        {
            get
            {
                var lista = new SelectList(new List<SelectListItem>() 
                { 
                    new SelectListItem() { Selected = true, Text = "Diario", Value = "1" },
                    new SelectListItem() { Selected = true, Text = "Semanal", Value = "2" },
                    new SelectListItem() { Selected = true, Text = "Quincenal", Value = "3" },
                    new SelectListItem() { Selected = true, Text = "Mensual", Value = "4" }

                }, "Value", "Text");

                return lista;
            }
        }
    }
}
