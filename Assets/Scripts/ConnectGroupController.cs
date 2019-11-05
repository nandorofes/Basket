using UnityEngine;
using System.Collections;

public class ConnectGroupController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Referencias
    [SerializeField]
    private MultiscreenMenuSystem multiscreenMenuSystem;

    // Comopnentes
    private Animator animatorComponent;

    // Banderas
    private bool alive;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        this.animatorComponent = this.GetComponent<Animator>();

        this.multiscreenMenuSystem.OnMenuChange += this.OnMenuChange;

        this.alive = true;
        this.StartCoroutine(this.CheckMenu());
    }

    private void OnDestroy()
    {
        this.multiscreenMenuSystem.OnMenuChange -= this.OnMenuChange;
    }

    // Corrutinas
    private IEnumerator CheckMenu()
    {
        while (this.alive)
        {
            this.OnMenuChange(this.multiscreenMenuSystem.CurrentMenu);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Manejadores de eventos
    private void OnMenuChange(int menuNumber)
    {
        if (menuNumber != 0)
        {
            this.animatorComponent.SetBool("Visible", true);
        }
        else
        {
            this.animatorComponent.SetBool("Visible", false);
        }
    }

	public void Mostrar(){
		animatorComponent.SetBool ("Visible", true);
	}

	public void Ocultar(){
		StopAllCoroutines ();
		animatorComponent.SetBool ("Visible", false);
	}

}