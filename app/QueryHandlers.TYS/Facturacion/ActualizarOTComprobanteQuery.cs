using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Facturacion.Parameters;
using QueryContracts.TYS.Facturacion.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.TYS.Facturacion
{
    public class ActualizarOTComprobanteQuery : IQueryHandler<ActualizarOTComprobanteParameters>
    {
        public QueryResult Handle(ActualizarOTComprobanteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                string query = "";
                if (parameters.fechacomprobante.HasValue)
                {
                    query = string.Format("update seguimiento.ordentrabajo"
                    + " set fechacomprobantepago = '{0}' ,"
                    + " facturado = 1,"
                    + " idestado = case when idestado = 21 then 25 else idestado end "
                    + " where idordentrabajo in ({1})",
                                   parameters.fechacomprobante.Value.ToString("M/d/yyyy HH:mm:ss"),
                                   parameters.idsordentrabajo);
                }
                else
                {
                    query = string.Format("update seguimiento.ordentrabajo set fechacomprobantepago = null , facturado = 0 where idordentrabajo in ({0})",
                                                                     parameters.idsordentrabajo);
                }

                var resultado = new ListarPendientePreliquidacionResult
                {
                    Hits = connection.Query<ListarPendientePreliquidacionDto>
                        (
                            query,
                            parametros,
                            commandType: CommandType.Text
                        ),
                };

                return resultado;
            }
        }
    }
}