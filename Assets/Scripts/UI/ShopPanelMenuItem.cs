using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelMenuItem : MonoBehaviour, MultiscreenMenuItem
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Lista de botones para bloquear durante las transiciones
    [SerializeField]
    private GameObject[] interactableList;

    // Animador
    private Animator animatorComponent;

	//Tutorial
	public GameObject tuto;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public event Action<MultiscreenMenuItem> OnHideEnd = delegate { };
    public event Action<MultiscreenMenuItem> OnShowEnd = delegate { };

    public event Action OnHideStart = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    public void Awake()
    {
        this.animatorComponent = this.GetComponent<Animator>();
    }

    // Métodos de MultiscreenMenuItem
    public void Hide()
    {
        this.SetInteractableListState(false);
        this.animatorComponent.SetBool("Visible", false);

        this.OnHideStart();
    }

    public void Show()
    {
        this.SetInteractableListState(true);

		if (GameManager.Instance.GamePersistentData.Tutorial == false && GameManager.Instance.GamePersistentData.Tickets >= 100) {
			tuto.transform.parent.gameObject.SetActive (true);
			tuto.SetActive (true);
		}

		if (this.animatorComponent != null) {
			this.animatorComponent.SetBool ("Visible", true);
		} else {
			this.GetComponent<Animator> ().SetBool ("Visible", true);
		}
    }

    public void ShowInstantly()
    {
        this.SetInteractableListState(true);
		if (GameManager.Instance.GamePersistentData.Tutorial == false && GameManager.Instance.GamePersistentData.Tickets >= 100) {
			tuto.transform.parent.gameObject.SetActive (true);
			tuto.SetActive (true);
		}

        if (this.animatorComponent != null)
        {
            this.animatorComponent.SetBool("Visible", true);
            this.animatorComponent.Play("Visible");
        }
        else
        {
            this.GetComponent<Animator>().SetBool("Visible", true);
            this.GetComponent<Animator>().Play("Visible");
        }
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