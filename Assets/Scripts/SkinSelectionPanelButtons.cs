using Extensions.System.Colections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectionPanelButtons : MonoBehaviour {

    public GameObject ActivatedImage;
    public Button button;
    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;

    void Start() {

    }

    public void Update() {
        button.onClick.AddListener(Activado);
    }

    public void Activado() {
        ActivatedImage.SetActive(true);
        btn1.SetActive(false);
        btn2.SetActive(false);
        btn3.SetActive(false);


    }

    public void Hide() {
        ActivatedImage.SetActive(false);
    }
}
