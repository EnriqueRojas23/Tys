

using QueryContracts.Common;
namespace QueryContracts.TYS.Ordenes.Parameters
{
    public class RegistrarOrdenServicioParameter : QueryParameter
    {
        public string CodNave { get; set; }
        public string Numviaje { get; set; }
        public string NavVia { get; set; }
        public string Ruc { get; set; }
        public string CodContenedor { get; set; }
        public string UserDB { get; set; }
        public string Perfil { get; set; }
        public string IMPINI { get; set; }
        public string  IMPFIN { get; set; }
        public string nroDet12 { get; set; }
        public string nroOrden { get; set; }



    }
}
