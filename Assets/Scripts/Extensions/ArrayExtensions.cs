using System;

namespace Extensions.System.SequentialArray
{
    public static class ArrayExtensions
    {
        public static int ClampToValidIndex(this Array array, int index)
        {
            int min = array.GetLowerBound(0);
            int max = array.GetUpperBound(0);

            if (min < max)
                return (index < min) ? min : (index > max ? max : index);
            return (index < max) ? max : (index > min ? min : index);
        }

        public static int ClampToValidIndex(this Array array, int index, int dimension)
        {
            try
            {
                int min = array.GetLowerBound(dimension);
                int max = array.GetUpperBound(dimension);
                
                if (min < max)
                    return (index < min) ? min : (index > max ? max : index);
                return (index < max) ? max : (index > min ? min : index);
            }
            catch
            {
                return -1;
            }
        }

        public static bool IndexInRange(this Array array, params int[] indexes)
        {
            for (int i = 0; i < array.Rank; i++)
            {
                if ((indexes[i] < array.GetLowerBound(i)) || (indexes[i] > array.GetUpperBound(i)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Devuelve un índice válido para esta instancia elegido al azar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int RandomIndex(this Array array)
        {
            Random r = new Random();
            return r.Next(array.Length);
        }
    }
    
}