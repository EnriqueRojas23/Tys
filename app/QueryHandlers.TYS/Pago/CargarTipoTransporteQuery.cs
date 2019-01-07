
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
    public class CargarTipoTransporteQuery : IQueryHandler<CargarTipoTransporteParameter>
    {

        public QueryResult Handle(CargarTipoTransporteParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idetapa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idetapa);
                parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedor);

                var resultado = new CargarTipoTransporteResult
                {
                    Hits = connection.Query<CargarTipoTransporteDto>
                        (
                            "pago.pa_asignacionlistartipotransporte",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
