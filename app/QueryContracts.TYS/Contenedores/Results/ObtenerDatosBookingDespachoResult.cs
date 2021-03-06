﻿

using QueryContracts.Common;
using System;
using System.Collections.Generic;
using Componentes.Common.Extensions;

namespace QueryContracts.TYS.Contenedores.Results
{
    public class ObtenerDatosBookingDespachoResult : QueryResult
    {
        public IEnumerable<ObtenerDatosBookingDespachoDto> Hits { get; set; }
    }

    public class ObtenerDatosBookingDespachoDto
    {

        public long rbd_int_id { get; set; }
        public long rb_int_id { get; set; }
        public string rbd_str_matricula_equipamiento { get; set; }
        public string rbd_str_fecha_estimada { get; set; }
        public string rbd_str_estado_bookingdet { get; set; }
        public string rbd_str_ent_cif_transportista { get; set; }
        public string rbd_str_ent_nombre_transportista { get; set; }
        public string rbd_str_trans_matricula_camion { get; set; }
        public string rbd_str_trans_nif_conductor { get; set; }
        public string rbd_str_trans_nombre_conductor { get; set; }
        public string rbd_str_fecha_recojo { get; set; }
        public string rbd_str_hora_recojo { get; set; }
        public string rb_str_numero_booking { get; set; }

        public DateTime? rbd_str_fecha_recojo_convertido 
        { 
            get  { return rbd_str_fecha_recojo.ToDate(); } 
        }

        public TimeSpan? rbd_str_hora_recojo_convertido
        {
            get { return rbd_str_hora_recojo.ToTime(); }
        }

    }
}
