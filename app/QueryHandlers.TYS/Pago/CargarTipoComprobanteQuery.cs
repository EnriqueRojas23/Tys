
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Pago.Parameters;
using QueryContracts.TYS.Pago.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Pago
{
    public class CargarTipoComprobanteQuery : IQueryHandler<CargarTipoComprobanteParameter>
    {

        public QueryResult Handle(CargarTipoComprobanteParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtipotransporte", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipotransporte);
                parametros.Add("idetapa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idetapa);
                parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedor);

                var resultado = new CargarTipoComprobanteResult
                {
                    Hits = connection.Query<CargarTipoComprobanteDto>
                        (
                            "pago.pa_asignacionlistarcomprobante",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
