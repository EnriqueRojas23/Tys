

using Data.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Ordenes
{
    public class ListarHBLQuery : IQueryHandler<ListarHBLParameter>
    {
        public QueryContracts.Common.QueryResult Handle(ListarHBLParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("sNavVia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.viaje);
                parametros.Add("sRucFwd", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.consolidador);
                parametros.Add("sCodCon", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.contenedor);
                parametros.Add("nav", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nave);
                parametros.Add("numvia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numvia);


                var resultado = new ListarHBLResult
                {
                    Hits = connection.Query<ListarHBLDto>
                        (
                            "ordenes.pa_listarHBL",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                var resultadofinal = (from r in resultado.Hits
                                      select new ListarHBLDto
                                      {
                                          IdOrden = r.IdOrden,
                                          Itrm = "OS",
                                          Moneda = "DOL",
                                          NroCon12 = r.NroCon12,
                                          NroDet12 = r.NroDet12,
                                          NroItem13 = r.NroItem13,
                                          NroOrs32 = r.NroOrs32,
                                          NumeroRuc = parameters.consolidador,
                                          Opera = "IL",
                                          Sucursal = r.Sucursal,
                                          Syst = "gwctest_sys",
                                          User = "gwctest",
                                          Valing32 = r.Valing32,
                                          Valing32_2 = r.Valing32_2,
                                          navvia11 = r.navvia11,
                                          CodCon = r.CodCon,
                                          nav = r.nav,
                                          numvia = r.numvia,
                                          MontoTotal = (Convert.ToDecimal(r.Valing32)  
                                          + Convert.ToDecimal(r.Valing32_2 == null? 0  :Convert.ToDecimal((r.Valing32_2)))).ToString()
                                           
                                          

                                      }).ToList();

                resultado.Hits = resultadofinal;
                return resultado;
            }
        }
    }
}
