
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
    public class ListarOperacionCargaQuery : IQueryHandler<ListarOperacionCargaParameters>
    {

        public QueryResult Handle(ListarOperacionCargaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("numcarga", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcarga);
                parametros.Add("idestado", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestado);
                parametros.Add("idestacionorigen", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestacion);
                


                var resultado = new ListarOperacionCargaResult
                {
                    Hits = connection.Query<ListarOperacionCargaDto>
                        (
                            "seguimiento.pa_listaroperacionescarga",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}

