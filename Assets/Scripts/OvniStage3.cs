using UnityEngine;
using System.Collections;

public class OvniStage3 : MonoBehaviour {

	[SerializeField]
	private AnimationCurve xMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));
	[SerializeField]
	private AnimationCurve yMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));

	GameSceneManager scenemanager;

	// Use this for initialization
	void Start () {
		scenemanager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
		xMovementCurve.preWrapMode = WrapMode.Loop;
		xMovementCurve.postWrapMode = WrapMode.Loop;
		yMovementCurve.preWrapMode = WrapMode.Loop;
		yMovementCurve.postWrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (scenemanager.fase >= 2) {
			for (int i = 2; i <= scenemanager.fase; i++) {
				float velocidad = 0.05f * i;
				transform.position = new Vector3 (xMovementCurve.Evaluate (Time.time * velocidad), yMovementCurve.Evaluate (Time.time * velocidad), transform.position.z);
			}
		} else {
			this.gameObject.transform.position = new Vector3 (0.223f, 2.088f, -2.16f);
		}
	}
}
