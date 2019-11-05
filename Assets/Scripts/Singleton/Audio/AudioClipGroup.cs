using Extensions.System.Colections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioClipGroup
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Públicos
    public List<AudioClip> audioClipList;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public AudioClip RandomItem
    {
        get { return this.audioClipList.RandomItem<AudioClip>(); }
    }

}