﻿using QueryContracts.Common;
namespace QueryContracts.TYS.Ordenes.Parameters
{
    public class EliminarTablaOfisisParameter : QueryParameter
    {
        public string NroOrden { get; set; }
        public decimal Valing32 { get; set; }
        public decimal Valing32New { get; set; } 
    }
}
