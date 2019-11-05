using Extensions.System.Numeric;
using Extensions.System.String;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinBuyItemController : MonoBehaviour, IComparable<SkinBuyItemController>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private string referencedSkinKey;

    [SerializeField]
    private Image ballIcon;
    [SerializeField]
    private Text ballName;

    [SerializeField]
    private GameObject acquiredDisplay;
    [SerializeField]
    private GameObject buyDisplay;

    [SerializeField]
    private Text amountText;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public string ReferencedSkinKey
    {
        get { return this.referencedSkinKey; }
        internal set { this.referencedSkinKey = value.Truncate(96); }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public static Action<string> OnSkinBuyItemPressed = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control
    public void SetAcquiredState(bool acquired)
    {
        this.acquiredDisplay.SetActive(acquired);
        this.buyDisplay.SetActive(!acquired);
    }

    public void SetAmount(int amount)
    {
        this.amountText.text = string.Format("{0}", amount.ClampTo(0, 9999));
    }

    public void SetIcon(Sprite icon)
    {
        this.ballIcon.sprite = icon;
    }

    public void SetName(string name)
    {
        this.ballName.text = name.Truncate(80);
    }

    // Métodos de IComparable<SkinBuyItemController>
    public int CompareTo(SkinBuyItemController other)
    {
        int thisValue = 0, otherValue = 0;

        var referencedSkin = GameManager.Instance.GetSkinInfo(this.referencedSkinKey);
        if (referencedSkin != null)
            thisValue = referencedSkin.Price;

        var referencedSkinOther = GameManager.Instance.GetSkinInfo(other.referencedSkinKey);
        if (referencedSkinOther != null)
            otherValue = referencedSkinOther.Price;
        
        return thisValue.CompareTo(otherValue);
    }

    // Manejadores de eventos de Unity
    public void ButtonPressed()
    {
        SkinBuyItemController.OnSkinBuyItemPressed(this.referencedSkinKey);
    }

}