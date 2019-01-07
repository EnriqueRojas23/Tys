

using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Facturacion.Results;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ActualizarOTPlanificacionQuery : IQueryHandler<ActualizarOTPlanificacionParameters>
    {

        public QueryResult Handle(ActualizarOTPlanificacionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                string query = "";
                if (parameters.idcarga.HasValue)
                {
                    query = string.Format("update seguimiento.ordentrabajo set idcarga = {0} , idestado = {1} , idtipooperacion = {2}, idruta = {3}, idestaciondestino = {4}, idagencia = {5}, fechaplanificacion = '{6}'  where idordentrabajo in ({7})",
                                   parameters.idcarga.Value.ToString(),
                                   parameters.idestado.ToString(),
                                   parameters.idtipooperacion.ToString(),
                                   
                                   (parameters.idruta.HasValue?parameters.idruta.ToString():"null"),
                                   (parameters.idestaciondestino.HasValue ? parameters.idestaciondestino.ToString() : "null"),
                                   (parameters.idagencia.HasValue ? parameters.idagencia.ToString() : "null"),

                                   parameters.fechaplanificacion.ToString("M/d/yyyy HH:mm:ss"),
                                   parameters.idsordentrabajo);
                }
                else
                {
                    query = string.Format("update seguimiento.ordentrabajo set fechacomprobantepago = null where idordentrabajo in ({0})",
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
