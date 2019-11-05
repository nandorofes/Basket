using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPopupPanelController : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private Text pointsText;
    [SerializeField]
    private TextMeshProUGUI freezeText;
    [SerializeField]
    private TextMeshProUGUI tripleText;
    [SerializeField]
    private TextMeshProUGUI bonusText;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control
    public void ShowPointsText(string pointsText)
    {
        this.pointsText.text = pointsText;
        this.StartCoroutine(this.AnimateText(this.pointsText));
    }

    public void ShowFreeze()
    {
        this.StartCoroutine(this.AnimateSpecialText(this.freezeText, 1.0f));
    }

    public void ShowTriple()
    {
        this.StartCoroutine(this.AnimateSpecialText(this.tripleText, 1.0f));
    }

    public void ShowBonus()
    {
        this.StartCoroutine(this.AnimateSpecialText(this.bonusText, 1.0f));
    }

    // Corrutinas
    private IEnumerator AnimateText(Text text)
    {
        Transform transform = text.transform;

        float time = 0.0f, normalizedTime = 1.0f / 0.5f;
        while (time < 1.0f)
        {
            transform.localScale = Vector3.one * time * 2.0f;
            text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - time);

            time += Time.deltaTime * normalizedTime;
            yield return null;
        }

        transform.localScale = Vector3.one * 2.0f;
        text.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    private IEnumerator AnimateSpecialText(TextMeshProUGUI text, float totalTime)
    {
        Transform transform = text.transform;

        float time = 0.0f, normalizedTime = 1.0f / totalTime;
        while (time < 1.0f)
        {
            transform.localScale = Vector3.one * time * 2.0f;
            text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - (time * time));

            time += Time.deltaTime * normalizedTime;
            yield return null;
        }

        transform.localScale = Vector3.one * 2.0f;
        text.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

}