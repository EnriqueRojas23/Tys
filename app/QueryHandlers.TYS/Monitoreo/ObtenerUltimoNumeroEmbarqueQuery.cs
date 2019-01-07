using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Monitoreo
{
    public class ObtenerUltimoNumeroEmbarqueQuery : IQueryHandler<ObtenerUltimoNumeroEmbarqueParameters>
    {

        public QueryResult Handle(ObtenerUltimoNumeroEmbarqueParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();


                var resultado = new ObtenerUltimoNumeroEmbarqueResults();
                resultado =  connection.Query<ObtenerUltimoNumeroEmbarqueResults>
                    (
                       "monitoreo.pa_obtenerultimonumeroembarque",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                
                return resultado;
            }
        

        }
    }
}
