using Extensions.UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HallOfFamePanelController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private GameObject loggedControls;
    [SerializeField]
    private GameObject unloggedControls;

    [SerializeField]
    private RankingEntryController rankingEntryPrefab;

    [SerializeField]
    private Transform rankingEntryHolder;

    // Caché de entradas
    private List<RankingEntryController> rankingEntryList;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control
    private void Awake()
    {
        FacebookManager.Instance.OnLoginSuccess += this.LoggedIn;
        FacebookManager.Instance.OnLogoutSuccess += this.LoggedOut;

        FacebookManager.Instance.OnRequestLeaderboardsSuccess += this.LeaderboardsWasLoaded;
        FacebookManager.Instance.OnRequestLeaderboardsUpdate += this.LeaderboardsWasUpdated;

        if (FacebookManager.Instance.LoggedIn)
            FacebookManager.Instance.RequestLeaderboards();

        this.UpdateControls();
    }

    // Métodos de control
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        this.UpdateControls();
    }

    private void UpdateControls()
    {
        if (FacebookManager.Instance.LoggedIn) // Inicaiada sesión en Facebook
        {
            this.loggedControls.SetActive(true);
            this.unloggedControls.SetActive(false);
        }
        else // No se ha inicaiado sesión en Facebook
        {
            this.loggedControls.SetActive(false);
            this.unloggedControls.SetActive(true);
        }
    }

    // Manejadores de eventos
    private void LoggedIn()
    {
        FacebookManager.Instance.RequestLeaderboards();
        this.UpdateControls();
    }

    private void LoggedOut()
    {
        this.UpdateControls();
    }

    private void LeaderboardsWasLoaded()
    {
        this.rankingEntryHolder.DestroyChildren();
        FacebookLeaderboards leaderboards = FacebookManager.Instance.Leaderboards;

        this.rankingEntryList = new List<RankingEntryController>();
        int position = 1;
        foreach (var leaderboardEntry in leaderboards)
        {
            RankingEntryController rankingEntry = RankingEntryController.Instantiate<RankingEntryController>(rankingEntryPrefab);
            rankingEntry.SetReferencedId(leaderboardEntry.User.Id);
            rankingEntry.SetPosition(position);
            rankingEntry.SetScore((int)leaderboardEntry.Score.ScoreValue);

            rankingEntry.transform.SetParent(this.rankingEntryHolder, false);
            this.rankingEntryList.Add(rankingEntry);

            position++;
        }
    }

    private void LeaderboardsWasUpdated()
    {
        foreach (var entry in this.rankingEntryList)
        {
            string id = entry.ReferencedId;
            FacebookUser user = FacebookManager.Instance.Leaderboards.UserFromId(id);
            if (user != null)
            {
                entry.SetProfilePicture(user.ProfilePicture);

                if (user.FirstName != null)
                    entry.SetUserName(user.FirstName);
            }
        }
    }

}