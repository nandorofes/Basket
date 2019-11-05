using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AutoLocalizingText : MonoBehaviour
{
    private string originalKey;

    private void Awake()
    {
        string currentLangauge = GameManager.Instance.GameLanguage;
        
        Text text = this.GetComponent<Text>();
        if (text != null)
        {
            GameManager.Instance.OnGameLanguageChanged += this.UpdateText;

            this.originalKey = text.text;
            text.text = Localization.GetString(currentLangauge, text.text);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameLanguageChanged -= this.UpdateText;
    }

    private void UpdateText(string languageCode)
    {
        Text text = this.GetComponent<Text>();
        if (text != null)
            text.text = Localization.GetString(languageCode, this.originalKey);
    }

}