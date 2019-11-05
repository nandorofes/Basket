using System;
using System.Collections.Generic;
using UnityEngine;

public class FacebookLeaderboards : IEnumerable<FacebookLeaderboardsEntry>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private List<FacebookLeaderboardsEntry> data;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public int Count
    {
        get { return this.data.Count; }
    }
    
    public FacebookLeaderboardsEntry this[int i]
    {
        get { return this.data[i]; }
        internal set { this.data[i] = value; }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructores
    // ---- ---- ---- ---- ---- ---- ---- ----
    private FacebookLeaderboards()
    {
        this.data = new List<FacebookLeaderboardsEntry>();
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de gestión.
    public void Add(FacebookUser user, FacebookScore score)
    {
        this.data.Add(new FacebookLeaderboardsEntry(user, score));
        this.data.Sort();
    }
    
    public void Clear()
    {
        this.data.Clear();
    }
    
    public bool Contains(FacebookUser user)
    {
        foreach (FacebookLeaderboardsEntry entry in this.data)
        {
            if (entry.User == user)
                return true;
        }
        return false;
    }
    
    public void Remove(FacebookUser user)
    {
        for (int i = this.data.Count - 1; i >= 0; i--)
        {
            FacebookLeaderboardsEntry entry = this.data[i];
            if (entry.User == user)
                this.data.RemoveAt(i);
        }
    }
    
    public void RemoveAt(int index)
    {
        this.data.RemoveAt(index);
    }

    public FacebookUser UserFromId(string id)
    {
        foreach (FacebookLeaderboardsEntry entry in this.data)
        {
            if (entry.User.Id == id)
                return entry.User;
        }
        return null;
    }

    // Métodos de "IEnumerable<FacebookLeaderboardsEntry>"
    public IEnumerator<FacebookLeaderboardsEntry> GetEnumerator()
    {
        foreach (FacebookLeaderboardsEntry entry in this.data)
            yield return entry;
    }
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos estáticos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public static FacebookLeaderboards Parse(IDictionary<string, object> data)
    {
        FacebookLeaderboards result = new FacebookLeaderboards();
        
        List<object> scoreList = data["data"] as List<object>;
        if (scoreList != null)
        {
            foreach (object scoreEntryObject in scoreList)
            {
                IDictionary<string, object> scoreEntryMap = scoreEntryObject as IDictionary<string, object>;
                if (scoreEntryMap != null)
                {
                    FacebookUser user = new FacebookUser();
                    Dictionary<string, object> userData = scoreEntryMap["user"] as Dictionary<string, object>;
                    if (userData != null)
                    {
                        user.Id = userData["id"] as string; 
                        user.Name = userData["name"] as string;

                        object scoreObject = scoreEntryMap["score"];
                        long scoreLong = (long)scoreObject;
                        uint score = (uint)scoreLong;

                        result.Add(user, new FacebookScore(score));
                    }
                }
            }
        }
        
        return result;
    }
    
}