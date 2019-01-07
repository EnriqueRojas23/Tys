

using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Ordenes
{
    public class RegistrarOrdenServicioQuery : IQueryHandler<RegistrarOrdenServicioParameter>
    {
        public QueryResult Handle(RegistrarOrdenServicioParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CodNave", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CodNave);
                parametros.Add("Numviaje", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Numviaje.Trim());
                parametros.Add("NavVia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NavVia);
                parametros.Add("Ruc", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Ruc);
                parametros.Add("CodContenedor", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CodContenedor);
                parametros.Add("UserDB", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.UserDB);
                parametros.Add("Perfil", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Perfil);
                parametros.Add("ini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.IMPINI);
                parametros.Add("fin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.IMPFIN);
                parametros.Add("nrode", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nroDet12);

                var resultado = new RegistrarOrdenServicioResult();
                resultado = connection.Query<RegistrarOrdenServicioResult>
                        (
                            "ordenes.pa_RegistrarOrdenServicio",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();


                return resultado;
            }
        }
    }
}
