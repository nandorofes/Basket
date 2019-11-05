using System;
using UnityEngine;
using UnityEngine.UI;

public class TrophiesShopPanelController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public ConfirmationPanelController confirmationPanel;
	public Image imagenMaquina;
	public Text textoDesbloqueo;
	public Text textoPrecio;
	public LevelSelectionPanelController levelControler;
	public LevelMultiplayerSelectionPanel levelMultiplayerControler;
	public PopUpNoMoney popUpMoney;
    //public BuyButtonController planetButtonController;

    private string stageKey;
	private int precio;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show(string stage)
    {
        this.gameObject.SetActive(true);
		switch (stage) {
		case "Stage.DroneZone":
			this.stageKey = stage;
			precio = 1000;
			imagenMaquina.sprite = GameManager.Instance.GetStageInfo (stage).EnabledIcon;
			textoDesbloqueo.text = string.Format (Localization.GetString("PopUp.Maquina"), precio.ToString ());
			textoPrecio.text = precio.ToString ();
			break;
		case "Stage.PlanetZone":
			this.stageKey = stage;
			precio = 2000;
			imagenMaquina.sprite = GameManager.Instance.GetStageInfo (stage).EnabledIcon;
			textoDesbloqueo.text = string.Format (Localization.GetString("PopUp.Maquina"), precio.ToString ());
			textoPrecio.text = precio.ToString ();
			break;
		case "Stage.4":
			this.stageKey = stage;
			precio = 3000;
			imagenMaquina.sprite = GameManager.Instance.GetStageInfo (stage).EnabledIcon;
			textoDesbloqueo.text = string.Format (Localization.GetString("PopUp.Maquina"), precio.ToString ());
			textoPrecio.text = precio.ToString ();
			break;
		case "Stage.5":
			this.stageKey = stage;
			precio = 4000;
			imagenMaquina.sprite = GameManager.Instance.GetStageInfo (stage).EnabledIcon;
			textoDesbloqueo.text = string.Format (Localization.GetString("PopUp.Maquina"), precio.ToString ());
			textoPrecio.text = precio.ToString ();
			break;
		}

    }

    // Manejadores de eventos de Unity

	public void ComprarRecreativa(){
		switch (stageKey) {
		case "Stage.DroneZone":
			if (GameManager.Instance.GamePersistentData.Tickets >= precio && !GameManager.Instance.GamePersistentData.Escenario2) {
				this.confirmationPanel.Show ();
				this.confirmationPanel.OnYesPressed += this.ConfirmBuy;
				this.confirmationPanel.OnNoPressed += this.CancelBuy;
			} else {
				popUpMoney.ShowNoTickets ();
			}
			break;
		case "Stage.PlanetZone":
			if (GameManager.Instance.GamePersistentData.Tickets >= precio && !GameManager.Instance.GamePersistentData.Escenario3) {
				this.confirmationPanel.Show ();
				this.confirmationPanel.OnYesPressed += this.ConfirmBuy;
				this.confirmationPanel.OnNoPressed += this.CancelBuy;
			} else {
				popUpMoney.ShowNoTickets ();
			}
			break;
		case "Stage.4":
			if (GameManager.Instance.GamePersistentData.Tickets >= precio && !GameManager.Instance.GamePersistentData.Escenario4) {
				this.confirmationPanel.Show ();
				this.confirmationPanel.OnYesPressed += this.ConfirmBuy;
				this.confirmationPanel.OnNoPressed += this.CancelBuy;
			} else {
				popUpMoney.ShowNoTickets ();
			}
			break;
		case "Stage.5":
			if (GameManager.Instance.GamePersistentData.Tickets >= precio && !GameManager.Instance.GamePersistentData.Escenario5) {
				this.confirmationPanel.Show ();
				this.confirmationPanel.OnYesPressed += this.ConfirmBuy;
				this.confirmationPanel.OnNoPressed += this.CancelBuy;
			} else {
				popUpMoney.ShowNoTickets ();
			}
			break;
		}
	}

    //public void BuyPlanet()
    //{
    //    if (GameManager.Instance.GamePersistentData.Tickets >= 3000 && !GameManager.Instance.GamePersistentData.Escenario4) {
    //        this.stageKey = "Stage.PlanetZone";
    //        this.confirmationPanel.Show();
    //        this.confirmationPanel.OnYesPressed += this.ConfirmBuy;
    //        this.confirmationPanel.OnNoPressed += this.CancelBuy;
    //    }
    //}

    // Manejadores de eventos
    private void ConfirmBuy()
    {
        switch (stageKey)
        {
        case "Stage.DroneZone":
			GameManager.Instance.GamePersistentData.Escenario2 = true;
        	break;
		case "Stage.PlanetZone":
			GameManager.Instance.GamePersistentData.Escenario3 = true;
			break;
		case "Stage.4":
			GameManager.Instance.GamePersistentData.Escenario4 = true;
			break;
		case "Stage.5":
			GameManager.Instance.GamePersistentData.Escenario5 = true;
			break;
        }

		GameManager.Instance.GamePersistentData.Tickets -= precio;
		GameManager.Instance.SaveData ();

        this.confirmationPanel.OnYesPressed -= this.ConfirmBuy;
        this.confirmationPanel.OnNoPressed -= this.CancelBuy;

        this.confirmationPanel.Hide();
		if (levelControler.gameObject.activeInHierarchy) {
			levelControler.Hide ();
			levelControler.Show ();
		}
		if (levelMultiplayerControler.gameObject.activeInHierarchy) {
			levelMultiplayerControler.Hide ();
			levelMultiplayerControler.Show ();
		}
		Hide ();
    }

    private void CancelBuy()
    {
        this.confirmationPanel.OnYesPressed -= this.ConfirmBuy;
        this.confirmationPanel.OnNoPressed -= this.CancelBuy;

        this.confirmationPanel.Hide();
    }

}