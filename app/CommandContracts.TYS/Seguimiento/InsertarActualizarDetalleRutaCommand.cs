﻿

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarDetalleRutaCommand : Command
    {
        public int? iddetalleruta { get; set; }
        public int idruta { get; set; }
        public int idorden { get; set; }
        public int idorigen { get; set; }
        public int? iddistrito { get; set; }
        public string km { get; set; }
        public int idtipotransporte { get; set; }
        public string leadida { get; set; }
        public string leadretorno { get; set; }
        public string leaddocumentario { get; set; }
        public string etapas { get; set; }
        public int iddepartamento { get; set; }
        public int? idprovincia { get; set; }
            

    }
}
