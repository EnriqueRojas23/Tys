
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
    public class ListarOrdenTrabajoComprobanteQuery : IQueryHandler<ListarOrdenTrabajoComprobanteParameters>
    {

        public QueryResult Handle(ListarOrdenTrabajoComprobanteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("guia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.guia);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("idvehiculo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idvehiculo);

                var resultado = new ListarOrdenTrabajoComprobanteResult
                {
                    Hits = connection.Query<ListarOrdenTrabajoComprobanteDto>
                        (
                            "seguimiento.pa_listarordentrabajocomprobante",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
