using UnityEngine;
using System.Collections;

public class DroneStage2 : MonoBehaviour {

	Animator drone;
	GameSceneManager sceneManager;
	[SerializeField]
	private AnimationCurve xMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));
	[SerializeField]
	private AnimationCurve yMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));

	// Use this for initialization
	void Start () {
	
		//drone = this.gameObject.GetComponent<Animator> ();
		sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
		xMovementCurve.preWrapMode = WrapMode.Loop;
		xMovementCurve.postWrapMode = WrapMode.Loop;
		yMovementCurve.preWrapMode = WrapMode.Loop;
		yMovementCurve.postWrapMode = WrapMode.Loop;
	}


	void Update()
	{
		if (sceneManager.fase >= 2) {
			for (int i = 2; i <= sceneManager.fase; i++) {
				float velocidad = 0.05f * i;
				transform.position = new Vector3 (xMovementCurve.Evaluate (Time.time * velocidad), yMovementCurve.Evaluate (Time.time * velocidad), transform.position.z);
			}
		} else {
			this.gameObject.transform.position = new Vector3 (0.339f, 2.27f, -2.16f);
		}
	}
}
