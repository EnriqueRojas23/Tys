

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
    public class ActualizarOTConfirmacionQuery : IQueryHandler<ActualizarOTConfirmacionParameters>
    {

        public QueryResult Handle(ActualizarOTConfirmacionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                string query = "";

                query = string.Format("update seguimiento.ordentrabajo set  idestado = {0} , fechaconfirmacion = '{1}'  where idordentrabajo in ({2})",
                                   parameters.idestado.ToString(),
                                   parameters.fechaconfirmacion.ToString("M/d/yyyy HH:mm:ss"),
                                   parameters.idsordentrabajo);
             


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
