using Extensions.System.String;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacebookTester : MonoBehaviour
{
    public RawImage image;
    public Text text;
    public InputField inputField;
    
    private void Awake()
    {
        FacebookManager.Instance.OnLoginSuccess += this.OnLoginSuccess;
        FacebookManager.Instance.OnRequestUserDataSuccess += this.OnRequestUserDataReceived;
        
        FacebookManager.Instance.OnRequestGetScoreSuccess += this.OnRequestGetScoreSuccess;
        FacebookManager.Instance.OnRequestSetScoreSuccess += this.OnRequestSaveScoreSuccess;
        FacebookManager.Instance.OnRequestLeaderboardsSuccess += this.OnRequestLeaderboardsSuccess;
        
        FacebookManager.Instance.OnRequestGetScoreError += this.OnRequestGetScoreError;
        FacebookManager.Instance.OnRequestSetScoreError += this.OnRequestSaveScoreError;
        FacebookManager.Instance.OnRequestLeaderboardsError += this.OnRequestLeaderboardsError;
    }
    
    public void GetScore()
    {
        FacebookManager.Instance.RequestGetScore();
    }
    
    public void SetScore()
    {
        string input = this.inputField.text;
        uint value = (uint)input.ToInt();
        FacebookScore score = new FacebookScore(value);
        
        FacebookManager.Instance.RequestSetScore(score);
    }
    
    public void GetLeaderBoards()
    {
        FacebookManager.Instance.RequestLeaderboards();
    }
    
    // Manejadores de eventos
    private void OnLoginSuccess()
    {
        FacebookManager.Instance.RequestUserData();
    }
    
    private void OnRequestUserDataReceived()
    {
        if (this.text != null)
            text.text = FacebookManager.Instance.CurrentUser.ToString();
        
        if (this.image != null)
            image.texture = FacebookManager.Instance.CurrentUser.ProfilePicture;
    }
    
    private void OnRequestGetScoreSuccess()
    {
        if (this.text != null)
        {
            text.text = string.Format("Score: {0}", FacebookManager.Instance.CurrentUserScore.ScoreValue.ToString());
        }
    }
    
    private void OnRequestSaveScoreSuccess()
    {
        if (this.text != null)
        {
            text.text = "Save score success!";
        }
    }
    
    private void OnRequestLeaderboardsSuccess()
    {
        if (this.text != null)
        {
            text.text = string.Empty;
            foreach (FacebookLeaderboardsEntry entry in FacebookManager.Instance.Leaderboards)
            {
                text.text += string.Format("{0}\n", entry.ToString());
            }
        }
    }
    
    private void OnRequestGetScoreError(string error)
    {
        if (this.text != null)
        {
            text.text = string.Format("Error getting score: {0}.", error);
        }
    }
    
    private void OnRequestSaveScoreError(string error)
    {
        if (this.text != null)
        {
            text.text = string.Format("Error saving score: {0}.", error);
        }
    }
    
    private void OnRequestLeaderboardsError(string error)
    {
        if (this.text != null)
        {
            text.text = string.Format("Error getting leaderboards: {0}.", error);
        }
    }
    
}