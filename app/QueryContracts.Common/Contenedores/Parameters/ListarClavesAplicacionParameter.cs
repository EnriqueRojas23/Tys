
using System.ComponentModel;
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;

namespace QueryContracts.Core.Contenedores.Parameters
{
    public class ListarClavesAplicacionParameter : QueryParameter
    {
        public enum TipoValor
        {
            [StringValue("UNIDADTEMPERATURA")] 
            UMTemperatura = 0,
            [StringValue("TIPOCONDICIONORIGEN")]
            TipoCondicionOrigen = 1,
            [StringValue("TIPOCARGA")]
            TipoCarga = 2

        }
        public TipoValor TipoValorApp { get; set; }
    }
}
