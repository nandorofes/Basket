using Extensions.UnityEngine;
using System;
using System.Collections;
using UnityEngine;

public class BottomButtonRowController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private MultiscreenMenuSystem multiscreenMenuSystem;

    public float transitionTime = 0.5f;

    private RectTransform rectTransform;
    private bool visible = false;

    private int currentMenu = 0;
    private bool alive;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        this.alive = true;
        this.StartCoroutine(this.CheckMenu());
    }

    private void Start()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.multiscreenMenuSystem.OnMenuChange += this.OnMenuChange;
    }

	public void Mostrar(){
		StartCoroutine (Show ());
	}

	public void Esconder(){
		StartCoroutine (Hide ());
	}

    // Corrutinas
    private IEnumerator Hide()
    {
        if (visible)
        {
            float time = 0.0f, inverseTotalTime = 1.0f / this.transitionTime;
            while (time < 1.0f)
            {
                float posY = Mathf.SmoothStep(0.0f, -180.0f, time);
                this.rectTransform.SetPosY(posY);

                time += Time.deltaTime * inverseTotalTime;
                yield return null;
            }

            this.visible = false;
            this.rectTransform.SetPosY(-180.0f);
        }
    }

    private IEnumerator Show()
    {
        if (this.rectTransform == null)
            this.rectTransform = this.GetComponent<RectTransform>();

        if (!visible)
        {
            float time = 0.0f, inverseTotalTime = 1.0f / this.transitionTime;
            while (time < 1.0f)
            {
                float posY = Mathf.SmoothStep(0.0f, -180.0f, 1.0f - time);
                this.rectTransform.SetPosY(posY);

                time += Time.deltaTime * inverseTotalTime;
                yield return null;
            }

            this.visible = true;
            this.rectTransform.SetPosY(0.0f);
        }
    }

    private IEnumerator CheckMenu()
    {
        while (this.alive)
        {
            if (this.currentMenu != this.multiscreenMenuSystem.CurrentMenu)
            {
                this.currentMenu = this.multiscreenMenuSystem.CurrentMenu;
                this.OnMenuChange(this.multiscreenMenuSystem.CurrentMenu);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Manejadores de eventos
    private void OnMenuChange(int obj)
    {
        switch (obj)
        {
            case 0:
                this.StartCoroutine(this.Hide());
                break;

            default:
                this.StartCoroutine(this.Show());
                break;
        }
        this.currentMenu = this.multiscreenMenuSystem.CurrentMenu;
    }
	
}