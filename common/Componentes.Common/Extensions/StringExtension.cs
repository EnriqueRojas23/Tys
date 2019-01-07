
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace Componentes.Common.Extensions
{
    public static class StringExtension
    {
        public static bool IsFormatSearch(this String str)
        {
            char[] caracteres = { ':', '{', '}' };
            var pos = str.IndexOfAny(caracteres);
            return pos > 0;
        }
        public static Dictionary<string, string> FormatSearch(this String str)
        {
            var resultado = new Dictionary<string, string>();
            if (!str.IsFormatSearch()) return null;

            char[] caracteres = { '{', '}' };
            string[] filtros = str.Split(caracteres);

            //quitando los espacios en blanco
            filtros = filtros.Where(x => String.IsNullOrWhiteSpace(x) == false).ToArray();
            foreach (var s in filtros)
            {
                var array1 = s.Split(new char[] { ':' });
                if (array1.Length > 0)
                {
                    resultado.Add(array1[0], array1[1]);
                }
            }

            return resultado;

        }
        public static DateTime? ToDate(this string probDate)
        {
            if (string.IsNullOrWhiteSpace(probDate)) return null;
            if (probDate.Length < 8) throw new FormatException("La longitud del parametro no cumple con lo requerido");
            DateTime converted;
            if (!DateTime.TryParseExact(probDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out converted))
            {
                throw new FormatException("La fecha no cumple con el formato requerido ");
            }
            return converted;
        }


        public static TimeSpan? ToTime(this string probTime)
        {
            if (string.IsNullOrWhiteSpace(probTime)) return null;
            if (probTime.Length < 6) throw new FormatException("La longitud del parametro no cumple con lo requerido");

            string[] format = { "hhmmss" };
            TimeSpan converted;
            if (TimeSpan.TryParseExact(probTime, format, CultureInfo.InvariantCulture, TimeSpanStyles.None, out converted))
            {
                throw new FormatException("La fecha no cumple con el formato requerido ");
            }
            return converted;
        }
    }
}
