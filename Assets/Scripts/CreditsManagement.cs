using UnityEngine;
using System.Collections;
using System;

public class CreditsManagement : MonoBehaviour {


	private int maxCredits=10;
	public int credits;
	public DateTime nextCredit;
	public TimeSpan tiempoNextCredit;
	public bool timePurchased=false;
	public DateTime finishTiempoFree;
	public TimeSpan tiempoFree;

	// Use this for initialization
	void Start () {

	
		//obtenemos el tiempo en el que se desconecto
		if (PlayerPrefs.HasKey ("savedTime")) {
			long temp = Convert.ToInt64 (PlayerPrefs.GetString ("savedTime"));
			nextCredit = DateTime.FromBinary (temp);
		} else {
			nextCredit = DateTime.Now;
		}

		//obtenemos los creditos que tenia
		if (PlayerPrefs.HasKey ("credits")) {
			credits = PlayerPrefs.GetInt ("credits");
		} else {
			credits = 10;
			PlayerPrefs.SetInt ("credits", credits);
		}

		//Comprobamos si habia comprado tiempo de creditos
		if (PlayerPrefs.HasKey ("buyedTime")) {
			long temp = Convert.ToInt64 (PlayerPrefs.GetString ("buyedTime"));
			finishTiempoFree = DateTime.FromBinary (temp);
			if (finishTiempoFree < DateTime.Now) {
				timePurchased = false;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = false;
			} else {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
			}
		}


	}

	// Update is called once per frame
	void Update () {

		if (credits < maxCredits) {
			if (nextCredit < DateTime.Now) {
				credits += 1;
				nextCredit = nextCredit.AddMinutes (15);
			}
			tiempoNextCredit = nextCredit.Subtract (DateTime.Now);
		}
		if (timePurchased == true) {
			tiempoFree = finishTiempoFree.Subtract(DateTime.Now);
			if (finishTiempoFree < DateTime.Now) {
				timePurchased = false;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = false;
			}
		}
	}

	public void OnTimePurchased (string time){
		switch (time) {
		case "horas1":
			if (finishTiempoFree > DateTime.Now) {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = finishTiempoFree.AddHours (1);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			} else {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = DateTime.Now.AddHours (1);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			}
			break;
		case "horas3":
			if (finishTiempoFree > DateTime.Now) {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = finishTiempoFree.AddHours (3);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			} else {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = DateTime.Now.AddHours (3);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			}
			break;
		case "horas12":
			if (finishTiempoFree > DateTime.Now) {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = finishTiempoFree.AddHours (12);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			} else {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = DateTime.Now.AddHours (12);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			}
			break;
		case "horas24":
			if (finishTiempoFree > DateTime.Now) {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = finishTiempoFree.AddHours (24);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			} else {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = DateTime.Now.AddHours (24);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			}
			break;
		case "horas48":
			if (finishTiempoFree > DateTime.Now) {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = finishTiempoFree.AddHours (48);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			} else {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = DateTime.Now.AddHours (48);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			}
			break;
		case "semana":
			if (finishTiempoFree > DateTime.Now) {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = finishTiempoFree.AddHours (168);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			} else {
				timePurchased = true;
				GameManager.Instance.GamePersistentData.FreeCreditsPurchased = true;
				finishTiempoFree = DateTime.Now.AddHours (168);
				PlayerPrefs.SetString("buyedTime", finishTiempoFree.ToBinary().ToString());
			}
			break;
		}
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.SetString("savedTime", nextCredit.ToBinary().ToString());
		PlayerPrefs.SetInt ("credits", credits);
	}
}
