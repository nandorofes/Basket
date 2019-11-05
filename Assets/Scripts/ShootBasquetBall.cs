using UnityEngine;
using System.Collections;
using System;

public class ShootBasquetBall : MonoBehaviour {

	private Vector2 mouseStart;
	private Vector2 mouseEnd;
	private Vector2 touchStart;
	private Vector2 touchEnd;

	public float minDistance = 0.2f;
	public float maxTime = 1f;

	public Camera mainCamera;

	private float gestureTime = 0.0f;
	private bool mouseIsDown = false;
	private bool touched = false;

	RaycastHit hit;
	Vector3 dist;

	
	// Update is called once per frame
	private void Update()
	{
		#if UNITY_EDITOR
		//#if !UNITY_ANDROID
		// Control por ratón
		if (Input.GetMouseButtonDown(0))
		{
			
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (ray, out hit);
			if (hit.transform.gameObject.tag.Equals("balon")||hit.transform.gameObject.tag.Equals("balonTriple")||hit.transform.gameObject.tag.Equals("balonTicket")||hit.transform.gameObject.tag.Equals("balonTiempo")) {
			
				this.mouseStart = Input.mousePosition;
				hit.collider.gameObject.GetComponent<Bola> ().rb.isKinematic=true;
				this.gestureTime = 0.0f;
				this.mouseIsDown = true;
				dist = Camera.main.WorldToScreenPoint(hit.collider.gameObject.transform.position);
			} else {
				return;
			}

		}

		if (this.mouseIsDown)
		{
			this.gestureTime += Time.deltaTime;

			Vector3 curPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z);  

			Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);

			if (hit.transform.gameObject.tag.Equals("balon")||hit.transform.gameObject.tag.Equals("balonTriple")||hit.transform.gameObject.tag.Equals("balonTicket")||hit.transform.gameObject.tag.Equals("balonTiempo"))
			hit.collider.gameObject.transform.position=worldPos;

		}

		if (Input.GetMouseButtonUp(0))
		{
			
			/*if (gestureTime > maxTime) {
				return;
			} else */if (mouseIsDown==true){
				this.SendDetectedShoot(this.mouseStart, Input.mousePosition);
				this.mouseIsDown = false;
			}
		}
		#endif
		// Control por toque
		if (Input.touchCount > 0)
		{
			
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				Ray ray = mainCamera.ScreenPointToRay (touch.position);
				Physics.Raycast (ray, out hit);
				if (hit.transform.gameObject.tag.Equals("balon")||hit.transform.gameObject.tag.Equals("balonTriple")||hit.transform.gameObject.tag.Equals("balonTicket")||hit.transform.gameObject.tag.Equals("balonTiempo")) {
					this.touchStart = touch.position;
					hit.collider.gameObject.GetComponent<Bola> ().rb.isKinematic=true;
					this.gestureTime = 0.0f;
					touched = true;
					dist = Camera.main.WorldToScreenPoint(hit.collider.gameObject.transform.position);
				} else {
					return;
				}
			}

			if (touch.phase == TouchPhase.Moved)
			{
				this.gestureTime += Time.deltaTime;

				Vector3 curPos = new Vector3(touch.position.x, touch.position.y, dist.z);  

				Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);

				if (hit.transform.gameObject.tag.Equals("balon")||hit.transform.gameObject.tag.Equals("balonTriple")||hit.transform.gameObject.tag.Equals("balonTicket")||hit.transform.gameObject.tag.Equals("balonTiempo"))
					hit.collider.gameObject.transform.position=worldPos;
			}

			if (touch.phase == TouchPhase.Ended) {
				/*if (gestureTime > maxTime) {
					return;
				} else */if (touched==true){
					this.SendDetectedShoot (this.touchStart, touch.position);
					touched = false;
				}
			}
		}
	}

	private void SendDetectedShoot(Vector2 startPosition, Vector2 endPosition)
	{
		// Normalizar distancia recorrida (el valor va a depender de centímetros reales al dividir por los DPI)
		//Vector2 touchDelta = (endPosition - startPosition) / Screen.dpi;


		// Ignorar el evento si no se recorre una distancia mínima
		//if (touchDelta.magnitude < this.minDistance) return;

		// Calcular dirección y fuerza
		//float direction = touchDelta.x;
		//float force = touchDelta.y-gestureTime;

		// Enviar información

		//Vector3 shootInfo = new Vector3 (direction,force,force);
		//if (startPosition != null) {
		//Chutar(shootInfo);
		hit.collider.gameObject.GetComponent<Bola> ().rb.isKinematic=false;
		hit.collider.gameObject.GetComponent<Bola> ().Chutar();
		//}
	}


}
