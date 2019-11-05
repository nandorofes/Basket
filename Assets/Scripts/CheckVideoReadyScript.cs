using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckVideoReadyScript : MonoBehaviour
{
    private Image image;
    private Button button;

    private bool alive;

    private void Start()
    {
        this.image = this.GetComponent<Image>();
        this.button = this.GetComponent<Button>();

        this.alive = true;
        this.StartCoroutine(this.CheckUnityVideo());
    }
	
    private void OnDestroy()
    {
        this.alive = false;
        this.StopAllCoroutines();
    }

    // Update is called once per frame
    private IEnumerator CheckUnityVideo()
    {
        while (this.alive)
        {
            yield return new WaitForSeconds(0.25f);
			if (this.button != null) {

			}
               // this.button.interactable = UnityAdsManager.Instance.RewardedVideoReady;

           // this.transform.localScale = Vector3.one * (UnityAdsManager.Instance.RewardedVideoReady ? 1.0f : 0.0f);
        }
    }

}