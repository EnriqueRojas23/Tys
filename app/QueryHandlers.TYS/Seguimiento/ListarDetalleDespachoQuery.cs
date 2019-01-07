
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ListarDetalleDespachoQuery : IQueryHandler<ListarDetalleDespachoParameters>
    {

        public QueryResult Handle(ListarDetalleDespachoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idvehiculo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idvehiculo);

                var resultado = new ListarDetalleDespachoResult
                {
                    Hits = connection.Query<ListarDetalleDespachoDto>
                        (
                            "seguimiento.pa_listardetalledespacho",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
