//Attach this to the GameObject you would like to spawn (the player).
//Make sure to create a NetworkManager with an HUD component in your Scene. To do this, create a GameObject, click on it, and click on the Add Component button in the Inspector window.  From there, Go to Network>NetworkManager and Network>NetworkManagerHUD respectively.
//Assign the GameObject you would like to spawn in the NetworkManager.
//Start the server and client for this to work.

//Use this script to send and update variables between Networked GameObjects
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerFBInfo : NetworkBehaviour
{
	//Detects when a health change happens and calls the appropriate function
	[SyncVar(hook = "OnFBIDRecive")]
	public string fbIDString="";

	public PlayerMultiplayer player;

	void Start()
	{
		player = this.gameObject.GetComponent<PlayerMultiplayer> ();
	}

	void Update()
	{
		if (isLocalPlayer) {
			if (FacebookManager.Instance.LoggedIn) {
				CmdSendFBID ();
			}
		}
	}

	void OnFBIDRecive(string fbID)
	{
		fbIDString = fbID;
		player.fbIDstring = fbIDString;
	}

	//This is a Network command, so the damage is done to the relevant GameObject
	[Command]
	void CmdSendFBID()
	{
		//Apply damage to the GameObject
		fbIDString=FacebookManager.Instance.CurrentUser.Id;
		SendFBID(FacebookManager.Instance.CurrentUser.Id);
		RpcReciveID (fbIDString);
	}

	[ClientRpc]
	void RpcReciveID(string id){
		fbIDString = id;
	}

	public void SendFBID(string id)
	{
//		if (!isServer) {
//			return;
//		}

		fbIDString = id;
	}
}
