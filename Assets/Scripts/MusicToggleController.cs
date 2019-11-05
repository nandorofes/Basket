using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggleController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private Sprite activatedSprite;
    [SerializeField]
    private Sprite deactivatedSprite;

    [SerializeField]
    private GameObject activatedGroup;
    [SerializeField]
    private GameObject deactivatedGroup;

    private Image imageComponent;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private void Awake()
    {
        this.imageComponent = this.GetComponent<Image>();
    }

    public void UpdateControl()
    {
        bool activated = !AudioManager.Instance.MusicMuted;

        this.activatedGroup.SetActive(activated);
        this.deactivatedGroup.SetActive(!activated);

        this.imageComponent.sprite = activated ? this.activatedSprite : this.deactivatedSprite;
    }

}