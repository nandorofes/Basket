using Extensions.UnityEngine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelMenuItem : MonoBehaviour, MultiscreenMenuItem
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Lista de botones para bloquear durante las transiciones
    [SerializeField]
    private GameObject[] interactableList;

    // Animador
    private Animator animatorComponent;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos de SlidingMenuItem
    public event Action<MultiscreenMenuItem> OnHideEnd = delegate { };
    public event Action<MultiscreenMenuItem> OnShowEnd = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        this.animatorComponent = this.GetComponent<Animator>();
    }

    // Métodos de MultiscreenMenuItem
    public void Show()
    {
        this.animatorComponent.SetBool("Visible", true);
        this.SetInteractableListState(true);
    }

    public void ShowInstantly()
    {
        this.animatorComponent.Play("Idle");
        this.SetInteractableListState(true);
    }

    public void Hide()
    {
        this.animatorComponent.SetBool("Visible", false);
        this.SetInteractableListState(false);
    }

    // Métodos de animación
    public void ReportHideEnd()
    {
        this.OnHideEnd(this);
    }

    public void ReportShowEnd()
    {
        this.OnShowEnd(this);
    }

    // Métodos auxiliares
    private void SetInteractableListState(bool interactable)
    {
        foreach (var item in this.interactableList)
        {
            Button itemButton = item.GetComponent<Button>();
            if (itemButton != null)
                itemButton.interactable = interactable;
        }
    }
    
}