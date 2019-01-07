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
    public class ObtenerPreliquidacionQuery : IQueryHandler<ObtenerPreliquidacionParameters>
    {
        public QueryResult Handle(ObtenerPreliquidacionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idpreliquidacion", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idpreliquidacion);

                var resultado = new ObtenerPreliquidacionResult
                {
                    Hits = connection.Query<ObtenerPreliquidacionDto>
                        (
                            "facturacion.pa_obtenerliquidacion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}