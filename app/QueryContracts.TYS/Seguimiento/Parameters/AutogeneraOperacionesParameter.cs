
using QueryContracts.Common;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class AutogeneraOperacionesParameter : QueryParameter
    {
        public long idordentrabajo { get; set; }
        public decimal vol { get; set; }
        public decimal peso { get; set; }
        public int? idproveedor { get; set; }
        public int idvehiculo { get; set; }
        public int? idruta { get; set; }
        public int idusuarioregistro { get; set; }
        public long? idcarga { get; set; }
        public long? iddespacho { get; set; }
        public long? idmanifiesto { get; set; }
    }
}
