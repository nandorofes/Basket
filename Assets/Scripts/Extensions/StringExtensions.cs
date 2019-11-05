using System;
using System.Globalization;

namespace Extensions.System.String
{
    public static class StringExtensions
    {
        /// <summary>
        /// Devuelve una copia de este objeto System.String con su primer carácter convertido a mayúsculas, aplicando
        /// las reglas de mayúsculas y minúsculas de la referencia cultural invariante.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Capitalize(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length == 1) return s.ToUpperInvariant();
                return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1);
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con su primer carácter convertido a mayúsculas, aplicando
        /// las reglas de mayúsculas y minúsculas de la referencia cultural especificada.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Capitalize(this string s, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length == 1) return s.ToUpper(cultureInfo);
                return s.Substring(0, 1).ToUpper(cultureInfo) + s.Substring(1);
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con uno o más de los primeros caracteres que contenía
        /// descartados. Si se especifica un número de caracteres a descartar mayor que la longitud del objeto
        /// System.String actual, se devuelve la cadena vacía.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        /// <returns></returns>
        public static string DropFirst(this string s, int charCount)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // A partir de aquí, la cadena no es nula y tiene al menos longitud 1.
                int resultLength = s.Length - charCount;
                if (resultLength > 0)
                    return s.Substring(charCount, s.Length - charCount);
                return string.Empty;
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con uno o más de los últimos caracteres que contenía
        /// descartados. Si se especifica un número de caracteres a descartar mayor que la longitud del objeto
        /// System.String actual, se devuelve la cadena vacía.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        /// <returns></returns>
        public static string DropLast(this string s, int charCount)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // A partir de aquí, la cadena no es nula y tiene al menos longitud 1.
                int resultLength = s.Length - charCount;
                if (resultLength > 0)
                    return s.Substring(0, s.Length - charCount);
                return string.Empty;
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con todos los caracteres en orden inverso.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount"></param>
        /// <returns></returns>
        public static string Reverse(this string s)
        {
            if (s.Length <= 1) return s;
            
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Genera una cadena de texto aleatoria con la longitud especificada y el número de palabras especificado.
        /// </summary>
        /// <param name="length">Longitud de la cadena de texto generada.</param>
        /// <param name="wordCount">Número de palabras.</param>
        public static string Placeholder(int length, int wordCount)
        {
            Random r = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
                result[i] = (char)r.Next(48, 125);

            for (int i = 0; i < wordCount - 1; i++)
                result[r.Next(length)] = ' ';

            return new string(result);
        }

        /// <summary>
        /// Devuelve una copia de este objeto System.String truncada a una determinada longitud máxima.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount">Número máximo de caracteres.</param>
        /// <returns></returns>
        public static string Truncate(this string s, int charCount)
        {
            return (s == null || s.Length < charCount) ? s : s.Substring(0, charCount);
        }
        
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene las subcadenas de esta instancia que están delimitadas por
        /// elementos de la matriz de caracteres Unicode especificada, y además quita todos los caracteres de espacio
        /// en blanco del principio y del final de cada objeto System.String de la matriz resultante.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static string[] SplitAndTrim(this string s, params char[] separators)
        {
            string[] result = s.Split(separators);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, int count)
        {
            string[] result = s.Split(separators, count);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, StringSplitOptions options)
        {
            string[] result = s.Split(separators, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, string[] separators, StringSplitOptions options)
        {
            string[] result = s.Split(separators, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, int count, StringSplitOptions options)
        {
            string[] result = s.Split(separators, count, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, string[] separators, int count, StringSplitOptions options)
        {
            string[] result = s.Split(separators, count, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de doble precisión, atendiendo a su
        /// representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s)
        {
            return StringExtensions.TryParse<double>(s, double.TryParse, 0.0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de doble precisión, atendiendo a su
        /// representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static double ToDouble(this string s, double fallbackValue)
        {
            return StringExtensions.TryParse<double>(s, double.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una enumeración del tipo especificado. Si no se puede
        /// convertir, devuelve el valor por defecto según el obtenido por la palabra clave default para la enumeración
        /// expecificada.
        /// </summary>
        /// <typeparam name="T">Tipo de enumeración a la que se desea convertir.</typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string s) where T : struct
        {
            return StringExtensions.ToEnum(s, default(T));//(T)Enum.Parse(typeof(T), s, true);
        }

        /// <summary>
        /// Convierte el objeto System.String actual en una enumeración del tipo especificado. Si no se puede
        /// convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <typeparam name="T">Tipo de enumeración a la que se desea convertir.</typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string s, T fallbackValue) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), s, true);
            }
            catch
            {
                return fallbackValue;
            }
        }

        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de simple precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToFloat(this string s)
        {
            return StringExtensions.TryParse<float>(s, float.TryParse, 0.0f);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de simple precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static float ToFloat(this string s, float fallbackValue)
        {
            return StringExtensions.TryParse<float>(s, float.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 64 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToLong(this string s)
        {
            return StringExtensions.TryParse<long>(s, long.TryParse, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 64 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static long ToLong(this string s, long fallbackValue)
        {
            return StringExtensions.TryParse<long>(s, long.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 32 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            return StringExtensions.TryParse<int>(s, int.TryParse, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 32 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static int ToInt(this string s, int fallbackValue)
        {
            return StringExtensions.TryParse<int>(s, int.TryParse, fallbackValue);
        }
        
        private delegate bool ParsingFunc<T>(string s, out T result);
        
        private static T TryParse<T>(this string s, ParsingFunc<T> parsingFunc, T defaultValue) where T : struct
        {
            T result = defaultValue;
            if (parsingFunc(s, out result))
                return result;
            return defaultValue;
        }
    }
    
}