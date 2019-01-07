

using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Facturacion.Parameters;
using QueryContracts.TYS.Facturacion.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Facturacion
{
    public class ObtenerComprobanteQuery : IQueryHandler<ObtenerComprobanteParameters>
    {

        public QueryResult Handle(ObtenerComprobanteParameters parameters)
        {                         
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idpreliquidacion", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idpreliquidacion);
                parametros.Add("idcomprobante", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idcomprobante);

                var resultado = new ObtenerComprobanteResult();
                resultado = connection.Query<ObtenerComprobanteResult>
                    (
                        "facturacion.pa_obtenercomprobante",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    ).LastOrDefault();

                return resultado;
            }
        }
    }
}
