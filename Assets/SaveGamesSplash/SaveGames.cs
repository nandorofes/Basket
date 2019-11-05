using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGames : MonoBehaviour {

	[SerializeField]
	private Animation anim;
	[SerializeField]
	private AudioSource sound;

	// Use this for initialization
	void Start () {
		StartCoroutine (LoadLevelAsync ());
	}

	IEnumerator LoadLevelAsync(){
		
		anim.Play ();
		AsyncOperation asyn = Application.LoadLevelAsync("MainMenuAnimated");
		asyn.allowSceneActivation = false;
		yield return new WaitForSeconds (0.5f);
		sound.Play ();
		yield return new WaitForSeconds (3);
		asyn.allowSceneActivation = true;

	}

}
