using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMultiplayer : NetworkBehaviour {

	public MultiplayerSceneManager sm;
	public GameObject bulletPrefab;
	bool llamadaACmd=false;
	public string fbIDstring;
	public MultiplayerFBInfo MultiFBInfo;
	public int pointsplayer;

	public PlayerMultiplayer rival;

	// Use this for initialization
	void Start () {
		sm = GameObject.Find ("SceneManager").GetComponent<MultiplayerSceneManager> ();
		MultiFBInfo = this.gameObject.GetComponent<MultiplayerFBInfo> ();
		rival = null;
		if (isLocalPlayer) {
			if (FacebookManager.Instance.LoggedIn) {
				Debug.Log ("fbIDLocalPlayer=" + FacebookManager.Instance.CurrentUser.Id);
				fbIDstring = FacebookManager.Instance.CurrentUser.Id;
//				int fbID1 = 0;
//				int fbID2 = 0;
//				int fbID3 = 0;
//				string temp1 = "";
//				string temp2 = "";
//				string temp3 = "";
//				for (int i = 0; i < FacebookManager.Instance.CurrentUser.Id.Length; i++) {
//					if (((float)FacebookManager.Instance.CurrentUser.Id.Length / 3) > i) {
//						temp1 += FacebookManager.Instance.CurrentUser.Id [i].ToString ();
//					}else if((((float)FacebookManager.Instance.CurrentUser.Id.Length / 3)*2) > i){
//						temp2+=FacebookManager.Instance.CurrentUser.Id [i].ToString ();
//					} else {
//						temp3+=FacebookManager.Instance.CurrentUser.Id [i].ToString ();
//					}
//				}
//				Debug.Log ("Before parse1=" + temp1);
//				Debug.Log ("Before parse2=" + temp2);
//				Debug.Log ("Before parse3=" + temp3);
//				int.TryParse (temp1,out fbID1);
//				Debug.Log ("After Parse fbID1=" + fbID1);
//				int.TryParse (temp2,out fbID2);
//				Debug.Log ("After Parse fbID2=" + fbID2);
//				int.TryParse (temp3,out fbID3);
//				Debug.Log ("After Parse fbID3=" + fbID3);
//
//				transform.localScale = new Vector3 (fbID1, fbID2, fbID3);
//				string test = transform.localScale.x.ToString()+transform.localScale.y.ToString()+transform.localScale.z.ToString();
//				Debug.Log ("After ToString()=" +test);
//			} else {
//				transform.localScale = new Vector3 (1, 1, 1);
			}
			StartCoroutine (PillarRival ());
		}
	}

	IEnumerator PillarRival(){
		GameObject [] jugadores= GameObject.FindGameObjectsWithTag ("Player");
		bool encontrado=false;
		while (!encontrado) {
			for (int i = 0; i < jugadores.Length; i++) {
				if (!jugadores [i].GetComponent<NetworkIdentity> ().isLocalPlayer) {
					rival = jugadores [i].GetComponent<PlayerMultiplayer> ();
				}
			}
			yield return null;
			if (rival == null) {
				jugadores = GameObject.FindGameObjectsWithTag ("Player");
			} else {
				encontrado = true;
				sm.Empezar ();
			}
		}

	}

	public void LlamarACmd(){
		//llamadaACmd = true;
		pointsplayer = sm.puntuacion;

	}
		
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			// exit from update if this is not the local player
			return;
		} else {

			if (rival != null) {
				//pointsplayer = sm.puntuacion;
				rival.pointsplayer = (int)rival.transform.position.x;
				sm.rivalPoints.text = rival.pointsplayer.ToString ("000");
			}

			if (sm.rematch) {
				transform.position = new Vector3 (0, 0, 1);
			} else {
				transform.position = new Vector3 (pointsplayer, 0, 0);
				if (!sm.jugando) {
					if (FacebookManager.Instance.LoggedIn) {
						int fbID1 = 0;
						int fbID2 = 0;
						int fbID3 = 0;
						string temp1 = "";
						string temp2 = "";
						string temp3 = "";
						for (int i = 0; i < FacebookManager.Instance.CurrentUser.Id.Length; i++) {
							if (((float)FacebookManager.Instance.CurrentUser.Id.Length / 3) > i) {
								temp1 += FacebookManager.Instance.CurrentUser.Id [i].ToString ();
							} else if ((((float)FacebookManager.Instance.CurrentUser.Id.Length / 3) * 2) > i) {
								temp2 += FacebookManager.Instance.CurrentUser.Id [i].ToString ();
							} else {
								temp3 += FacebookManager.Instance.CurrentUser.Id [i].ToString ();
							}
						}
						int.TryParse (temp1, out fbID1);
						int.TryParse (temp2, out fbID2);
						int.TryParse (temp3, out fbID3);

						transform.position = new Vector3 (fbID1, fbID2, fbID3);
					}
				}
			}
		}
	}
		
}
