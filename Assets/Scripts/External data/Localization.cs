using Extensions.System.String;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Localization
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Estructuras de datos
    private static SortedDictionary<string, LocalizationData> localizationMap;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de gestión
    public static void Initialize()
    {
        Localization.localizationMap = new SortedDictionary<string, LocalizationData>();
        
        TextAsset localizationFile = Resources.Load<TextAsset>("Localization");
        if (localizationFile != null)
            Localization.Parse(localizationFile.text);
    }
    
    /// <summary>
    /// Obtiene una cadena de texto según el idioma actual y una cadena
    /// de texto que actúa como clave común.
    /// </summary>
    /// <returns>La cadena de texto traducida al idioma especificado.</returns>
    /// <param name="key">Clave.</param>
    public static string GetString(string key)
    {
        string language = GameManager.Instance.GameLanguage;
        if (Localization.localizationMap.ContainsKey(language))
        {
            LocalizationData data = Localization.localizationMap[language];
            return data[key];
        }
        return key;
    }

    /// <summary>
    /// Obtiene una cadena de texto según el idioma especificado y una cadena
    /// de texto que actúa como clave común.
    /// </summary>
    /// <returns>La cadena de texto traducida al idioma especificado.</returns>
    /// <param name="language">Código de idioma.</param>
    /// <param name="key">Clave.</param>
    public static string GetString(string language, string key)
    {
        if (Localization.localizationMap.ContainsKey(language))
        {
            LocalizationData data = Localization.localizationMap[language];
            return data[key];
        }
        return key;
    }
    
    // Métodos auxiliares
    private static void Parse(string p)
    {
        CsvData data = new CsvData(p);
        
        // Obtener idiomas disponibles del archivo
        foreach (string language in data[0])
            Localization.localizationMap.Add(language, new LocalizationData());
        
        // Obtener cadenas de texto
        for (int i = 1; i < data.Count; i++)
        {
            CsvRow dataRow = data[i];
            for (int j = 1; j < dataRow.Count; j++)
            {
                LocalizationData ld = Localization.localizationMap[data[0][j]];
                ld.AddItem(dataRow[0], dataRow[j]);
            }
        }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Clases internas
    // ---- ---- ---- ---- ---- ---- ---- ----
    private class LocalizationData
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private SortedDictionary<string, string> localizationData;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        public string this[string key]
        {
            get
            {
                if (this.localizationData.ContainsKey(key))
                    return this.localizationData[key];
                return key;
            }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public LocalizationData()
        {
            this.localizationData = new SortedDictionary<string,string>();
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public bool AddItem(string key, string value)
        {
            try
            {
                if (!this.localizationData.ContainsKey(key))
                {
                    this.localizationData.Add(key, value);
                    return this.localizationData.ContainsKey(key);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public void Clear()
        {
            this.localizationData = new SortedDictionary<string, string>();
        }
        
        public bool RemoveItem(string key)
        {
            try
            {
                if (this.localizationData.ContainsKey(key))
                {
                    this.localizationData.Remove(key);
                    return !this.localizationData.ContainsKey(key);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
    
}