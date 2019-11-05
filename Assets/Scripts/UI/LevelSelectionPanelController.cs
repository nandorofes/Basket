using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanelController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----

	[SerializeField]
	private ScrollSnapRect scroll;
	[SerializeField]
	private GameObject next;
	[SerializeField]
	private GameObject prev;

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

	void Update (){
		if (scroll._currentPage == 0) {
			prev.SetActive (false);
		} else if (scroll._currentPage == 4) {
			next.SetActive (false);
		} else {
			prev.SetActive (true);
			next.SetActive (true);
		}
	}
}