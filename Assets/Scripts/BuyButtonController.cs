using Extensions.System.Numeric;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public string stageKey;
    public int buyValue = 0;

    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private Sprite buttonAquiredSprite;
    [SerializeField]
    private Sprite buttonBuySprite;

    [SerializeField]
    private GameObject ticketImage;

    [SerializeField]
    private Text acquiredText;
    [SerializeField]
    private Text buyText;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        this.UpdateAppearance();
    }

    // Métodos de control
    public void SetAcquiredMode()
    {
        this.buttonImage.sprite = buttonAquiredSprite;
        this.ticketImage.SetActive(false);

        this.acquiredText.gameObject.SetActive(true);
        this.buyText.gameObject.SetActive(false);
    }
	
    public void SetBuyMode()
    {
        this.buttonImage.sprite = buttonBuySprite;
        this.ticketImage.SetActive(true);

        this.acquiredText.gameObject.SetActive(false);
        this.buyText.gameObject.SetActive(true);
    }

    public void UpdateAppearance()
    {
        var x = GameManager.Instance.GetStageInfo(stageKey);
        var stageNumber = x.NumeroMaquina;
        bool unlocked = false;

        switch (stageNumber)
        {
            case 0:
                unlocked = GameManager.Instance.GamePersistentData.Escenario2;
                break;
            case 1:
                unlocked = GameManager.Instance.GamePersistentData.Escenario3;
                break;
            case 2:
                unlocked = GameManager.Instance.GamePersistentData.Escenario4;
                break;
        }
        if (unlocked)
            this.SetAcquiredMode();
        else
            this.SetBuyMode();

        this.buyText.text = string.Format("{0}", this.buyValue.ClampTo(0, 9999));
    }

}