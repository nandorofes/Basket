using System;
using System.Collections.Generic;

public class FacebookFriendList : IEnumerable<FacebookUser>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private List<FacebookUser> friendList;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Valores calculados.
    public int Count
    {
        get { return this.friendList.Count; }
    }
    
    // Indizadores.
    public FacebookUser this[int i]
    {
        get { return this.friendList[i]; }
        internal set { this.friendList[i] = value; }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructores
    // ---- ---- ---- ---- ---- ---- ---- ----
    public FacebookFriendList()
    {
        this.friendList = new List<FacebookUser>();
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de gestión.
    public void Add(FacebookUser facebookUser)
    {
        this.friendList.Add(facebookUser);
    }
    
    public void Clear()
    {
        this.friendList.Clear();
    }
    
    public bool Contains(FacebookUser facebookUser)
    {
        return this.friendList.Contains(facebookUser);
    }
    
    public bool Remove(FacebookUser facebookUser)
    {
        return this.friendList.Remove(facebookUser);
    }
    
    public void RemoveAt(int index)
    {
        this.friendList.RemoveAt(index);
    }
    
    // Métodos de "IEnumerable<FacebookUser>".
    public IEnumerator<FacebookUser> GetEnumerator()
    {
        foreach (FacebookUser facebookUser in this.friendList)
            yield return facebookUser;
    }
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    
    public static FacebookFriendList Parse(IDictionary<string, object> dictionary)
    {
        FacebookFriendList result = new FacebookFriendList();
        
        // Analizar datos principales del resultado
        object resultData = null;
        if (dictionary.ContainsKey("data"))
            resultData = dictionary["data"];
        
        if (resultData != null)
        {
            IList<object> data = resultData as IList<object>;
            if (data != null)
            {
                foreach (var userData in data)
                {
                    FacebookUser user = FacebookUser.Parse(userData);
                    result.Add(user);
                }
            }
        }
        
        return result;
    }
    
}