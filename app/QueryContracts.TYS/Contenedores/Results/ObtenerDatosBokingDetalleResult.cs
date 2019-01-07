using System;
using System.Collections.Generic;
using QueryContracts.Common;

namespace QueryContracts.TYS.Contenedores.Results
{
    public class ObtenerDatosBokingDetalleResult : QueryResult
    {
        public IEnumerable<ObtenerDatosBokingDetalleDto> Hits { get; set; }
    }

    public class ObtenerDatosBokingDetalleDto
    {
      
        
        public long? rbd_int_id { get; set; }
        public long? rb_int_id { get; set; }
        public string rbd_str_matricula_equipamiento { get; set; }
        public string rbd_str_fecha_estimada { get; set; }
        public int rbd_int_tamanyo { get; set; }
        public string rbd_str_tipo { get; set; }
        public string rbd_str_precintos { get; set; }
        public string rbd_str_temperatura { get; set; }
        public string rbd_str_unidad_temperatura { get; set; }
        public string rbd_str_humedad { get; set; }
        public string rbd_str_ventilacion { get; set; }
        public string rbd_str_tipo_carga { get; set; }
        public string rbd_str_reefers { get; set; }
        public string rbd_str_estado_bookingdet { get; set; }
        public string rbd_str_estado_bookingdet_descripcion { get; set; }
        public string rbd_str_ent_cif_transportista { get; set; }
        public string rbd_str_ent_nombre_transportista { get; set; }
        public string rbd_str_trans_matricula_camion { get; set; }
        public string rbd_str_trans_nif_conductor { get; set; }
        public string rbd_str_trans_nombre_conductor { get; set; }
        public string rbd_str_fecha_recojo { get; set; }
        public string rbd_str_hora_recojo { get; set; }
        public DateTime rbd_dat_fecha_creacion { get; set; }
        public string rbd_str_usuario_creacion { get; set; }
        public string rbd_str_co2 { get; set; }
        public string rbd_str_o2 { get; set; }
        


    }

}
