using UnityEngine;
using System.Collections;

public class entraCanasta : MonoBehaviour {

	public bool entro;

	void OnTriggerEnter (Collider obj){
		if (obj.gameObject.tag == "balon"||obj.gameObject.tag == "balonTriple"||obj.gameObject.tag == "balonTicket"||obj.gameObject.tag == "balonTiempo") {
			entro = true;
		}
	}
}
