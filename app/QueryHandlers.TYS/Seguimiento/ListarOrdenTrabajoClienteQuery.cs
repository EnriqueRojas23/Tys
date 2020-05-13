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
    public class ListarOrdenTrabajoClienteQuery : IQueryHandler<ListarOrdenTrabajoClienteParameters>
    {
        public QueryResult Handle(ListarOrdenTrabajoClienteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idscliente);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecinicio);
                parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecfin);
                parametros.Add("grr", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.grr);
                parametros.Add("docgeneral", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.docreferencia);
                parametros.Add("idestado", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idestado);
                parametros.Add("iddestino", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.iddestino);

                var resultado = new ListarOrdenTrabajoClienteResult
                {
                    Hits = connection.Query<ListarOrdenTrabajoClienteDto>
                        (
                            "seguimiento.pa_listarordentrabajo_cliente",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}