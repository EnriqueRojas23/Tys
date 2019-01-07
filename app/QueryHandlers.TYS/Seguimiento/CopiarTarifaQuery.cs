
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Seguimiento
{
    public class CopiarTarifaQuery : IQueryHandler<CopiarTarifaParameter>
    {

        public QueryResult Handle(CopiarTarifaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtarifa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtarifa);
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);

                var resultado = new CopiarTarifaResult();
                resultado = connection.Query<CopiarTarifaResult>
                        (
                            "seguimiento.pa_copiartarifa",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
