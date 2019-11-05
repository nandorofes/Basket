using UnityEngine;
using System.Collections;

public class VideoButtonController : MonoBehaviour
{
    public float speed = 2.0f;

    [SerializeField]
    private CanvasGroup textGroup;
    [SerializeField]
    private CanvasGroup rewardGroup;

    // Update is called once per frame
    private void Update()
    {
        float sinFactor = 0.5f + (Mathf.Sin(Time.timeSinceLevelLoad * this.speed) * 0.5f);

        this.textGroup.alpha = sinFactor;
        this.rewardGroup.alpha = 1.0f - sinFactor;
    }

}