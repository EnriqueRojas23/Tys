

using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarLiquidacionOperacionesResults : QueryResult
    {
        public IEnumerable<ListarLiquidacionOperacionesDto> Hits { get; set; }
        
    }
    public class ListarLiquidacionOperacionesDto
    {
         

        public long idordentrabajo { get; set; }
        [Export(Cabecera = "OT", Orden = 1, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string numcp { get; set; }
        [Export(Cabecera = "GRT", Orden = 2, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string guiatransportista { get; set; }
        [Export(Cabecera = "Remitente", Orden = 3, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string remitente { get; set; }
        [Export(Cabecera = "Fecha Recojo", Orden = 4, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public DateTime fecharecojo { get; set; }
        [Export(Cabecera = "Fecha Despacho", Orden = 5, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public DateTime fechadespacho { get; set; }
        [Export(Cabecera = "Destino", Orden = 6, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string destino { get; set; }
        [Export(Cabecera = "Coordinador", Orden = 7, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string coordinador { get; set; }
        [Export(Cabecera = "Ultima Etapa", Orden = 8, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string UltimaIncidencia { get; set; }
        [Export(Cabecera = "Lead Documentario", Orden = 9, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string LeadDocumentario { get; set; }
        [Export(Cabecera = "Dias Transcurridos", Orden = 10, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public int DiasTranscurridos { get; set; }
        [Export(Cabecera = "Destinatario", Orden = 11, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string destinatario { get; set; }
        [Export(Cabecera = "Concepto Cobro", Orden = 12, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string conceptocobro { get; set; }

    }
}
