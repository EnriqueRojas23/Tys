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
    public class ObtenerFactElectronicaQuery : IQueryHandler<ObtenerFactElectronicaParameters>
    {
        public QueryResult Handle(ObtenerFactElectronicaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("docentry", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.DocEnty);

                var resultado = new ObtenerFactElectronicaResult();
                resultado = connection.Query<ObtenerFactElectronicaResult>
                        (
                            "facturacion.obtenerfactelectronica",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}