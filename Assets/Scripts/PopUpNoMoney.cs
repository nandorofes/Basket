using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpNoMoney : MonoBehaviour {

	[SerializeField]
	private Text text;
	[SerializeField]
	private Image image;
	[SerializeField]
	private Sprite creditsSprite;
	[SerializeField]
	private Sprite ticketsSprite;
	[SerializeField]
	private MainMenuSceneManager mainMenu;
	[SerializeField]
	private TrophiesShopPanelController trophiesBuyPanel;
	[SerializeField]
	private BallsShopPanel ballsBuyPanel;
	[SerializeField]
	private SkinsBuyPanel skinsBuyPanel;

	public void Hide()
	{
		this.gameObject.SetActive(false);
	}

	public void ShowNoTickets()
	{
		text.text = Localization.GetString("NotEnought.Tickets");
		image.sprite = ticketsSprite;
		this.gameObject.SetActive(true);
	}

	public void ShowNoCredits()
	{
		text.text = Localization.GetString("NotEnought.Credits");
		image.sprite = creditsSprite;
		this.gameObject.SetActive(true);
	}

	public void BotonShop(){
		Hide ();
		mainMenu.selectionLevelPanel.Hide ();
		mainMenu.selectionLevelMultiplayerPanel.Hide ();
		mainMenu.multiscreenMenuSystem.SetMenuInstantly(2);
		if (trophiesBuyPanel.gameObject.activeInHierarchy) {
			trophiesBuyPanel.Hide ();
		}
		if (ballsBuyPanel.gameObject.activeInHierarchy) {
			ballsBuyPanel.Hide ();
		}
		if (skinsBuyPanel.gameObject.activeInHierarchy) {
			skinsBuyPanel.Hide ();
		}
	}
}
