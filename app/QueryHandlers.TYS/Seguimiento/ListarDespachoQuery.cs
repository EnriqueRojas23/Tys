
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
    public class ListarDespachoQuery : IQueryHandler<ListarDespachoParameters>
    {

        public QueryResult Handle(ListarDespachoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("numcarga", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcarga);
                parametros.Add("idestado", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestado);
                parametros.Add("idvehiculo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idvehiculo);

                var resultado = new ListarDespachoResult
                {
                    Hits = connection.Query<ListarDespachoDto>
                        (
                            "seguimiento.pa_listardespacho",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
