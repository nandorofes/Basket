using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entraCanastaOnline : MonoBehaviour {

	public bool entro;

	void OnTriggerEnter (Collider obj){
		if (obj.gameObject.tag == "balon"||obj.gameObject.tag == "balonTriple"||obj.gameObject.tag == "balonTicket"||obj.gameObject.tag == "balonTiempo") {
			entro = true;
		}
	}
}
