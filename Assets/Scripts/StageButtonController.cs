using Extensions.System.Numeric;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public string stageKey;

    [SerializeField]
    private Image logoImage;
    [SerializeField]
    private GameObject creditsDisplay;
    [SerializeField]
    private Text creditsText;
    [SerializeField]
    private GameObject lockDisplay;
	[SerializeField]
	private GameObject botonBuy;

    private int creditsValue = 0;
    private Button buttonComponent;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public int CreditsValue
    {
        get { return this.creditsValue; }
        set
        {
            this.creditsValue = value.ClampTo(-9, 9);
            this.creditsText.text = string.Format("{0}", this.creditsValue);
        }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control
    public void SetIcon(Sprite sprite)
    {
        this.logoImage.sprite = sprite;
    }

    public void SetLocked(bool locked)
    {
        StageDefinition referencedStage = GameManager.Instance.GetStageInfo(this.stageKey);
        if (referencedStage != null)
            this.logoImage.sprite = locked ? referencedStage.DisabledIcon : referencedStage.EnabledIcon;

        this.creditsDisplay.SetActive(!locked);
		this.botonBuy.SetActive(locked);
        this.lockDisplay.SetActive(locked);

        this.buttonComponent.interactable = !locked;
    }

    // Métodos de MonoBehaviour
    private void Awake()
    {
        this.buttonComponent = this.GetComponent<Button>();
        //logoImage.material = new Material(logoImage.material);

        this.UpdateControl();
    }

    private void OnEnable()
    {
        this.UpdateControl();
    }

    // Métodos auxiliares
    private void UpdateControl()
    {
        StageDefinition referencedStage = GameManager.Instance.GetStageInfo(this.stageKey);
        if (referencedStage != null)
        {
            this.SetIcon(referencedStage.EnabledIcon);
            int stageNumber = referencedStage.NumeroMaquina;
            //this.SetLocked(false);
            this.SetLocked(true);
			if (stageNumber == 0)
                this.SetLocked(false);
            if (stageNumber == 1 && GameManager.Instance.GamePersistentData.Escenario2)
                this.SetLocked(false);
            if (stageNumber == 2 && GameManager.Instance.GamePersistentData.Escenario3)
                this.SetLocked(false);
			if (stageNumber == 3 && GameManager.Instance.GamePersistentData.Escenario4)
				this.SetLocked(false);
			if (stageNumber == 4 && GameManager.Instance.GamePersistentData.Escenario5)
				this.SetLocked(false);
        }
    }

}