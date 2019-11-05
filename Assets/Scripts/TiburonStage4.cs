using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiburonStage4 : MonoBehaviour {

	[SerializeField]
	private AnimationCurve xMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));
	[SerializeField]
	private AnimationCurve yMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));
	[SerializeField]
	private AnimationCurve ScaleCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));
	[SerializeField]
	private float velocidad;

	GameSceneManager scenemanager;

	// Use this for initialization
	void Start () {
		scenemanager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
		xMovementCurve.preWrapMode = WrapMode.Loop;
		xMovementCurve.postWrapMode = WrapMode.Loop;
		yMovementCurve.preWrapMode = WrapMode.Loop;
		yMovementCurve.postWrapMode = WrapMode.Loop;
		ScaleCurve.preWrapMode = WrapMode.Loop;
		ScaleCurve.postWrapMode = WrapMode.Loop;
	}

	// Update is called once per frame
	void Update()
	{
		if (scenemanager.fase >= 2) {
			for (int i = 2; i <= scenemanager.fase; i++) {
				transform.position = new Vector3 (xMovementCurve.Evaluate (Time.time * velocidad), yMovementCurve.Evaluate (Time.time * velocidad), -3);
				transform.localScale = new Vector3 (1, ScaleCurve.Evaluate (Time.time * velocidad), 1);
			}
		} else {
			this.gameObject.transform.position = new Vector3 (0.223f,-5f, -2.16f);
		}
	}
}
