
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
    public class ObtenerGuiaQuery : IQueryHandler<ObtenerGuiaParameter>
    {

        public QueryResult Handle(ObtenerGuiaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("nroguia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numguia);
                parametros.Add("idguiaremisioncliente", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idguiaremisioncliente);


                var resultado = new ObtenerGuiaResult();
                resultado = connection.Query<ObtenerGuiaResult>
                        (
                            "seguimiento.pa_traernroguia",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
