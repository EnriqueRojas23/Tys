
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
    public class AutogeneraOperacionesQuery : IQueryHandler<AutogeneraOperacionesParameter>
    {

        public QueryResult Handle(AutogeneraOperacionesParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idordentrabajo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idordentrabajo);
                parametros.Add("vol", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: parameters.vol);
                parametros.Add("peso", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: parameters.peso);
                parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedor);
                parametros.Add("idvehiculo", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idvehiculo);
                parametros.Add("idruta", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idruta);
                parametros.Add("idusuarioregistro", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idusuarioregistro);
                parametros.Add("idcarga", dbType: DbType.Int64, direction: ParameterDirection.Output, value: parameters.idcarga);
                parametros.Add("iddespacho", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.iddespacho);
                parametros.Add("idmanifiesto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idmanifiesto);


                var resultado = new AutogeneraOperacionesResult();
                resultado = connection.Query<AutogeneraOperacionesResult>
                        (
                            "seguimiento.pa_autogeneraroperaciones",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
