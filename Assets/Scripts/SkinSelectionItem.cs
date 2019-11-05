using Extensions.System.Numeric;
using Extensions.System.String;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectionItem : MonoBehaviour
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
    private GameObject selectedDisplay;
    [SerializeField]
    private GameObject unselectedDisplay;

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
    public static Action<string> OnSkinSelectionItemPressed = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control
    public void SetSelectedState(bool selected)
    {
        this.selectedDisplay.SetActive(selected);
        this.unselectedDisplay.SetActive(!selected);
    }

    public void SetIcon(Sprite icon)
    {
        this.ballIcon.sprite = icon;
    }

    public void SetName(string name)
    {
        this.ballName.text = name.Truncate(80);
    }

    // Manejadores de eventos de Unity
    public void ButtonPressed()
    {
        SkinSelectionItem.OnSkinSelectionItemPressed(this.referencedSkinKey);
    }

}