
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
    public class CargarMonedasQuery : IQueryHandler<CargarMonedasParameter>
    {

        public QueryResult Handle(CargarMonedasParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtipocomprobante", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipocomprobante);
                parametros.Add("idtipotransporte", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipotransporte);
                parametros.Add("idetapa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idetapa);
                parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedor);


                var resultado = new CargarMonedasResult
                {
                    Hits = connection.Query<CargarMonedasDto>
                        (
                            "pago.pa_asignacionlistarmoneda",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
