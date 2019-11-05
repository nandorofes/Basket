using UnityEngine;
using System.Collections;

public class Tuto : MonoBehaviour {

	public bool startMovement=false;
	public bool startbocadillo=false;
	public GameObject negra;
	public GameObject bocadillo;
	public GameObject buttonShop;
	public GameObject flecha;
	float time = 0;
	float time2=0;
	[SerializeField]
	private AnimationCurve negraMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));
	[SerializeField]
	private AnimationCurve flechaMovementCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));
	[SerializeField]
	private AnimationCurve globoScaleCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 0.0f));

	public void Show(){
		this.gameObject.SetActive (true);
		StartCoroutine(MostrarGlobo(1f));
		startMovement = true;

	}
	// Use this for initialization
	void Start () {
		flechaMovementCurve.preWrapMode = WrapMode.Loop;
		flechaMovementCurve.postWrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		if (startMovement) {
			time += Time.deltaTime;
			negra.transform.position = new Vector3 (negraMovementCurve.Evaluate ( time* 0.7f), negra.transform.position.y, negra.transform.position.z);
			flecha.transform.position = new Vector3 (flecha.transform.position.x, flechaMovementCurve.Evaluate (Time.time ), flecha.transform.position.z);
		}
		if (startbocadillo) {
			time2 += Time.deltaTime;
			bocadillo.transform.localScale = new Vector3 (globoScaleCurve.Evaluate (time2*1.5f), bocadillo.transform.localScale.y, bocadillo.transform.localScale.z);
		}
	}

	IEnumerator MostrarGlobo(float time){
		yield return new WaitForSeconds (time);
		bocadillo.SetActive (true);
		buttonShop.SetActive (true);
		flecha.SetActive (true);
		startbocadillo = true;
	}
}
