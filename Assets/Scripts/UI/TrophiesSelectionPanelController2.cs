using System;
using UnityEngine;
using UnityEngine.UI;
using Extensions.System.Numeric;

public class TrophiesSelectionPanelController2 : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public Sprite[] trophySpriteList;

    [SerializeField]
    private Image trophyImage;
    [SerializeField]
    private Text noTrophiesText;
	[SerializeField]
	private GameObject shopButton;

    private int trophyCount = 0;
    private int trophyIndex = 0;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private void Awake()
    {
        this.trophyCount = 0;
        //if (GameManager.Instance.GamePersistentData.Escenario2)
        //    trophyCount++;
        //if (GameManager.Instance.GamePersistentData.Escenario3)
        //    trophyCount++;
        if (GameManager.Instance.GamePersistentData.Escenario4)
            trophyCount++;
        

        this.trophyImage.gameObject.SetActive(this.trophyCount > 0);
        this.noTrophiesText.gameObject.SetActive(this.trophyCount == 0);
		this.shopButton.SetActive (this.trophyCount == 0);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
		this.trophyCount = 0;
		//if (GameManager.Instance.GamePersistentData.Escenario2)
		//	trophyCount++;
		//if (GameManager.Instance.GamePersistentData.Escenario3)
		//	trophyCount++;
        if (GameManager.Instance.GamePersistentData.Escenario4)
            trophyCount++;

		this.trophyImage.gameObject.SetActive(this.trophyCount > 0);
		this.noTrophiesText.gameObject.SetActive(this.trophyCount == 0);
		this.shopButton.SetActive (this.trophyCount == 0);
    }

    //public void GoToNext()
    //{
    //    this.trophyIndex = (this.trophyIndex + 1).Mod(this.trophyCount);
    //    this.trophyImage.sprite = this.trophySpriteList[this.trophyIndex];
    //}

    //public void GoToPrevious()
    //{
    //    this.trophyIndex = (this.trophyIndex - 1).Mod(this.trophyCount);
    //    this.trophyImage.sprite = this.trophySpriteList[this.trophyIndex];
    //}

	public void GoToShop ()
	{
		MainMenuSceneManager sceneManager = GameObject.Find ("Main menu scene manager").GetComponent<MainMenuSceneManager> ();
		sceneManager.GoToMenu(2);
		this.gameObject.SetActive(false);
	}
}