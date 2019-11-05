using System;

namespace Extensions.System.Numeric
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Restringe el valor de esta instancia a un intervalo de valores,
        /// devolviendo el valor de los límites cuando el valor está fuera del
        /// rango.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        /// <returns></returns>
        public static float ClampTo(this float f, float min, float max)
        {
            if (min < max)
                return (f < min) ? min : (f > max ? max : f);
            return (f < max) ? max : (f > min ? min : f);
        }
        
        /// <summary>
        /// Devuelve el valor de esta instancia si es positivo, o cero en otro
        /// caso.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float ClampToPositive(this float f)
        {
            return (f > 0.0f) ? f : 0.0f;
        }
        
        /// <summary>
        /// Restringe el valor de esta instancia al intervalo [0.0, 1.0].
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float ClampToUnit(this float f)
        {
            return (f < 0.0f) ? 0.0f : (f > 1.0f ? 1.0f : f);
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia se encuentra en el
        /// intervalo de valores especificado.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        /// <returns></returns>
        public static bool InRange(this float f, float min, float max)
        {
            return (min < max) ? (f >= min) && (f <= max) : (f >= max) && (f <= min);
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia no es infinito o
        /// indefinido (NaN).
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool IsDefinite(this float f)
        {
            return !float.IsInfinity(f) && !float.IsNaN(f);
        }
        
        /// <summary>
        /// Redondea el valor de esta instancia para que tenga el número de
        /// cifras significativas especificado.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="digits">Número de cifras significativas.</param>
        /// <returns></returns>
        public static float ToPrecision(float num, int digits)
        {
            if (num == 0.0f) return 0.0f;
            
            double d = Math.Ceiling(Math.Log10(num < 0.0f ? -num : num));
            int power = digits - (int)Math.Truncate(d);
            
            double magnitude = Math.Pow(10, power);
            long shifted = (long)Math.Round(num * magnitude);
            return (float)((double)shifted / magnitude);
        }
    }
    
}