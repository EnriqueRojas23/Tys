
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
    public class ListarImpresionManifiestoQuery : IQueryHandler<ListarImpresionManifiestoParameters>
    {

        public QueryResult Handle(ListarImpresionManifiestoParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("numgrt", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numgrt);
                parametros.Add("nummanifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nummanifiesto);
                parametros.Add("numhojaruta", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numhojaruta);
                parametros.Add("numcarga", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcarga);
                parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecinicio);
                parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fecfin);


                var resultado = new ListarImpresionManifiestoResult
                {
                    Hits = connection.Query<ListarImpresionManifiestoDto>
                        (
                            "seguimiento.pa_vistareimpresionxmanifiesto",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
