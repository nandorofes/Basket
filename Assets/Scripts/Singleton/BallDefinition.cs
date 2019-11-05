using Extensions.System.Numeric;
using Extensions.System.String;
using System;
using UnityEngine;

[Serializable]
public sealed class BallDefinition
{
    // ---- ---- ---- ---- ---- ---- ---- ----
	// Atributos
	// ---- ---- ---- ---- ---- ---- ---- ----
	[SerializeField]
	private int numeroBola;
	[SerializeField]
	private string bolaKey;
	[SerializeField]
	private Texture textureBola;
	[SerializeField]
	private GameObject bola;
	[SerializeField]
	private int bolaPoints;
	[SerializeField]
	private bool bolaAdquired;

	// ---- ---- ---- ---- ---- ---- ---- ----
	// Propiedades
	// ---- ---- ---- ---- ---- ---- ---- ----
	public int NumeroBola
	{
		get { return this.numeroBola;}
		internal set { this.numeroBola = value;}
	}

	public string BolaKey
	{
		get { return this.bolaKey;}
		internal set { this.bolaKey = value.Truncate(96); }
	}

	public Texture TextureBola
	{
		get { return this.textureBola; }
		internal set { this.textureBola = value; }
	}

	public GameObject Bola
	{
		get { return this.bola; }
		internal set { this.bola = value; }
	}

	public int BolaPoints
	{
		get { return this.bolaPoints;}
		internal set { this.bolaPoints = value; }
	}
		
	public bool BolaAdquired
	{
		get { return this.bolaAdquired; }
		internal set { this.bolaAdquired = value; }
	}

}

