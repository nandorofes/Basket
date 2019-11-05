using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    public MultiscreenMenuSystem multiscreenMenuSystem;

    //MAINMENU
    public Text creditos;
    public Text tiempoNextCredit;
    public Text tickets;

    CreditsManagement creditsManagement;

    //Mode Free Credits for time
    public GameObject buyButtonCredits;
    public GameObject buyButtonCreditsPurchased;
    public Text creditsTimeText;

    //Panels
    public GameObject gameButonsPanel;
    public LevelSelectionPanelController selectionLevelPanel;
	public LevelMultiplayerSelectionPanel selectionLevelMultiplayerPanel;
    public ShopPanelMenuItem shopPanelController;
    public GameObject mainMenuButtons;
    public GameObject trophiesPanel;
    public SkinsBuyPanel skinsBuyPanel;
    public SkinSelectionPanelController skinSelectionPanel;
	public PopUpNoMoney popUpNoMoney;
	public GameObject popUpNoInternet;
    public ConfirmationPanelController confirmationPanel;

    private string skinAboutToBuy;

    [SerializeField]
    private LoadingPanelController loadingPanel;

    [SerializeField]
    private AudioClip buttonClip;
    [SerializeField]
    private AudioClip menuChangeClip;

	public bool isConected=false;

	//[SerializeField]
	//private Text textMenuDebug;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        #pragma warning disable 168, 3021
        var gm = GameManager.Instance;
        var am = AudioManager.Instance;
        var fm = FacebookManager.Instance;
        //var gam = GoogleAdsManager.Instance;
        //var uam = UnityAdsManager.Instance;
        #pragma warning restore 168, 3021

        creditsManagement = this.gameObject.GetComponent<CreditsManagement>();

        if (GameManager.Instance.VieneDeTienda)
        {
            this.multiscreenMenuSystem.SetMenuInstantly(2);
            GameManager.Instance.VieneDeTienda = false;
        }

        this.StartCoroutine(this.ShowMenuEntryAd());

		//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Main Menu");   

        // Enlazar eventos
        SkinSelectionItem.OnSkinSelectionItemPressed += this.OnSkinSelected;
        SkinSelectionBuyShortcut.OnSkinSelectionBuyShortcutPressed += this.OnBuyShortcutPressed;

        SkinBuyItemController.OnSkinBuyItemPressed += this.OnSkinBuyAttempt;

        //UnityAdsManager.Instance.OnVideoWatched += this.OnVideoWatched;

		if (GameManager.Instance.AfterMatch) {
			multiscreenMenuSystem.SetMenuInstantly (1);
			GameManager.Instance.AfterMatch = false;
			Debug.Log ("SelectedNormalSkinList.Count=" + GameManager.Instance.GamePersistentData.SelectedNormalSkinList.Count);
			for (int i = 0; i < GameManager.Instance.GamePersistentData.SelectedNormalSkinList.Count; i++) {
				Debug.Log ("SelectedNormalSkin[" + i + "]=" + GameManager.Instance.GamePersistentData.SelectedNormalSkinList [i]);
			}
		} else {
			GameManager.Instance.SetSkinInicio ();
		}

		//GameManager.Instance.GamePersistentData.Tickets = ;
    }

    private void OnDestroy()
    {
        // Desvincular eventos
        SkinSelectionItem.OnSkinSelectionItemPressed -= this.OnSkinSelected;
        SkinSelectionBuyShortcut.OnSkinSelectionBuyShortcutPressed -= this.OnBuyShortcutPressed;

        SkinBuyItemController.OnSkinBuyItemPressed -= this.OnSkinBuyAttempt;

       // UnityAdsManager unityAdsManager = UnityAdsManager.Instance;
        //if (unityAdsManager != null)
         //   unityAdsManager.OnVideoWatched -= this.OnVideoWatched;
    }

    void Start()
    {
       // OneSignal.Init("63e8d4c6-bcc3-492c-9779-5cb16701eb7e", "665772302517", HandleNotification);
       // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.DEBUG, OneSignal.LOG_LEVEL.DEBUG);

		StartCoroutine(checkInternetConnection());

		//textMenuDebug.text = "He pasado por start, asi que si no cambio es que lo siguiente da un null";
		//StartCoroutine (TestPackage ());
		//HeyzapAds.SetGdprConsent(true);
		//textMenuDebug.text = "Si no cambio es que da null al iniciar heyzap";
		//HeyzapAds.Start("f49cba9425b3939235e7fbc7fdbfc33a", HeyzapAds.FLAG_NO_OPTIONS);
		//textMenuDebug.text = "Cargo heyzap";
		//StartCoroutine (MediationTest ());
    }

	void AsignSkins(){
		Debug.Log ("SelectedNormalSkinList.Count=" + GameManager.Instance.GamePersistentData.SelectedNormalSkinList.Count);

		for (int i = 0; i < GameManager.Instance.GamePersistentData.SelectedNormalSkinList.Count; i++) {
			Debug.Log ("SelectedNormalSkin[" + i + "]=" + GameManager.Instance.GamePersistentData.SelectedNormalSkinList [i]);
			GameManager.Instance.SetSkin (GameManager.Instance.GamePersistentData.SelectedNormalSkinList [i]);
		}
		if (GameManager.Instance.GamePersistentData.BalonTicketExtra) {
			GameManager.Instance.SetSkin (GameManager.Instance.GamePersistentData.SelectedBonusSkin);
		}
		if (GameManager.Instance.GamePersistentData.BalonTriple) {
			GameManager.Instance.SetSkin (GameManager.Instance.GamePersistentData.SelectedTripleSkin);
		}
		if (GameManager.Instance.GamePersistentData.BalonSegundoExtra) {
			GameManager.Instance.SetSkin (GameManager.Instance.GamePersistentData.SelectedTimeSkin);
		}
	}

	IEnumerator checkInternetConnection(){
		WWW www = new WWW("http://google.com");
		yield return www;
		if (www.error != null) {
			isConected = false;
		} else {
			isConected = true;
		}
	} 

	IEnumerator MediationTest(){
		yield return new WaitForSeconds(5);
		//HeyzapAds.ShowMediationTestSuite();
		//textMenuDebug.text = "Inicio la mediation test suite";
		yield return new WaitForSeconds(5);
		//HeyzapAds.ShowMediationTestSuite();
		//textMenuDebug.text = "Probando de nuevo despues de 10s";
	}

	IEnumerator TestPackage(){
		yield return new WaitForSeconds (5);
		//textMenuDebug.text=(Application.identifier);
		yield return new WaitForSeconds (2.5f);
		yield return new WaitForSeconds (2.5f);

	}

    private void Update()
    {
        tickets.text = GameManager.Instance.GamePersistentData.Tickets.ToString();
        creditos.text = creditsManagement.credits.ToString();

        if (creditsManagement.timePurchased)
        {
            buyButtonCredits.SetActive(false);
            buyButtonCreditsPurchased.SetActive(true);

            int hours = Mathf.FloorToInt((float)creditsManagement.tiempoFree.TotalHours);
            int minutes = creditsManagement.tiempoFree.Minutes;
            int seconds = creditsManagement.tiempoFree.Seconds;
            creditsTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            buyButtonCredits.SetActive(true);
            buyButtonCreditsPurchased.SetActive(false);
            if (creditsManagement.credits >= 10)
            {
                tiempoNextCredit.text = "MAX";
            }
            else
            {
                int minutes = creditsManagement.tiempoNextCredit.Minutes;
                int seconds = creditsManagement.tiempoNextCredit.Seconds;
                tiempoNextCredit.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        // Salida del juego
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            this.confirmationPanel.Show();
            this.confirmationPanel.SetMessage(Localization.GetString("ConfirmationPanel.WantToExit"));

            this.confirmationPanel.OnYesPressed += ConfirmExit;
            this.confirmationPanel.OnNoPressed += CancelExit;
        }
    }

    // --
    private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive)
    {
    }

    public void GoToMenu(int menuNumber)
    {
        this.multiscreenMenuSystem.SetMenu(menuNumber);
    }

    public void PlayButtonPressed()
    {
        selectionLevelPanel.gameObject.SetActive(true);
    }

	public void MultiplayerButtonPressed()
	{
		selectionLevelMultiplayerPanel.gameObject.SetActive(true);
	}

    public void StageLoad(string stageKey)
    {
        var referencedStage = GameManager.Instance.GetStageInfo(stageKey);
		if (creditsManagement.credits >= 1) {
			if (referencedStage != null) {
				GameManager.Instance.GamePersistentData.NumMaquina = referencedStage.NumeroMaquina;
				PlayerPrefs.SetString ("savedTime", creditsManagement.nextCredit.ToBinary ().ToString ());
				PlayerPrefs.SetInt ("credits", creditsManagement.credits);

				// Cargar escena
				this.loadingPanel.Show ();
				SceneManager.LoadSceneAsync ("prueba");
			}
		} else {
			//Aqui habra que meter un pop up avisando de que no le quedan creditos
			popUpNoMoney.ShowNoCredits();
		}
    }

	public void MultiplayerStageLoad(string stageKey)
	{
		if (isConected) {
			if (creditsManagement.credits >= 1) {
				var referencedStage = GameManager.Instance.GetStageInfo (stageKey);
				if (referencedStage != null) {
					GameManager.Instance.GamePersistentData.NumMaquina = referencedStage.NumeroMaquina;
					GameManager.Instance.Multiplayer = true;
					switch (stageKey) {
					case "Stage.BullZone":
						GameManager.Instance.TicketsBet = 100;
				//GameManager.Instance.GamePersistentData.Tickets -= 50;
						break;
					case "Stage.DroneZone":
						GameManager.Instance.TicketsBet = 200;
				//GameManager.Instance.GamePersistentData.Tickets -= 100;
						break;
					case "Stage.PlanetZone":
						GameManager.Instance.TicketsBet = 500;
				//GameManager.Instance.GamePersistentData.Tickets -= 250;
						break;
					case "Stage.4":
						GameManager.Instance.TicketsBet = 1000;
				//GameManager.Instance.GamePersistentData.Tickets -= 500;
						break;
					case "Stage.5":
						GameManager.Instance.TicketsBet = 2000;
				//GameManager.Instance.GamePersistentData.Tickets -= 1000;
						break;
					}

					if (GameManager.Instance.GamePersistentData.Tickets >= GameManager.Instance.TicketsBet / 2) {
						
						// Cargar escena
						this.loadingPanel.Show ();
						if (creditsManagement.credits >= 10)
						{
							creditsManagement.nextCredit = DateTime.Now.AddMinutes(15);
						}
						creditsManagement.credits -= 1;
						PlayerPrefs.SetString ("savedTime", creditsManagement.nextCredit.ToBinary ().ToString ());
						PlayerPrefs.SetInt ("credits", creditsManagement.credits);
						SceneManager.LoadSceneAsync ("Multiplayer");
					} else {
						//AQUI HABRA QUE METER ALGO, PopUp avisando de que no tiene dinero con un boton a la tienda y otro a ver video
						popUpNoMoney.ShowNoTickets();
					}
				}
			} else {
				//Aqui habra que meter un pop up avisando de que no le quedan creditos
				popUpNoMoney.ShowNoCredits();
			}
		} else {
			//no tiene internet
			popUpNoInternet.SetActive(true);
			StartCoroutine(checkInternetConnection());
		}
	}

    // Manejadores de eventos de Unity

    public void TappedToPlay()
    {
       // GoogleAdsManager.Instance.ShowInterstitialWhenReady();
    }

    public void InviteFacebookFriends()
    {
        FacebookManager.Instance.RequestAppInvitation(new Uri("https://fb.me/1096033620456399"));/*"Top Real Basketball 2016", Localization.GetString("Facebook.AppInvitation"));*/
    }

    public void LoginToFacebook()
    {
        if (!FacebookManager.Instance.LoggedIn)
            FacebookManager.Instance.RequestLogin();
//        else
//            FacebookManager.Instance.RequestLogout();
    }

    public void WatchVideo()
    {
		//UnityAdsManager.Instance.ShowRewardedVideo();
		GameManager.Instance.ShowRewardedVideo(false,true,false);
    }

    public void BackDesdeSelectStage()
    {
        mainMenuButtons.SetActive(true);
        selectionLevelPanel.gameObject.SetActive(false);
    }

    public void ShowTrophiesPanel()
    {
        this.trophiesPanel.SetActive(true);
    }

    // Manejadores de eventos de Unity(Start Panel/RSS)

    public void InstagramButtonPressed()
    {
        Application.OpenURL("https://www.instagram.com/savegamesstudio/?hl=es");
    }

    public void TwitterButtonPressed()
    {
        Application.OpenURL("https://twitter.com/SaveGamesStudio?lang=es");
    }

    public void FacebookButtonPressed()
    {
        Application.OpenURL("https://www.facebook.com/SaveGameStudio/");
    }

    // Manejadores de eventos de Unity (barra inferior)

    public void SaveGamesButtonPressed()
    {
        Application.OpenURL("https://www.savegames.es/");
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.MusicMuted = !AudioManager.Instance.MusicMuted;
    }

    public void ToggleSound()
    {
        AudioManager.Instance.AmbientSoundMuted = !AudioManager.Instance.AmbientSoundMuted;
        AudioManager.Instance.EffectSoundMuted = !AudioManager.Instance.EffectSoundMuted;
    }

    public void SwitchLanguage()
    {
        if (GameManager.Instance.GameLanguage == "en")
            GameManager.Instance.GameLanguage = "es";
        else
            GameManager.Instance.GameLanguage = "en";
    }

    // Manejadores de eventos de Unity (otros)
    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySoundEffect(this.buttonClip);
    }

    public void PlayMenuChangeSound()
    {
        AudioManager.Instance.PlaySoundEffect(this.menuChangeClip);
    }

    public void RedirectToFacebookPage()
    {
        //Application.OpenURL("https://www.facebook.com/toprealbasketball2016/");
    }

    // Corrutinas
    private IEnumerator ExitCorroutine()
    {
       // GoogleAdsManager.Instance.OnInterstitialReady += () =>
        //{ 
       //     GameManager.Instance.ExitApplication(0.25f);
       // };
       // GoogleAdsManager.Instance.OnInterstitialWasNotLoaded += () =>
       // {
            GameManager.Instance.ExitApplication(0.25f);
       // };

        //GoogleAdsManager.Instance.RequestAndShowInterstitial();
        yield return null;
    }

    private IEnumerator ShowMenuEntryAd()
    {
        yield return new WaitForSeconds(0.5f);
       // GoogleAdsManager.Instance.RequestInterstitial();
    }

    // Manejadores de eventos (compra de skins)
    private void OnSkinBuyAttempt(string skinKey)
    {
        this.skinAboutToBuy = skinKey;
        int currentTickets = GameManager.Instance.GamePersistentData.Tickets;
        int skinTickets = GameManager.Instance.GetSkinInfo(this.skinAboutToBuy).Price;
		SkinTypes skinType= GameManager.Instance.GetSkinInfo (skinAboutToBuy).Type;
        bool skinNotPurchased = !GameManager.Instance.GamePersistentData.AcquiredSkinsList.Contains(skinKey);

		if ((currentTickets >= skinTickets) && skinNotPurchased) {
			confirmationPanel.messageText.text = Localization.GetString ("Are.You.Sure");// "¿Estás seguro?";
			switch (skinType) {
			case SkinTypes.Bonus:
				if (!GameManager.Instance.GamePersistentData.BalonTicketExtra) {
					confirmationPanel.messageText.text = Localization.GetString ("Not.Skin.Yet");// "Aún no tienes el balón para esta skin ¿Estás seguro?";
				}
				break;
			case SkinTypes.Time:
				if (!GameManager.Instance.GamePersistentData.BalonSegundoExtra) {
					confirmationPanel.messageText.text = Localization.GetString ("Not.Skin.Yet");// "Aún no tienes el balón para esta skin ¿Estás seguro?";
				}
				break;
			case SkinTypes.Triple:
				if (!GameManager.Instance.GamePersistentData.BalonTriple) {
					confirmationPanel.messageText.text = Localization.GetString ("Not.Skin.Yet");// "Aún no tienes el balón para esta skin ¿Estás seguro?";
				}
				break;
			}

			this.confirmationPanel.Show ();

			this.confirmationPanel.OnYesPressed += ConfirmSkinBuy;
			this.confirmationPanel.OnNoPressed += CancelSkinBuy;
		} else {
			popUpNoMoney.ShowNoTickets ();
		}
    }

    private void CancelSkinBuy()
    {
        this.confirmationPanel.Hide();

        this.confirmationPanel.OnYesPressed -= ConfirmSkinBuy;
        this.confirmationPanel.OnNoPressed -= CancelSkinBuy;
    }

    private void ConfirmSkinBuy()
    {
        SkinDefinition skin = GameManager.Instance.GetSkinInfo(this.skinAboutToBuy);
        if (skin != null)
        {
            GameManager.Instance.GamePersistentData.Tickets -= skin.Price;
            GameManager.Instance.GamePersistentData.AcquiredSkinsList.Add(skin.Key);
			//GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Tickets", skin.Price, skin.Key, skin.NameLocalizationKey);
            this.skinsBuyPanel.UpdateControls();
        }

        GameManager.Instance.SetSkin(this.skinAboutToBuy);

        this.confirmationPanel.Hide();

        this.confirmationPanel.OnYesPressed -= ConfirmSkinBuy;
        this.confirmationPanel.OnNoPressed -= CancelSkinBuy;
    }

    // Manejadores de eventos (selección de skins)
    private void OnSkinSelected(string skinKey)
    {
        GameManager.Instance.SetSkin(skinKey);
        this.skinSelectionPanel.UpdateControls();
    }

    private void OnBuyShortcutPressed()
    {
        this.skinSelectionPanel.Hide();
		skinsBuyPanel.Show ();
		GameObject tabSelected = skinSelectionPanel.transform.GetChild (0).GetChild (0).GetChild (3).gameObject;
		switch (tabSelected.name) {
		case "Normal tab":
			skinsBuyPanel.SetActiveTab (0);
			GameObject.Find ("Normal tab buton").GetComponent<SkinSelectionPanelButtons> ().Activado ();
			break;
		case "Ice tab":
			skinsBuyPanel.SetActiveTab (2);
			GameObject.Find ("Ice tab buton").GetComponent<SkinSelectionPanelButtons> ().Activado ();
			break;
		case"Bonus tab ":
			skinsBuyPanel.SetActiveTab (3);
			GameObject.Find ("Bonus tab buton").GetComponent<SkinSelectionPanelButtons> ().Activado ();
			break;
		case "Triple tab":
			skinsBuyPanel.SetActiveTab (1);
			GameObject.Find ("Triple tab buton").GetComponent<SkinSelectionPanelButtons> ().Activado ();
			break;
		default:
			skinsBuyPanel.SetActiveTab (0);
			GameObject.Find ("Normal tab buton").GetComponent<SkinSelectionPanelButtons> ().Activado ();
			break;
		}

        //this.GoToMenu(2);
    }

    // Manejadores de eventos (otros)
    private void OnVideoWatched()
    {
        GameManager.Instance.GamePersistentData.Tickets += 10;
		GameManager.Instance.SaveData ();
    }

    private void CancelExit()
    {
        this.confirmationPanel.Hide();

        this.confirmationPanel.OnYesPressed -= ConfirmExit;
        this.confirmationPanel.OnNoPressed -= CancelExit;
    }

    private void ConfirmExit()
    {
        this.confirmationPanel.Hide();
        this.StartCoroutine(this.ExitCorroutine());

        this.confirmationPanel.OnYesPressed -= ConfirmExit;
        this.confirmationPanel.OnNoPressed -= CancelExit;
    }

	public void ReturnMainMenuFromShop(){
		StartCoroutine (ShopToMainTransition ());
	}

	IEnumerator ShopToMainTransition(){
		shopPanelController.Hide ();
		yield return new WaitForSeconds (0.5f);
		shopPanelController.gameObject.SetActive (false);
		mainMenuButtons.SetActive (true);
		mainMenuButtons.GetComponent<MainPanelMenuItem> ().Show ();
	}

	public void MainMenuToShop(){
		StartCoroutine (MainToShopTransition ());
	}

	IEnumerator MainToShopTransition(){
		mainMenuButtons.GetComponent<MainPanelMenuItem> ().Hide ();
		yield return new WaitForSeconds (0.5f);
		shopPanelController.gameObject.SetActive (true);
		mainMenuButtons.SetActive (false);
		shopPanelController.Show ();
	}

	public void DesactivarTodo(){
		selectionLevelPanel.Hide();
		selectionLevelMultiplayerPanel.Hide();
		shopPanelController.Hide();
		skinsBuyPanel.Hide();
		skinSelectionPanel.Hide();
		mainMenuButtons.SetActive (false);
		popUpNoInternet.SetActive (false);
		popUpNoMoney.gameObject.SetActive (false);
	}
}