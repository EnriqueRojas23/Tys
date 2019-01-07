
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
    public class ObtenerUltimaOrdenCamionCompletoQuery : IQueryHandler<ObtenerUltimaOrdenCamionCompletoParameter>
    {

        public QueryResult Handle(ObtenerUltimaOrdenCamionCompletoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                //parametros.Add("idorden", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorden);

                var resultado = new ObtenerUltimaOrdenCamionCompletoResult();
                resultado = connection.Query<ObtenerUltimaOrdenCamionCompletoResult>
                        (
                            "seguimiento.pa_obtenerultimaordencamioncompleto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
