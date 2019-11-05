using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions.UnityEngine
{
    public enum TransformSpace
    {
        Global,
        Local
    }
    
    public static class TransformExtensions
    {
        /// <summary>
        /// Destruye todos los hijos de esta instancia, llamando al método
        /// GameObject.Destroy para cada uno de ellos.
        /// </summary>
        /// <param name="transform"></param>
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                GameObject.Destroy(transform.GetChild(i).gameObject);
        }
        
        /// <summary>
        /// Destruye todos los hijos de esta instancia, llamando al método
        /// GameObject.DestroyImmediate para cada uno de ellos.
        /// </summary>
        /// <param name="transform"></param>
        public static void DestroyChildrenImmediate(this Transform transform)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
                children.Add(child);
            
            children.ForEach(t => GameObject.DestroyImmediate(t.gameObject));
        }

        public static Transform FirstChild(this Transform transform)
        {
            return transform.childCount > 0 ? transform.GetChild(0) : null;
        }

        public static T FirstChild<T>(this Transform transform) where T : MonoBehaviour
        {
            return transform.childCount > 0 ? transform.GetChild(0).GetComponent<T>() : null;
        }

        public static Transform LastChild(this Transform transform)
        {
            return transform.childCount > 0 ? transform.GetChild(transform.childCount - 1) : null;
        }

        public static T LastChild<T>(this Transform transform) where T : MonoBehaviour
        {
            return transform.childCount > 0 ? transform.GetChild(transform.childCount - 1).GetComponent<T>() : null;
        }

        /// <summary>
        /// Reinicia la transformación de esta instancia, moviéndolo al origen
        /// del sistema de coordenadas local, eliminando la rotación, y
        /// asignando la escala unitaria.
        /// </summary>
        /// <param name="transform"></param>
        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        /// <summary>
        /// Asigna el componente X de la posición global de esta instancia.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente X.</param>
        public static void SetPositionX(this Transform transform, float value)
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        
        /// <summary>
        /// Asigna el componente X de la posición de esta instancia en el
        /// espacio de coordenadas especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente X.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetPositionX(this Transform transform, float value, TransformSpace transformSpace)
        {
            if (transformSpace == TransformSpace.Global)
                transform.position = new Vector3(value, transform.position.y, transform.position.z);
            else
                transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
        }
        
        /// <summary>
        /// Asigna el componente Y de la posición global de esta instancia.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Y.</param>
        public static void SetPositionY(this Transform transform, float value)
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        
        /// <summary>
        /// Asigna el componente Y de la posición de esta instancia en el
        /// espacio de coordenadas especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Y.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetPositionY(this Transform transform, float value, TransformSpace transformSpace)
        {
            if (transformSpace == TransformSpace.Global)
                transform.position = new Vector3(transform.position.y, value, transform.position.z);
            else
                transform.localPosition = new Vector3(transform.localPosition.y, value, transform.localPosition.z);
        }
        
        /// <summary>
        /// Asigna el componente Z de la posición global de esta instancia.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Z.</param>
        public static void SetPositionZ(this Transform transform, float value)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }
        
        /// <summary>
        /// Asigna el componente Z de la posición de esta instancia en el
        /// espacio de coordenadas especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Z.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetPositionZ(this Transform transform, float value, TransformSpace transformSpace)
        {
            if (transformSpace == TransformSpace.Global)
                transform.position = new Vector3(transform.position.x, transform.position.y, value);
            else
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
        }

        public static void SetRotationEulerZ(this Transform transform, float value)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                  transform.rotation.eulerAngles.y,
                                                  value);
        }

        public static void SetRotationEulerZ(this Transform transform, float value, TransformSpace transformSpace)
        {
            if (transformSpace == TransformSpace.Global)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                      transform.rotation.eulerAngles.y,
                                                      value);
            else
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                                                           transform.localRotation.eulerAngles.y,
                                                           value);
        }
    }
    
}