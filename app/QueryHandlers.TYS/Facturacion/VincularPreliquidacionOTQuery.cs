

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
    public class VincularPreliquidacionOTQuery : IQueryHandler<VincularPreliquidacionOTParameters>
    {

        public QueryResult Handle(VincularPreliquidacionOTParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                string query = "";
                if (parameters.idpreliquidacion.HasValue)
                {
                    query = string.Format("update seguimiento.ordentrabajo set idpreliquidacion = {0} where idordentrabajo in ({1})",
                                   parameters.idpreliquidacion.ToString(),
                                   parameters.idsordentrabajo);
                }
                else
                {
                    query = string.Format("update seguimiento.ordentrabajo set idpreliquidacion = null where idordentrabajo in ({0})",
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
