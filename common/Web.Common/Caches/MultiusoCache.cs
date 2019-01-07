
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QueryContracts.Common;
using ServiceAgents.Common;
using log4net;
using QueryContracts.Common.Configuracion.Results;
using QueryContracts.Common.Configuracion.Parameters;

namespace Web.Common.Caches
{
    public class MultiusoCache
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static volatile List<Multiuso> multiuso;
        private static object sync = new Object();

        public static IEnumerable<Multiuso> Multiuso
        {
            get
            {
                //FillMultiuso();

                if (multiuso == null)
                {
                    log.Info("Primera Carga Multiuso");
                    lock (sync)
                    {
                        log.Info("Entro en el Lock");
                        var result = (MultiusoResult)(new MultiusoParameter()).Execute();
                        if (result == null) log.Warn("No hay Multiuso en Base de Datos");
                        var collection = result.Hits;
                        if (collection != null && collection.Any())
                        {
                            multiuso = (from m in collection
                                        select new Multiuso
                                        {
                                            mlt_dat_fecha_creacion = m.mlt_dat_fecha_creacion,
                                            mlt_int_id = m.mlt_int_id,
                                            mlt_int_id_padre = m.mlt_int_id_padre,
                                            mlt_str_alcance = m.mlt_str_alcance,
                                            mlt_str_descripcion = m.mlt_str_descripcion,
                                            mlt_str_nombre = m.mlt_str_nombre,
                                            mlt_str_usuario_creacion = m.mlt_str_usuario_creacion,
                                            mlt_str_valor = m.mlt_str_valor
                                        }).ToList();
                        }
                        log.Info("Fin en el Lock");

                    }
                }
                log.Info("Respuesta Lock");
                return multiuso;
            }
        }

        public static void RegenerarMultiuso()
        {
            multiuso = null;
            log.Info("MULTIUSO ReLoad");
           
        }

     

    }


}
