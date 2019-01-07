
using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.Contenedores.Parameters;
using QueryContracts.Core.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;

namespace QueryHandlers.Core.Contenedores
{
    public class insetarDocumentosSolportQuery : IQueryHandler<InsertarDocumentosSolportParameter>
    {
        public QueryResult Handle(InsertarDocumentosSolportParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdReserva", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdReserva);
                parametros.Add("Usuario", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Usuario);
                parametros.Add("Fichero", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Fichero);
                parametros.Add("DesFichero", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.DesFichero);

                var resultado = new insertarDocumentosSolportResult();
                var multiquery = connection.QueryMultiple
                  (
                      commandType: CommandType.StoredProcedure,
                      sql: "usp_NOL_RegistraRutaArchivosCargados",
                      param: parametros
                  );

                return resultado;
            }
        }
    }
}
