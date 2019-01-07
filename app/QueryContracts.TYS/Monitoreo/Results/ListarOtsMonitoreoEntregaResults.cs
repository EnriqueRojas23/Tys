
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarOtsMonitoreoEntregaResults : QueryResult
    {
        public IEnumerable<ListarOtsMonitoreoEntregaDto> Hits { get; set; }
        
    }
    public class ListarOtsMonitoreoEntregaDto
    {
        public long idordentrabajo { get; set; }
        public string numcp { get; set; }
        public int iddestino { get; set; }
        public string distritodestino { get; set; }
        public int idmanifiesto { get; set; }
        public string nummanifiesto { get; set; }

        public DateTime? fechadespacho { get; set; }
        public string horadespacho { get; set; }
        public DateTime fecharegistro { get; set; }
        public DateTime? fechaentrega { get; set; }
        public string horaentrega { get; set; }

        public int bulto { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public string origen { get; set; }
        public string tienda { get; set; }
        public string tipotransporte { get; set; }
        public string grr { get; set; }
        public string remitente { get; set; }
        public string destinatario { get; set; }
        public string direcciondestino { get; set; }
        public string tipooperacion { get; set; }
        public string placa { get; set; }
        public string ultimaetapa { get; set; }
        public string ultimorecurso { get; set; }
        public string ultimodocumento { get; set; }
        public string reintegro { get; set; }
        public string estacionorigen { get; set; }
        public string Incidencia { get; set; } // si o no
        public string entrega { get; set; }
        public int? idmaestroetapa { get; set; }
        public DateTime? fechaarribo { get; set; }
        public DateTime? fechaconocimientoembarque { get; set; }
        public DateTime? fechadesembarque { get; set; }
        public DateTime? fechaembarque { get; set; }
        public DateTime? fechallegadaalmacen { get; set; }
        public DateTime? fechallegadapuerto { get; set; }
        public DateTime? fechasalidadepuerto { get; set; }
        public string numeroconocimiento { get; set; }
  


    }
}
