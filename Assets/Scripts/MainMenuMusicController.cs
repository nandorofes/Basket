using UnityEngine;
using System.Collections;

public class MainMenuMusicController : MonoBehaviour
{
    public AudioClip mainMenuMusicClip;

    private void Awake()
    {
        if (this.mainMenuMusicClip != null)
        {
            AudioManager.Instance.MusicVolume = 0.5f;
            AudioManager.Instance.PlayMusic(mainMenuMusicClip);
        }
    }
}