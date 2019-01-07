
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Monitoreo
{
    public class InsertarDetalleEmbarqueQuery : IQueryHandler<InsertarDetalleEmbarqueParameter>
    {

        public QueryResult Handle(InsertarDetalleEmbarqueParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
               // connection.Open();
                DataTable dt = crear_DataTable("monitoreo.detalleembarque");

                var objetapas = parameters.Hits;
                foreach (var item in objetapas)
                {
                    DataRow dr = dt.NewRow();
                    //if(item.fechacontrolsunat.HasValue)
                    //  dr["fechacontrolsunat"] = item.fechacontrolsunat;
                    //else
                    //    dr["fechacontrolsunat"] = DBNull.Value;

                    //if (item.fechacontrolsunat.HasValue)
                    //    dr["fechadescarga"] = item.fechacontrolsunat;
                    //else
                    //    dr["fechadescarga"] = DBNull.Value;

                    dr["idembarque"] = item.idembarque;

                    dr["embarquecompleto"] = false;

                    dr["idordentrabajo"] = item.idordentrabajo;
                    //dr["idpuerto"] = item.idpuerto;
                    //dr["idusuariocontrolsunat"] = item.idusuariocontrolsunat;
                    //dr["idusuariodescarga"] = item.idusuariodescarga;
                    dt.Rows.Add(dr);
                }

                using (SqlBulkCopy s = new SqlBulkCopy(connection))
                {
                    s.DestinationTableName = dt.TableName;

                    foreach (var column in dt.Columns)
                    {
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    }

                    s.WriteToServer(dt);
                }
                var result = new InsertarEtapaResult { respuesta = true };
                return result;
            }
        }
        private static DataTable crear_DataTable(string tabla)
        {

            DataTable dt = new DataTable(tabla);

            DataColumn idembarque = new DataColumn();
            idembarque.DataType = Type.GetType("System.Int64");
            idembarque.ColumnName = "idembarque";
            dt.Columns.Add(idembarque);

            //DataColumn idmanifiesto = new DataColumn();
            //idmanifiesto.DataType = Type.GetType("System.Int64");
            //idmanifiesto.ColumnName = "idmanifiesto";

            //dt.Columns.Add(idmanifiesto);

            DataColumn idordentrabajo = new DataColumn();
            idordentrabajo.DataType = Type.GetType("System.Int64");
            idordentrabajo.ColumnName = "idordentrabajo";
            dt.Columns.Add(idordentrabajo);


            DataColumn embarquecompleto = new DataColumn();
            embarquecompleto.DataType = Type.GetType("System.Boolean");
            embarquecompleto.ColumnName = "embarquecompleto";
            dt.Columns.Add(embarquecompleto);


            //DataColumn idpuerto = new DataColumn();
            //idpuerto.DataType = Type.GetType("System.Int32");
            //idpuerto.ColumnName = "idpuerto";
            //dt.Columns.Add(idpuerto);

            //DataColumn fechadescarga = new DataColumn();
            //fechadescarga.DataType = Type.GetType("System.DateTime");
            //fechadescarga.ColumnName = "fechadescarga";
            //dt.Columns.Add(fechadescarga);


            //DataColumn idusuariodescarga = new DataColumn();
            //idusuariodescarga.DataType = Type.GetType("System.Int32");
            //idusuariodescarga.ColumnName = "idusuariodescarga";
            //dt.Columns.Add(idusuariodescarga);


            //DataColumn idusuariocontrolsunat = new DataColumn();
            //idusuariocontrolsunat.DataType = Type.GetType("System.Int32");
            //idusuariocontrolsunat.ColumnName = "idusuariocontrolsunat";
            //dt.Columns.Add(idusuariocontrolsunat);

            //DataColumn fechacontrolsunat = new DataColumn();
            //fechacontrolsunat.DataType = Type.GetType("System.DateTime");
            //fechacontrolsunat.ColumnName = "fechacontrolsunat";
            //dt.Columns.Add(fechacontrolsunat);


            return dt;
        }
    }
}
