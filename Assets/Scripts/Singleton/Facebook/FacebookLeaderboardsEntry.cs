using System;
using System.Collections.Generic;
using UnityEngine;

public class FacebookLeaderboardsEntry : IComparable<FacebookLeaderboardsEntry>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private FacebookUser user;
    private FacebookScore score;
    
    public FacebookUser User
    {
        get { return this.user; }
        internal set { this.user = value; }
    }
    
    public FacebookScore Score
    {
        get { return this.score; }
        internal set { this.score = value; }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructores
    // ---- ---- ---- ---- ---- ---- ---- ----
    public FacebookLeaderboardsEntry()
    {
        this.user = new FacebookUser();
        this.score = new FacebookScore();
    }
    
    public FacebookLeaderboardsEntry(FacebookUser user, FacebookScore score)
    {
        this.user = user;
        this.score = score;
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos reemplazados
    public override string ToString()
    {
        return string.Format("[User = {0}, Score = {1}]", this.user.ToString(), this.score.ScoreValue);
    }
    
    // Métodos de "IComparable<FacebookLeaderboardsEntry>"
    public int CompareTo(FacebookLeaderboardsEntry other)
    {
        if (this.score.ScoreValue > other.score.ScoreValue)
            return -1;
        else
            return (this.score.ScoreValue == other.score.ScoreValue) ? 0 : 1;
    }
    
}