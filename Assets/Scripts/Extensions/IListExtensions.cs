using System;
using System.Collections.Generic;

namespace Extensions.System.Colections
{
    public static class IListExtensions
    {
        /// <summary>
        /// Obtiene el elemento que se encuentra situado en la mitad
        /// de la lista, aproximadamente a la misma distancia del
        /// principio y del final.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static T MiddleItem<T>(this IList<T> list)
        {
            if (list.Count > 1)
                return list[list.Count / 2];
            return list.Count == 1 ? list[0] : default(T);
        }
        
        /// <summary>
        /// Saca un elemento de la lista y lo reinserta en otra posición,
        /// preservando el orden de cualquier elemento intermedio.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i">Índice del elemento a mover.</param>
        /// <param name="j">Índice de destino.</param>
        public static void Move<T>(this IList<T> list, int fromIndex, int toIndex)
        {
            if (list.IndexInRange<T>(fromIndex) && list.IndexInRange<T>(toIndex))
            {
                T item = list[fromIndex];
                list.RemoveAt(fromIndex);
                list.Insert(toIndex, item);
            }
        }
        
        /// <summary>
        /// Comprueba si el índice especificado es un índice válido para esta
        /// instancia.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index">Índice a comprobar.</param>
        /// <returns></returns>
        public static bool IndexInRange<T>(this IList<T> list, int index)
        {
            return (index >= 0) && (index < list.Count);
        }
        
        /// <summary>
        /// Devuelve el primer elemento de esta lista, sacándolo además de la
        /// colección.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T PullFirst<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                T item = list[0];
                list.RemoveAt(0);
                return item;
            }
            else
                return default(T);
        }

        /// <summary>
        /// Devuelve un elemento de esta lista elegido al azar, sacándolo
        /// además de la colección.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T PullRandomItem<T>(this IList<T> list)
        {
            Random r = new Random();
            int index = r.Next(list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
        
        /// <summary>
        /// Devuelve un índice válido para esta instancia elegido al azar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int RandomIndex<T>(this IList<T> list)
        {
            Random r = new Random();
            return r.Next(list.Count);
        }
        
        /// <summary>
        /// Devuelve un elemento de esta lista elegido al azar, sin sacarlo de
        /// la colección.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                Random r = new Random();
                return list[r.Next(list.Count)];
            }
            return default(T);
        }
        
        /// <summary>
        /// Reordena los elementos de esta lista al azar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random r = new Random();
            for (int i = list.Count; i > 1; i--)
            {
                int dest = r.Next(i);
                T value = list[dest];
                list[dest] = list[i];
                list[i] = value;
            }
        }
        
        /// <summary>
        /// Intercambia la posición de dos elementos de esta lista según sus
        /// índices.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i">Índice del primer elemento.</param>
        /// <param name="j">Índice del segundo elemento.</param>
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            T value = list[i];
            list[i] = list[j];
            list[j] = value;
        }
    }
    
}