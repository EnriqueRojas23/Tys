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
    public class ListarEmbarqueFluvialQuery : IQueryHandler<ListarEmbarqueFluvialParameters>
    {

        public QueryResult Handle(ListarEmbarqueFluvialParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idtransporte", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idvehiculo);
                parametros.Add("numeroembarque", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numeroembarque);


                var resultado = new ListarEmbarqueFluvialResults
                {
                    Hits = connection.Query<ListarEmbarqueFluvialDto>
                    (
                       "monitoreo.pa_listaembarque",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
