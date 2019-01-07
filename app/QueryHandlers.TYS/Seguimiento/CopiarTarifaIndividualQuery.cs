
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
    public class CopiarTarifaIndividualQuery : IQueryHandler<CopiarTarifaIndividualParameter>
    {

        public QueryResult Handle(CopiarTarifaIndividualParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtarifa", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtarifa);

                var resultado = new CopiarTarifaIndividualResult();
                resultado = connection.Query<CopiarTarifaIndividualResult>
                        (
                            "seguimiento.pa_copiartarifaindividual",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
