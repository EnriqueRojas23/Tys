

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
    public class ObtenerUltimaPreliquidacionQuery : IQueryHandler<ObtenerUltimaPreliquidacionParameters>
    {

        public QueryResult Handle(ObtenerUltimaPreliquidacionParameters parameters)
        {                         
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                var resultado = new ObtenerUltimaPreliquidacionResult();
                resultado = connection.Query<ObtenerUltimaPreliquidacionResult>
                    (
                        "facturacion.pa_obtenerultimapreliquidacion",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    ).LastOrDefault();

                return resultado;
            }
        }
    }
}
