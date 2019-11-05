using Extensions.UnityEngine;
using System;
using System.Collections;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private MultiscreenMenuSystem multiscreenMenuSystem;
    [SerializeField]
    private ShopPanelMenuItem shopPanelMenuItem;

    private Animator animatorComponent;
    private bool alive;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        // Obtener componente Animator
        this.animatorComponent = this.GetComponent<Animator>();

        // Enlazar eventos
        this.multiscreenMenuSystem.OnMenuChange += this.OnMenuChange;
        this.shopPanelMenuItem.OnHideStart += this.OnHideStart;

        this.alive = true;
        this.StartCoroutine(this.CheckMenu());
    }

    private void OnDestroy()
    {
        // Desvincular eventos
        this.multiscreenMenuSystem.OnMenuChange -= this.OnMenuChange;
        this.shopPanelMenuItem.OnHideStart -= this.OnHideStart;

        this.alive = false;
    }

    // Manejadores de eventos
    private void OnHideStart()
    {
        this.animatorComponent.SetBool("Visible", true);
        this.animatorComponent.SetBool("Shop mode", false);
    }

    private void OnMenuChange(int obj)
    {
        switch (obj)
        {
            case 0:
                this.animatorComponent.SetBool("Visible", false);
                break;

            case 1:
                this.animatorComponent.SetBool("Visible", true);
                this.animatorComponent.SetBool("Shop mode", false);
                break;

            case 2:
                this.animatorComponent.SetBool("Visible", true);
                this.animatorComponent.SetBool("Shop mode", true);
                break;
        }
    }

    private IEnumerator CheckMenu()
    {
        while (this.alive)
        {
            this.OnMenuChange(this.multiscreenMenuSystem.CurrentMenu);
            yield return new WaitForSeconds(0.25f);
        }
    }

}