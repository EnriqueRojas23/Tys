using QueryContracts.Common;


namespace QueryContracts.TYS.Pago.Parameters
{
    public class ValidaSerieParameters : QueryParameter
    {
        public string serie { get; set; }
        public int? idetapa { get; set; }
        public int? idtipocomprobante { get; set; }
        public int? idproveedor { get; set; }
        public long? idcomprobante { get; set; }
    }
}
