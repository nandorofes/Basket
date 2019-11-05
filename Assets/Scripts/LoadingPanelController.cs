using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private Text creditsAmountText;
    [SerializeField]
    private Text ticketsAmountText;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    public void Awake()
    {
        this.UpdateControls();
    }

    // Métodos de control
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        this.UpdateControls();
    }

    private void UpdateControls()
    {
        this.creditsAmountText.text = string.Format("{0}", GameManager.Instance.GamePersistentData.Credits);
        this.ticketsAmountText.text = string.Format("{0}", GameManager.Instance.GamePersistentData.Tickets);
    }

}