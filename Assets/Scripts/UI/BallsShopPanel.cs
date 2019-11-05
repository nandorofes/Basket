using UnityEngine;
using System.Collections;

public class BallsShopPanel : MonoBehaviour {

	public GameObject basicoAdquirido;
	public GameObject hieloAdquirido;
	public GameObject triplesAdquirido;
	public GameObject ticketsAdquirido;
	public GameObject tuto;
	bool comprarNormal=false;
	bool comprarHielo=false;
	bool comprarTriple=false;
	bool comprarTicket=false;
	public ConfirmationPanelController confirmationPanel;
	public ShopPanelMenuItem shopPanel;
	public PopUpNoMoney popUpNoMoney;

	[SerializeField]
	private ScrollSnapRect scroll;
	[SerializeField]
	private GameObject next;
	[SerializeField]
	private GameObject prev;

	void Awake(){
		if (this.confirmationPanel != null)
		{
			this.confirmationPanel.OnYesPressed += this.OnConfirmPurchase;
			this.confirmationPanel.OnNoPressed += this.OnCancelPurchase;
		}
	}

	public void Show(){
		this.gameObject.SetActive (true);
		if (GameManager.Instance.GamePersistentData.Tutorial == false && GameManager.Instance.GamePersistentData.Tickets >= 100) {
			tuto.transform.parent.GetChild (0).gameObject.SetActive (false);
			tuto.SetActive (true);
		}

		if (GameManager.Instance.GamePersistentData.BalonNormal) {
			basicoAdquirido.SetActive (true);
		}
		if (GameManager.Instance.GamePersistentData.BalonSegundoExtra) {
			hieloAdquirido.SetActive (true);
		}
		if (GameManager.Instance.GamePersistentData.BalonTriple) {
			triplesAdquirido.SetActive (true);
		}
		if (GameManager.Instance.GamePersistentData.BalonTicketExtra) {
			ticketsAdquirido.SetActive (true);
		}
	}

	public void Hide(){
		this.gameObject.SetActive (false);
	}

	public void BuyBasico(){
		if (GameManager.Instance.GamePersistentData.Tickets >= 100 && !GameManager.Instance.GamePersistentData.BalonNormal) {
			confirmationPanel.Show ();
			comprarNormal = true;
		} else {
			popUpNoMoney.ShowNoTickets ();
		}
	}

	public void BuyHielo(){
		if (GameManager.Instance.GamePersistentData.Tickets >= 1000&& !GameManager.Instance.GamePersistentData.BalonSegundoExtra) {
			confirmationPanel.Show ();
			comprarHielo = true;
		} else {
			popUpNoMoney.ShowNoTickets ();
		}
	}

	public void BuyTriples(){
		if (GameManager.Instance.GamePersistentData.Tickets >= 2500&& !GameManager.Instance.GamePersistentData.BalonTriple) {
			confirmationPanel.Show ();
			comprarTriple = true;
		} else {
			popUpNoMoney.ShowNoTickets ();
		}
	}

	public void BuyTickets(){
		if (GameManager.Instance.GamePersistentData.Tickets >= 5250&& !GameManager.Instance.GamePersistentData.BalonTicketExtra) {
			confirmationPanel.Show ();
			comprarTicket = true;
		} else {
			popUpNoMoney.ShowNoTickets ();
		}
	}

	void OnConfirmPurchase (){

		if (comprarNormal) {
			GameManager.Instance.GamePersistentData.Tickets -= 100;
			GameManager.Instance.GamePersistentData.BalonNormal = true;
			basicoAdquirido.SetActive(true);
			//GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Tickets", 100, "BasicBall", "BasicBall");
			GameManager.Instance.SaveData ();
			comprarNormal = false;
			tuto.SetActive (false);
			tuto.transform.parent.gameObject.SetActive (false);
			//Hide ();
			shopPanel.tuto.SetActive (false);
			GameManager.Instance.GamePersistentData.Tutorial = true;
		}
		if (comprarHielo) {
			GameManager.Instance.GamePersistentData.Tickets -= 1000;
			GameManager.Instance.GamePersistentData.BalonSegundoExtra = true;
			hieloAdquirido.SetActive(true);
			//GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Tickets", 1000, "IceBall", "IceBall");
			GameManager.Instance.SaveData ();
			comprarHielo = false;
		}
		if (comprarTriple) {
			GameManager.Instance.GamePersistentData.Tickets -= 2500;
			GameManager.Instance.GamePersistentData.BalonTriple = true;
			triplesAdquirido.SetActive(true);
			//GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Tickets", 2500, "TripleBall", "TripleBall");
			GameManager.Instance.SaveData ();
			comprarTriple = false;
		}
		if (comprarTicket) {
			GameManager.Instance.GamePersistentData.Tickets -= 5250;
			GameManager.Instance.GamePersistentData.BalonTicketExtra = true;
			ticketsAdquirido.SetActive(true);
			//GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Tickets", 5250, "BonusBall", "BonusBall");
			GameManager.Instance.SaveData ();
			comprarTicket = false;
		}
		confirmationPanel.Hide ();
	}

	void OnCancelPurchase(){
		confirmationPanel.Hide();
		comprarNormal=false;
		comprarHielo=false;
		comprarTriple=false;
		comprarTicket=false;
	}

	void Update (){
		if (scroll._currentPage == 0) {
			prev.SetActive (false);
		} else if (scroll._currentPage == 3) {
			next.SetActive (false);
		} else {
			prev.SetActive (true);
			next.SetActive (true);
		}
	}
}
