using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPanelController : MonoBehaviour
{
    public Text messageText;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public event Action OnNoPressed = delegate { };
    public event Action OnYesPressed = delegate { };
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    // Métodos de presentación
    public void SetMessage(string text)
    {
        this.messageText.text = text;
    }

    // Manejadores de eventos de Unity
    public void NoButtonPressed()
    {
        this.OnNoPressed();
    }
    
    public void YesButtonPressed()
    {
        this.OnYesPressed();
    }
    
}