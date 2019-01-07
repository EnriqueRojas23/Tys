
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
    public class ListarDetalleOperacionQuery : IQueryHandler<ListarDetalleOperacionParameters>
    {

        public QueryResult Handle(ListarDetalleOperacionParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcarga", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idcarga);

                var resultado = new ListarDetalleOperacionResult
                {
                    Hits = connection.Query<ListarDetalleOperacionDto>
                        (
                            "seguimiento.pa_listarordentrabajooperacion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
