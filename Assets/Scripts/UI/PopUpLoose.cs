using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUpLoose : MonoBehaviour {

	public GameObject botonVideo;
	public Text timeText;
	public float time=5;
	bool verVideo;



	public void Show(){
		this.gameObject.SetActive (true);
		//if (UnityAdsManager.Instance.RewardedVideoReady) {
			//UnityAdsManager.Instance.OnVideoWatched += this.DarPremio;
		//	botonVideo.SetActive (true);
		//} else {
		//	botonVideo.SetActive (false);
		//}
		time = 5;
		verVideo = false;
		StartCoroutine(CuentaAtras(5));
	}

	public void Video(){
		//UnityAdsManager.Instance.ShowRewardedVideo ();
		verVideo = true;
		GameManager.Instance.ShowRewardedVideo (false, false, true);

	}

	public void DarPremio(){
		
		GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
		sceneManager.continuar = true;
		sceneManager.Empezar ();
		//UnityAdsManager.Instance.OnVideoWatched -= this.DarPremio;
		AudioManager.Instance.LlamarAwake ();
		sceneManager.loosePanel.SetActive (false);
		//GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Video", 1f , "RetryVideo", "RetryVideo");
		this.gameObject.SetActive (false);


	}

	public void Continuar(){
		GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
		CreditsManagement creditsManagement = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<CreditsManagement> ();
		if (creditsManagement.credits >= 2) {
			creditsManagement.credits -= 2;
			sceneManager.continuar = true;
			sceneManager.Empezar ();
			sceneManager.loosePanel.SetActive (false);
			//GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Credit", 2f , "Retry", "Retry");
		}
		//UnityAdsManager.Instance.OnVideoWatched -= this.DarPremio;
		this.gameObject.SetActive (false);

	}

	public void BotonTryAgain(){
		GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
		//UnityAdsManager.Instance.OnVideoWatched -= this.DarPremio;
		this.gameObject.SetActive (false);
		sceneManager.Reintentar ();
	}

	IEnumerator CuentaAtras(float tiempo){
		yield return new WaitForSeconds (tiempo);
		if (!verVideo) {
			GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
			if (GameManager.Instance.GamePersistentData.Tutorial == false && GameManager.Instance.GamePersistentData.Tickets >= 100) {
				sceneManager.tuto.Show ();
			}
			//UnityAdsManager.Instance.OnVideoWatched -= this.DarPremio;
			this.gameObject.SetActive (false);
		}
		this.gameObject.SetActive (false);
	}

	void Update (){
		time -= Time.deltaTime;
		timeText.text = time.ToString ("0");
	}
}
