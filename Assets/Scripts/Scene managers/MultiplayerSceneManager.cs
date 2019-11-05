using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Extensions.System.Numeric;

public class MultiplayerSceneManager : MonoBehaviour {

		// ---- ---- ---- ---- ---- ---- ---- ----
		// Atributos
		// ---- ---- ---- ---- ---- ---- ---- ----
		//GAMEPLAY
	public GameObject loockingRivalText;
	public GameObject panelVS;
	public GameObject botonCancelar;
	public int fase=3;
		public float time = 40;
		public int puntuacion = 0;
		public float attempts = 0;
		public float made = 0;
		public int tickets = 0;
		public int temporalTickets = 0;
		public Text puntos;
		public Text targetText;
		public Text rivalPoints;
		public Text tiempo;
		public Text attemptsStats;
		public Text madeStats;
		public Text percentageStats;
		public Text pointsStats;
		public Text recordGlobal;
		public Text recordPersonal;
		public Text stageinfoInPause;
		public Text infoTextInPause;
		public Text zoneNameLoose;
		public GameObject posCanasta;
		public GameObject menuPause;
		public GameObject finishPanel;
		public GameObject botonDobleoNada;
		//public Text looseText;
		//public Text ticketsObtenidos;
		//public Text textoBoton;
		//public Text stageInfo;
		//public Text zoneName;
		public bool jugando = false;
		public Animator barra;
	public CanastaOnline canasta;
	public CanastaOnline canasta2;
		private int numMaquina;
	public GameObject panelEmpezar;

		public Material matMaquina;

		CreditsManagement creditsManagement;
	public Text ticketsText;
	public Text credits;
	public Text tiempoHastaSiguiente;
	public Text creditsTimeText;
	public GameObject buyButtonCredits;
	public GameObject buyButtonCreditsPurchased;
	public GameObject continueButtonCredits;

		public AudioClip timeEndingSound;
		public AudioClip timeEndSound;
		private float timeEndingSoundOffset;

		[SerializeField] private BasketAIBehaviour basketAIBehaviour;
		[SerializeField] private TMPro.TextMeshProUGUI basketAIDebugText;

	[SerializeField] 
	private SimpleMatchMaker matchMaker;
	IEnumerator revanchaIE;

	public Text cuentaAtras;
	private bool aiMatch=false;
	public bool rematch=false;

		private bool timeFrozen = false;

		// ---- ---- ---- ---- ---- ---- ---- ----
		// Propiedades
		// ---- ---- ---- ---- ---- ---- ---- ----
		public bool TimeFrozen
		{
			get { return this.timeFrozen; }
		}

		// ---- ---- ---- ---- ---- ---- ---- ----
		// Métodos
		// ---- ---- ---- ---- ---- ---- ---- ----
		// Métodos de MonoBehaviour
		private void Awake()
		{
			var gameManager = GameManager.Instance;
			var facebookManager = FacebookManager.Instance;
		//DEBUG------------------------------------------------------------------
		//GameManager.Instance.TicketsBet=1000;
		//GameManager.Instance.GamePersistentData.NumMaquina = 0;
		//-----------------------------------------------------------------------
		numMaquina =  GameManager.Instance.GamePersistentData.NumMaquina;
			creditsManagement = this.gameObject.GetComponent<CreditsManagement>();
			matMaquina.mainTexture = GameManager.Instance.GetStageInfo(numMaquina).TextureMaquina;

			AudioManager.Instance.PlayMusic(GameManager.Instance.GetStageInfo(numMaquina).AmbientClip);
			AudioManager.Instance.MusicVolume = GameManager.Instance.GetStageInfo(numMaquina).AmbientClipVolume;

			this.timeEndingSoundOffset = 0.0f;


			// Inicializar AI
			this.basketAIBehaviour.GenerateRandomPersonality();
			this.basketAIBehaviour.OnAIOutput += this.OnAIOutput;
		}

	public BasketAIBehaviour AI{
		get{return this.basketAIBehaviour;}
	}

		private void OnAIOutput (float aiResult)
		{
			if (aiResult.InRange(0.333f, 1.0f))
				this.basketAIBehaviour.MyPoints += 2;
			this.basketAIBehaviour.PlayerPoints = puntuacion;

//			string debugText = string.Format(
//				"Points: {0}\n" +
//				"<size=75%>Skill level: {1:0.000}\n" +
//				"Concentration: {2:0.000}\n" +
//				"Competitivenes: {3:0.000}\n" +
//				"Current emotion: {4}",
//				this.basketAIBehaviour.MyPoints,
//				this.basketAIBehaviour.AISkillLevel,
//				this.basketAIBehaviour.AIConcentration,
//				this.basketAIBehaviour.AICompetitiveness,
//				this.basketAIBehaviour.AIEmotion
//			);

		rivalPoints.text = this.basketAIBehaviour.MyPoints.ToString("000");

			//this.basketAIDebugText.text = debugText;
		}

		private void Start()
		{
			AsignarBalonesYSkins();
		this.GetComponent<GameSceneManager> ().fase = 3;
			for (int i = 0; i < 6; i++)
			{
				if (GameManager.Instance.GetBallInfo(i).BolaAdquired == false)
					GameManager.Instance.GetBallInfo(i).Bola.SetActive(false);
			}
			
		//StartCoroutine (DelayStartGame ());
			Instantiate(GameManager.Instance.GetStageInfo(numMaquina).BarrierPrefab);
			//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, GameManager.Instance.GetStageInfo(numMaquina).Key, "MULTIPLAYER", puntuacion);
		if (numMaquina == 4) {
			ExcepcionTiburon ();
		}
		FacebookManager.Instance.OnRequestRivalDataSuccess+=OnGetRivalData;
		}
		

	IEnumerator DelayStartGame(){
		float time = 3;

		while (time>0) {
			time -= Time.deltaTime;
			yield return null;
		}
		//AQUI EMPEZAR LA CUENTA ATRAS
		//Empezar la IA despues de la cuenta atras
		Empezar ();
	}


	public void Empezar()
	{
		GameManager.Instance.GamePersistentData.Tickets -= GameManager.Instance.TicketsBet / 2;
		canasta.player = null;
		StartCoroutine (PillarPlayer ());

		this.timeEndingSoundOffset = 0.0f;
		Time.timeScale = 1;

		StartCoroutine(ShowAnimComienzo());

		//targetText.text = "TIME";
		//this.basketAIBehaviour.Activate();
	}

	IEnumerator ShowAnimComienzo(){
		
		if (FacebookManager.Instance.LoggedIn) {
			panelVS.transform.GetChild (2).GetComponent<Text> ().text = FacebookManager.Instance.CurrentUser.FirstName;
			panelVS.transform.GetChild (1).GetChild (0).GetChild (0).GetComponent<RawImage> ().texture = FacebookManager.Instance.CurrentUser.ProfilePicture;
		}
		if (!aiMatch) {
			while (canasta.player == null) {
				yield return null;
			}
			while (canasta.player.rival == null) {
				yield return null;
			}
			yield return null;
			//Debug.Log ("RivalID="+canasta.player.rival.transform.localScale.x.ToString ()+canasta.player.rival.transform.localScale.y.ToString ()+canasta.player.rival.transform.localScale.z.ToString ());
			string tempID = canasta.player.rival.transform.position.x.ToString () + canasta.player.rival.transform.position.y.ToString () + canasta.player.rival.transform.position.z.ToString ();
			Debug.Log ("tempID=" + tempID);
			Debug.Log ("RivalIDNewMethod="+canasta.player.rival.MultiFBInfo.fbIDString);
			if (canasta.player.rival.MultiFBInfo.fbIDString != "") {
				FacebookManager.Instance.RequestRivalData (canasta.player.rival.MultiFBInfo.fbIDString);
			}else if(canasta.player.rival.transform.position.y!=0){
				FacebookManager.Instance.RequestRivalData (tempID);
			} else {
				loockingRivalText.SetActive (false);
				panelVS.SetActive (true);
				botonCancelar.SetActive (false);
				yield return new WaitForSeconds (3);
				StartCoroutine (StartBackCount ());
			}
		} else {
			loockingRivalText.SetActive (false);
			panelVS.SetActive (true);
			botonCancelar.SetActive (false);
			yield return new WaitForSeconds (3);
			StartCoroutine (StartBackCount ());
		}
	}

	private void OnGetRivalData()
	{
		panelVS.transform.GetChild (4).GetComponent<Text> ().text = FacebookManager.Instance.CurrentRival.FirstName;
		panelVS.transform.GetChild (3).GetChild (0).GetChild (0).GetComponent<RawImage> ().texture = FacebookManager.Instance.CurrentRival.ProfilePicture;
		StartCoroutine (StartMatchAfterGetRivalData ());
	}

	IEnumerator StartMatchAfterGetRivalData(){
		loockingRivalText.SetActive (false);
		panelVS.SetActive (true);
		botonCancelar.SetActive (false);
		yield return new WaitForSeconds (3);
		StartCoroutine (StartBackCount ());
	}

	void OnDestroy(){
		FacebookManager.Instance.OnRequestRivalDataSuccess -= OnGetRivalData;
	}

	public void EmpezarAI()
	{
		GameManager.Instance.GamePersistentData.Tickets -= GameManager.Instance.TicketsBet / 2;
		canasta.player = null;
		aiMatch = true;
		//StartCoroutine (PillarPlayer ());
		this.timeEndingSoundOffset = 0.0f;
		Time.timeScale = 1;

		StartCoroutine(ShowAnimComienzo());
		//targetText.text = "TIME";

	}

	IEnumerator StartBackCount(){
		panelEmpezar.SetActive(false);
		cuentaAtras.gameObject.SetActive (true);
		cuentaAtras.text = "3";
		yield return new WaitForSeconds (1);
		cuentaAtras.text = "2";
		yield return new WaitForSeconds (1);
		cuentaAtras.text = "1";
		yield return new WaitForSeconds (1);
		rematch = false;
		cuentaAtras.text = "GO";
		jugando = true;
		barra.SetBool("jugando", true);
		yield return new WaitForSeconds (1);
		cuentaAtras.gameObject.SetActive (false);
		if (aiMatch) {
			this.basketAIBehaviour.Activate();
		}
	}

	IEnumerator PillarPlayer(){
		GameObject [] jugadores= GameObject.FindGameObjectsWithTag ("Player");
		bool encontrado=false;
		while (!encontrado) {
			for (int i = 0; i < jugadores.Length; i++) {
				if (jugadores [i].GetComponent<UnityEngine.Networking.NetworkIdentity> ().isLocalPlayer) {
					canasta.player = jugadores [i].GetComponent<PlayerMultiplayer> ();
				}
			}
			yield return null;
			if (canasta.player == null) {
				jugadores = GameObject.FindGameObjectsWithTag ("Player");
			} else {
				encontrado = true;
			}
		}

	}


		private void Update()
		{
			if (jugando)
			{
				//if (!this.timeFrozen)
					time -= Time.deltaTime;

				puntos.text = puntuacion.ToString("000");
				tiempo.text = time.ToString("00");

				if (time.InRange(0.01f, (3.0f - this.timeEndingSoundOffset)))
				{
					AudioManager.Instance.PlaySoundEffect(this.timeEndingSound);
					this.timeEndingSoundOffset += 1.0f;
				}

				if (time <= 0)
				{
					Finish();
					AudioManager.Instance.PlaySoundEffect(this.timeEndSound);
				}
			}

		ticketsText.text = GameManager.Instance.GamePersistentData.Tickets.ToString();
		credits.text = creditsManagement.credits.ToString();
		if (creditsManagement.timePurchased)
		{
			buyButtonCredits.SetActive(false);
			buyButtonCreditsPurchased.SetActive(true);
			creditsTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", Mathf.Floor((float)creditsManagement.tiempoFree.TotalHours), creditsManagement.tiempoFree.Minutes, creditsManagement.tiempoFree.Seconds);

		}
		else
		{
			buyButtonCredits.SetActive(true);
			buyButtonCreditsPurchased.SetActive(false);
			if (creditsManagement.credits >= 10)
			{
				tiempoHastaSiguiente.text = "MAX";
			}
			else
			{
				tiempoHastaSiguiente.text = string.Format("{0:00}:{1:00}", creditsManagement.tiempoNextCredit.Minutes, creditsManagement.tiempoNextCredit.Seconds);
			}
		}

		}

		// Métodos de auxiliares
		private void AsignarBalonesYSkins()
		{
			//Asignar los balones
			GameManager.Instance.GetBallInfo(0).Bola = GameObject.Find("BalonNormal1");
			//saber si estan comprados
			GameManager.Instance.GetBallInfo(0).BolaAdquired = true;
			//y ponerles el skin que haya decidido el jugador
			GameObject.Find("BalonNormal1").GetComponent<Renderer>().material.mainTexture = GameManager.Instance.GetBallInfo(0).TextureBola;
			//Y hacerlo otra vez con todos los balones

			// Balón normal 2
			GameManager.Instance.GetBallInfo(1).Bola = GameObject.Find("BalonNormal2");
			GameManager.Instance.GetBallInfo(1).BolaAdquired = true;
			GameObject.Find("BalonNormal2").GetComponent<Renderer>().material.mainTexture = GameManager.Instance.GetBallInfo(1).TextureBola;

			// Balón normal 3
			GameManager.Instance.GetBallInfo(2).Bola = GameObject.Find("BalonNormal3");
			GameManager.Instance.GetBallInfo(2).BolaAdquired = GameManager.Instance.GamePersistentData.BalonNormal;
			GameObject.Find("BalonNormal3").GetComponent<Renderer>().material.mainTexture = GameManager.Instance.GetBallInfo(2).TextureBola;

			// Balón Triple
			GameManager.Instance.GetBallInfo(3).Bola = GameObject.Find("BalonTriple");
			GameManager.Instance.GetBallInfo(3).BolaAdquired = GameManager.Instance.GamePersistentData.BalonTriple;
			GameObject.Find("BalonTriple").GetComponent<Renderer>().material.mainTexture = GameManager.Instance.GetBallInfo(3).TextureBola;

			// Balón Tickets
			GameManager.Instance.GetBallInfo(4).Bola = GameObject.Find("BalonTickets");
			GameManager.Instance.GetBallInfo(4).BolaAdquired = GameManager.Instance.GamePersistentData.BalonTicketExtra;
			GameObject.Find("BalonTickets").GetComponent<Renderer>().material.mainTexture = GameManager.Instance.GetBallInfo(4).TextureBola;

			// Balón Tiempo
			GameManager.Instance.GetBallInfo(5).Bola = GameObject.Find("BalonTiempo");
			GameManager.Instance.GetBallInfo(5).BolaAdquired = GameManager.Instance.GamePersistentData.BalonSegundoExtra;
			GameObject.Find("BalonTiempo").GetComponent<Renderer>().material.mainTexture = GameManager.Instance.GetBallInfo(5).TextureBola;

			//asignar obstaculos
			//GameManager.Instance.GetStageInfo(1).BarrierPrefab=GameObject.Find ("DroneEscenario2");
			//GameManager.Instance.GetStageInfo(2).BarrierPrefab=GameObject.Find ("OVNIescenario3");
		}

		private void Finish()
		{
			jugando = false;
			barra.SetBool("jugando", false);

			basketAIBehaviour.Deactivate ();
			var currentMachineKey = GameManager.Instance.GetStageInfo(numMaquina).Key;
		finishPanel.SetActive (true);
			//
			//AQUI TOCARA METER QUE PASA CUANDO SE TERMINA EL TIEMPO
			//

		if (puntuacion > Int32.Parse (rivalPoints.text)) {
			finishPanel.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = "\nYOU WIN!";
			finishPanel.transform.GetChild (0).GetChild (0).GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -55);
			GameManager.Instance.GamePersistentData.Tickets += GameManager.Instance.TicketsBet;
			finishPanel.transform.GetChild (0).GetChild (6).GetComponent<Text> ().text = GameManager.Instance.TicketsBet.ToString ();
			finishPanel.transform.GetChild (0).GetChild (4).gameObject.SetActive (true);
			finishPanel.transform.GetChild (0).GetChild (5).gameObject.SetActive (true);
			finishPanel.transform.GetChild (0).GetChild (6).gameObject.SetActive (true);
		} else if (puntuacion < Int32.Parse (rivalPoints.text)) {
			finishPanel.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = "\nYOU LOSE!";
			finishPanel.transform.GetChild (0).GetChild (0).GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -135);
			finishPanel.transform.GetChild (0).GetChild (4).gameObject.SetActive (false);
			finishPanel.transform.GetChild (0).GetChild (5).gameObject.SetActive (false);
			finishPanel.transform.GetChild (0).GetChild (6).gameObject.SetActive (false);
		} else {
			finishPanel.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = "\nYOU DRAW";
			finishPanel.transform.GetChild (0).GetChild (0).GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -55);
			GameManager.Instance.GamePersistentData.Tickets +=GameManager.Instance.TicketsBet/2;
			finishPanel.transform.GetChild (0).GetChild (6).GetComponent<Text> ().text = (GameManager.Instance.TicketsBet/2).ToString ();
			finishPanel.transform.GetChild (0).GetChild (4).gameObject.SetActive (true);
			finishPanel.transform.GetChild (0).GetChild (5).gameObject.SetActive (true);
			finishPanel.transform.GetChild (0).GetChild (6).gameObject.SetActive (true);
		}

				zoneNameLoose.text = Localization.GetString(GameManager.Instance.GetStageInfo(numMaquina).Key);


				//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, GameManager.Instance.GetStageInfo(numMaquina).Key, "MULTIPLAYER", puntuacion);
				//FacebookManager.Instance.LogCustomEvent(string.Format("{0}.{1}", currentMachineKey, "MULTIPLAYER"), "Lose");
		Debug.Log("Hago el Finish()");
		}

		// Otros

		public void Revancha()
		{
		time = 45;
		if (!aiMatch) {
			basketAIBehaviour.MyPoints = 0;
			puntuacion = 0;
			rivalPoints.text = "000";
			puntos.text = "000";
			canasta.player.pointsplayer = 0;

			GameObject balon1 = GameObject.Find ("BalonNormal1");
			balon1.transform.position = new Vector3 (0.419f, 0.87f, -3.5f);
			GameObject balon2 = GameObject.Find ("BalonNormal2");
			balon2.transform.position = new Vector3 (0, 0.87f, -3.5f);
			GameObject balon3 = GameObject.Find ("BalonNormal3");
			if (balon3 != null) {
				balon3.transform.position = new Vector3 (-0.387f, 0.87f, -3.5f);
			}
			GameObject balonTriple = GameObject.Find ("BalonTriple");
			if (balonTriple != null) {
				balonTriple.transform.position = new Vector3 (0.214f, 0.911f, -3.208f);
			}
			GameObject balonTiempo = GameObject.Find ("BalonTiempo");
			if (balonTiempo != null) {
				balonTiempo.transform.position = new Vector3 (0f, 0.938f, -2.886f);
			}
			GameObject balonTickets = GameObject.Find ("BalonTickets");
			if (balonTickets != null) {
				balonTickets.transform.position = new Vector3 (-0.213f, 0.911f, -3.208f);
			}

			Empezar ();
			//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, GameManager.Instance.GetStageInfo(numMaquina).Key, "MULTIPLAYER", puntuacion);
		} else {
			basketAIBehaviour.MyPoints = 0;
			puntuacion = 0;
			rivalPoints.text = "000";
			puntos.text = "000";

			GameObject balon1 = GameObject.Find ("BalonNormal1");
			balon1.transform.position = new Vector3 (0.419f, 0.87f, -3.5f);
			GameObject balon2 = GameObject.Find ("BalonNormal2");
			balon2.transform.position = new Vector3 (0, 0.87f, -3.5f);
			GameObject balon3 = GameObject.Find ("BalonNormal3");
			if (balon3 != null) {
				balon3.transform.position = new Vector3 (-0.387f, 0.87f, -3.5f);
			}
			GameObject balonTriple = GameObject.Find ("BalonTriple");
			if (balonTriple != null) {
				balonTriple.transform.position = new Vector3 (0.214f, 0.911f, -3.208f);
			}
			GameObject balonTiempo = GameObject.Find ("BalonTiempo");
			if (balonTiempo != null) {
				balonTiempo.transform.position = new Vector3 (0f, 0.938f, -2.886f);
			}
			GameObject balonTickets = GameObject.Find ("BalonTickets");
			if (balonTickets != null) {
				balonTickets.transform.position = new Vector3 (-0.213f, 0.911f, -3.208f);
			}

			EmpezarAI ();
		}
		}

	public void ButtonRematch(){
		StartCoroutine (ButtonRematchIE ());
		basketAIBehaviour.MyPoints = 0;
		puntuacion = 0;
		rivalPoints.text = "000";
		puntos.text = "000";
		rematch = true;
	}

	IEnumerator ButtonRematchIE(){
		finishPanel.SetActive (false);
		panelEmpezar.SetActive (true);
		panelVS.SetActive (false);
		botonCancelar.SetActive (true);
		loockingRivalText.SetActive (true);
		loockingRivalText.GetComponent<Text> ().text = Localization.GetString ("Multi.Wait");
		if (aiMatch) {
			yield return new WaitForSeconds (3);
			Revancha ();
		} else {
			revanchaIE = WaitForRematch ();
			StartCoroutine (revanchaIE);
		}
		yield return null;
	}

	IEnumerator WaitForRematch(){
		GameObject [] jugadores= GameObject.FindGameObjectsWithTag ("Player");
		bool startAgain = true;
		while (jugadores.Length > 1) {
			startAgain = true;
			for (int i = 0; i < jugadores.Length; i++) {
				if (jugadores [i] != null) {
					if (jugadores [i].transform.position.z != 1) {
						startAgain = false;
						;
					}
				}
			}
			if (startAgain) {
				Revancha ();
				StopCoroutine (revanchaIE);
			} else {
				jugadores= GameObject.FindGameObjectsWithTag ("Player");
			}
			yield return null;
		}
		loockingRivalText.GetComponent<Text> ().text = Localization.GetString ("Multi.Disconected");
		yield return new WaitForSeconds (2);
		Exit ();
	}

		public void IrALaTienda()
		{
			temporalTickets = 0;
			Time.timeScale = 1.0f;
			SceneManager.LoadScene("MainMenuAnimated");
			GameManager.Instance.VieneDeTienda = true;
		}

		public void Pausa()
		{
			//
			//REMODELAR TODO EL MENU PAUSA, SI HAY
		    //
		}

		public void Exit()
		{
			Time.timeScale = 1.0f;


			PlayerPrefs.SetString("savedTime", creditsManagement.nextCredit.ToBinary().ToString());
			PlayerPrefs.SetInt("credits", creditsManagement.credits);
			GameManager.Instance.SaveData();
		GameManager.Instance.AfterMatch = true;
			SceneManager.LoadScene("MainMenuAnimated");
		if (!aiMatch) {
			matchMaker.DelateMatchAtFinish ();
		}
		}

		public void Continue()
		{
			//ESTO SOLO INTERESA SI HAY PAUSA
		}

	public void CancelarCuandoBusca()
	{
		Time.timeScale = 1.0f;
		PlayerPrefs.SetString("savedTime", creditsManagement.nextCredit.ToBinary().ToString());
		PlayerPrefs.SetInt("credits", creditsManagement.credits);
		GameManager.Instance.SaveData();
		GameManager.Instance.AfterMatch = true;
		GameManager.Instance.GamePersistentData.Tickets +=GameManager.Instance.TicketsBet/2;
		if (matchMaker.matchCreated) {
			matchMaker.DelateMatchAtFinish ();
		}
		SceneManager.LoadScene("MainMenuAnimated");
	}
		

		void ExcepcionTiburon(){
			canasta2.gameObject.SetActive (true);
			canasta.transform.localPosition = new Vector3 (0.35f, 3.207f, 0.334f);
			canasta2.transform.localPosition = new Vector3 (-0.35f, 2.661f, 0.334f);
		}

	}