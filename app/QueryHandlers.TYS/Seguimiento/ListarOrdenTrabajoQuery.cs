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
    public class ListarOrdenTrabajoQuery : IQueryHandler<ListarOrdenTrabajoParameters>
    {
        public QueryResult Handle(ListarOrdenTrabajoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecinicio);
                parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecfin);
                parametros.Add("idestacion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestacion);

                var resultado = new ListarOrdenTrabajoResult
                {
                    Hits = connection.Query<ListarOrdenTrabajoDto>
                        (
                            "seguimiento.pa_listarordentrabajo",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}