using System;

namespace Extensions.System.Numeric
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Restringe el valor de esta instancia a un intervalo de valores,
        /// devolviendo el valor de los límites cuando el valor está fuera del
        /// rango.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        /// <returns></returns>
        public static int ClampTo(this int i, int min, int max)
        {
            if (min < max)
                return (i < min) ? min : (i > max ? max : i);
            return (i < max) ? max : (i > min ? min : i);
        }
        
        /// <summary>
        /// Devuelve el valor de esta instancia si es positivo, o cero en otro
        /// caso.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int ClampToPositive(this int i)
        {
            return (i > 0) ? i : 0;
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia se encuentra en el
        /// intervalo de valores especificado.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        /// <returns></returns>
        public static bool InRange(this int i, int min, int max)
        {
            return (min < max) ? (i >= min) && (i <= max) : (i >= max) && (i <= min);
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia es par.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEven(this int i) { return (i % 2) == 0; }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia es impar.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsOdd(this int i) { return (i % 2) == 1; }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia es primo.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsPrime(this int i)
        {
            if ((i % 2) == 0)
                return i == 2;
            
            int limit = (int)Math.Sqrt(i);
            for (int divisor = 3; divisor <= limit; divisor = divisor + 2)
            {
                if ((i % divisor) == 0)
                    return false;
            }
            return i != 1;
        }
        
        /// <summary>
        /// Extrae los dígitos especificados que contiene esta instancia según
        /// el sistema de numeración en base 10 y los devuelve en un nuevo
        /// número entero de 32 bits.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="from">Índice del dígito de comienzo. El valor 0
        /// representa las unidades, el valor 1 las decenas, y así
        /// sucesivamente.</param>
        /// <param name="digitCount">Número de digitos a incluir en el
        /// resultado, contados a partir del dígito de comienzo.</param>
        /// <returns></returns>
        public static int GetDigits(this int i, int from, int digitCount)
        {
            int fromPower = 1, toPower = 1, counter;
            for (counter = 0; counter < from; counter++)
                fromPower *= 10;
            
            for (counter = 0; counter < digitCount; counter++)
                toPower *= 10;
            
            return (i / fromPower) % toPower;
        }

        public static float ToFloat(this int i)
        {
            return (float)i;
        }

        public static int FitToMultiple(this int i, int multiple)
        {
            return (i / multiple) * multiple;
        }

        public static int Mod(this int i, int m)
        {
            int r = i % m;
            return r < 0 ? r + m : r;
        }
    }
    
}