using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using System.Collections;

public class SimpleMatchMaker : MonoBehaviour
{
	[SerializeField]
	private MultiplayerSceneManager msm;

	private MatchInfo infoMatch;

	private int attempts=0;
	private int attempts2=0;

	public bool matchCreated;

	void Start()
	{
		matchCreated = false;
		NetworkManager.singleton.StartMatchMaker();
		StartCoroutine (WaitForConexion ());
	}

	IEnumerator WaitForConexion(){
		yield return new WaitForSeconds (3);
		FindInternetMatch ("Default");
	}

	//call this method to request a match to be created on the server
	public void CreateInternetMatch(string matchName)
	{
		matchCreated = true;
		NetworkManager.singleton.matchMaker.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
	}

	//this method is called when your request for creating a match is returned
	private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Create match succeeded");

			MatchInfo hostInfo = matchInfo;
			infoMatch = matchInfo;
			NetworkServer.Listen(hostInfo, 9000);

			NetworkManager.singleton.StartHost(hostInfo);
			StartCoroutine (WaitForRival (hostInfo));
		}
		else
		{
			Debug.LogError("Create match failed");
			if (attempts >= 3) {
				msm.EmpezarAI ();
			} else {
				attempts += 1;
				CreateInternetMatch ("Default");
			}
		}
	}

	IEnumerator WaitForRival (MatchInfo matchInfo){
		yield return new WaitForSeconds (30);
		if (NetworkServer.objects.Count <= 1) {
			NetworkManager.singleton.matchMaker.DestroyMatch (matchInfo.networkId, 0, OnMatchDestroyed);
		}
	}

	private void OnMatchDestroyed(bool success, string info){
		if (success) {
			matchCreated = false;
			msm.EmpezarAI ();
		}
	}

	//call this method to find a match through the matchmaker
	public void FindInternetMatch(string matchName)
	{
		NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList);
	}

	//this method is called when a list of matches is returned
	private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success)
		{
			if (matches.Count != 0)
			{
				Debug.Log("A list of matches was returned");

				//join the last server (just in case there are two...)
				NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
			}
			else
			{
				Debug.Log("No matches in requested room! Attempts="+attempts2);
				if (attempts2 >= 3) {
					CreateInternetMatch ("Default");
				} else {
					attempts2 += 1;
					StartCoroutine (DelayFindMatchList ());
				}
			}
		}
		else
		{
			Debug.LogError("Couldn't connect to match maker");
			msm.EmpezarAI ();
		}
	}

	IEnumerator DelayFindMatchList(){
		yield return new WaitForSeconds (1);
		FindInternetMatch ("Default");
	}

	//this method is called when your request to join a match is returned
	private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			//Debug.Log("Able to join a match");

			MatchInfo hostInfo = matchInfo;
			infoMatch = matchInfo;
			NetworkManager.singleton.StartClient(hostInfo);
		}
		else
		{
			Debug.LogError("Join match failed");
			CreateInternetMatch ("Default");
		}
	}

	public void DelateMatchAtFinish(){
		NetworkManager.singleton.matchMaker.DestroyMatch (infoMatch.networkId, 0, OnMatchDestroyed);
		Debug.Log ("Match Delated");
	}
}