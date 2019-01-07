
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarMonitoreoOperacionesResults : QueryResult
    {
        public IEnumerable<ListarMonitoreoOperacionesDto> Hits { get; set; }
        
    }
    public class ListarMonitoreoOperacionesDto
    {
        public long idordentrabajo { get; set; }
        public string nummanifiesto { get; set; }
        [Export(Cabecera = "OT", Orden = 1, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string numcp { get; set; }
        [Export(Cabecera = "Fecha de Registro", Orden = 2, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public DateTime fecregistro { get; set; }
        [Export(Cabecera = "Destino", Orden = 3, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string destino { get; set; }
        [Export(Cabecera = "Origen", Orden = 4, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string origen { get; set; }
        [Export(Cabecera = "Modo Transporte", Orden = 5, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string tipotransporte { get; set; }
        [Export(Cabecera = "Remitente", Orden = 6, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string remitente { get; set; }
        [Export(Cabecera = "Bultos", Orden = 7, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public int bulto { get; set; }
        [Export(Cabecera = "Peso", Orden = 8, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public decimal peso { get; set; }
        [Export(Cabecera = "Volumen", Orden = 9, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public decimal volumen { get; set; }
        [Export(Cabecera = "Destinatario", Orden = 10, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string destinatario { get; set; }
        [Export(Cabecera = "Tipo Operación", Orden = 11, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string tipooperacion { get; set; }

        [Export(Cabecera = "Recursos", Orden = 12, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string recursos { get; set; }


        

        [Export(Cabecera = "Documentos", Orden = 13, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string documentos { get; set; }

        [Export(Cabecera = "Cod. Origen", Orden = 14, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string coddireccionorigen { get; set; }
        [Export(Cabecera = "Dirección Destino", Orden = 15, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string coddirecciondestino { get; set; }
        [Export(Cabecera = "Tipo Carga", Orden = 16, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string descripciongeneral { get; set; }
        [Export(Cabecera = "Fecha Salida", Orden = 17, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public DateTime fechasalida { get; set; }
        [Export(Cabecera = "Nro Guia", Orden = 18, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string nroguia { get;set; }
        [Export(Cabecera = "Placa", Orden = 19, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string placa { get; set; }
        
        [Export(Cabecera = "UltimoRecursos", Orden = 20, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string UltimoRecurso { get; set; }
        [Export(Cabecera = "UltimoRecursos", Orden = 21, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string UltimoDocumento { get; set; }


        public int idestado { get; set; }
    }
}
