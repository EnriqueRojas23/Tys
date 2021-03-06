﻿using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDetalleOperacionResult : QueryResult
    {
        public IEnumerable<ListarDetalleOperacionDto> Hits { get; set; }
    }
    public class ListarDetalleOperacionDto
    {
            public long idordentrabajo { get; set; }
            public string numcp { get; set; }
            public string destino { get; set; }
            public string origen { get; set; }
            public string remitente { get; set; }
            public int bulto { get; set; }
            public string destinatario { get; set; }
            public decimal peso { get; set; }
            public decimal volumen { get; set; }
            public string nummanifiesto { get; set; }
            public string tipooperacion { get; set; }
            public int idruta { get; set; }
            public int idorigen { get; set; }
            public int? iddestino { get; set; }
            public int idposicion { get; set; }
            public long iddespacho { get; set; }
            public long idmanifiesto { get; set; }
            public int idtipooperacion { get; set; }
            public long idcarga { get; set; }

    }
}


