using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace QueryHandlers.TYS.Ordenes
{
    public class AgregarDetalleQuery: IQueryHandler<AgregarDetalleParameter>
    {
        public QueryContracts.Common.QueryResult Handle(AgregarDetalleParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("sNroCar_", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NroCar);
                parametros.Add("pNroOr32", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NroOr32);
                parametros.Add("sNroSec_", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NroSec);
                parametros.Add("sSucursal_", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Sucursal);
                parametros.Add("pValing32", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: parameters.Valing32);

                var result = connection.Query<AgregarDetalleResult>(
                                    "ordenes.guardar_detalles",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).LastOrDefault();
                return result;
            }
        }
    }
}
