using Extensions.System.Numeric;
using Extensions.System.String;
using System;
using UnityEngine;

[Serializable]
public class StageDefinition
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
	[SerializeField]
	private int numeroMaquina;
    [SerializeField]
    private string key;
    [SerializeField]
    private Sprite enabledIcon;
    [SerializeField]
    private Sprite disabledIcon;
    [SerializeField]
    private Texture textureMaquina;
	[SerializeField]
	private Texture fondo;
    [SerializeField]
    private GameObject barrierPrefab;
    [SerializeField]
    private AudioClip ambientClip;
    [SerializeField]
    private float ambientClipVolume = 1.0f;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
	public int NumeroMaquina
	{
		get { return this.numeroMaquina; }
		internal set { this.numeroMaquina = value; }
	}

	public string Key
    {
        get { return this.key; }
        internal set { this.key = value.Truncate(96); }
    }

    public Sprite EnabledIcon
    {
        get { return this.enabledIcon; }
        internal set { this.enabledIcon = value; }
    }

    public Sprite DisabledIcon
    {
        get { return this.disabledIcon; }
        internal set { this.disabledIcon = value; }
    }

	public Texture TextureMaquina
    {
		get { return this.textureMaquina; }
		internal set { this.textureMaquina = value; }
    }

	public Texture Fondo
	{
		get { return this.fondo; }
		internal set { this.fondo = value; }
	}

    public GameObject BarrierPrefab
    {
        get { return this.barrierPrefab; }
        internal set { this.barrierPrefab = value; }
    }

    public AudioClip AmbientClip
    {
        get { return this.ambientClip; }
        internal set { this.ambientClip = value; }
    }

    public float AmbientClipVolume
    {
        get { return this.ambientClipVolume; }
        internal set { this.ambientClipVolume = value.ClampToUnit(); }
    }

}