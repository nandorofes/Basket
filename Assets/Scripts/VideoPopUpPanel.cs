using UnityEngine;
using System;
using System.Collections;

public class VideoPopUpPanel : MonoBehaviour {

	public void Show (){
		//if (UnityAdsManager.Instance.RewardedVideoReady) {
			this.gameObject.SetActive (true);
		//	UnityAdsManager.Instance.OnVideoWatched += this.DarPremio;
		//}
	}

	public void Yes(){
		//UnityAdsManager.Instance.ShowRewardedVideo ();
		GameManager.Instance.ShowRewardedVideo(true,false,false);
	}

	public void DarPremio(){
		CreditsManagement creditsManagement = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<CreditsManagement> ();
		if (this.gameObject.name == "VideoPopUpCredits") {
			creditsManagement.credits += 1;
			//UnityAdsManager.Instance.OnVideoWatched -= this.DarPremio;
			this.gameObject.SetActive (false);
		}
	}

	public void No(){
		//UnityAdsManager.Instance.OnVideoWatched -= this.DarPremio;
		this.gameObject.SetActive (false);
	}
}
