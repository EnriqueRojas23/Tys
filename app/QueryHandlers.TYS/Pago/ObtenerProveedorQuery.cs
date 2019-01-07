
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Pago.Parameters;
using QueryContracts.TYS.Pago.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Pago
{
    public class ObtenerProveedorQuery : IQueryHandler<ObtenerProveedorParameters>
    {

        public QueryResult Handle(ObtenerProveedorParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.id);

                var resultado = new ObtenerProveedorResult();
                resultado = connection.Query<ObtenerProveedorResult>
                        (
                            "pago.pa_traerproveedor",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
