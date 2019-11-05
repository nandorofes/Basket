using UnityEngine;
using System.Collections;

public class Flecha : MonoBehaviour {


	[SerializeField]
	private AnimationCurve flechaMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));

	// Use this for initialization
	void Start () {
		flechaMovementCurve.preWrapMode = WrapMode.Loop;
		flechaMovementCurve.postWrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, flechaMovementCurve.Evaluate (Time.time ), this.gameObject.transform.position.z);
	}
}
