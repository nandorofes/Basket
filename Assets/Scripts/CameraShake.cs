using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Vector3 centerPosition;
    [SerializeField]
    private AnimationCurve intensityCurve;

    private bool alive;

    // Use this for initialization
    private void Awake()
    {
        this.alive = true;
    }
	
    private void OnDestroy()
    {
        this.alive = false;
    }

    // Update is called once per frame
    public void Shake(float intensity, float frequency, float time)
    {
        this.StartCoroutine(this.MakeShake(intensity, frequency, time));
    }

    private IEnumerator MakeShake(float intensity, float frequency, float totalTime)
    {
        float time = 0.0f, inverseTotalTime = 1.0f / totalTime;
        while (time < totalTime && this.alive)
        {
            float normalizedTime = time * inverseTotalTime;
            float intensityMultiplier = this.intensityCurve.Evaluate(normalizedTime);

            float noiseSampleX = Mathf.PerlinNoise(Time.timeSinceLevelLoad * frequency, 0.0f) - 0.5f;
            float noiseSampleY = Mathf.PerlinNoise(0.0f, Time.timeSinceLevelLoad * frequency) - 0.5f;
            float noiseSampleZ = noiseSampleX * noiseSampleY;

            Vector3 offset = new Vector3(noiseSampleX * 2.0f, noiseSampleY * 0.5f, noiseSampleZ * 0.5f) * intensity;
            this.transform.position = this.centerPosition + (offset * intensityMultiplier);

            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = this.centerPosition;
    }

}