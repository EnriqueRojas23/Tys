
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
    public class ObtenerCamionCompletoQuery : IQueryHandler<ObtenerCamionCompletoParameter>
    {

        public QueryResult Handle(ObtenerCamionCompletoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcamioncompleto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idcamioncompleto);

                var resultado = new ObtenerCamionCompletoResult();
                resultado = connection.Query<ObtenerCamionCompletoResult>
                        (
                            "seguimiento.pa_obtenercamioncompleto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
