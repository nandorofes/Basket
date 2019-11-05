using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelMenuItem : MonoBehaviour, MultiscreenMenuItem
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos públicos
    public float hidingTime = 2.0f;

    // Animador
    private Animator animatorComponent;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
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

    // Métodos de SlidingMenuItem
    public void Hide()
    {
        this.animatorComponent.SetBool("Visible", false);
        this.StartCoroutine(this.ReportHiding());
    }

    public void Show()
    {
        this.animatorComponent.SetBool("Visible", true);
    }

    public void ShowInstantly()
    {
        this.animatorComponent.SetBool("Visible", true);
    }

    // Corrutinas
    private IEnumerator ReportHiding()
    {
        yield return new WaitForSeconds(this.hidingTime);
        this.OnHideEnd(this);
    }

}