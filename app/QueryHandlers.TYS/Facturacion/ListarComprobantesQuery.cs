

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
    public class ListarComprobantesQuery : IQueryHandler<ListarComprobantesParameters>
    {
        public QueryResult Handle(ListarComprobantesParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("idestado", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestado);
                parametros.Add("idtipocomprobante", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idtipocomprobante);
                parametros.Add("numerocomprobante", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numerocomprobante);
                parametros.Add("numeroliquidacion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numeroliquidacion);
                parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecinicio);
                parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecfin);

                var resultado = new ListarComprobantesResult
                {
                    Hits = connection.Query<ListarComprobantesDto>
                        (
                            "facturacion.pa_listarcomprobante2",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };
                return resultado;
            }
        }
    }
}
