using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Extensions.System.Numeric;

public class GameSceneManager : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    //GAMEPLAY
    public float time = 40;
    public int puntuacion = 0;
    public float attempts = 0;
    public float made = 0;
    public int tickets = 0;
    public int temporalTickets = 0;
    public Text puntos;
    public Text targetText;
    public Text target;
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
    public GameObject panelEmpezar;
    public GameObject botonExit;
    public VideoPopUpPanel botonVideo;
    public GameObject menuPause;
    public GameObject loosePanel;
    public PopUpLoose popUpLoosePanel;
    public GameObject botonContinuar;
    public GameObject imagenCredits;
    public Text looseText;
    public Text ticketsObtenidos;
    public Text textoBoton;
    public Text stageInfo;
    public Text zoneName;
    public bool jugando = false;
    public bool continuar = false;
    bool menuFueraPausa = false;
    public Animator barra;
    public canasta canasta;
	public canasta canasta2;
    public int fase = 0;
    int puntosObj = 15;
    int puntosIncrementoObj = 10;
    private int numMaquina;

    public Material matMaquina;

    CreditsManagement creditsManagement;
    public Text ticketsText;
    public Text credits;
    public Text tiempoHastaSiguiente;
    public Text creditsTimeText;
    public GameObject buyButtonCredits;
    public GameObject buyButtonCreditsPurchased;
    public GameObject continueButtonCredits;
	public GameObject popUpConfirmation;

    public AudioClip timeEndingSound;
    public AudioClip timeEndSound;
    private float timeEndingSoundOffset;

    //TUTO
    public Tuto tuto;

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

        numMaquina = GameManager.Instance.GamePersistentData.NumMaquina;
		//Para debug
		//numMaquina=4;
		//
        creditsManagement = this.gameObject.GetComponent<CreditsManagement>();
        matMaquina.mainTexture = GameManager.Instance.GetStageInfo(numMaquina).TextureMaquina;
		if (numMaquina == 4) {
			ExcepcionTiburon ();
		}
        AudioManager.Instance.PlayMusic(GameManager.Instance.GetStageInfo(numMaquina).AmbientClip);
        AudioManager.Instance.MusicVolume = GameManager.Instance.GetStageInfo(numMaquina).AmbientClipVolume;

        this.timeEndingSoundOffset = 0.0f;


    }

	void ExcepcionTiburon(){
		if (!GameManager.Instance.Multiplayer) {
			canasta2.gameObject.SetActive (true);
			canasta.transform.localPosition = new Vector3 (0.35f, 3.207f, 0.334f);
			canasta2.transform.localPosition = new Vector3 (-0.35f, 2.661f, 0.334f);
		}
	}

    private void Start()
    {
        if (GameManager.Instance.GamePersistentData.FreeCreditsPurchased)
        {
            imagenCredits.SetActive(false);
        }
        else
        {
            imagenCredits.SetActive(true);
        }
        textoBoton.text = "\n\n\n" + Localization.GetString("StartPanel.Get") + puntosObj.ToString() + Localization.GetString("StartPanel.Points");
        zoneName.text = Localization.GetString(GameManager.Instance.GetStageInfo(numMaquina).Key);
        stageInfo.text = Localization.GetString("InGameMenu.Stage") + (fase + 1).ToString();
        botonExit.SetActive(true);
        targetText.text = " STAGE";
        target.text = (fase + 1).ToString();
        AsignarBalonesYSkins();
        for (int i = 0; i < 6; i++)
        {
            if (GameManager.Instance.GetBallInfo(i).BolaAdquired == false)
                GameManager.Instance.GetBallInfo(i).Bola.SetActive(false);
        }

        Instantiate(GameManager.Instance.GetStageInfo(numMaquina).BarrierPrefab);
       // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, GameManager.Instance.GetStageInfo(numMaquina).Key, fase.ToString(), puntuacion);

    }

    private void Update()
    {
        if (jugando)
        {
            if (!this.timeFrozen)
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

        var currentMachineKey = GameManager.Instance.GetStageInfo(numMaquina).Key;

        if (fase % 2 == 1)
        {
            //GoogleAdsManager.Instance.ShowInterstitial(1.0f);
        }

        if (puntuacion >= puntosObj)
        {
            panelEmpezar.SetActive(true);
            imagenCredits.SetActive(false);
            botonExit.SetActive(false);
            fase += 1;
            textoBoton.text = "\n\n\n" + Localization.GetString("InGameMenu.Stage") + fase.ToString() + Localization.GetString("StartPanel.Completed") + "\n\n" + Localization.GetString("StartPanel.NextStage") + "\n" + Localization.GetString("StartPanel.Get") + (puntosObj + puntosIncrementoObj + 5).ToString() + Localization.GetString("StartPanel.Points");
            stageInfo.text = Localization.GetString("InGameMenu.Stage") + (fase + 1).ToString();
            targetText.text = " STAGE";
            target.text = (fase + 1).ToString();

            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, currentMachineKey, (fase - 1).ToString(), puntuacion);
            //FacebookManager.Instance.LogCustomEvent(string.Format("{0}.{1}", currentMachineKey, fase), "Win");
        }
        else
        {
            if (fase % 2 == 0)
            {
                //GoogleAdsManager.Instance.RequestAndShowInterstitial();
            }
            loosePanel.SetActive(true);
            targetText.text = " STAGE";
            target.text = (fase + 1).ToString();
            /*if (creditsManagement.timePurchased) {
                continueButtonCredits.SetActive (false);
            } else {
                continueButtonCredits.SetActive (true);
            }*/

            zoneNameLoose.text = Localization.GetString(GameManager.Instance.GetStageInfo(numMaquina).Key);
            looseText.text = "\n" + Localization.GetString("LoosePanel.LooseInfo");
			Text tempRecord = loosePanel.transform.GetChild (0).GetChild (8).GetChild (1).GetComponent<Text> ();
			switch (numMaquina)
			{
			case 0:
				tempRecord.text = GameManager.Instance.GamePersistentData.MatchRecordBullZone.ToString();
				break;
			case 1:
				tempRecord.text = GameManager.Instance.GamePersistentData.MatchRecordDroneZone.ToString();
				break;
			case 2:
				tempRecord.text = GameManager.Instance.GamePersistentData.MatchRecordPlanetZone.ToString();
				break;
			case 3:
				tempRecord.text = GameManager.Instance.GamePersistentData.MatchRecordPulpo.ToString();
				break;
			case 4:
				tempRecord.text = GameManager.Instance.GamePersistentData.MatchRecordTiburon.ToString();
				break;
			}
			loosePanel.transform.GetChild (0).GetChild (8).GetChild (2).GetComponent<Text> ().text = attempts.ToString();
			loosePanel.transform.GetChild (0).GetChild (8).GetChild (3).GetComponent<Text> ().text = made.ToString();
			float porcentage = (made / attempts) * 100.0f;
			loosePanel.transform.GetChild (0).GetChild (8).GetChild (4).GetComponent<Text> ().text = porcentage.ToString("00.00");
			loosePanel.transform.GetChild (0).GetChild (8).GetChild (5).GetComponent<Text> ().text = puntuacion.ToString();

           // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, GameManager.Instance.GetStageInfo(numMaquina).Key, fase.ToString(), puntuacion);
            //FacebookManager.Instance.LogCustomEvent(string.Format("{0}.{1}", currentMachineKey, fase), "Lose");

            switch (numMaquina)
            {
                case 0:
                    if (puntuacion > GameManager.Instance.GamePersistentData.MatchRecordBullZone)
                    {
                        GameManager.Instance.GamePersistentData.MatchRecordBullZone = puntuacion;
                    }
                    break;
                case 1:
                    if (puntuacion > GameManager.Instance.GamePersistentData.MatchRecordDroneZone)
                    {
                        GameManager.Instance.GamePersistentData.MatchRecordDroneZone = puntuacion;
                    }
                    break;
                case 2:
                    if (puntuacion > GameManager.Instance.GamePersistentData.MatchRecordPlanetZone)
                    {
                        GameManager.Instance.GamePersistentData.MatchRecordPlanetZone = puntuacion;
                    }
                    break;
			case 3:
				if (puntuacion > GameManager.Instance.GamePersistentData.MatchRecordPulpo)
				{
					GameManager.Instance.GamePersistentData.MatchRecordPulpo = puntuacion;
				}
				break;
			case 4:
				if (puntuacion > GameManager.Instance.GamePersistentData.MatchRecordTiburon)
				{
					GameManager.Instance.GamePersistentData.MatchRecordTiburon = puntuacion;
				}
				break;
            }

//            if (FacebookManager.Instance.LoggedIn)
//            {
//                uint totalScore = (uint)(GameManager.Instance.GamePersistentData.MatchRecordBullZone + GameManager.Instance.GamePersistentData.MatchRecordDroneZone + GameManager.Instance.GamePersistentData.MatchRecordPlanetZone);
//                FacebookManager.Instance.RequestSetScore(new FacebookScore(totalScore));
//            }

            if (continuar == true)
            {
                ticketsObtenidos.text = "+" + (temporalTickets + tickets).ToString();
                GameManager.Instance.GamePersistentData.Tickets += tickets;
                GameManager.Instance.SaveData();
                if (GameManager.Instance.GamePersistentData.Tutorial == false && GameManager.Instance.GamePersistentData.Tickets >= 100)
                {
                    TutoComprarBalon();
                }
                tickets = 0;
                temporalTickets = 0;

            }
            else
            {
                temporalTickets = tickets;
                popUpLoosePanel.Show();
                ticketsObtenidos.text = "+" + tickets.ToString();
                GameManager.Instance.GamePersistentData.Tickets += tickets;
                GameManager.Instance.SaveData();
                tickets = 0;
            }
        }
    }

    private void TutoComprarBalon()
    {
        //tuto.Show();
    }

    // Métodos de control
    public void FreezeTime(float freezeTime)
    {
        this.StartCoroutine(this.FreezeTimeCorroutine(freezeTime));
    }

    // Otros
    public void OnClickStart()
    {
        if (creditsManagement.timePurchased)
        {
            Empezar();
        }
        else
        {
            if (fase == 0)
            {
                if (creditsManagement.credits > 0)
                {
                    if (creditsManagement.credits >= 10)
                    {
                        creditsManagement.nextCredit = DateTime.Now.AddMinutes(15);
                    }
                    creditsManagement.credits -= 1;
                    PlayerPrefs.SetInt("credits", creditsManagement.credits);
                    Empezar();
                }
                else
                {
                    botonVideo.Show();
                }
            }
            else
            {
                Empezar();
            }
        }
    }

    public void Empezar()
    {
       
        this.timeEndingSoundOffset = 0.0f;
        Time.timeScale = 1;
        if (fase % 2 == 1)
        {
            //GoogleAdsManager.Instance.RequestInterstitial();
        }

        panelEmpezar.SetActive(false);
        jugando = true;
        time = 40;
        puntosObj = 15;
        puntosIncrementoObj = 10;
        barra.SetBool("jugando", true);
		if (fase == 0) {
			attempts = 0;
			made = 0;
		}
        for (int i = 0; i < fase; i++)
        {
            puntosIncrementoObj += 5;
            puntosObj += puntosIncrementoObj;
            if (time > 20)
            {
                time -= 5;
            }
        }
        targetText.text = "TARGET";
        target.text = puntosObj.ToString("000");

    }

    public void Reintentar()
    {
		
        Time.timeScale = 1;
        fase = 0;
        canasta.gameObject.transform.position = posCanasta.transform.position;
		if (numMaquina == 4) {
			ExcepcionTiburon ();
		}
        puntosIncrementoObj = 10;
        puntosObj = 15;
        puntuacion = 0;
        temporalTickets = 0;
        puntos.text = puntuacion.ToString("000");
        //botonContinuar.SetActive (true);
        panelEmpezar.SetActive(true);
        botonExit.SetActive(true);
        if (creditsManagement.timePurchased)
        {
            imagenCredits.SetActive(false);
        }
        else
        {
            imagenCredits.SetActive(true);
        }
        textoBoton.text = "\n\n\n" + Localization.GetString("StartPanel.Get") + puntosObj.ToString() + Localization.GetString("StartPanel.Points");
        stageInfo.text = Localization.GetString("InGameMenu.Stage") + (fase + 1).ToString();
        targetText.text = " STAGE";
        target.text = (fase + 1).ToString();
        continuar = false;
        loosePanel.SetActive(false);

        GameObject balon1 = GameObject.Find("BalonNormal1");
        balon1.transform.position = new Vector3(0.419f, 0.87f, -3.5f);
        GameObject balon2 = GameObject.Find("BalonNormal2");
        balon2.transform.position = new Vector3(0, 0.87f, -3.5f);
        GameObject balon3 = GameObject.Find("BalonNormal3");
        balon3.transform.position = new Vector3(-0.387f, 0.87f, -3.5f);
        GameObject balonTriple = GameObject.Find("BalonTriple");
        balonTriple.transform.position = new Vector3(0.214f, 0.911f, -3.208f);
        GameObject balonTiempo = GameObject.Find("BalonTiempo");
        balonTiempo.transform.position = new Vector3(0f, 0.938f, -2.886f);
        GameObject balonTickets = GameObject.Find("BalonTickets");
        balonTickets.transform.position = new Vector3(-0.213f, 0.911f, -3.208f);
		
       // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, GameManager.Instance.GetStageInfo(numMaquina).Key, fase.ToString(), puntuacion);
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
        if (jugando)
        {
            jugando = false;
            menuPause.SetActive(true);
        }
        else
        {
            menuFueraPausa = true;
            menuPause.SetActive(true);
        }
        stageinfoInPause.text = Localization.GetString("InGameMenu.Stage") + (fase + 1).ToString();
        infoTextInPause.text = "\n\n" + Localization.GetString("StartPanel.Get") + puntosObj.ToString() + Localization.GetString("StartPanel.Points");
        recordGlobal.text = "  ";
        switch (numMaquina)
        {
            case 0:
                recordPersonal.text = GameManager.Instance.GamePersistentData.MatchRecordBullZone.ToString();
                break;
            case 1:
                recordPersonal.text = GameManager.Instance.GamePersistentData.MatchRecordDroneZone.ToString();
                break;
            case 2:
                recordPersonal.text = GameManager.Instance.GamePersistentData.MatchRecordPlanetZone.ToString();
                break;
        }
        attemptsStats.text = attempts.ToString();
        madeStats.text = made.ToString();
        float porcentage = (made / attempts) * 100.0f;
        percentageStats.text = porcentage.ToString("00.00");
        pointsStats.text = puntuacion.ToString();
        Time.timeScale = 0;
    }

    public void Exit()
    {
		if (menuPause.activeInHierarchy) {
			popUpConfirmation.SetActive (true);
		} else {
			Time.timeScale = 1.0f;
			PlayerPrefs.SetString ("savedTime", creditsManagement.nextCredit.ToBinary ().ToString ());
			PlayerPrefs.SetInt ("credits", creditsManagement.credits);
			temporalTickets = 0;
			GameManager.Instance.SaveData ();
			GameManager.Instance.AfterMatch = true;
			SceneManager.LoadScene ("MainMenuAnimated");
		}
    }

	public void ExitSi(){
		Time.timeScale = 1.0f;
		PlayerPrefs.SetString ("savedTime", creditsManagement.nextCredit.ToBinary ().ToString ());
		PlayerPrefs.SetInt ("credits", creditsManagement.credits);
		temporalTickets = 0;
		GameManager.Instance.SaveData ();
		GameManager.Instance.AfterMatch = true;
		SceneManager.LoadScene ("MainMenuAnimated");
	}

	public void ExitNo(){
		popUpConfirmation.SetActive (false);
	}

    public void Continue()
    {
        Time.timeScale = 1.0f;
        if (menuFueraPausa)
        {
            menuFueraPausa = false;
            menuPause.SetActive(false);
        }
        else
        {
            jugando = true;
            menuPause.SetActive(false);
        }
    }

    // Corrutinas
    private IEnumerator FreezeTimeCorroutine(float freezeTime)
    {
        this.timeFrozen = true;
        yield return new WaitForSeconds(freezeTime);
        this.timeFrozen = false;
    }

}