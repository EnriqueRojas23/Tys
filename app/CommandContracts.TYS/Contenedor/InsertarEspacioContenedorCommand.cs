
using System;
using System.ComponentModel.DataAnnotations;
using CommandContracts.Common;
using Componentes.Common.Extensions;

namespace CommandContracts.TYS.Contenedor
{
    public class InsertarEspacioContenedorCommand : Command
    {
        public long rb_int_id { get; set; }

        public decimal? rb_int_identificador_terminal { get; set; }

        [Display(Name = "Nro Contenedores")]
        public int rb_int_nro_contenedores { get; set; }

        ////[Display(Name = "Equipamiento")]
        ////public string rbd_str_matricula_equipamiento { get; set; }

        [Display(Name = "Fecha Estimada")]
        public DateTime? rbd_dat_fecha_estimada 
        {
            get { return rbd_str_formato_fecha_estimada.ToDate(); }
            set { rbd_str_formato_fecha_estimada = value.HasValue ? value.Value.ToString("yyyyMMdd") : null; }  
        }

        public string rbd_str_formato_fecha_estimada { get; set; }

        public string rbd_str_formato_hora_estimada { get; set; }


        [Display(Name = "Tamaño")]
        public int rbd_int_tamanyo { get; set; }

        [Display(Name = "Tipo Contenedor")]
        public string rbd_str_tipo { get; set; }

        [Display(Name = "Temperatura")]
        public string rbd_str_temperatura { get; set; }

        [Display(Name = "UM Temperatura")]
        public string rbd_str_unidad_temperatura { get; set; }

        [Display(Name = "Ventilación")]
        public string rbd_str_ventilacion { get; set; }

        [Display(Name = "Humedad")]
        public string rbd_str_humedad { get; set; }

        [Display(Name = "Tipo Carga")]
        public string rbd_str_tipo_carga { get; set; }

        public string rbd_str_usuario_creacion { get; set; }

        [Display(Name = "CO2      ")]
        public string rbd_str_co2 { get; set; }


        [Display(Name = "O2      ")]
        public string rbd_str_o2 { get; set; }
      


    }
}
