using System;
using System.Collections.Generic;
using UnityEngine;

public class FacebookUser
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public string Id { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public FacebookAgeRanges Age { get; set; }
    public FacebookGenders Gender { get; set; }
    
    public string Locale { get; set; }
    
    public Texture2D ProfilePicture { get; set; }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public override string ToString()
    {
        return string.Format("[Id = {0}, Name = {1}]", this.Id, this.Name);
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos estáticos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public static FacebookUser Parse(object userData)
    {
        FacebookUser result = new FacebookUser();
        
        IDictionary<string, object> userDictionary = userData as IDictionary<string, object>;
        if (userDictionary != null)
        {
            // Identificador único de usuario
            if (userDictionary.ContainsKey("id"))
                result.Id = userDictionary["id"] as string;
            
            // Nombre completo
            if (userDictionary.ContainsKey("name"))
                result.Name = userDictionary["name"] as string;
            
            // Nombre de pila
            if (userDictionary.ContainsKey("first_name"))
                result.FirstName = userDictionary["first_name"] as string;
            
            // Apellidos
            if (userDictionary.ContainsKey("last_name"))
                result.LastName = userDictionary["last_name"] as string;
            
            // Rango de edad
            if (userDictionary.ContainsKey("age_range"))
            {
                IDictionary<string, int> ageRangeData = userDictionary["age_range"] as IDictionary<string, int>;
                if (ageRangeData != null)
                {
                    if (ageRangeData.ContainsKey("min"))
                    {
                        int minAge = ageRangeData["min"];
                        
                        result.Age = FacebookAgeRanges.From21;
                        if (minAge < 21)
                            result.Age = FacebookAgeRanges.From18To20;
                        if (minAge < 18)
                            result.Age = FacebookAgeRanges.From13To17;
                    }
                }
            }
            
            // Sexo
            if (userDictionary.ContainsKey("gender"))
            {
                string gender = userDictionary["gender"] as string;
                result.Gender = FacebookGenders.Custom;
                if (gender == "male")
                    result.Gender = FacebookGenders.Male;
                if (gender == "female")
                    result.Gender = FacebookGenders.Female;
            }
            
            // Idioma de la interfaz
            if (userDictionary.ContainsKey("locale"))
                result.Locale = userDictionary["locale"] as string;
        }
        
        return result;
    }
    
}