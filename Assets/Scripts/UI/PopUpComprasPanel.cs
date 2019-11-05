using UnityEngine;
using System.Collections;

public class PopUpComprasPanel : MonoBehaviour {

	//COSTE BALONES
	public int balonNormalCost=10;
	public int balonTriplesCost=10;
	public int balonSegundoCost=10;
	public int balonTicketCost=10;

	//COSTE TROFEOS
	public int trofeo1cost=10;
	public int trofeo2cost=20;
	public int trofeo3cost=30;
	public int trofeo4cost=40;

	//COMPRAR BALONES
	public void BuyBalonNormal (){
		if (GameManager.Instance.GamePersistentData.BalonNormal == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < balonNormalCost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= balonNormalCost;
				GameManager.Instance.GamePersistentData.BalonNormal = true;
			}
		}
	}

	public void BuyBalonTriple (){
		if (GameManager.Instance.GamePersistentData.BalonTriple == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < balonTriplesCost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= balonTriplesCost;
				GameManager.Instance.GamePersistentData.BalonTriple = true;
			}
		}
	}

	public void BuyBalonTicket (){
		if (GameManager.Instance.GamePersistentData.BalonTicketExtra == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < balonTicketCost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= balonTicketCost;
				GameManager.Instance.GamePersistentData.BalonTicketExtra = true;
			}
		}
	}

	public void BuyBalonSegundo (){
		if (GameManager.Instance.GamePersistentData.BalonSegundoExtra == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < balonSegundoCost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= balonSegundoCost;
				GameManager.Instance.GamePersistentData.BalonSegundoExtra = true;
			}
		}
	}

	//COMPRAR TROFEOS
	public void BuyTrofeo1 (){
		if (GameManager.Instance.GamePersistentData.Escenario2 == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < trofeo1cost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= trofeo1cost;
				GameManager.Instance.GamePersistentData.Escenario2 = true;
			}
		}
	}

	public void BuyTrofeo2 (){
		if (GameManager.Instance.GamePersistentData.Escenario3 == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < trofeo2cost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= trofeo2cost;
				GameManager.Instance.GamePersistentData.Escenario3 = true;
			}
		}
	}

	public void BuyTrofeo3 (){
		if (GameManager.Instance.GamePersistentData.Escenario4 == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < trofeo3cost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= trofeo3cost;
				GameManager.Instance.GamePersistentData.Escenario4 = true;
			}
		}
	}

	public void BuyTrofeo4 (){
		if (GameManager.Instance.GamePersistentData.Escenario5 == true) {
			Debug.Log ("Ya tienes este objeto");
		} else {
			if (GameManager.Instance.GamePersistentData.Tickets < trofeo4cost) {
				//Aqui tiene que salir la opcion de comprar tickets
				return;
			} else {
				GameManager.Instance.GamePersistentData.Tickets -= trofeo4cost;
				GameManager.Instance.GamePersistentData.Escenario5 = true;
			}
		}
	}
}
