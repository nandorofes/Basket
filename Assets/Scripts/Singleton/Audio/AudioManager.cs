﻿using Extensions.System.Numeric;
using System;
using System.Collections;
using UnityEngine;

[Prefab("Audio manager", true)]
public class AudioManager : Singleton<AudioManager>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Públicos
    public AudioClip mainMenuMusic;
    public AudioClip buttonSound;

    private AudioSource ambientSoundAudioSource;
    private AudioSource[] effectSoundAudioSourceList;
    private AudioSource musicAudioSource;

    private int effectSoundAudioSourcePointer = 0;

    private float ambientSoundVolume = 1.0f;
    private float effectSoundVolume = 1.0f;
    private float musicVolume = 1.0f;
    
    private bool effectSoundMuted = false;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    /// <summary>
    /// Obtiene el clip de sonido que está asignado actualmente en el canal de
    /// sonidos ambientales.
    /// </summary>
    /// <value>Clip de sonidos actual.</value>
    public AudioClip CurrentAmbientSound
    {
        get { return this.ambientSoundAudioSource.clip; }
    }
    
    /// <summary>
    /// Obtiene el clip de música que está asignado actualmente en el canal de
    /// música.
    /// </summary>
    /// <value>Clip de música actual.</value>
    public AudioClip CurrentMusic
    {
        get { return this.musicAudioSource.clip; }
    }
    
    /// <summary>
    /// Obtiene o asigna el volumen de los sonidos ambientales, en el rango de
    /// valores entre 0.0 y 1.0.
    /// </summary>
    /// <value>Volumen de los sonidos ambientales.</value>
    public float AmbientSoundVolume
    {
        get { return this.ambientSoundAudioSource.volume; }
        set
        {
            this.ambientSoundVolume = value.ClampToUnit();
            this.ambientSoundAudioSource.volume = value.ClampToUnit();
        }
    }
    
    /// <summary>
    /// Obtiene o asigna el volumen de los efectos de sonido, en el rango de
    /// valores entre 0.0 y 1.0.
    /// </summary>
    /// <value>Volumen de los efectos de sonido.</value>
    public float EffectSoundVolume
    {
        get { return this.effectSoundVolume; }
        set
        { 
            this.effectSoundVolume = value.ClampToUnit();
            foreach (var effectSoundAudioSource in effectSoundAudioSourceList)
                effectSoundAudioSource.volume = value.ClampToUnit();
        }
    }
    
    /// <summary>
    /// Obtiene o asigna el volumen de la música, en el rango de valores entre
    /// 0.0 y 1.0.
    /// </summary>
    /// <value>Volumen de la música.</value>
    public float MusicVolume
    {
        get { return this.musicAudioSource.volume; }
        set
        {
            this.musicVolume = value.ClampToUnit();
            this.musicAudioSource.volume = value.ClampToUnit();
        }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de música se
    /// está reproduciendo en bucle.
    /// </summary>
    public bool MusicLoop
    {
        get { return this.musicAudioSource.loop; }
        set { this.musicAudioSource.loop = value; }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de sonidos
    /// ambientales se está reproduciendo en bucle.
    /// </summary>
    public bool AmbientSoundLoop
    {
        get { return this.ambientSoundAudioSource.loop; }
        set { this.ambientSoundAudioSource.loop = value; }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de música está
    /// silenciado.
    /// </summary>
    public bool MusicMuted
    {
        get { return this.musicAudioSource.mute; }
        set
        {
            this.musicAudioSource.mute = value;
            if (!value)
                this.musicAudioSource.volume = this.musicVolume;
        }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de sonidos
    /// ambientales está silenciado.
    /// </summary>
    public bool AmbientSoundMuted
    {
        get { return this.ambientSoundAudioSource.mute; }
        set
        {
            this.ambientSoundAudioSource.mute = value;
            if (!value)
                this.ambientSoundAudioSource.volume = this.ambientSoundVolume;
        }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de efectos de sonido
    /// está silenciado.
    /// </summary>
    public bool EffectSoundMuted
    {
        get { return this.effectSoundMuted; }
        set { this.effectSoundMuted = value; }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control de música
    /// <summary>
    /// Reproduce el clip de música asignado actualmente en el canal de
    /// música, o continua su reproducción si estaba pausado.
    /// </summary>
    public void PlayMusic()
    {
        this.musicAudioSource.Play();
    }
    
    /// <summary>
    /// Reproduce un clip de música, deteniendo una música anterior si se
    /// estaba reproduciendo. El cambio se produce instantáneamente.
    /// </summary>
    /// <param name="musicClip">Clip de música.</param>
    public void PlayMusic(AudioClip musicClip)
    {
        this.musicAudioSource.clip = musicClip;
        this.musicAudioSource.Play();
    }
    
    /// <summary>
    /// Reproduce un clip de música, deteniendo una música anterior si se
    /// estaba reproduciendo. Se produce un efecto de desvanecimiento que dura
    /// el tiempo especificado.
    /// </summary>
    /// <param name="musicClip">Clip de música.</param>
    /// <param name="crossFadeTime">Tiempo de desvanecimiento.</param>
    public void PlayMusic(AudioClip musicClip, float crossFadeTime)
    {
        this.StartCoroutine(this.CrossFadeMusic(musicClip, crossFadeTime));
    }
    
    /// <summary>
    /// Pausa la música.
    /// </summary>
    public void PauseMusic()
    {
        this.musicAudioSource.Pause();
    }
    
    /// <summary>
    /// Detiene la música.
    /// </summary>
    public void StopMusic()
    {
        this.musicAudioSource.Stop();
    }
    
    // Métodos de control de sonidos ambiente
    /// <summary>
    /// Reproduce el clip de sonido ambiental asignado actualmente en el canal
    /// de sonidos ambientales, o continua su reproducción si estaba pausado.
    /// </summary>
    public void PlayAmbientSound()
    {
        if (this.CurrentAmbientSound != null)
            this.ambientSoundAudioSource.Play();
    }
    
    /// <summary>
    /// Reproduce un clip de sonido ambiental, deteniendo un clip anterior si
    /// se estaba reproduciendo. El cambio se produce instantáneamente.
    /// </summary>
    /// <param name="ambientSoundClip">Clip de sonido ambiental.</param>
    public void PlayAmbientSound(AudioClip ambientSoundClip)
    {
        if (ambientSoundClip != null)
        {
            this.ambientSoundAudioSource.clip = ambientSoundClip;
            this.ambientSoundAudioSource.Play();
        }
    }
    
    /// <summary>
    /// Reproduce un clip de sonido ambiental, deteniendo un clip anterior si
    /// se estaba reproduciendo. Se produce un efecto de desvanecimiento que
    /// dura el tiempo especificado.
    /// </summary>
    /// <param name="ambientSoundClip">Clip de sonido ambiental.</param>
    /// <param name="crossFadeTime">Tiempo de desvanecimiento.</param>
    public void PlayAmbientSound(AudioClip ambientSoundClip, float crossFadeTime)
    {
        if (ambientSoundClip != null)
        {
            this.StartCoroutine(this.CrossFadeAmbientSound(ambientSoundClip, crossFadeTime));
        }
    }
    
    /// <summary>
    /// Pausa el clip de sonido ambiental.
    /// </summary>
    public void PauseAmbientSound()
    {
        this.ambientSoundAudioSource.Pause();
    }
    
    /// <summary>
    /// Detiene el clip de sonido ambiental.
    /// </summary>
    public void StopAmbientSound()
    {
        this.ambientSoundAudioSource.Stop();
    }
    
    // Métodos de control de efectos de sonido
    /// <summary>
    /// Reproduce un efecto de sonido sin tener en cuenta su posición en el
    /// espacio.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip)
    {
        if (!this.effectSoundMuted && (effectSoundClip != null))
        {
            AudioSource selectedAudioSource = this.effectSoundAudioSourceList[this.effectSoundAudioSourcePointer];

            selectedAudioSource.clip = effectSoundClip;
            selectedAudioSource.pitch = 1.0f;
            selectedAudioSource.Play();

            int totalChannels = this.effectSoundAudioSourceList.Length;
            this.effectSoundAudioSourcePointer = (this.effectSoundAudioSourcePointer + 1) % totalChannels;
        }
    }

    /// <summary>
    /// Reproduce un efecto de sonido sin tener en cuenta su posición en el
    /// espacio y con el tono especificado.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip, float pitch)
    {
        if (!this.effectSoundMuted && (effectSoundClip != null))
        {
            AudioSource selectedAudioSource = this.effectSoundAudioSourceList[this.effectSoundAudioSourcePointer];

            selectedAudioSource.clip = effectSoundClip;
            selectedAudioSource.pitch = pitch;
            selectedAudioSource.Play();

            int totalChannels = this.effectSoundAudioSourceList.Length;
            this.effectSoundAudioSourcePointer = (this.effectSoundAudioSourcePointer + 1) % totalChannels;
        }
    }

    /// <summary>
    /// Reproduce un efecto de sonido 3D en la posición especificada por un
    /// objeto.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    /// <param name="position">Objeto del juego.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip, GameObject gameObject)
    {
        if (!this.effectSoundMuted && (effectSoundClip != null))
            AudioSource.PlayClipAtPoint(effectSoundClip, gameObject.transform.position, this.effectSoundVolume);
    }
    
    /// <summary>
    /// Reproduce un efecto de sonido 3D en la posición especificada por un
    /// vector.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    /// <param name="position">Vector de posición.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip, Vector3 position)
    {
        if (!this.effectSoundMuted && (effectSoundClip != null))
            AudioSource.PlayClipAtPoint(effectSoundClip, position, this.effectSoundVolume);
    }
    
    // Corrutinas
    private IEnumerator CrossFadeMusic(AudioClip musicClip, float crossFadeTime)
    {
        float halfCrossFadeTimeInversed = 1.0f / (crossFadeTime * 0.5f);
        
        float timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            this.musicAudioSource.volume = (1.0f - timeCounter) * this.musicVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
        
        this.musicAudioSource.clip = musicClip;
        this.musicAudioSource.Play();
        
        timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            this.musicAudioSource.volume = timeCounter * this.musicVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
    }
    
    private IEnumerator CrossFadeAmbientSound(AudioClip ambientSoundClip, float crossFadeTime)
    {
        float halfCrossFadeTimeInversed = 1.0f / (crossFadeTime * 0.5f);
        
        float timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            this.musicAudioSource.volume = (1.0f - timeCounter) * this.ambientSoundVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
        
        this.musicAudioSource.clip = ambientSoundClip;
        this.musicAudioSource.Play();
        
        timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            this.musicAudioSource.volume = timeCounter * this.ambientSoundVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
    }
    
    // Métodos de MonoBehaviour
    private void Awake()
    {
        // Crear componentes en tiempo de ejecución
        this.musicAudioSource = this.gameObject.AddComponent<AudioSource>();
        this.ambientSoundAudioSource = this.gameObject.AddComponent<AudioSource>();

        this.effectSoundAudioSourceList = new AudioSource[4];
        for (int i = 0; i < effectSoundAudioSourceList.Length; i++)
            this.effectSoundAudioSourceList[i] = this.gameObject.AddComponent<AudioSource>();
        
        // Configurar componentes
        this.musicAudioSource.loop = true;
        this.musicAudioSource.mute = false;
        this.musicAudioSource.priority = 0;
        this.musicAudioSource.spatialBlend = 0.0f;
        this.musicAudioSource.spatialize = false;
        
        this.ambientSoundAudioSource.loop = true;
        this.ambientSoundAudioSource.mute = false;
        this.ambientSoundAudioSource.priority = 8;
        this.ambientSoundAudioSource.spatialBlend = 0.0f;
        this.ambientSoundAudioSource.spatialize = false;

        foreach (var effectSoundAudioSource in effectSoundAudioSourceList)
        {
            effectSoundAudioSource.loop = false;
            effectSoundAudioSource.mute = false;
            effectSoundAudioSource.priority = 16;
            effectSoundAudioSource.spatialBlend = 0.0f;
            effectSoundAudioSource.spatialize = false;

            effectSoundAudioSource.volume = this.effectSoundVolume;
        }

        // Asignar volumen
        this.musicAudioSource.volume = this.musicVolume;
        this.ambientSoundAudioSource.volume = this.ambientSoundVolume;
    }

	public void LlamarAwake(){
		Awake ();
	}
    
}