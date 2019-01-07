
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Contenedores.Results
{
    public class ListarDatosBookingAdjuntosResult : QueryResult
    {
        public IEnumerable<ListarDatosBookingAdjuntosDto> Hits { get; set; }
    }

    public class ListarDatosBookingAdjuntosDto
    {
        public long rba_int_id { get; set; }
        public long rb_int_id { get; set; }
        public long mlt_int_id_tipo_documento { get; set; }
        public string mlt_str_valor_tipo_documento { get; set; }
        public string rba_str_nombre_archivo { get; set; }
        public DateTime rba_dat_fecha_creacion { get; set; }
        public string rba_str_usuario_creacion { get; set; }
        public string mlt_str_tipo_documento_descripcion { get; set; }
        public string rb_str_numero_booking { get; set; }

    }
}
