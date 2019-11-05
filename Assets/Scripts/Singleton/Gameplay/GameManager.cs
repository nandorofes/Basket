using Extensions.System.Numeric;
using Extensions.System.SequentialArray;
using Extensions.System.String;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Prefab("Game manager", true)]
public sealed class GameManager : Singleton<GameManager>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Pelotas del juego

    //public AdMobLoad adMob;
    //public ConfirmationPanelController confirmationPanel;
	//Test Debug Skins
	public List<string> debugSkinList;
    // Definición de elementos
    
    // Lenguaje
    private string gameLanguage;

    // Datos guardados del juego
    private GamePersistentData gamePersistentData;

    // Logros
    //private ArchivementData archivementData;

    // Gestión de niveles
    private string currentStageKey;
    private int currentPhaseIndex;
    private bool vieneDeTienda;

    // Variables volátiles
    private int currentPoints = 0;
	private bool skinSelection;
	private bool afterMatch;

    private DateTime lastVideoView = new DateTime(1980, 1, 1);
    private string lastVideoStage = string.Empty;
    private int lastVideoPhase = 0;

    [SerializeField]
    private GameObject googleAnalyticsPrefab;

    [SerializeField]
    private StageDefinition[] stageDefinitionList;

    [SerializeField]
    private BallDefinition[] ballDefinitionList;

    [SerializeField]
    private SkinDefinition[] skinDefinitionList;

	//Multiplayer stats
	private int ticketsApostados;
	private bool multiplayer;
	private bool videoTickets;
	private bool videoCoins;
	private bool videoReintentar;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Lenguaje
	public int TicketsBet{
		get{ return ticketsApostados; }
		set{ ticketsApostados = value; }
	}

	public bool Multiplayer{
		get{ return multiplayer; }
		set{ multiplayer = value; }
	}

    public string GameLanguage
    {
        get { return GameManager.Instance.gameLanguage; }
        set
        {
            GameManager.Instance.gameLanguage = value;
            if (GameManager.Instance.OnGameLanguageChanged != null)
                GameManager.Instance.OnGameLanguageChanged(value);
        }
    }

    // Datos guardados del juego
    public GamePersistentData GamePersistentData
    {
        get
        {
            if (GameManager.Instance.gamePersistentData == null)
                GameManager.Instance.gamePersistentData = new GamePersistentData();
            return GameManager.Instance.gamePersistentData;
        }
    }

    // Variables volátiles
    public bool VieneDeTienda
    {
        get{ return GameManager.Instance.vieneDeTienda; }
        internal set { GameManager.Instance.vieneDeTienda = value; }
    }

    public int CurrentPoints
    {
        get { return GameManager.Instance.currentPoints; }
        internal set
        {
            int setValue = value.ClampToPositive();
            if (GameManager.Instance.currentPoints != setValue)
            {
                GameManager.Instance.currentPoints = setValue;
                GameManager.Instance.OnCurrentPointsChanged(setValue);
            }
        }
    }

	public bool SkinSelection
	{
		get{return skinSelection;}
		set{ skinSelection = value; }
	}

	public bool AfterMatch
	{
		get{return afterMatch;}
		set{ afterMatch = value; }
	}

    public DateTime LastVideoView
    {
        get { return GameManager.Instance.lastVideoView; }
        internal set { GameManager.Instance.lastVideoView = value; }
    }

    public string LastVideoStage
    {
        get { return GameManager.Instance.lastVideoStage; }
        internal set { GameManager.Instance.lastVideoStage = value.Truncate(96); }
    }

    public int LastVideoPhase
    {
        get { return GameManager.Instance.lastVideoPhase; }
        internal set { GameManager.Instance.lastVideoPhase = value; }
    }

    public IList<SkinDefinition> SkinDefinitionList
    {
        get { return new List<SkinDefinition>(GameManager.Instance.skinDefinitionList).AsReadOnly(); }
    }
		

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public event Action<int> OnCurrentPointsChanged = delegate { };
    public event Action<string> OnGameLanguageChanged = delegate { };
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos del juego
    public StageDefinition GetStageInfo(int numeroMaquina)
    {
        foreach (var item in stageDefinitionList)
        {
            if (item.NumeroMaquina == numeroMaquina)
                return item;
        }
        return null;
    }

    public StageDefinition GetStageInfo(string stageKey)
    {
        foreach (var item in stageDefinitionList)
        {
            if (item.Key == stageKey)
                return item;
        }
        return null;
    }

    public BallDefinition GetBallInfo(int numeroBola)
    {
        foreach (var item in ballDefinitionList)
        {
            if (item.NumeroBola == numeroBola)
                return item;
        }
        return null;
    }

    public SkinDefinition GetSkinInfo(string skinKey)
    {
        foreach (var item in GameManager.Instance.skinDefinitionList)
        {
            if (item.Key == skinKey)
                return item;
        }
        return null;
    }

	public void SetSkinInicio()
	{

			List<string> normalList = GameManager.Instance.GamePersistentData.SelectedNormalSkinList;
			foreach (var item in ballDefinitionList)
			{
				if (item.BolaKey == "NormalBall1" && normalList.Count > 0)
				{
					SkinDefinition referencedNormalSkin = GameManager.Instance.GetSkinInfo(normalList[0]);
					if (referencedNormalSkin != null)
						item.TextureBola = referencedNormalSkin.Texture;
				}

				if (item.BolaKey == "NormalBall2" && normalList.Count > 1)
				{
					SkinDefinition referencedNormalSkin = GameManager.Instance.GetSkinInfo(normalList[1]);
					if (referencedNormalSkin != null)
						item.TextureBola = referencedNormalSkin.Texture;
				}

				if (item.BolaKey == "NormalBall3" && normalList.Count > 2)
				{
					SkinDefinition referencedNormalSkin = GameManager.Instance.GetSkinInfo(normalList[2]);
					if (referencedNormalSkin != null)
						item.TextureBola = referencedNormalSkin.Texture;
				}

			if (item.BolaKey == "Time") {
				SkinDefinition referencedTimeSkin = GameManager.Instance.GetSkinInfo (GameManager.Instance.GamePersistentData.SelectedTimeSkin);
				if (referencedTimeSkin != null)
					item.TextureBola = referencedTimeSkin.Texture;
			}
			if (item.BolaKey == "Triple") {
				SkinDefinition referencedTripleSkin = GameManager.Instance.GetSkinInfo (GameManager.Instance.GamePersistentData.SelectedTripleSkin);
				if (referencedTripleSkin != null)
					item.TextureBola = referencedTripleSkin.Texture;
			}
			if (item.BolaKey == "Tickets") {
				SkinDefinition referencedTicketsSkin = GameManager.Instance.GetSkinInfo (GameManager.Instance.GamePersistentData.SelectedBonusSkin);
				if (referencedTicketsSkin != null)
					item.TextureBola = referencedTicketsSkin.Texture;
			}
			}

	}

    public void SetSkin(string skinKey)
    {
        SkinDefinition referencedSkin = GameManager.Instance.GetSkinInfo(skinKey);
        if (referencedSkin != null)
        {
            string keyFragment = "Undefined";
            switch (referencedSkin.Type)
            {
			case SkinTypes.Normal:
				GameManager.Instance.GamePersistentData.SelectedNormalSkinList.Add (skinKey);
				if (GameManager.Instance.GamePersistentData.SelectedNormalSkinList.Count > 3) {
					GameManager.Instance.GamePersistentData.SelectedNormalSkinList.RemoveAt (0);
				}
                    break;

                case SkinTypes.Time:
                    keyFragment = "Time";
                    GameManager.Instance.GamePersistentData.SelectedTimeSkin = skinKey;
                    break;

                case SkinTypes.Triple:
                    keyFragment = "Triple";
                    GameManager.Instance.GamePersistentData.SelectedTripleSkin = skinKey;
                    break;

                case SkinTypes.Bonus:
                    keyFragment = "Tickets";
                    GameManager.Instance.GamePersistentData.SelectedBonusSkin = skinKey;
                    break;
            }

            List<string> normalList = GameManager.Instance.GamePersistentData.SelectedNormalSkinList;
            foreach (var item in ballDefinitionList)
            {
                if (item.BolaKey == "NormalBall1" && normalList.Count > 0)
                {
                    SkinDefinition referencedNormalSkin = GameManager.Instance.GetSkinInfo(normalList[0]);
                    if (referencedNormalSkin != null)
                        item.TextureBola = referencedNormalSkin.Texture;
                }

                if (item.BolaKey == "NormalBall2" && normalList.Count > 1)
                {
                    SkinDefinition referencedNormalSkin = GameManager.Instance.GetSkinInfo(normalList[1]);
                    if (referencedNormalSkin != null)
                        item.TextureBola = referencedNormalSkin.Texture;
                }

                if (item.BolaKey == "NormalBall3" && normalList.Count > 2)
                {
                    SkinDefinition referencedNormalSkin = GameManager.Instance.GetSkinInfo(normalList[2]);
                    if (referencedNormalSkin != null)
                        item.TextureBola = referencedNormalSkin.Texture;
                }
                
                if (item.BolaKey.Contains(keyFragment))
                    item.TextureBola = referencedSkin.Texture;
            }

        }
    }

    // Métodos de Monobehaviour
    private void Awake()
    {
        GameManager.Instance.LoadData();

        GameManager.Instance.gameLanguage = "en";
        Localization.Initialize();

       
    }

	void Start(){
		IniciarPubli ();
		//IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; 
	}

	public void RewardedVideoAdEndedEvent(){//IronSourcePlacement placement){
		
//		GameObject.Find ("DebugText").GetComponent<Text> ().text="He llamado al rewarded";	
//		//aqui dar premio por ver rewarded video.
//		if (videoCoins) {
//			GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
//			GameObject.Find ("DebugText").GetComponent<Text> ().text="Encuentro el Scene Manager";	
//			sceneManager.botonVideo.DarPremio ();
//			GameObject.Find ("DebugText").GetComponent<Text> ().text="Doy la recompensa por ver el video";	
//		}
//		if (videoTickets) {
//			GamePersistentData.Tickets += 25;
//		}
//		if (videoReintentar) {
//			GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
//			GameObject.Find ("DebugText").GetComponent<Text> ().text="Encuentro el Scene Manager";	
//			sceneManager.popUpLoosePanel.DarPremio ();
//			GameObject.Find ("DebugText").GetComponent<Text> ().text="Doy la recompensa por ver el video";	
//		}
	}

	void IniciarPubli(){

		string appKey = "7727b2b5";
		string uniqueUserID =IronSource.Agent.getAdvertiserId();

		//IronSource tracking sdk
		IronSource.Agent.reportAppStarted ();

		//Dynamic config example
		IronSourceConfig.Instance.setClientSideCallbacks (true);

		IronSource.Agent.validateIntegration ();

		//Debug.Log ("unity-script: unity version" + IronSource.unityVersion ());

		// SDK init
		Debug.Log ("unity-script: IronSource.Agent.init");
		IronSource.Agent.setUserId (uniqueUserID);
		IronSource.Agent.init (appKey);
		StartCoroutine (TodoCorrect ());
	}

	IEnumerator TodoCorrect(){
		yield return new WaitForSeconds (10);
		IronSource.Agent.validateIntegration();
		IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
		IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent; 
		IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
		IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
		IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; 
		IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
	}

	public void RewardedVideoAdOpenedEvent(){
		//GameObject.Find ("DebugText").GetComponent<Text> ().text="El video se ha abierto";
	}
	public void RewardedVideoAdClosedEvent(){
		//GameObject.Find ("DebugText").GetComponent<Text> ().text="El video se ha cerrado";
		//aqui dar premio por ver rewarded video.
		if (videoCoins) {
			GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
			//GameObject.Find ("DebugText").GetComponent<Text> ().text="Encuentro el Scene Manager";	
			sceneManager.botonVideo.DarPremio ();
			//GameObject.Find ("DebugText").GetComponent<Text> ().text="Doy la recompensa por ver el video";	
		}
		if (videoTickets) {
			GamePersistentData.Tickets += 25;
		}
		if (videoReintentar) {
			GameSceneManager sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<GameSceneManager> ();
			//GameObject.Find ("DebugText").GetComponent<Text> ().text="Encuentro el Scene Manager";	
			sceneManager.popUpLoosePanel.DarPremio ();
			//GameObject.Find ("DebugText").GetComponent<Text> ().text="Doy la recompensa por ver el video";	
		}
	}
	public void RewardedVideoAdStartedEvent(){
		//GameObject.Find ("DebugText").GetComponent<Text> ().text="El video ha empezado a reproducirse";
	}
	public void  RewardedVideoAdRewardedEvent(IronSourcePlacement placement){
		//GameObject.Find ("DebugText").GetComponent<Text> ().text="Se ha cerrado el video y toca dar las recompensas";
	}
	public void RewardedVideoAdShowFailedEvent(IronSourceError error){
		//GameObject.Find ("DebugText").GetComponent<Text> ().text="Error a la hora de mostrar el video";
	}

    protected override void OnDestroy()
    {
        GameManager.Instance.SaveData();
        base.OnDestroy();
    }

	void OnApplicationPause(bool isPaused) {                 
		IronSource.Agent.onApplicationPause(isPaused);
	}

	public void ShowRewardedVideo(bool coins, bool tickets, bool retry){
		videoCoins = coins;
		videoTickets = tickets;
		videoReintentar = retry;
		StartCoroutine(LoadRewardedVideo());
	}

	IEnumerator LoadRewardedVideo(){//Esto tendria que cambiarlo
		//GameObject.Find ("DebugText").GetComponent<Text> ().text="Llamo al metodo para que muestre el video";
		bool available = IronSource.Agent.isRewardedVideoAvailable();
		while (!available) {
			yield return null;
			available = IronSource.Agent.isRewardedVideoAvailable();
		}
		//GameObject.Find ("DebugText").GetComponent<Text> ().text="El video esta disponible y se llama a la funcion que lo ejecuta";
		IronSource.Agent.showRewardedVideo();
	}

    private void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameManager.Instance.confirmationPanel.Show();
            GameManager.Instance.confirmationPanel.SetCaption(Localization.GetString("Menu.Panel.WantToExit"));

            GameManager.Instance.confirmationPanel.OnYesPressed += this.ConfirmExit;
            GameManager.Instance.confirmationPanel.OnNoPressed += this.CancelExit;
        }
        */
        #if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.D))
            GameManager.Instance.DeleteData();
        if (Input.GetKeyUp(KeyCode.Alpha1))
            GameManager.Instance.GameLanguage = "es";
        if (Input.GetKeyUp(KeyCode.Alpha2))
            GameManager.Instance.GameLanguage = "en";
        if (Input.GetKeyUp(KeyCode.Alpha3))
            GameManager.Instance.GameLanguage = "fr";
        if (Input.GetKeyUp(KeyCode.Alpha4))
            GameManager.Instance.GameLanguage = "de";
        if (Input.GetKeyUp(KeyCode.Alpha5))
            GameManager.Instance.GameLanguage = "ca";
        if (Input.GetKeyUp(KeyCode.Alpha6))
            GameManager.Instance.GameLanguage = "pt";
        if (Input.GetKeyUp(KeyCode.Alpha7))
            GameManager.Instance.GameLanguage = "it";
		//if (Input.GetKeyUp(KeyCode.A))
			//GameObject.Find ("DebugText").GetComponent<Text> ().text="Encuentro el text";
		debugSkinList = GameManager.Instance.GamePersistentData.SelectedNormalSkinList;
        #endif
    }

    // Métodos de gestión de la salida
    public void ExitApplication()
    {
        #if UNITY_EDITOR
        Debug.Log("Salida de la aplicación recibida.");
        #endif
        SaveData();
        Application.Quit();
    }

    public void ExitApplication(float delayTime)
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.DelayExitApplication(delayTime));
    }

    /* public void RequestExitApplication()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.ExitWithAd());
    }
    */
    // Métodos de guardado y carga de datos
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        this.LoadData();
    }

    public void LoadData()
    {
        // Cargar datos persistentes del juego
        if (PlayerPrefs.HasKey("GamePersistentData"))
        {
            string persistentData = PlayerPrefs.GetString("GamePersistentData");
            GameManager.Instance.gamePersistentData = JsonUtility.FromJson<GamePersistentData>(persistentData);
        }
        else
        {
            GameManager.Instance.gamePersistentData = new GamePersistentData();
        }
			
        // Cargar idioma actual del juego
        if (PlayerPrefs.HasKey("Language"))
            GameManager.Instance.gameLanguage = PlayerPrefs.GetString("Language");
        else
        {
            GameManager.Instance.gameLanguage = "en";
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Catalan:
                    GameManager.Instance.gameLanguage = "ca";
                    break;
                case SystemLanguage.French:
                    GameManager.Instance.gameLanguage = "fr";
                    break;
                case SystemLanguage.German:
                    GameManager.Instance.gameLanguage = "de";
                    break;
                case SystemLanguage.Italian:
                    GameManager.Instance.gameLanguage = "it";
                    break;
                case SystemLanguage.Portuguese:
                    GameManager.Instance.gameLanguage = "pt";
                    break;
                case SystemLanguage.Spanish:
                    GameManager.Instance.gameLanguage = "es";
                    break;
                default:
                    GameManager.Instance.gameLanguage = "en";
                    break;
            }
        }

    }

    public void SaveData()
    {
       
        // Guardar datos persistentes del juego
        string persistentDataString = JsonUtility.ToJson(GameManager.Instance.gamePersistentData);
        PlayerPrefs.SetString("GamePersistentData", persistentDataString);

        // Guardar idioma actual del juego
        PlayerPrefs.SetString("Language", GameManager.Instance.gameLanguage);
    }

    // Corrutinas
    private IEnumerator DelayExitApplication(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameManager.Instance.ExitApplication();
    }
		
    /*
    // Manejadores de eventos
    private void ConfirmExit()
    {
        GameManager.Instance.confirmationPanel.Hide();

        GameManager.Instance.confirmationPanel.OnYesPressed -= this.ConfirmExit;
        GameManager.Instance.confirmationPanel.OnNoPressed -= this.CancelExit;

        GameManager.Instance.RequestExitApplication();
    }

    private void CancelExit()
    {
        GameManager.Instance.confirmationPanel.Hide();

        GameManager.Instance.confirmationPanel.OnYesPressed -= this.ConfirmExit;
        GameManager.Instance.confirmationPanel.OnNoPressed -= this.CancelExit;
    }
    */
}