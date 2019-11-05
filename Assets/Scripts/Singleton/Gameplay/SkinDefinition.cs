using System;
using System.Collections;
using System.Collections.Generic;
using Extensions.System.Numeric;
using Extensions.System.SequentialArray;
using Extensions.System.String;
using UnityEngine;

[Serializable]
public sealed class SkinDefinition
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private string key;
    [SerializeField]
    private string nameLocalizationKey;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private Texture texture;
    [SerializeField]
    private SkinTypes type;
    [SerializeField]
    private int price;
    [SerializeField]
    private bool basic;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public string Key
    {
        get { return this.key; }
        internal set { this.key = value.Truncate(96); }
    }

    public string NameLocalizationKey
    {
        get { return this.nameLocalizationKey; }
        internal set { this.nameLocalizationKey = value.Truncate(160); }
    }

    public Sprite Icon
    {
        get { return this.icon; }
        internal set { this.icon = value; }
    }

    public Texture Texture
    {
        get { return this.texture; }
        internal set { this.texture = value; }
    }

    public SkinTypes Type
    {
        get { return this.type; }
        internal set { this.type = value; }
    }

    public int Price
    {
        get { return this.price; }
        internal set { this.price = value.ClampTo(0, 99999); }
    }

    public bool Basic
    {
        get { return this.basic; }
        internal set { this.basic = value; }
    }

}